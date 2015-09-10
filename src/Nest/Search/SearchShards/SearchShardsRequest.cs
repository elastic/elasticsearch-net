using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISearchShardsRequest : IQueryPath<SearchShardsRequestParameters>
	{
	}

	public interface ISearchShardsRequest<T> : ISearchShardsRequest {}

	public partial class SearchShardsRequest : QueryPathBase<SearchShardsRequestParameters>, ISearchShardsRequest
	{
		public SearchShardsRequest() {}

		public SearchShardsRequest(IndexName index, TypeName type = null) : base(index, type) { }

		public SearchShardsRequest(IEnumerable<IndexName> indices, IEnumerable<TypeName> types = null) : base(indices, types) { }
	}

	public partial class SearchShardsRequest<T> : QueryPathBase<SearchShardsRequestParameters, T>, ISearchShardsRequest
		where T : class
	{
		public SearchShardsRequest() {}

		public SearchShardsRequest(IndexName index, TypeName type = null) : base(index, type) { }

		public SearchShardsRequest(IEnumerable<IndexName> indices, IEnumerable<TypeName> types = null) : base(indices, types) { }
	}


	/// <summary>
	/// A descriptor wich describes a search operation for _search_shards
	/// </summary>
	public partial class SearchShardsDescriptor<T> : QueryPathDescriptorBase<SearchShardsDescriptor<T>, SearchShardsRequestParameters, T>, ISearchShardsRequest 
		where T : class
	{
	}
}
