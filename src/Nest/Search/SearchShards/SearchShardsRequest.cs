using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISearchShardsRequest : IRequest<SearchShardsRequestParameters>
	{
	}

	public interface ISearchShardsRequest<T> : ISearchShardsRequest {}

	public partial class SearchShardsRequest : RequestBase<SearchShardsRequestParameters>, ISearchShardsRequest
	{
	}

	public partial class SearchShardsRequest<T> : RequestBase<SearchShardsRequestParameters>, ISearchShardsRequest
		where T : class
	{
	}

	/// <summary>
	/// A descriptor wich describes a search operation for _search_shards
	/// </summary>
	public partial class SearchShardsDescriptor<T> : RequestDescriptorBase<SearchShardsDescriptor<T>, SearchShardsRequestParameters>, ISearchShardsRequest 
		where T : class
	{
	}
}
