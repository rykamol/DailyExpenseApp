$(function () {
    loadExpenses();
    LoadFormatedDate();
});

function LoadFormatedDate(d = null) {

    var time = new Date();

    if (d != null || d != undefined) {
        var time = new Date(d);
    }

    var day = ("0" + time.getDate()).slice(-2);
    var month = ("0" + (time.getMonth() + 1)).slice(-2);
    var today = time.getFullYear() + "-" + (month) + "-" + (day);
    $('#ExpenseDate').val(today);
}

function loadExpenses() {
    var options = { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' };
    $.ajax({
        url: "Expenses/GetExpenses",
        type: "get",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        success: function (response) {
            if (response == null || response == undefined || response.length == 0) {
                var object = '';
                object += '<tr>';
                object += '<td colspan="5">' + 'No expense found' + '</td>';

                $('#expenseTable').html(object);
            } else {
                var object = '';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + item.title + '</td>';
                    object += '<td>' + item.moneySpent + '</td>';
                    object += '<td>' + new Date(item.expenseDate).toLocaleDateString("en-US", options) + '</td>';
                    object += '<td>' + item.category + '</td>';
                    object += '<td> <a href="#" class="btn btn-primary btn-sm" onclick="edit(' + item.id + ')">Edit</a> <a href="#" class="btn btn-danger btn-sm" onclick="remove(' + item.id + ')">Delete</a> </td>'
                })
                $('#expenseTable').html(object);
            }
        },
        error: function () {
            toastr.error('Unable to load expenses!');
        }
    })
}

$('#btnCreateExpense').on('click', function () {
    createForm();
    $('#addExpense').modal('show');
    $('#exampleModalLabel').text('Add Expense');
    $('#create').css('display', 'block');
    $('#update').css('display', 'none');

    //loadFormatedDate();
})

function createForm() {
    $.ajax({
        url: "Expenses/Create",
        type: "get",
        success: function (response) {
            $('#Id').val(response.id);
            $('#Title').val(response.title);
            $('#MoneySpent').val(response.moneySpent);
            $('#Category').val(response.category);
        },
        error: function (response) {
            toastr.error(response);
        }
    })
}

function create() {

    var result = validate();
    if (result == false) {
        return false;
    }

    var formData = new Object();
    formData.title = $('#Title').val();
    formData.moneySpent = $('#MoneySpent').val();
    formData.category = $('#Category').val();
    formData.expenseDate = $('#ExpenseDate').val();

    $.ajax({
        url: "Expenses/Create",
        type: "post",
        data: formData,
        success: function (response) {
            if (response === null || response === undefined) {
                toastr.error('Unable to create expense!');
            } else {
                hideModal();
                loadExpenses();
                toastr.success(response);
            }
        },
        error: function (response) {
            toastr.error(response);
        }
    })
}
function edit(id) {
    $.ajax({
        url: 'Expenses/Edit?id=' + id,
        type: "get",
        contentType: 'application/json,charset=utf-8',
        datatype: 'json',
        success: function (response) {
            if (response === null || response === undefined) {
                toastr.error('Unable to load the expense!');
            } else if (response.length == 0) {
                toastr.error('Expense not found with the id' + id);
            } else {
                $('#addExpense').modal('show');
                $('#exampleModalLabel').text('Update Expense');
                $('#create').css('display', 'none');
                $('#update').css('display', 'block');

                $('#Id').val(id);
                $('#Title').val(response.title);
                $('#MoneySpent').val(response.moneySpent);
                $('#Category').val(response.category);
                LoadFormatedDate(response.expenseDate);
            }
        },
        error: function (response) {
            toastr.error(response);
        }
    })
}

function update() {
    var result = validate();
    if (result == false) {
        return false;
    }

    var formData = new Object();
    formData.id = $('#Id').val();
    formData.title = $('#Title').val();
    formData.moneySpent = $('#MoneySpent').val();
    formData.category = $('#Category').val();
    formData.expenseDate = $('#ExpenseDate').val();


    $.ajax({
        url: "Expenses/Update",
        type: "post",
        data: formData,
        success: function (response) {
            if (response === null || response === undefined) {
                toastr.error('Unable to update expense!');
            } else {
                hideModal();
                loadExpenses();
                toastr.success(response);
            }
        },
        error: function () {
            toastr.error('Unable to update expense!');
        }
    })
}

function remove(id) {

    if (confirm('Are you sure you want to delete the record?')) {
        $.ajax({
            url: 'Expenses/Delete?id=' + id,
            type: "post",
            success: function (response) {
                if (response === null || response === undefined) {
                    toastr.error('Unable to delete expense!');
                } else if (response.length == 0) {
                    toastr.error('Expense not available with the id' + id);
                } else {
                    loadExpenses();
                    toastr.success(response);
                }
            },
            error: function (respnse) {
                toastr.error(respnse);
            }
        })
    }
}

function hideModal() {
    clearData();
    $('#addExpense').modal('hide');
}

function clearData() {
    $('#Id').val('');
    $('#Title').val('');
    $('#MoneySpent').val('');
    $('#Category').val('');
    LoadFormatedDate();


    $('#Title').css('border-color', 'lightgray');
    $('#MoneySpent').css('border-color', 'lightgray');
    $('#Category').css('border-color', 'lightgray');
    $('#ExpenseDate').css('border-color', 'lightgray');
}

function validateTitle() {
    if ($('#Title').val().trim() == "") {
        $('#Title').css('border-color', 'Red');
        return false;
    } else {
        $('#Title').css('border-color', 'lightgray');
        return true;
    }
}

function validateMoneySpent() {
    if ($('#MoneySpent').val().trim() == "") {
        $('#MoneySpent').css('border-color', 'Red');
        return false;
    } else {
        var price = $('#MoneySpent').val();
        console.log(price);
        if (price <= 0) {
            $('#MoneySpent').css('border-color', 'Red');
            return false;
        } else {
            $('#MoneySpent').css('border-color', 'lightgray');
            return true;
        }
    }
}

function validateCategory() {
    if ($('#Category').val().trim() == "") {
        $('#Category').css('border-color', 'Red');
        return false;
    } else {
        $('#Category').css('border-color', 'lightgray');
        return true;
    }
}

function validateExpenseDate() {
    if ($('#ExpenseDate').val().trim() == "") {
        $('#ExpenseDate').css('border-color', 'Red');
        return false;
    } else {
        $('#ExpenseDate').css('border-color', 'lightgray');
        return true;
    }
}

function validate() {

    var isvalid = true;

    isvalid = validateTitle();

    isvalid = validateMoneySpent();

    isvalid = validateCategory();

    return isvalid;
}

$('#Title').focusout(function () {
    validateTitle();
})

$('#Category').focusout(function () {
    validateCategory();
})

$('#MoneySpent').focusout(function () {
    validateMoneySpent();
})


