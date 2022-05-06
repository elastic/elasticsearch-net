// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Aggregations;
using Elastic.Clients.Elasticsearch.Helpers;
using Elastic.Clients.Elasticsearch.Mapping;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;
using Playground;

// const string IndexName = "stock-demo-v1";

var settings = new ElasticsearchClientSettings(new InMemoryConnection())
	.DefaultIndex("default-index")
	.DefaultMappingFor<Person>(m => m
		.DisableIdInference()
		.IndexName("people")
		.IdProperty(id => id.SecondaryId)
		.RoutingProperty(id => id.SecondaryId)
		.RelationName("relation"))
	//.DefaultFieldNameInferrer(s => $"{s}_2")
	.EnableDebugMode();

var client = new ElasticsearchClient(settings);

var createIndexResponse = await client.Indices.CreateAsync("aa", i => i
	.Mappings(m => m.Properties<Person>(p => p.Boolean(b => b.Name(n => n.IsDeleted).NullValue(true).Store(false)))));



var filterResponse = await client.SearchAsync<Person>(s => s
	.Query(q => q
		.Bool(b => b
			.Filter(
				f => f.Term(t => t.Field(f => f.Age).Value(37)),
				f => f.Term(t => t.Field(f => f.FirstName).Value("Steve"))
			))));

var person = new Person { Id = 101, FirstName = "Steve", LastName = "Gordon", Age = 37, Email = "sgordon@example.com" };

var propertyName = (IUrlParameter)Infer.Property<Person>(p => p.SecondaryId);
var propertyName2 = (IUrlParameter)Infer.Property<PersonV3>(p => p.SecondaryId);

//var a = new PropertyMapping();
//var b = PropertyMapping.Default;

//var equal = a.Equals(b);

var propertyNameString = propertyName.GetString(settings);
propertyNameString = propertyName.GetString(settings);
propertyNameString = propertyName2.GetString(settings);

var response = await client.IndexAsync(person);

//var serializedPerson = JsonSerializer.Serialize(person);

Console.WriteLine("DONE");
Console.ReadKey();

//var existsResponse = await client.Indices.ExistsAsync(IndexName);

//if (!existsResponse.Exists)
//{
//	var newIndexResponse = await client.Indices.CreateAsync(IndexName, i => i
//		.Mappings(m => m
//			.Properties(new Elastic.Clients.Elasticsearch.Mapping.Properties
//			{
//				{ "symbol", new KeywordProperty() },
//				{ "high", new FloatNumberProperty() },
//				{ "low", new FloatNumberProperty() },
//				{ "open", new FloatNumberProperty() },
//				{ "close", new FloatNumberProperty() },
//			}))
//		//.Map(m => m
//		//    .AutoMap<StockData>()
//		//    .Properties<StockData>(p => p.Keyword(k => k.Name(n => n.Symbol))))
//		.Settings(s => s.NumberOfShards(1).NumberOfReplicas(0)));

//	if (!newIndexResponse.IsValid || newIndexResponse.Acknowledged is false)
//		throw new Exception("Oh no!");

//	var bulkAll = client.BulkAll(ReadStockData(), r => r
//		.Index(IndexName)
//		.BackOffRetries(20)
//		.BackOffTime(TimeSpan.FromSeconds(10))
//		.ContinueAfterDroppedDocuments()
//		.DroppedDocumentCallback((r, d) =>
//		{
//			Console.WriteLine(r.Error?.Reason ?? "NO REASON");
//		})
//		.MaxDegreeOfParallelism(4)
//		.Size(1000));

//	bulkAll.Wait(TimeSpan.FromMinutes(10), r => Console.WriteLine("Data indexed"));
//}

//var aggExampleResponse = await client.SearchAsync<StockData>(s => s
//	.Index(IndexName)
//	.Size(0)
//		.Query(q => q
//			.Bool(b => b
//				.Filter(new[] { new QueryContainer(new TermQuery { Field = "symbol", Value = "MSFT" }) })))
//	.Aggregations(a => a
//		.DateHistogram("by-month", dh => dh
//			.CalendarInterval(CalendarInterval.Month)
//			.Field(fld => fld.Date)
//			.Order(HistogramOrder.KeyDescending)
//			.Aggregations(agg => agg
//				.Sum("trade-volumes", sum => sum.Field(fld => fld.Volume))))));

//if (!aggExampleResponse.IsValid) throw new Exception("Oh no");

//var monthlyBuckets = aggExampleResponse.Aggregations?.GetDateHistogram("by-month")?.Buckets ?? Array.Empty<DateHistogramBucket>();

//foreach (var monthlyBucket in monthlyBuckets)
//{
//	var volume = monthlyBucket.GetSum("trade-volumes");
//	Console.WriteLine($"{monthlyBucket.Key.DateTimeOffset:d} : {volume:n0}");
//}

//Console.WriteLine("Press any key to exit.");
//Console.ReadKey();

//static IEnumerable<StockData> ReadStockData()
//{
//	var file = new StreamReader("c:\\stock-data\\all_stocks_5yr.csv");

//	string? line;
//	while ((line = file.ReadLine()) is not null)
//	{
//		yield return new StockData(line);
//	}
//}

public class StockData
{
	private static readonly Dictionary<string, string> CompanyLookup = new()
	{
		{ "AAL", "American Airlines Group Inc" },
		{ "MSFT", "Microsoft Corporation" },
		{ "AME", "AMETEK, Inc." },
		{ "M", "Macy's inc" }
	};

	public StockData(string dataLine)
	{
		var columns = dataLine.Split(',', StringSplitOptions.TrimEntries);

		if (DateTime.TryParse(columns[0], out var date))
			Date = date;

		if (double.TryParse(columns[1], out var open))
			Open = open;

		if (double.TryParse(columns[2], out var high))
			High = high;

		if (double.TryParse(columns[3], out var low))
			Low = low;

		if (double.TryParse(columns[4], out var close))
			Close = close;

		if (uint.TryParse(columns[5], out var volume))
			Volume = volume;

		Symbol = columns[6];

		if (CompanyLookup.TryGetValue(Symbol, out var name))
			Name = name;
	}

	public DateTime Date { get; init; }
	public double Open { get; init; }
	public double Close { get; init; }
	public double High { get; init; }
	public double Low { get; init; }
	public uint Volume { get; init; }
	public string Symbol { get; init; }
	public string? Name { get; init; }
}

//using System.Text;
//using Elastic.Clients.Elasticsearch;
//using Elastic.Clients.Elasticsearch.IndexManagement;
//using Elastic.Clients.Elasticsearch.Mapping;
//using Elastic.Transport;
//using Playground;




//var settings = new ElasticsearchClientSettings(new InMemoryConnection())
//	.EnableDebugMode()
//	.DefaultMappingFor<Person>(p => p
//		.PropertyName(pn => pn.Age, "custom-name")
//		.Ignore(pn => pn.Email));

//var client = new ElasticsearchClient(settings);

//var response = await client.IndexAsync(new Person { FirstName = "Steve", LastName = "Gordon", Age = 37, Email = "test@example.com", Id = 1000 }, "test-index");



//var putTemplateRequest = new PutIndexTemplateRequest("my-template")
//{
//	Template = new IndexTemplateMapping
//	{
//		Mappings = new TypeMapping
//		{
//			Properties = new Properties
//			{
//				{ "field1", new KeywordProperty { Boost = 2.0, Store = false } }
//			}
//		},
//		Settings = new IndexSettings
//		{
//			Index = new IndexSettings
//			{
//				NumberOfReplicas = 1,
//				Priority = 2,
//			}
//		}
//	}
//};

////var putMappingRequest = new PutMappingRequest("test-index")
////{
////	Properties = new Properties
////	{
////		{ "field1", new TextProperty { Boost = 2.0, Store = false } }
////	}
////};

//var stream = new MemoryStream();

//client.RequestResponseSerializer.Serialize(putTemplateRequest, stream);

//stream.Position = 0;

//var sr = new StreamReader(stream);

//var json = sr.ReadToEnd();

//Console.WriteLine(json);

//Console.ReadKey();

////const string propertiesJson = @"{""field1"":{""boost"":2,""type"":""text"",""store"":false},""field2"":{""type"":""ip""},""name"":{""properties"":{""first"":{""type"":""text"",""fields"":{""keyword"":{""type"":""keyword"",""ignore_above"":256}}},""last"":{""type"":""text"",""fields"":{""keyword"":{""type"":""keyword"",""ignore_above"":256}}}}}}";

////stream = new MemoryStream(Encoding.UTF8.GetBytes(propertiesJson));

////var properties = client.RequestResponseSerializer.Deserialize<Properties>(stream);

////if (properties.TryGetProperty<TextProperty>("field1", out var textProperty))
////{
////	Console.WriteLine($"Found field1 with boost: {textProperty.Boost}");
////}

//Console.WriteLine("DONE");

//Console.ReadKey();
