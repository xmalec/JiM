using System;
using System.Collections.Generic;
using System.Linq;
using Bluesoft.Utils.AzureSearchFilterBuilder;

namespace AzureSearch.Models;

public class JobRequisitionsQueryModel : QueryBaseModel
{
    public JobRequisitionsQueryModel(string searchString, int take, int skip) : base(searchString, take, skip)
    {
    }

    public IList<string> Fields { get; set; }
    public IList<string> Categories { get; set; }
    public IList<string> Localities { get; set; }
    public string Language { get; set; }

    public override IReadOnlyList<string> Selects { get; } = Array.Empty<string>();

    public override IReadOnlyList<string> OrderBys { get; } = new List<string>
    {
        SearchFilterBuilder.OrderBy(nameof(JobRequisitionIndexModel.PostingStartDate), true)
    };

    public override (IList<string> Selected, IList<string> Unselected) DividedFilters
    {
        get
        {
            var selected = new List<string>();
            var unselected = new List<string>();
            if(Fields.Any())
            {
                selected.Add(nameof(JobRequisitionIndexModel.FieldSlugs));
            } else
            {
                unselected.Add(nameof(JobRequisitionIndexModel.FieldSlugs));
            }
            if (Localities.Any())
            {
                selected.Add(nameof(JobRequisitionIndexModel.LocationSlugs));
            }
            else
            {
                unselected.Add(nameof(JobRequisitionIndexModel.LocationSlugs));
            }
            if (Categories.Any())
            {
                selected.Add(nameof(JobRequisitionIndexModel.CategorySlug));
            }
            else
            {
                unselected.Add(nameof(JobRequisitionIndexModel.CategorySlug));
            }
            return (selected, unselected);
        }
    }
}
