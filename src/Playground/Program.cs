using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Clients.Elasticsearch.Experimental;
using Elastic.Transport;
using System.Collections.Generic;

namespace Playground
{
	internal class Program
	{
		private static void Main()
		{
			var ec = new Client();

//#pragma warning disable IDE0039 // Use local function
//			//Func<BoolQueryDescriptor<Person>, IBoolQuery> test = b => b.Name("thing");
//			//Local variables change type
//			Action<ExampleRequestDescriptor> test = b => b.Name("thing");
//#pragma warning restore IDE0039 // Use local function

//			//static IBoolQuery TestBoolQuery(BoolQueryDescriptor<Person> b) => b.Name("thing");

//			//Local functions become void returning
//			static void TestBoolQuery(ExampleRequestDescriptor b) => b.Name("thing");

//			ec.SomeEndpoint(TestBoolQuery);

//			ec.SomeEndpoint(new ExampleRequest
//			{
//				Name = "Object test",
//				Subtype = new ClusterSubtype
//				{
//					Identifier = "AnID"
//				},
//				Query = new Elastic.Clients.Elasticsearch.Experimental.QueryContainer(new Elastic.Clients.Elasticsearch.Experimental.BoolQuery { Tag = "variant_string" })
//			});

//			ec.SomeEndpoint(new ExampleRequest
//			{
//				Name = "Object test",
//				Subtype = new ClusterSubtype
//				{
//					Identifier = "AnID"
//				},
//				// Static query "helper" provides a way to use the fluent syntax that can be combined with object initialiser code
//				// at the cost of an extra object allocation
//				Query = Query.Bool(b => b.Tag("using_query_helper"))
//			});

			ec.SomeEndpoint(new ExampleRequest
			{
				Name = "Object test",
				Subtype = new ClusterSubtypeDescriptor().Identifier("implictly-assigned"),
				Query = Query.Bool(b => b.Tag("using_query_helper"))
			});

			//ec.SomeEndpoint(c => c
			//	.Name("Descriptor test")
			//	.Subtype(s => s.Identifier("AnID"))
			//	.Query(c => c.Bool(v => v.Tag("some_tag"))));

			//var descriptor = new ClusterSubtypeDescriptor().Identifier("AnID");

			//ec.SomeEndpoint(c => c
			//	.Name("Descriptor test")
			//	.Subtype(descriptor)
			//	.Query(c => c.Boosting(v => v.BoostAmount(10))));

			//ec.SomeEndpoint(c => c
			//	.Name("Mixed object and descriptor test")
			//	.Subtype(new ClusterSubtype { Identifier = "AnID" }));

			//var requestDescriptor = new ExampleRequestDescriptor().Name("descriptor_usage");

			//ec.SomeEndpoint(requestDescriptor);

			//var boolQuery = new Elastic.Clients.Elasticsearch.Experimental.BoolQuery { Tag = "TEST" };

			//var container = boolQuery.WrapInContainer();

			//if (container.TryGetBoolQuery(out boolQuery))
			//{
			//	Console.WriteLine(boolQuery.Tag);
			//}

			ec.CombinedEndpoint(r => r.WithName("Steve").WithThing(t => t.WithTitle("Title")));

			var client = new ElasticClient(new ElasticsearchClientSettings(new Uri("https://azure.es.eastus.azure.elastic-cloud.com:9243/"))
				.CertificateFingerprint("1E69964DFF1259B9ADE47556144E501F381A84B07E5EEC84B81ECF7D4B850C1D")
				.Authentication(new BasicAuthentication("elastic", "Z9vNfZN86RxHJ97Poi1BYhC6")));

			// client.ElasticsearchClientSettings.ResponseHeadersToParse

			var searchAgain = new SearchRequest()
			{
				Query = new Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer(new Elastic.Clients.Elasticsearch.QueryDsl.BoolQuery { Boost = 1.2F }),
				MinScore = 10.0,
				Profile = true,
				RequestConfiguration = new RequestConfiguration() { ResponseHeadersToParse = new HeadersList("made-up") }
			};

			var response = client.Search<Person>(searchAgain);

			// TODO: The original search request includes header parsing as the config is cached - we should reset this on the product check flow?

			response = client.Search<Person>(searchAgain);

			//var response = client.Transport.Request<BytesResponse>(HttpMethod.GET, "test");



			//var stream = new MemoryStream();
			//IMatchQuery match = new MatchQuery() { QueryName = "test_match", Field = "firstName", Query = "Steve" };
			//client.ElasticsearchClientSettings.SourceSerializer.Serialize(match, stream);
			//stream.Position = 0;
			//var json = Encoding.UTF8.GetString(stream.ToArray());

			//var matchAll = new QueryContainer(new MatchAllQuery() { QueryName = "test_query" });
			//var boolQuery = new QueryContainer(new BoolQuery() { Filter = new[] { new QueryContainer(new MatchQuery() { QueryName = "test_match", Field = "firstName", Query = "Steve" }) }});

			var search = new SearchRequest()
			{
				Query = new Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer(new Elastic.Clients.Elasticsearch.QueryDsl.BoolQuery { Boost = 1.2F }),
				MinScore = 10.0,
				Profile = true
			};

			var stream1 = new MemoryStream();
			client.ElasticsearchClientSettings.SourceSerializer.Serialize(search, stream1);
			stream1.Position = 0;
			var json1 = Encoding.UTF8.GetString(stream1.ToArray());

			ISearchRequest d = new SearchRequestDescriptor().MinScore(10.0).Profile(true);

			var stream = new MemoryStream();
			client.ElasticsearchClientSettings.SourceSerializer.Serialize(d, stream);
			stream.Position = 0;
			var json = Encoding.UTF8.GetString(stream.ToArray());

			if (json.Length > 0)
				Console.WriteLine(json);

			var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
			var request = client.ElasticsearchClientSettings.SourceSerializer.Deserialize<ISearchRequest>(jsonStream);

			if (request is not null)
				Console.WriteLine("DONE");

			//var response = await client.SearchAsync<Person>(search);

			//var fluentResponse = client.SearchAsync<Person>(s => s.Query(matchAll));

			//var matchQueryOne = Query<Person>.Match(m => m.Field(f => f.FirstName).Query("Steve"));
			//var matchQueryTwo = new QueryContainer(new MatchQuery() { Field = "firstName"/*Infer.Field<Person>(f => f.FirstName)*/, Query = "Steve" });
			//var matchQueryThree = new QueryContainerDescriptor<Person>().Match(m => m.Query(SearchQuery.String("Steve")));
			//var matchQueryFour = new QueryContainerDescriptor<Person>().Match(m => m.Analyzer(""));

			//var s = new ElasticsearchClientSettings();

			//IElasticClient client = new ElasticClient(s);

			//var indexName = Guid.NewGuid().ToString();

			//// Get cluster health
			//var clusterHealthRequest = new HealthRequest {Level = Level.Cluster};
			//var clusterHealthResponse = await client.Cluster.HealthAsync(clusterHealthRequest);
			//var clusterHealthResponseTwo = await client.Cluster.HealthAsync(d => d.Level(Level.Cluster));

			//var allocExplainRequest = new AllocationExplainRequest { Primary = true };
			//var allocExplainResponse = await client.Cluster.AllocationExplainAsync(allocExplainRequest);

			//var personDoc = new Person("Steve Gordon") { Age = 36, Email = "sgordon@example.com" };
			//var indexResponseOne = await client.IndexAsync(new IndexRequest<Person>("people") { Document = personDoc });
			//var indexResponseTwo = await client.IndexAsync(personDoc, "people");

			//if (!clusterHealthResponse.IsValid)
			//	throw new Exception("Failed to get cluster health");
			//if (clusterHealthResponse.Status == Health.Red)
			//	throw new Exception("Cluster is unhealthy");

			//// Create an index
			//var createResponse = await client.IndexManagement.CreateAsync(new CreateRequest(indexName)
			//{
			//	Mappings = new TypeMapping
			//	{
			//		DateDetection = false,
			//		Properties = new Dictionary<PropertyName, PropertyBase>
			//		{
			//			{"age", new NumberProperty {Type = NumberType.Integer}},
			//			{"name", new TextProperty()},
			//			{"email", new KeywordProperty()}
			//		},
			//		Meta = new Metadata { { "foo", "bar" } }
			//	}
			//});

			//if (!createResponse.IsValid)
			//	throw new Exception($"Failed to create index {indexName}");
			//if (!createResponse.Acknowledged)
			//	throw new Exception("The create index request has not been acknowledged");

			//// TODO - Check index exists

			//// Index a document
			//var indexResponse =
			//	await client.IndexAsync(new IndexRequest<Person>(indexName, new Id("1"))
			//	{
			//		Document = new Person("Steve") { Age = 36, Email = "test@example.com" }
			//	});

			//if (!indexResponse.IsValid)
			//	throw new Exception("Failed to index the document");

			//Console.WriteLine($"Indexed document with ID {indexResponse.Id}");

			//// Index a document without explicit ID
			//var indexResponse2 =
			//	await client.IndexAsync(new IndexRequest<Person>(indexName)
			//	{
			//		Document = new Person("Joe") { Age = 40, Email = "test2@example.com" }
			//	});

			//if (!indexResponse2.IsValid)
			//	throw new Exception("Failed to index the document");

			//Console.WriteLine($"Indexed document with ID {indexResponse2.Id}");

			//// Check the document exists
			//var documentExistsResponse = await client.ExistsAsync(indexName, indexResponse.Id);

			//if (!documentExistsResponse.IsValid)
			//	throw new Exception("Failed to check if the document exists");
			//if (!documentExistsResponse.Exists)
			//	throw new Exception($"Document with ID {indexResponse.Id} does not exist");

			//// Get the document by its ID
			//var getDocumentResponse = await client.GetAsync<Person>(indexName, indexResponse.Id);

			//if (!getDocumentResponse.IsValid)
			//	throw new Exception($"Failed to get a document with ID {indexResponse.Id}");

			////var refreshResponse = await client.IndexManagement.RefreshAsync(new RefreshRequest(indexName));

			////if (!refreshResponse.IsValid)
			////	throw new Exception($"Failed to refresh index {indexName}");

			//// Basic search
			//var searchResponse =
			//	await client.SearchAsync<Person>(new SearchRequest(indexName) { RestTotalHitsAsInt = true });

			//var searchResponseTwo =
			//	await client.SearchAsync<Person>(new SearchRequest(Indices.All) { RestTotalHitsAsInt = true });

			//if (!searchResponse.IsValid)
			//	throw new Exception("Failed to search for any documents");

			//// TODO - Union deserialisation not yet implemented
			////long hits = 0;
			////searchResponse.Hits.Total.Match(a => hits = a.Value, b => hits = b);
			//// Console.WriteLine($"The basic search found {hits} hits");

			//Console.WriteLine($"The basic search found {searchResponse.Hits.Hits.Count} hits");

			//// Basic terms agg

			//// Original
			////var request = new SearchRequest<Person>
			////{
			////	Aggregations = new TermsAggregation("symbols")
			////	{
			////		Field = Infer.Field<Person>(f => f.Name),
			////		Size = 1000
			////	},
			////	Size = 0
			////};

			//// Basic terms aggregation
			//var request = new SearchRequest(indexName)
			//{
			//	Aggregations =
			//		new
			//			Dictionary<string,	AggregationContainer> // TODO - Can this be improved to align with our existing style?
			//			{
			//				{
			//					"names",
			//					new()
			//					{
			//						Terms = new TermsAggregation
			//						{
			//							//Field = Infer.Field<Person>(f => f.Email!),
			//							Field = "email",
			//							Size = 100
			//						}
			//					}
			//				}
			//			},
			//	Size = 0,
			//	//Profile = true,
			//	RestTotalHitsAsInt = true // required for now until union converter is able to handle objects! TODO
			//};

			//searchResponse = await client.SearchAsync<Person>(request);

			//if (!searchResponse.IsValid)
			//	throw new Exception("Failed to search for any documents (using aggregation)");

			//// TODO - Search with aggregation to get average age

			//// Delete document
			//var deleteDocumentResponse = await client.DeleteAsync(indexName, indexResponse.Id);

			//if (!deleteDocumentResponse.IsValid || deleteDocumentResponse.Result != Result.Deleted)
			//	throw new Exception($"Failed to delete document with ID {indexResponse.Id}");

			//// Delete index
			//var deleteIndexResponse = await client.IndexManagement.DeleteAsync(new Elastic.Clients.Elasticsearch.IndexManagement.DeleteRequest(indexName));

			//if (!deleteIndexResponse.IsValid)
			//	throw new Exception($"Failed to delete index {indexName}");
		}
	}
}
