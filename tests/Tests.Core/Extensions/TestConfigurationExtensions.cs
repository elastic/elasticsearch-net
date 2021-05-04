// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;
using Tests.Configuration;

namespace Tests.Core.Extensions
{
	public static class TestConfigurationExtensions
	{
		public static IConnection CreateConnection(this TestConfigurationBase configuration, bool forceInMemory = false, byte[] response = null) =>
			forceInMemory
				? new InMemoryConnection(response)
				: configuration.RunIntegrationTests
					? (IConnection)new HttpConnection()
					: new InMemoryConnection(response);

		public static bool InRange(this TestConfigurationBase configuration, string range) =>
			configuration.ElasticsearchVersion.InRange(range);
	}
}
