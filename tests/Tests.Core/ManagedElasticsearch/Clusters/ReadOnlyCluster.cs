// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Net.Http;
using static Elastic.Stack.ArtifactsApi.Products.ElasticsearchPlugin;

namespace Tests.Core.ManagedElasticsearch.Clusters
{
	public class ReadOnlyCluster : ClientTestClusterBase
	{
		public ReadOnlyCluster() : base(MapperMurmur3) { }

		//protected override void SeedNode() => new DefaultSeeder(Client).SeedNode();

		protected override void SeedNode()
		{
			// TODO: Very, very temporary!

			var client = new HttpClient();
			client.PutAsync("http://127.0.0.1:9200/project", new StringContent(string.Empty)).GetAwaiter().GetResult();
		}
	}
}
