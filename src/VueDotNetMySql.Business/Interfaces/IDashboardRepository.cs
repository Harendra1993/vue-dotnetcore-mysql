using System.Collections.Generic;
using VueDotNetMySql.Common.Entities;

namespace VueDotNetMySql.Business.Interfaces
{
    public interface IDashboardRepository
    {
        IList<Book> Books();
    }
}