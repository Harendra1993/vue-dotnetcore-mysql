using System.Collections.Generic;
using System.Linq;
using VueDotNetMySql.Business.Interfaces;
using VueDotNetMySql.Common.Entities;
using VueDotNetMySql.Data;

namespace VueDotNetMySql.Business.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {

        private readonly AppDbContext _dataContext;
        public DashboardRepository()
        {
            _dataContext = new AppDbContext();
        }
        public IList<Book> Books()
        {
            IList<Book> data = _dataContext.Book.ToList();

            return data;
        }
    }
}