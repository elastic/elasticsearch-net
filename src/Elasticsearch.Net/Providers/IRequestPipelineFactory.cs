namespace Elasticsearch.Net_5_2_0
{
	public interface IRequestPipelineFactory
	{
		IRequestPipeline Create(IConnectionConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider, IMemoryStreamFactory memorystreamFactory, IRequestParameters requestParameters);
	}

	public class RequestPipelineFactory : IRequestPipelineFactory
	{
		public IRequestPipeline Create(IConnectionConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider, IMemoryStreamFactory memorystreamFactory, IRequestParameters requestParameters) =>
			new RequestPipeline(configurationValues, dateTimeProvider, memorystreamFactory, requestParameters);
    }
}