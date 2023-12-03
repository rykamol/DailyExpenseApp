using ExpenseManagement.Data.Repository.IRepository;
using ExpenseManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Data.Repository
{
    public class ExpenseRepository : Repository<ExpenseModel>, IExpenseRepository
    {
        private ExpenseDbContext _context;
        public ExpenseRepository(ExpenseDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<ExpenseModel> Search(string searchString)
        {
            try
            {
                return _context.Expenses.Where(x => x.Title.Contains(searchString)).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(ExpenseModel expense)
        {
            _context.Expenses.Update(expense);
        }
    }
}
