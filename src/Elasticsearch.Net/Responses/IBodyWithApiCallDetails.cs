namespace Elasticsearch.Net
{
	public interface IBodyWithApiCallDetails
	{
		IApiCallDetails CallDetails { get; set; }
	}
}