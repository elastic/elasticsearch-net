using System;
using System.Threading.Tasks;
using Nest;

namespace PlaygroundV7x
{
	internal class Program
	{
		private static async Task Main()
		{


			var aggs = new AggregationDictionary
			{
				{ "startDates", new TermsAggregation("startDates") { Field = "startedOn" } },
				{ "endDates", new DateHistogramAggregation("endDates") { Field = "endedOn" } }
			};

			var a = new SearchRequest()
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

			var client = new ElasticClient();

			var response = client.Index(new Person(), e => e.Index("test"));

			var settingsResponse = await client.Indices.CreateAsync("a", i => i.Settings(s => s.Analysis(a => a.TokenFilters(tf => tf
				.Shingle("my-shingle", s => s.MinShingleSize(2))
				.Snowball("my_snowball", s => s.Version("v1"))))));

			//var c1 = new ElasticClient(new ConnectionSettings(new Uri("https://azure.es.eastus.azure.elastic-cloud.com:9243")).BasicAuthentication("a", "b").ThrowExceptions());

			//var r1 = await c1.PingAsync();

			


#pragma warning disable IDE0039 // Use local function
			Func<BoolQueryDescriptor<Person>, IBoolQuery> test = b => b.Name("thing");
#pragma warning restore IDE0039 // Use local function

			static IBoolQuery TestBoolQuery(BoolQueryDescriptor<Person> b) => b.Name("thing");

			var thing = Query<Person>.Bool(test);
			thing = Query<Person>.Bool(TestBoolQuery);

			var matchQueryOne = Query<Person>.Match(m => m.Field(f => f.FirstName).Query("Steve"));
			var matchQueryTwo = new QueryContainer(new MatchQuery() { Field = Infer.Field<Person>(f => f.FirstName), Query = "Steve" });
			var matchQueryThree = new QueryContainerDescriptor<Person>().Match(m => m.Field(f => f.FirstName).Query("Steve"));



			//var a = client.IndexMany<Person>(new Person[0] { }, a => a.)

			var matchAll = new QueryContainer(new MatchAllQuery() { Name = "test_query", IsVerbatim = true });
			//var filter = Query<Person>.Bool(b => b.Filter(f => f.Match(m => m.Field(fld => fld.FirstName).Query("Steve").Name("test_match"))));
			var boolQuery = new QueryContainer(new BoolQuery() { Filter = new[] { new QueryContainer(new MatchQuery() { Name = "test_match", Field = "firstName", Query = "Steve" }) } });

			var spanQuery = new QueryContainer(new SpanContainingQuery()
			{
				Big = new SpanQuery()
				{
					//SpanTerm = new SpanTermQuery { Field = "test", Value = "foo", Name = "span_term_name" },
					SpanNear = new SpanNearQuery
					{
						Slop = 5,
						InOrder = true,
						Clauses = new ISpanQuery[]
						{
							new SpanQuery() { SpanTerm = new SpanTermQuery { Field = "test", Value = "bar", Name = "span_term_inner_name_1" } },
							new SpanQuery() { SpanTerm = new SpanTermQuery { Field = "test", Value = "baz", Name = "span_term_inner_name_2" } },
						}
					}
				}
			});

			spanQuery = new QueryContainer(new SpanNearQuery()
			{
				Clauses = new [] { new SpanQuery() { SpanGap = new SpanGapQuery() { Field = "test", Width = 10 } } }
			});

			//var spanQueryRaw = new SpanQuery()
			//{
			//	SpanFirst = new SpanFirstQuery(),
			//	SpanContaining = new SpanContainingQuery()
			//};

			var search = new SearchRequest()
			{
				Query = spanQuery
			};

			_ = await client.SearchAsync<Person>(new SearchDescriptor<Person>());
			_ = await client.CountAsync(new CountDescriptor<Person>());
			
			//var response = await client.SearchAsync<Person>(search);

			var r = await client.Indices.CreateAsync("", c => c.Settings(s => s.Analysis(a => a.CharFilters(cf => cf
				.HtmlStrip("name", h => h)
				.PatternReplace("name-2", p => p)))));

			//var indexName = Guid.NewGuid().ToString();

			//// Create an index
			//var createResponse = await client.Indices.CreateAsync(new CreateIndexRequest(indexName)
			//{
			//	Mappings = new TypeMapping
			//	{
			//		DateDetection = false,
			//		Properties = new Properties
			//		{
			//			{"age", new NumberProperty(NumberType.Integer)},
			//			{"name", new TextProperty()},
			//			{"email", new KeywordProperty()}
			//		},
			//		Meta = new Dictionary<string, object>()
			//		{
			//			{ "foo", "bar" }
			//		}
			//	}
			//});

			//var intervalsQuery = new IntervalsQuery()
			//{
			//	Match = new IntervalsMatch()
			//	{

			//	},
			//	AllOf = new IntervalsAllOf()
			//	{

			//	}
			//}
		}
	}
}
