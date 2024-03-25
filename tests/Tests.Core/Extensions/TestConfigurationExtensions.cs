// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using Elastic.Transport;
using Tests.Configuration;

namespace Tests.Core.Extensions
{
	public static class TestConfigurationExtensions
	{
		public static IRequestInvoker CreateConnection(this TestConfigurationBase configuration, bool forceInMemory = false, byte[] response = null)
		{
			var headers = new Dictionary<string, IEnumerable<string>> { { "x-elastic-product", new[] { "Elasticsearch" } } };

			return forceInMemory
				? new InMemoryRequestInvoker(response, headers: headers)
				: configuration.RunIntegrationTests
					? new HttpRequestInvoker()
					: new InMemoryRequestInvoker(response, headers: headers);
		}

		public static bool InRange(this TestConfigurationBase configuration, string range) =>
			configuration.ElasticsearchVersion.InRange(range);
	}
}
