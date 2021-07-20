using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace Playground
{
	internal class Program
	{
		private static async Task Main()
		{
			IElasticClient client = new ElasticClient(new Uri("http://localhost:9200"));

			var indexName = Guid.NewGuid().ToString();

			// Get cluster health
			var clusterHealthRequest = new ClusterHealthRequest {Level = Level.Cluster};
			var clusterHealthResponse = await client.Cluster.HealthAsync(clusterHealthRequest);

			if (!clusterHealthResponse.IsValid)
				throw new Exception("Failed to get cluster health");
			if (clusterHealthResponse.Status == Health.Red)
				throw new Exception("Cluster is unhealthy");

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

			if (!createResponse.IsValid)
				throw new Exception("Failed to create the index");
			if (!createResponse.Acknowledged)
				throw new Exception("The create index request has not been acknowledged");

			// TODO - Check index exists

			// Index document
			var indexResponse =
				await client.IndexAsync(new IndexRequest<Person>(indexName, 1) {Document = new Person("Steve")});

			if (!indexResponse.IsValid)
				throw new Exception("Failed to index the document");

			Console.WriteLine($"Indexed document with ID {indexResponse.Id}");

			var documentExistsResponse = await client.ExistsAsync(indexName, indexResponse.Id);

			// Check the document exists
			if (!documentExistsResponse.IsValid)
				throw new Exception("Failed to check if the document exists");
			if (!documentExistsResponse.Exists)
				throw new Exception($"Document with ID {indexResponse.Id} does not exist");

			// TODO - Retrieve document by ID

			// TODO - Search

			// TODO - Delete document
			var deleteDocumentResponse = await client.DeleteAsync(indexName, indexResponse.Id);

			if (!deleteDocumentResponse.IsValid || deleteDocumentResponse.Result != Result.Deleted)
				throw new Exception($"Failed to delete document with ID {indexResponse.Id}");

			// Delete index
			var deleteIndexResponse = await client.Indices.DeleteAsync(new DeleteIndicesRequest(indexName));

			if (!deleteIndexResponse.IsValid)
				throw new Exception("Failed to delete the index");
		}
	}
}
