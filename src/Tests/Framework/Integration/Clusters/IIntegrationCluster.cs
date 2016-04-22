using System;
using Nest;

namespace Tests.Framework.Integration
{
	public interface IIntegrationCluster
	{
		ElasticsearchNode Node { get; }
		IElasticClient Client(Func<ConnectionSettings, ConnectionSettings> settings, bool forceInMemory = false);
	}
}
