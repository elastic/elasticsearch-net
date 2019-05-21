namespace Nest
{
	public partial interface ISearchShardsRequest { }

	public partial interface ISearchShardsRequest<TDocument> : ISearchShardsRequest { }

	public partial class SearchShardsRequest { }

	public partial class SearchShardsRequest<TDocument> where TDocument : class { }

	/// <summary>
	/// A descriptor which describes a search operation for _search_shards
	/// </summary>
	public partial class SearchShardsDescriptor<TDocument> where TDocument : class { }
}
