using System.Collections.Generic;
using PPR.Data.Entities;

namespace PPR.Business.Interfaces {
    public interface IDashboardRepository {
        IList<Book> Books ();
    }
}