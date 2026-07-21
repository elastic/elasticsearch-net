// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Linq;
using Elastic.Clients.Elasticsearch.IndexManagement;
using Tests.Serialization;

namespace Tests.IndexManagement.TemplateManagement;

public class GetTemplateResponseSerializationTests : SerializerTestBase
{
	private const string ResponseJson = @"{
  ""index_templates"": [
    {
      ""name"": ""template_1"",
      ""index_template"": {
        ""index_patterns"": [
          ""te*""
        ],
        ""template"": {
          ""settings"": {
            ""index"": {
              ""number_of_shards"": ""2""
            }
          }
        },
        ""composed_of"": [],
        ""priority"": 1
      }
    }
  ]
}";

	[U]
	public void GetIndexTemplateResponse_DeserializesCorrectly_WhenIndexPatternsAreIncluded()
	{
		var response = DeserializeJsonString<GetIndexTemplateResponse>(ResponseJson);

		response.IndexTemplates.Should().HaveCount(1);

		var template = response.IndexTemplates.First();

		template.Name.Should().Be("template_1");
		template.IndexTemplate.IndexPatterns.Should().HaveCount(1);

		template.IndexTemplate.IndexPatterns.First().Should().Be("te*");
	}
}
