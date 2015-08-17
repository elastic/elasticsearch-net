namespace Tests.Framework.Integration
{
	public interface IIntegrationCluster
	{
		ElasticsearchNode Node { get; }
	}
}