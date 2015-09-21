using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{
	//TODO removed typed variant is this ok?
	public partial interface ISearchShardsRequest 
	{
	}

	public partial class SearchShardsRequest : RequestBase<SearchShardsRequestParameters>, ISearchShardsRequest
	{
	}

	/// <summary>
	/// A descriptor wich describes a search operation for _search_shards
	/// </summary>
	public partial class SearchShardsDescriptor<T> where T : class
	{
	}
}
