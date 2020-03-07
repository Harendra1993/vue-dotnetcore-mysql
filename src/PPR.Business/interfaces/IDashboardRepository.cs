using System.Collections.Generic;
using PPR.Data.Entites;

namespace PPR.Business.interfaces {
    public interface IDashboardRepository {
        IList<Book> Books ();
    }
}