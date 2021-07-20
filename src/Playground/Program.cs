using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace Playground
{
	public class Person
	{
		public Person(string name) => Name = name;

		public string Name { get; }

		public int? Age { get; init; }

		public string? Email { get; init; }
	}

	internal class Program
	{
		private static async Task Main()
		{
			IElasticClient client = new ElasticClient(new Uri("http://localhost:9200"));

			var indexName = Guid.NewGuid().ToString();

			// Get cluster health
			var clusterHealthRequest = new ClusterHealthRequest {Level = Level.Cluster};
			var clusterHealthResponse = await client.Cluster.HealthAsync(clusterHealthRequest);

			// Create Index
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

			// Index document
			var indexResponse =
				await client.IndexAsync(new IndexRequest<Person>(indexName, 1) {Document = new Person("Steve")});

			// TODO - Retrieve document by ID

			// TODO - Search

			// Delete index
			var deleteResponse = await client.Indices.DeleteAsync(new DeleteIndicesRequest(indexName));
		}
	}
}
