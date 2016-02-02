using Newtonsoft.Json;

namespace Nest
{
    /// <summary>
    /// The reindex response for a reindexing step
    /// </summary>
    public interface IReindexResponse<T> where T : class
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
        int Scroll { get; }

        /// <summary>
        /// Whether both the scroll and reindex result are valid
        /// </summary>
        bool IsValid { get; }
    }

    /// <summary>
    /// POCO representing the reindex response for a each step
    /// </summary>
    [JsonObject]
	public class ReindexResponse<T> : IReindexResponse<T> where T : class
	{
		public IBulkResponse BulkResponse { get; internal set; }
		public ISearchResponse<T> SearchResponse { get; internal set; }

		public int Scroll { get; internal set; }

		public bool IsValid => 
			this.BulkResponse != null && this.BulkResponse.IsValid
			&& this.SearchResponse != null && this.SearchResponse.IsValid;
	}
}
