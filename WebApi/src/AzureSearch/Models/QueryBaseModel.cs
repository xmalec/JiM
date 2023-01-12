using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AzureSearch.Models;
public abstract class QueryBaseModel
{
    protected QueryBaseModel(string searchString, int take, int skip)
    {
        Take = take;
        Skip = skip;
        SearchString = searchString;
    }

    public string SearchString { get; set; }
    public int Take { get; set; }
    public int Skip { get; set; }
    public abstract IReadOnlyList<string> Selects { get; }
    public abstract IReadOnlyList<string> OrderBys { get; }
    public abstract (IList<string> Selected, IList<string> Unselected) DividedFilters { get; }
}
