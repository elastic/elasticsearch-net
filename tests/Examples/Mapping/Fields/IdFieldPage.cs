// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;
using Elasticsearch.Net;

namespace Examples.Mapping.Fields
{
	public class IdFieldPage : ExampleBase
	{
		[U]
		[Description("mapping/fields/id-field.asciidoc:12")]
		public void Line12()
		{
			// tag::8d9a63d7c31f08bd27d92ece3de1649c[]
			var indexResponse1 = client.Index(new
			{
				text = "Document with ID 1"
			}, i => i.Index("my_index").Id(1));

			var indexResponse2 = client.Index(new
			{
				text = "Document with ID 2"
			}, i => i.Index("my_index").Id(2).Refresh(Refresh.True));

			var searchResponse = client.Search<object>(s => s
				.Index("my_index")
				.Query(q => q
					.Terms(t => t
						.Field("_id")
						.Terms("1", "2")
					)
				)
			);
			// end::8d9a63d7c31f08bd27d92ece3de1649c[]

			indexResponse1.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""text"": ""Document with ID 1""
			}");

			indexResponse2.MatchesExample(@"PUT my_index/_doc/2?refresh=true
			{
			  ""text"": ""Document with ID 2""
			}");

			searchResponse.MatchesExample(@"GET my_index/_search
			{
			  ""query"": {
			    ""terms"": {
			      ""_id"": [ ""1"", ""2"" ] <1>
			    }
			  }
			}");
		}
	}
}
