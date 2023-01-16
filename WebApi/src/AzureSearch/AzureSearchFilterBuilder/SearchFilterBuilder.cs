using AzureSearch.AzureSearchFilterBuilder.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AzureSearch.AzureSearchFilterBuilder;

public class SearchFilterBuilder
{
    private readonly SearchFilterOperator searchFilterOperator;

    private readonly StringBuilder stringBuilder = new();

    public SearchFilterBuilder() : this(SearchFilterOperator.And)
    {
    }

    public SearchFilterBuilder(SearchFilterOperator searchFilterOperator)
    {
        this.searchFilterOperator = searchFilterOperator;
    }

    /// <summary>
    /// Add this to search options - to the OrderBy IList 
    /// </summary>
    /// <param name="fieldName"></param>
    /// <param name="descending">Set true for descending order, false for ascending</param>
    /// <returns></returns>
    public static string OrderBy(string fieldName, bool descending = false) =>
        $"{fieldName} {(descending ? "desc" : "asc")}";

    public SearchFilterBuilder AddFilter(string filter)
    {
        if (string.IsNullOrEmpty(filter))
        {
            return this;
        }

        stringBuilder.Append($"{filter} {searchFilterOperator.ToString().ToLowerInvariant()} ");

        return this;
    }

    public SearchFilterBuilder AddFilters(string filterLeft, SearchFilterOperator @operator, string filterRight)
    {
        var filter = string.Empty;
        if (!string.IsNullOrEmpty(filterLeft) && !string.IsNullOrEmpty(filterRight))
        {
            filter = $"({filterLeft} {@operator.ToString().ToLowerInvariant()} {filterRight})";
        }

        if (string.IsNullOrEmpty(filterLeft))
        {
            filter = filterRight;
        }

        if (string.IsNullOrEmpty(filterRight))
        {
            filter = filterLeft;
        }

        AddFilter(filter);

        return this;
    }

    public SearchFilterBuilder OnSite(string siteName) =>
        AddEqualsFilter("sys_site", siteName.ToLowerInvariant());

    public SearchFilterBuilder AddEqualsFilter(string filterName, bool? value, bool doNotAddTheFilter = false)
    {
        return value is bool val && !doNotAddTheFilter
            ? AddFilter($"{filterName} {GetConditionOperator(SearchConditionOperator.Equal)} {val.ToString().ToLowerInvariant()}")
            : this;
    }

    public SearchFilterBuilder AddEqualsFilter(string filterName, string value)
    {
        return value is string val
            ? AddFilter(GenerateFilterString(filterName, val, SearchConditionOperator.Equal))
            : this;
    }

    public SearchFilterBuilder AddNotEqualsFilter(string filterName, string value)
    {
        return value is string val
            ? AddFilter(GenerateFilterString(filterName, val, SearchConditionOperator.NotEquals))
            : this;
    }

    private string GenerateFilterString(string filterName, string val, SearchConditionOperator @operator) =>
        $"{filterName} {GetConditionOperator(@operator)} \'{val}\'";

    public SearchFilterBuilder AddFilter(string filterName, int? value, SearchConditionOperator @operator)
    {
        return value is int val
            ? AddFilter($"{filterName} {GetConditionOperator(@operator)} {val.ToString(CultureInfo.InvariantCulture)}")
            : this;
    }

    public SearchFilterBuilder AddInFilter(string filterName, ICollection<string> values, bool doNotAddTheFilter = false)
    {
        var orValues = values?.Select(v => GenerateFilterString(filterName, v, SearchConditionOperator.Equal)).ToArray();
        return values == null || values.Count == 0 || doNotAddTheFilter
            ? this
            : AddFilter($"({string.Join(" or ", orValues)})");
    }

    public SearchFilterBuilder AddAnyFilter(string filterName, string value, bool doNotAddTheFilter = false)
    {
        return AddAnyFilter(filterName, new List<string>() { value }, doNotAddTheFilter);
    }

    public SearchFilterBuilder AddAnyFilter(string filterName, ICollection<string> values, bool doNotAddTheFilter = false)
    {
        var orValues = values?.Select(v => GenerateFilterString("x", v, SearchConditionOperator.Equal)).ToArray();
        return values == null || values.Count == 0 || doNotAddTheFilter
            ? this
            : AddFilter($"{filterName}/any(x:{string.Join(" or ", orValues)})");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filterName"></param>
    /// <param name="from">Value from</param>
    /// <param name="to">Value to</param>
    /// <param name="inclusive">Include values from and to?</param>
    /// <returns></returns>
    public SearchFilterBuilder AddBetweenFilter(string filterName, int? from, int? to, bool inclusive = true)
    {
        return AddFilter(filterName, from, inclusive ? SearchConditionOperator.GreaterThanOrEqual : SearchConditionOperator.GreaterThan)
            .AddFilter(filterName, to, inclusive ? SearchConditionOperator.LessThanOrEqual : SearchConditionOperator.LessThan);
    }

    private string GetConditionOperator(SearchConditionOperator @operator) => @operator switch
    {
        SearchConditionOperator.Equal => "eq",
        SearchConditionOperator.NotEquals => "ne",
        SearchConditionOperator.GreaterThan => "gt",
        SearchConditionOperator.LessThan => "lt",
        SearchConditionOperator.GreaterThanOrEqual => "ge",
        SearchConditionOperator.LessThanOrEqual => "le",
        _ => throw new NotImplementedException(),
    };

    public string Build()
    {
        if (stringBuilder.Length == 0)
        {
            return string.Empty;
        }

        return stringBuilder.ToString(0, stringBuilder.Length - (searchFilterOperator.ToString().Length + 2));
    }
}
