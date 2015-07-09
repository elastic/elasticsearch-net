namespace Nest
{
	/// <summary>
	/// POCO representing the reindex response for a each step
	/// </summary>
	public interface IReindexResponse<T>  where T : class
	{
		/// <summary>
		/// The bulk result indexing the search results into the new index.
		/// </summary>
		IBulkResponse BulkResponse { get; }

		/// <summary>
		/// The scroll result
		/// </summary>
		ISearchResponse<T> SearchResponse { get; }

		/// <summary>
		/// The no of scroll this result represents
		/// </summary>
		int Scroll { get;  }

		/// <summary>
		/// Whether both the scroll and reindex result are valid
		/// </summary>
		bool IsValid { get; }
	}
}