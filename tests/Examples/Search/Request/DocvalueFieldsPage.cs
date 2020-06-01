// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Search.Request
{
	public class DocvalueFieldsPage : ExampleBase
	{
		[U]
		[Description("search/request/docvalue-fields.asciidoc:8")]
		public void Line8()
		{
			// tag::097a6bc1d76c3fc92fb299001d27896e[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.MatchAll()
				)
				.DocValueFields(d => d
					.Field("my_ip_field")
					.Field("my_keyword_field")
					.Field("my_date_field", format: DateFormat.epoch_millis)
				)
			);
			// end::097a6bc1d76c3fc92fb299001d27896e[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match_all"": {}
			    },
			    ""docvalue_fields"" : [
			        ""my_ip_field"", \<1>
			        {
			            ""field"": ""my_keyword_field"" \<2>
			        },
			        {
			            ""field"": ""my_date_field"",
			            ""format"": ""epoch_millis"" \<3>
			        }
			    ]
			}", e => e.ApplyBodyChanges(json =>
			{
				json["docvalue_fields"][1] = "my_keyword_field";
			}));
		}

		[U]
		[Description("search/request/docvalue-fields.asciidoc:36")]
		public void Line36()
		{
			// tag::1518ad2c540fd55f9df84bbe75c81606[]
			var searchResponse = client.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.MatchAll()
				)
				.DocValueFields(d => d
					.Field("*_date_field", format: DateFormat.epoch_millis)
				)
			);
			// end::1518ad2c540fd55f9df84bbe75c81606[]

			searchResponse.MatchesExample(@"GET /_search
			{
			    ""query"" : {
			        ""match_all"": {}
			    },
			    ""docvalue_fields"" : [
			        {
			            ""field"": ""*_date_field"", \<1>
			            ""format"": ""epoch_millis"" \<2>
			        }
			    ]
			}");
		}
	}
}
