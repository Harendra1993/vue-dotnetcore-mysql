using System.Collections.Generic;
using PPR.Data.Entities;

namespace PPR.Business.interfaces {
    public interface IDashboardRepository {
        IList<Book> Books ();
    }
}