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

			// Create an index
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
				throw new Exception($"Failed to create index {indexName}");
			if (!createResponse.Acknowledged)
				throw new Exception("The create index request has not been acknowledged");

			// TODO - Check index exists

			// Index a document
			var indexResponse =
				await client.IndexAsync(new IndexRequest<Person>(indexName, 1)
				{
					Document = new Person("Steve") {Age = 36, Email = "test@example.com"}
				});

			if (!indexResponse.IsValid)
				throw new Exception("Failed to index the document");

			Console.WriteLine($"Indexed document with ID {indexResponse.Id}");

			// Index a document without explicit ID
			var indexResponse2 =
				await client.IndexAsync(new IndexRequest<Person>(indexName)
				{
					Document = new Person("Joe") {Age = 40, Email = "test2@example.com"}
				});

			if (!indexResponse2.IsValid)
				throw new Exception("Failed to index the document");

			Console.WriteLine($"Indexed document with ID {indexResponse2.Id}");

			// Check the document exists
			var documentExistsResponse = await client.ExistsAsync(indexName, indexResponse.Id);

			if (!documentExistsResponse.IsValid)
				throw new Exception("Failed to check if the document exists");
			if (!documentExistsResponse.Exists)
				throw new Exception($"Document with ID {indexResponse.Id} does not exist");

			// Get the document by its ID
			var getDocumentResponse = await client.GetAsync<Person>(indexName, indexResponse.Id);

			if (!getDocumentResponse.IsValid)
				throw new Exception($"Failed to get a document with ID {indexResponse.Id}");

			var refreshResponse = await client.Indices.RefreshAsync(new IndicesRefreshRequest(indexName));

			if (!refreshResponse.IsValid)
				throw new Exception($"Failed to refresh index {indexName}");

			// Basic search
			var searchResponse =
				await client.SearchAsync<Person>(new SearchRequest(indexName) {RestTotalHitsAsInt = true});

			if (!searchResponse.IsValid)
				throw new Exception("Failed to search for any documents");

			// TODO - Union deserialisation not yet implemented
			//long hits = 0;
			//searchResponse.Hits.Total.Match(a => hits = a.Value, b => hits = b);
			// Console.WriteLine($"The basic search found {hits} hits");

			Console.WriteLine($"The basic search found {searchResponse.Hits.Hits.Count} hits");

			// Basic terms agg

			// Original
			//var request = new SearchRequest<Person>
			//{
			//	Aggregations = new TermsAggregation("symbols")
			//	{
			//		Field = Infer.Field<Person>(f => f.Name),
			//		Size = 1000
			//	},
			//	Size = 0
			//};

			// Basic terms aggregation
			var request = new SearchRequest(indexName)
			{
				Aggregations =
					new
						Dictionary<string,
							AggregationContainer> // TODO - Can this be improved to align with our existing style?
						{
							{
								"names",
								new()
								{
									Terms = new TermsAggregation
									{
										Field = Infer.Field<Person>(f => f.Email!), Size = 100
									}
								}
							}
						},
				Size = 0,
				//Profile = true,
				RestTotalHitsAsInt = true // required for now until union converter is able to handle objects! TODO
			};

			searchResponse = await client.SearchAsync<Person>(request);

			if (!searchResponse.IsValid)
				throw new Exception("Failed to search for any documents (using aggregation)");

			// TODO - Search with aggregation to get average age

			// Delete document
			var deleteDocumentResponse = await client.DeleteAsync(indexName, indexResponse.Id);

			if (!deleteDocumentResponse.IsValid || deleteDocumentResponse.Result != Result.Deleted)
				throw new Exception($"Failed to delete document with ID {indexResponse.Id}");

			// Delete index
			var deleteIndexResponse = await client.Indices.DeleteAsync(new DeleteIndicesRequest(indexName));

			if (!deleteIndexResponse.IsValid)
				throw new Exception($"Failed to delete index {indexName}");
		}
	}
}
