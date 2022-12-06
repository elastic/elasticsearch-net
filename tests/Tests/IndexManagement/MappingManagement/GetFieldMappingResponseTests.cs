// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.IndexManagement;
using Elastic.Clients.Elasticsearch.Mapping;
using Tests.Serialization;

namespace Tests.IndexManagement.MappingManagement;

public class GetFieldMappingResponseTests : SerializerTestBase
{
	private const string JsonResponse = @"{""test1"":{""mappings"":{""id"":{""full_name"":""id"",""mapping"":{""id"":{""type"":""text"",""fields"":{""keyword"":{""type"":""keyword"",""ignore_above"":256}}}}}}}}";

	[U]
	public void DeserializesResponse()
	{
		var response = DeserializeJsonString<GetFieldMappingResponse>(JsonResponse);

		var property = response.GetProperty("test1", Infer.Field<Thing>(f => f.Id));

		property.Should().NotBeNull();
		property.Should().BeOfType<TextProperty>();
	}

	private class Thing
	{
		public string Id { get; set; }
	}
}
