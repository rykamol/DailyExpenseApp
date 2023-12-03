
using ExpenseManagement.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private ExpenseDbContext _db;

        public UnitOfWork(ExpenseDbContext db)
        {
            _db = db;
            Expense = new ExpenseRepository(_db);
        }

        public IExpenseRepository Expense { get; private set; }

        void IUnitOfWork.SaveChanges()
        {
            _db.SaveChanges();
        }
    }
}
