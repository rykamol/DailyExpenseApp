using ExpenseManagement.Data.Repository.IRepository;
using ExpenseManagement.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace ExpenseManagement.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;


        public ExpensesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult GetExpenses()
        {
            IEnumerable<ExpenseModel> lists = _unitOfWork.Expense.GetAll().ToList();

            return Json(lists);
        }

        [HttpGet]
        public JsonResult Create()
        {
            var expense = new ExpenseModel();
            return Json(expense);
        }

        [HttpPost]
        public JsonResult Create(ExpenseModel expense)
        {
            if (ModelState.IsValid)
            {
                //expense.ExpenseDate = DateTime.Now;

                _unitOfWork.Expense.Create(expense);
                _unitOfWork.SaveChanges();

                return Json("Expense saved!");
            }
            else
            {
                return Json("Model validation failed");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id = 0)
        {
            if (id <= 0) { return Json("Expense not found with id " + id); }

            var expense = _unitOfWork.Expense.GetFirstOrDefault(x => x.Id == id);
            return Json(expense);
        }

        [HttpPost]
        public IActionResult Update(ExpenseModel expenseModel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Expense.Update(expenseModel);
                _unitOfWork.SaveChanges();
                return Json("Expense Updated!");
            }
            else
            {
                return Json("Expense Model validation failed!");
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var expense = _unitOfWork.Expense.GetFirstOrDefault(x => x.Id == id);

            if (expense != null)
            {
                _unitOfWork.Expense.Delete(expense);
                _unitOfWork.SaveChanges();
                return Json("Expense Deleted!");
            }
            else
            {
                return Json("Expense not found");
            }
        }
    }
}
