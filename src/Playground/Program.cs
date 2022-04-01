// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Mapping;

var client = new ElasticsearchClient();

var putTemplateRequest = new PutIndexTemplateRequest("my-template")
{
	Template = new IndexTemplateMapping
	{
		Mappings = new TypeMapping
		{
			Properties = new Properties
			{
				{ "field1", new TextProperty { Boost = 2.0, Store = false } }
			}
		},
		Settings = new IndexSettings
		{
			Index = new IndexSettings
			{
				NumberOfReplicas = 1,
				Priority = 2,
			}
		}
	}
};

//var putMappingRequest = new PutMappingRequest("test-index")
//{
//	Properties = new Properties
//	{
//		{ "field1", new TextProperty { Boost = 2.0, Store = false } }
//	}
//};

var stream = new MemoryStream();

client.RequestResponseSerializer.Serialize(putTemplateRequest, stream);

stream.Position = 0;

var sr = new StreamReader(stream);

var json = sr.ReadToEnd();

Console.WriteLine(json);

Console.ReadKey();

//const string propertiesJson = @"{""field1"":{""boost"":2,""type"":""text"",""store"":false},""field2"":{""type"":""ip""},""name"":{""properties"":{""first"":{""type"":""text"",""fields"":{""keyword"":{""type"":""keyword"",""ignore_above"":256}}},""last"":{""type"":""text"",""fields"":{""keyword"":{""type"":""keyword"",""ignore_above"":256}}}}}}";

//stream = new MemoryStream(Encoding.UTF8.GetBytes(propertiesJson));

//var properties = client.RequestResponseSerializer.Deserialize<Properties>(stream);

//if (properties.TryGetProperty<TextProperty>("field1", out var textProperty))
//{
//	Console.WriteLine($"Found field1 with boost: {textProperty.Boost}");
//}

Console.WriteLine("DONE");

Console.ReadKey();
