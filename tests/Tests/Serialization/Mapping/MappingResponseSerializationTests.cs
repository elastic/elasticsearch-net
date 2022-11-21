// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Elastic.Clients.Elasticsearch.IndexManagement;

namespace Tests.Serialization.Mapping;

public class GetMappingResponseSerializationTests : SerializerTestBase
{
	private const string ResponseJson = @"{""project"":{""mappings"":{""_routing"":{""required"":true},""properties"":{""dateString"":{""type"":""date""}}}}}";

	[U]
	public void CanDeserialize_MappingResponse()
	{
		var reponse = DeserializeJsonString<GetMappingResponse>(ResponseJson);
		reponse.Indices["project"].Mappings.Properties["dateString"].Type.Should().Be("date");
	}
}
