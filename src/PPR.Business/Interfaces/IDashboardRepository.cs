using System.Collections.Generic;
using PPR.Common.Entities;

namespace PPR.Business.Interfaces {
    public interface IDashboardRepository {
        IList<Book> Books ();
    }
}