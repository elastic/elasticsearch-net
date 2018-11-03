namespace Elasticsearch.Net
{
	/// <summary>
	/// The minimum interface your custom responses should implement when providing a response type
	/// to the low level client
	/// </summary>
	public interface IElasticsearchResponse
	{
		/// <summary>
		/// Sets and returns the <see cref="IApiCallDetails" /> diagnostic information
		/// </summary>
		IApiCallDetails ApiCall { get; set; }

		bool TryGetServerErrorReason(out string reason);
	}
}
