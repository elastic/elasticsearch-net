using System.Collections.Generic;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ISearchShardsRequest : IQueryPath<SearchShardsRequestParameters>
	{
	}

	public interface ISearchShardsRequest<T> : ISearchShardsRequest {}

	internal static class SearchShardsPathInfo
	{
		public static void Update(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchShardsRequestParameters> pathInfo, ISearchShardsRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.GET; 
		}
	}
	
	public partial class SearchShardsRequest : QueryPathBase<SearchShardsRequestParameters>, ISearchShardsRequest
	{
		public SearchShardsRequest() {}

		public SearchShardsRequest(IndexNameMarker index, TypeNameMarker type = null) : base(index, type) { }

		public SearchShardsRequest(IEnumerable<IndexNameMarker> indices, IEnumerable<TypeNameMarker> types = null) : base(indices, types) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchShardsRequestParameters> pathInfo)
		{
			SearchShardsPathInfo.Update(settings, pathInfo, this);
		}
	}

	public partial class SearchShardsRequest<T> : QueryPathBase<SearchShardsRequestParameters, T>, ISearchShardsRequest
		where T : class
	{
		public SearchShardsRequest() {}

		public SearchShardsRequest(IndexNameMarker index, TypeNameMarker type = null) : base(index, type) { }

		public SearchShardsRequest(IEnumerable<IndexNameMarker> indices, IEnumerable<TypeNameMarker> types = null) : base(indices, types) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchShardsRequestParameters> pathInfo)
		{
			SearchShardsPathInfo.Update(settings,pathInfo, this);
		}
	}


	/// <summary>
	/// A descriptor wich describes a search operation for _search_shards
	/// </summary>
	public partial class SearchShardsDescriptor<T> : QueryPathDescriptorBase<SearchShardsDescriptor<T>, SearchShardsRequestParameters, T>, ISearchShardsRequest 
		where T : class
	{

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchShardsRequestParameters> pathInfo)
		{
			SearchShardsPathInfo.Update(settings,pathInfo, this);
		}

	}
}
