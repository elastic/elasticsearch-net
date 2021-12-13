using System;
using System.IO;
using System.Text;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Clients.Elasticsearch.Helpers;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;

namespace Playground
{
	internal class Program
	{
		public class Thing
		{
			public QueryContainer? Query { get; set; }
		}

		private static void Main()
		{


			//var ec = new Client();

			////#pragma warning disable IDE0039 // Use local function
			////			//Func<BoolQueryDescriptor<Person>, IBoolQuery> test = b => b.Name("thing");
			////			//Local variables change type
			////			Action<ExampleRequestDescriptor> test = b => b.Name("thing");
			////#pragma warning restore IDE0039 // Use local function

			////			//static IBoolQuery TestBoolQuery(BoolQueryDescriptor<Person> b) => b.Name("thing");

			////			//Local functions become void returning
			////			static void TestBoolQuery(ExampleRequestDescriptor b) => b.Name("thing");

			////			ec.SomeEndpoint(TestBoolQuery);

			////			ec.SomeEndpoint(new ExampleRequest
			////			{
			////				Name = "Object test",
			////				Subtype = new ClusterSubtype
			////				{
			////					Identifier = "AnID"
			////				},
			////				Query = new Elastic.Clients.Elasticsearch.Experimental.QueryContainer(new Elastic.Clients.Elasticsearch.Experimental.BoolQuery { Tag = "variant_string" })
			////			});

			////			ec.SomeEndpoint(new ExampleRequest
			////			{
			////				Name = "Object test",
			////				Subtype = new ClusterSubtype
			////				{
			////					Identifier = "AnID"
			////				},
			////				// Static query "helper" provides a way to use the fluent syntax that can be combined with object initialiser code
			////				// at the cost of an extra object allocation
			////				Query = Query.Bool(b => b.Tag("using_query_helper"))
			////			});

			//ec.SomeEndpoint(new ExampleRequest
			//{
			//	Name = "Object test",
			//	Subtype = new ClusterSubtypeDescriptor().Identifier("implictly-assigned"),
			//	Query = Query.Bool(b => b.Tag("using_query_helper"))
			//});

			//ec.SomeEndpoint(c => c
			//	.Name("Descriptor test")
			//	.Subtype(s => s.Identifier("AnID"))
			//	.Query(c => c.Bool(v => v.Tag("some_tag"))));

			////var descriptor = new ClusterSubtypeDescriptor().Identifier("AnID");

			////ec.SomeEndpoint(c => c
			////	.Name("Descriptor test")
			////	.Subtype(descriptor)
			////	.Query(c => c.Boosting(v => v.BoostAmount(10))));

			////ec.SomeEndpoint(c => c
			////	.Name("Mixed object and descriptor test")
			////	.Subtype(new ClusterSubtype { Identifier = "AnID" }));

			////var requestDescriptor = new ExampleRequestDescriptor().Name("descriptor_usage");

			////ec.SomeEndpoint(requestDescriptor);

			////var boolQuery = new Elastic.Clients.Elasticsearch.Experimental.BoolQuery { Tag = "TEST" };

			////var container = boolQuery.WrapInContainer();

			////if (container.TryGetBoolQuery(out boolQuery))
			////{
			////	Console.WriteLine(boolQuery.Tag);
			////}

			//ec.CombinedEndpoint(r => r.WithName("Steve").WithThing(t => t.WithTitle("Title")));




			var client = new ElasticClient(new ElasticsearchClientSettings(new Uri("https://localhost:9600"))
				.Authentication(new BasicAuthentication("elastic", "Ey5c7EYcZ=g0JtMwo-+y"))
				.ServerCertificateValidationCallback((a, b, c, d) => true));
				//.CertificateFingerprint("3842926c8a7ef04bb24ecaf8a4c44b7e24a416d682f3b818cf553fde39470451"));

			var people = new Person[] { new Person { FirstName = "Steve" } };

			var bulkAll = new BulkAllObservable<Person>(client, new BulkAllRequest<Person>(people) { Index = "people-test" });

			var observer = bulkAll.Wait(TimeSpan.FromMinutes(1), n => { });



			client.Search<Person>(s => s
				.Size(1)
				.From(0)
				.Aggregations(a => a.Terms("my-terms", t => t.Field("firstName")))
				.Query(q => q.MatchAll()));

			var serialiser = client.RequestResponseSerializer;

			//var response = client.IndexManagement.CreateIndex("testing", i => i
			//	.Settings(s => s.Add("thing", 10)));

			//var qc = new QueryContainer(new BoolQuery { QueryName = "a_bool_query", Must = new[] { new QueryContainer(new TermQuery { Boost = 0.5f, Field = "the_field", Value = "the_value", CaseInsensitive = true }) } });
			//var thing = new Thing { Query = qc };

			var thing = new IndexRequest<Person>(new Person() { FirstName = "Steve", LastName = "Gordon", Age = 37 });

			var aggs = new AggregationDictionary
			{
				{ "startDates", new TermsAggregation("startDates") { Field = "startedOn" } },
				{ "endDates", new DateHistogramAggregation("endDates") { Field = "endedOn" } }
			};

			var search = new SearchRequest()
			{
				From = 10,
				Size = 20,
				Query = new QueryContainer(new MatchAllQuery()),
				Aggregations = aggs,
				PostFilter = new QueryContainer(new TermQuery
				{
					Field = "state",
					Value = "Stable"
				})
			};

			var stream = new MemoryStream();
			serialiser.Serialize(search, stream);
			stream.Position = 0;
			var json = Encoding.UTF8.GetString(stream.ToArray());

			stream.Position = 0;

			if (json.Length > 0)
			{
				var deserialised = serialiser.Deserialize<Thing>(stream);
			}

			//var response = client.Ping();

			//if (response.IsValid)
			//{

			//}

			//var searchAgain = new SearchRequest()
			//{
			//	Query = new Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer(new Elastic.Clients.Elasticsearch.QueryDsl.BoolQuery { Boost = 1.2F }),
			//	MinScore = 10.0,
			//	Profile = true
			//};

			//var jsonStream = new MemoryStream();
			//client.ElasticsearchClientSettings.RequestResponseSerializer.Serialize(searchAgain, jsonStream);
			//jsonStream.Position = 0;
			//var json = Encoding.UTF8.GetString(jsonStream.ToArray());

			//var response = client.Search<Person>(searchAgain);

			//// TODO: The original search request includes header parsing as the config is cached - we should reset this on the product check flow?

			//response = client.Search<Person>(searchAgain);

			////var response = client.Transport.Request<BytesResponse>(HttpMethod.GET, "test");



			////var stream = new MemoryStream();
			////IMatchQuery match = new MatchQuery() { QueryName = "test_match", Field = "firstName", Query = "Steve" };
			////client.ElasticsearchClientSettings.SourceSerializer.Serialize(match, stream);
			////stream.Position = 0;
			////var json = Encoding.UTF8.GetString(stream.ToArray());

			////var matchAll = new QueryContainer(new MatchAllQuery() { QueryName = "test_query" });
			////var boolQuery = new QueryContainer(new BoolQuery() { Filter = new[] { new QueryContainer(new MatchQuery() { QueryName = "test_match", Field = "firstName", Query = "Steve" }) }});

			//var search = new SearchRequest()
			//{
			//	Query = new Elastic.Clients.Elasticsearch.QueryDsl.QueryContainer(new Elastic.Clients.Elasticsearch.QueryDsl.BoolQuery { Boost = 1.2F }),
			//	MinScore = 10.0,
			//	Profile = true
			//};

			//var stream = new MemoryStream();
			//client.ElasticsearchClientSettings.SourceSerializer.Serialize(search, stream);
			//stream.Position = 0;
			//var json1 = Encoding.UTF8.GetString(stream.ToArray());

			//ISearchRequest d = new SearchRequestDescriptor().MinScore(10.0).Profile(true);

			//var newStream = new MemoryStream();
			//client.ElasticsearchClientSettings.SourceSerializer.Serialize(d, newStream);
			//stream.Position = 0;
			//var json = Encoding.UTF8.GetString(stream.ToArray());

			//if (json.Length > 0)
			//	Console.WriteLine(json);

			//var jsonStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
			//var request = client.ElasticsearchClientSettings.SourceSerializer.Deserialize<ISearchRequest>(jsonStream);

			//if (request is not null)
			//	Console.WriteLine("DONE");

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
