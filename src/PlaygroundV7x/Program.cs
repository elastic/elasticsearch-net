// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;

namespace PlaygroundV7x
{
	internal class Program
	{
		private static readonly IElasticClient Client =
			new ElasticClient(new ConnectionSettings(new InMemoryConnection(Array.Empty<byte>(), 502, null, null))
				.DefaultIndex("index")
				.EnableHttpCompression(false)
			);

		private static void Main()
		{
			var things = new List<string>() { "a" };

			var bulkAll = Client.BulkAll(things, r => r
				.BackOffRetries(0) // Number of document retries. Can set this to zero to never retry
				.BackOffTime("30s") // Time to wait between retries
				.RetryDocumentPredicate((bulkResponseItem, logEntry) => false) // Decide if a document should be retried in the event of a failure.
																			   // The default behaviour is to retry only if bulkResponseItem.Status == 429.
																			   // This can be overridden by providing a more specific delegate, or opting to never retry failed operations.
				.ContinueAfterDroppedDocuments() // Continue indexing remaining items from the IEnumerable of documents even after some are dropped.
				.DroppedDocumentCallback((bulkResponseItem, logEntry) => // If a document cannot be indexed this delegate is called
				{
					Console.WriteLine($"{bulkResponseItem.Status} : {bulkResponseItem.Error.Reason}"); // Access operation failure information for the document
				})
				.MaxDegreeOfParallelism(4)
				.Size(1000));

			try
			{
				bulkAll.Wait(TimeSpan.FromMinutes(10), r => Console.WriteLine("Data indexed"));
			}
			catch (ElasticsearchClientException ex) when (ex.Response is not null)
			{
				if (ex.Response.HttpStatusCode.HasValue)
				{
					HandleStatusCode(ex.Response.HttpStatusCode);
				}
				else if (ex.Response.OriginalException is ElasticsearchClientException esException && esException.FailureReason == PipelineFailure.FailedProductCheck)
				{
					HandleStatusCode(esException.Response?.HttpStatusCode);
				}
			}

			static void HandleStatusCode(int? statusCode)
			{
				if (statusCode.HasValue)
				{
					switch (statusCode.Value)
					{
						case 404:
							Console.WriteLine("404"); // handle as required
							break;

						case 410:
							Console.WriteLine("410");
							break;

						// etc.
					}
				}
			}



//			var aggs = new AggregationDictionary
//			{
//				{ "startDates", new TermsAggregation("startDates") { Field = "startedOn" } },
//				{ "endDates", new DateHistogramAggregation("endDates") { Field = "endedOn" } }
//			};

//			var a = new SearchRequest()
//			{
//				From = 10,
//				Size = 20,
//				Query = new Query(new MatchAllQuery()),
//				Aggregations = aggs,
//				PostFilter = new Query(new TermQuery
//				{
//					Field = "state",
//					Value = "Stable"
//				})
//			};

//			var client = new ElasticClient(new ConnectionSettings(new InMemoryTransportClient())
//				.DefaultIndex("default-index")
//				//.DefaultMappingFor<Person>(m => m
//					//.DisableIdInference()
//					//.IndexName("people"))
//				//.IdProperty(id => id.SecondaryId)
//				//.RoutingProperty(id => id.SecondaryId)
//				//.RelationName("relation"))
//				//.DefaultFieldNameInferrer(s => $"{s}_2")
//				.EnableDebugMode());


//			var createIndexResponse = await client.Indices.CreateAsync("aa", i => i
//				.Map<Person>(m => m.Properties(p => p.Boolean(b => b))));



//			var filterResponse = await client.SearchAsync<Person>(s => s
//				.Query(q => q
//					.Bool(b => b
//						.Filter(
//							f => f.Term(t => t.Field(f => f.Age).Value(37)),
//							f => f.Term(t => t.Field(f => f.FirstName).Value("Steve"))
//						))));

//			var person = new Person { Id = 101, FirstName = "Steve", LastName = "Gordon", Age = 37, Email = "sgordon@example.com" };

//			var routingResponse = await client.IndexAsync(person, r => r);

//			client.Update<Person>("a", d => d.Index("test").Script(s => s.Source("script").Params(new Dictionary<string, object?> { { "null", new Person { FirstName = null, LastName = "test-surname" } } })));

//			var people = new List<Person>()
//			{
//				new Person{ FirstName = "Steve", LastName = "Gordon"},
//				new Person{ FirstName = "Steve", LastName = "Gordon"},
//				new Person{ FirstName = "Steve", LastName = "Gordon"},
//				new Person{ FirstName = "Steve", LastName = "Gordon"},
//				new Person{ FirstName = "Steve", LastName = "Gordon"},
//			};

//			//using var bulk = client.BulkAll(people, r => r.Index("testing-v7"));
//			//var result = bulk.Wait(TimeSpan.FromSeconds(60), a => { Console.WriteLine(a.Items.Count); });
//			//var a1 = result.TotalNumberOfRetries;
//			//var b1 = result.TotalNumberOfFailedBuffers;

//			using var bulk2 = client.BulkAll(people, r => r);
//			var result2 = bulk2.Wait(TimeSpan.FromSeconds(60), a => { Console.WriteLine(a.Items.Count); });
//			var a12 = result2.TotalNumberOfRetries;
//			var b12 = result2.TotalNumberOfFailedBuffers;

//			//var responseBulk = client.Bulk(new BulkRequest
//			//{
//			//	Operations = new List<IBulkOperation>
//			//{
//			//	new BulkIndexOperation<Person>(new Person()) { Index = "people" } ,
//			//	new BulkIndexOperation<Person>(new Person()) { Index = "people", IfSequenceNumber = -1, IfPrimaryTerm = 0 }
//			//}
//			//});

//			var response = client.Index(new Person(), e => e.Index("test"));

//			var settingsResponse = await client.Indices.CreateAsync("a", i => i.Settings(s => s.Analysis(a => a.TokenFilters(tf => tf
//				.Shingle("my-shingle", s => s.MinShingleSize(2))
//				.Snowball("my_snowball", s => s.Version("v1"))))));

//			//var c1 = new ElasticClient(new ConnectionSettings(new Uri("https://azure.es.eastus.azure.elastic-cloud.com:9243")).BasicAuthentication("a", "b").ThrowExceptions());

//			//var r1 = await c1.PingAsync();




//#pragma warning disable IDE0039 // Use local function
//			Func<BoolQueryDescriptor<Person>, IBoolQuery> test = b => b.Name("thing");
//#pragma warning restore IDE0039 // Use local function

//			static IBoolQuery TestBoolQuery(BoolQueryDescriptor<Person> b) => b.Name("thing");

//			var thing = Query<Person>.Bool(test);
//			thing = Query<Person>.Bool(TestBoolQuery);

//			var matchQueryOne = Query<Person>.Match(m => m.Field(f => f.FirstName).Query("Steve"));
//			var matchQueryTwo = new Query(new MatchQuery() { Field = Infer.Field<Person>(f => f.FirstName), Query = "Steve" });
//			var matchQueryThree = new QueryDescriptor<Person>().Match(m => m.Field(f => f.FirstName).Query("Steve"));


//			//var a = client.IndexMany<Person>(new Person[0] { }, a => a.)

//			var matchAll = new Query(new MatchAllQuery() { Name = "test_query", IsVerbatim = true });
//			//var filter = Query<Person>.Bool(b => b.Filter(f => f.Match(m => m.Field(fld => fld.FirstName).Query("Steve").Name("test_match"))));
//			var boolQuery = new Query(new BoolQuery() { Filter = new[] { new Query(new MatchQuery() { Name = "test_match", Field = "firstName", Query = "Steve" }) } });

//			var spanQuery = new Query(new SpanContainingQuery()
//			{
//				Big = new SpanQuery()
//				{
//					//SpanTerm = new SpanTermQuery { Field = "test", Value = "foo", Name = "span_term_name" },
//					SpanNear = new SpanNearQuery
//					{
//						Slop = 5,
//						InOrder = true,
//						Clauses = new ISpanQuery[]
//						{
//							new SpanQuery() { SpanTerm = new SpanTermQuery { Field = "test", Value = "bar", Name = "span_term_inner_name_1" } },
//							new SpanQuery() { SpanTerm = new SpanTermQuery { Field = "test", Value = "baz", Name = "span_term_inner_name_2" } },
//						}
//					}
//				}
//			});

//			spanQuery = new Query(new SpanNearQuery()
//			{
//				Clauses = new[] { new SpanQuery() { SpanGap = new SpanGapQuery() { Field = "test", Width = 10 } } }
//			});

//			//var spanQueryRaw = new SpanQuery()
//			//{
//			//	SpanFirst = new SpanFirstQuery(),
//			//	SpanContaining = new SpanContainingQuery()
//			//};

//			var search = new SearchRequest()
//			{
//				Query = spanQuery
//			};

//			_ = await client.SearchAsync<Person>(new SearchDescriptor<Person>());
//			_ = await client.CountAsync(new CountDescriptor<Person>());

//			//var response = await client.SearchAsync<Person>(search);

//			var r = await client.Indices.CreateAsync("", c => c.Settings(s => s.Analysis(a => a.CharFilters(cf => cf
//				.HtmlStrip("name", h => h)
//				.PatternReplace("name-2", p => p)))));

//			//var indexName = Guid.NewGuid().ToString();

//			//// Create an index
//			//var createResponse = await client.Indices.CreateAsync(new CreateIndexRequest(indexName)
//			//{
//			//	Mappings = new TypeMapping
//			//	{
//			//		DateDetection = false,
//			//		Properties = new Properties
//			//		{
//			//			{"age", new NumberProperty(NumberType.Integer)},
//			//			{"name", new TextProperty()},
//			//			{"email", new KeywordProperty()}
//			//		},
//			//		Meta = new Dictionary<string, object>()
//			//		{
//			//			{ "foo", "bar" }
//			//		}
//			//	}
//			//});

//			//var intervalsQuery = new IntervalsQuery()
//			//{
//			//	Match = new IntervalsMatch()
//			//	{

//			//	},
//			//	AllOf = new IntervalsAllOf()
//			//	{

//			//	}
//			//}
		}
	}
}
