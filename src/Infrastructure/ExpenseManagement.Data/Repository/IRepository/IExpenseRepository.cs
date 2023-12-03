using ExpenseManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ExpenseManagement.Data.Repository.IRepository
{
    public interface IExpenseRepository : IRepository<ExpenseModel>
    {
        void Update(ExpenseModel expense);
        IEnumerable<ExpenseModel> Search(string searchString);
    }
}
