using System.Collections.Generic;
using System.Linq;
using VueDotNetCoreMySql.Business.Interfaces;
using VueDotNetCoreMySql.Common.Entities;
using VueDotNetCoreMySql.Data;

namespace VueDotNetCoreMySql.Business.Repositories
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