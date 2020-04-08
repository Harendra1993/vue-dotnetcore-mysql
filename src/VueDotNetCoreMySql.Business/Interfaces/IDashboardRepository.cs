using System.Collections.Generic;
using VueDotNetCoreMySql.Common.Entities;

namespace VueDotNetCoreMySql.Business.Interfaces
{
    public interface IDashboardRepository
    {
        IList<Book> Books();
    }
}