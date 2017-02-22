namespace Elasticsearch.Net_5_2_0
{
	/// <summary>
	/// Implementing this interface on your response objects will cause the low level client to
	/// automatically set <see cref="IApiCallDetails"/> diagnostic information on the object.
	/// </summary>
	public interface IBodyWithApiCallDetails
	{
		/// <summary>
		/// Sets and returns the <see cref="IApiCallDetails"/> diagnostic information
		/// </summary>
		IApiCallDetails ApiCall { get; set; }
	}
}
