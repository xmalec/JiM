using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Search.Documents.Indexes;

namespace AzureSearch.Models;
public abstract class BaseIndexModel
{
    /// <summary>
    /// Content item id concatenated with language
    /// </summary>
    [SimpleField(IsKey = true, IsFilterable = true)]
    public string Id { get; set; }
}
