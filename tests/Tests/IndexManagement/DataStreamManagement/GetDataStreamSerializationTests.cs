// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Tests.Serialization;

namespace Tests.IndexManagement.DataStreamManagement;

public class GetDataStreamSerializationTests : SerializerTestBase
{
	[U]
	public void GetIndexResponse_IsDeserializedCorrectly()
	{
		const string json = @"{
  ""data_streams"": [
    {
      ""name"": ""logs-dev"",
      ""timestamp_field"": {
        ""name"": ""@timestamp""
      },
      ""indices"": [
        {
          ""index_name"": "".ds-logs-dev-2023.02.16-000001"",
          ""index_uuid"": ""xyWXN5T1Rm6_sOCayv7GDA""
        }
      ],
      ""generation"": 1,
      ""_meta"": {
        ""description"": ""default logs template installed by x-pack"",
        ""managed"": true
      },
      ""status"": ""GREEN"",
      ""template"": ""logs"",
      ""ilm_policy"": ""logs"",
      ""hidden"": false,
      ""system"": false,
      ""allow_custom_routing"": false,
      ""replicated"": false
    }
  ]
}";

		var response = DeserializeJsonString<GetDataStreamResponse>(json);

		response.DataStreams.Count.Should().Be(1);

		var dataStream = response.DataStreams.First();

		dataStream.Name.Should().Be("logs-dev");
		dataStream.TimestampField.Name.Should().Be("@timestamp");

		dataStream.Indices.Count.Should().Be(1);
		var indices = dataStream.Indices.First();
		indices.IndexName.Should().Be(".ds-logs-dev-2023.02.16-000001");
		indices.IndexUuid.Should().Be("xyWXN5T1Rm6_sOCayv7GDA");

		dataStream.Generation.Should().Be(1);
		dataStream.Meta["description"].Should().Be("default logs template installed by x-pack");
		dataStream.Meta["managed"].Should().Be(true);
		dataStream.Status.Should().Be(HealthStatus.Green);
		dataStream.Template.Should().Be("logs");
		dataStream.IlmPolicy.Should().Be("logs");
		dataStream.Hidden.Should().Be(false);
		dataStream.System.Should().Be(false);
		dataStream.AllowCustomRouting.Should().Be(false);
		dataStream.Replicated.Should().Be(false);
	}
}

