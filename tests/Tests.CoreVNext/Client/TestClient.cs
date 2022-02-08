// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest;
using Tests.Configuration;
using Tests.Core.Client.Settings;
using Tests.Domain.Extensions;

namespace Tests.Core.Client
{
	public static class TestClient
	{
		public static readonly TestConfigurationBase Configuration = TestConfiguration.Instance;
		public static readonly IElasticClient Default = new ElasticClient(new TestConnectionSettings().ApplyDomainSettings());
		public static readonly IElasticClient DefaultInMemoryClient = new ElasticClient(new AlwaysInMemoryConnectionSettings().ApplyDomainSettings());
		
		public static IElasticClient FixedInMemoryClient(byte[] response) => new ElasticClient(
			new AlwaysInMemoryConnectionSettings(response)
				.ApplyDomainSettings()
				.DisableDirectStreaming()
				.EnableHttpCompression(false)
			);

		public static readonly IElasticClient DisabledStreaming =
			new ElasticClient(new TestConnectionSettings().ApplyDomainSettings().DisableDirectStreaming());
	}
}
