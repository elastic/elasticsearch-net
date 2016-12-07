using Newtonsoft.Json;

namespace Nest
{
	/// <summary> A response returned for each scroll in ScrollAll() </summary>
	public interface IScrollAllResponse<T> where T : class
	{
		/// <summary>
		/// The scroll result
		/// </summary>
		ISearchResponse<T> SearchResponse { get; }

		/// <summary>
		/// The nth scroll this response represents
		/// </summary>
		long Scroll { get; }

		/// <summary>
		/// The nth slice this response belongs to
		/// </summary>
		int Slice { get; }
	}

	/// <summary> A response returned for each scroll in ScrollAll() </summary>
	[JsonObject]
	public class ScrollAllResponse<T> : IScrollAllResponse<T> where T : class
	{
		/// <inheritdoc />
		public ISearchResponse<T> SearchResponse { get; internal set; }
		/// <inheritdoc />
		public long Scroll { get; internal set; }
		/// <inheritdoc />
		public int Slice { get; internal set; }
		/// <inheritdoc />
		public bool IsValid => this.SearchResponse != null && this.SearchResponse.IsValid;
	}
}
