using System.Collections.Generic;
using System.Linq;
using PPR.Business.Interfaces;
using PPR.Data;
using PPR.Data.Entities;

namespace PPR.Business.Repositories {
    public class DashboardRepository : IDashboardRepository {

        private readonly AppDbContext _dataContext;
        public DashboardRepository () {
            _dataContext = new AppDbContext ();
        }
        public IList<Book> Books () {
            IList<Book> data = _dataContext.Book.ToList ();

            return data;
        }
    }
}