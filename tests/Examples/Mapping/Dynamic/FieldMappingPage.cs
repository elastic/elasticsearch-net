// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using System.ComponentModel;

namespace Examples.Mapping.Dynamic
{
	public class FieldMappingPage : ExampleBase
	{
		[U]
		[Description("mapping/dynamic/field-mapping.asciidoc:50")]
		public void Line50()
		{
			// tag::4909bf2f9e86b4bdd6af1d0b13c0015d[]
			var indexResponse = client.Index<object>(
				new { create_date = "2015/09/02" },
				i => i.Index("my_index").Id(1));

			var getMappingResponse = client.Indices.GetMapping<object>(m => m
				.Index("my_index")
			);
			// end::4909bf2f9e86b4bdd6af1d0b13c0015d[]

			indexResponse.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""create_date"": ""2015/09/02""
			}");

			getMappingResponse.MatchesExample(@"GET my_index/_mapping \<1>");
		}

		[U]
		[Description("mapping/dynamic/field-mapping.asciidoc:68")]
		public void Line68()
		{
			// tag::95fa846e5d0a75210f9ad1fa1acfa8f3[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.DateDetection(false)
				)
			);

			var indexResponse = client.Index<object>(
				new { create_date = "2015/09/02" },
				i => i.Index("my_index").Id(1));
			// end::95fa846e5d0a75210f9ad1fa1acfa8f3[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""date_detection"": false
			  }
			}");

			indexResponse.MatchesExample(@"PUT my_index/_doc/1 \<1>
			{
			  ""create_date"": ""2015/09/02""
			}");
		}

		[U]
		[Description("mapping/dynamic/field-mapping.asciidoc:90")]
		public void Line90()
		{
			// tag::4eae628c9aaa259f80711c6e9cc6fd25[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.DynamicDateFormats(new[] { "MM/dd/yyyy" })
				)
			);

			var indexResponse = client.Index<object>(
				new { create_date = "09/25/2015" },
				i => i.Index("my_index").Id(1));
			// end::4eae628c9aaa259f80711c6e9cc6fd25[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""dynamic_date_formats"": [""MM/dd/yyyy""]
			  }
			}");

			indexResponse.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""create_date"": ""09/25/2015""
			}");
		}

		[U]
		[Description("mapping/dynamic/field-mapping.asciidoc:115")]
		public void Line115()
		{
			// tag::fa3cd4ffaec8273656a328ae29f32c65[]
			var createIndexResponse = client.Indices.Create("my_index", c => c
				.Map(m => m
					.NumericDetection(true)
				)
			);

			var indexResponse = client.Index<object>(
				new { my_float = "1.0", my_integer = "1" },
				i => i.Index("my_index").Id(1));
			// end::fa3cd4ffaec8273656a328ae29f32c65[]

			createIndexResponse.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""numeric_detection"": true
			  }
			}");

			indexResponse.MatchesExample(@"PUT my_index/_doc/1
			{
			  ""my_float"":   ""1.0"", \<1>
			  ""my_integer"": ""1"" \<2>
			}");
		}
	}
}
