using AzureSearch.AzureSearchFilterBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureSearch.Models.ParentPage;

public class ParentPageQueryModel : QueryBaseModel
{
    public ParentPageQueryModel(string searchString, int take, int skip) : base(searchString, take, skip)
    {
    }

    public IList<string> Fields { get; set; }
    public IList<string> Categories { get; set; }
    public IList<string> Localities { get; set; }
    public string Language { get; set; }

    public override IReadOnlyList<string> Selects { get; } = Array.Empty<string>();

    public override IReadOnlyList<string> OrderBys { get; } = new List<string>
    {
        SearchFilterBuilder.OrderBy(nameof(ParentPageIndexModel.PostingStartDate), true)
    };

    public override (IList<string> Selected, IList<string> Unselected) DividedFilters
    {
        get
        {
            var selected = new List<string>();
            var unselected = new List<string>();
            if (Localities.Any())
            {
                selected.Add(nameof(ParentPageIndexModel.Tags));
            }
            else
            {
                unselected.Add(nameof(ParentPageIndexModel.Tags));
            }
            return (selected, unselected);
        }
    }
}
