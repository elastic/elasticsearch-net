using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace Playground
{
	public class MyType
	{
		public MyType(string name) => Name = name;

		public string Name { get; }
	}

	internal class Program
	{
		private static async Task Main()
		{
			IElasticClient client = new ElasticClient(new Uri("http://localhost:9200"));

			var indexName = Guid.NewGuid().ToString();

			var clusterHealthRequest = new ClusterHealthRequest {Level = Level.Cluster};
			var clusterHealthResponse = await client.Cluster.HealthAsync(clusterHealthRequest);

			var createResponse = await client.Indices.CreateAsync(new IndicesCreateRequest(indexName)
			{
				Mappings = new TypeMapping
				{
					DateDetection = false,
					Properties = new Dictionary<PropertyName, PropertyBase>
					{
						{"age", new NumberProperty {Type = NumberType.Integer}},
						{"name", new TextProperty()},
						{"email", new KeywordProperty()}
					},
					Meta = new Metadata {{"foo", "bar"}}
				}
			});

			var deleteResponse = await client.Indices.DeleteAsync(new DeleteIndicesRequest(indexName));

			//var response = await client.IndexAsync(new IndexRequest<MyType>("test-001", Guid.NewGuid()){ Document = new MyType("Steve Gordon")});
			//if (response.IsValid)
			//{

			//}

			//await client.PingAsync(new PingRequest());

			//await client.Cluster.HealthAsync();


			//var clusterHealthResponse = await client.Cluster.HealthAsync(clusterHealthRequest);

			//if (clusterHealthResponse.IsValid)
			//{
			//	Console.WriteLine($"Cluster Name: {clusterHealthResponse.ClusterName}");
			//	Console.WriteLine($"Status: {clusterHealthResponse.Status:G}");
			//	Console.WriteLine($"Active Primaries: {clusterHealthResponse.ActivePrimaryShards}");
			//}

			//var createIndexRequest = new IndicesCreateRequest($"test-{Guid.NewGuid()}");
			//var createIndexResponse = await client.Indices.CreateAsync(createIndexRequest);

			//if (createIndexResponse.IsValid)
			//	Console.WriteLine($"Shared Acknowledged: {createIndexResponse.ShardsAcknowledged}");

			//createIndexRequest = new IndicesCreateRequest($"test-{Guid.NewGuid()}")
			//{
			//	Mappings = new TypeMapping
			//	{
			//		Properties = new Dictionary<PropertyName, Property>
			//		{
			//			//{"fieldOne", new TextProperty() }
			//		}
			//	}
			//};

			// TODO: It might be kinda nice if the client accepts no request here and uses a cached default instance
			//var searchResponse = await client.SearchAsync(new SearchRequest());

			//if (searchResponse.IsValid)
			//{
			//	Console.WriteLine($"Took: {searchResponse.Took}");
			//}

			//var clientConcrete = new ElasticClient(new Uri("http://localhost:9200"));

			//await clientConcrete.PingAsync();
			//await clientConcrete.Indices.DeleteAsync("testing");
		}
	}
}
