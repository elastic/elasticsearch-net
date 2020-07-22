// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Indices
{
	public class GetFieldMappingPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("indices/get-field-mapping.asciidoc:14")]
		public void Line14()
		{
			// tag::02c9cadc657f6afd4ca854c577188d31[]
			var response0 = new SearchResponse<object>();
			// end::02c9cadc657f6afd4ca854c577188d31[]

			response0.MatchesExample(@"GET /twitter/_mapping/field/user");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/get-field-mapping.asciidoc:70")]
		public void Line70()
		{
			// tag::f0f0f778e19134fbe3e4be98fc47bd34[]
			var response0 = new SearchResponse<object>();
			// end::f0f0f778e19134fbe3e4be98fc47bd34[]

			response0.MatchesExample(@"PUT /publications
			{
			    ""mappings"": {
			        ""properties"": {
			            ""id"": { ""type"": ""text"" },
			            ""title"":  { ""type"": ""text""},
			            ""abstract"": { ""type"": ""text""},
			            ""author"": {
			                ""properties"": {
			                    ""id"": { ""type"": ""text"" },
			                    ""name"": { ""type"": ""text"" }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/get-field-mapping.asciidoc:92")]
		public void Line92()
		{
			// tag::299900fb08da80fe455cf3f1bb7d62ee[]
			var response0 = new SearchResponse<object>();
			// end::299900fb08da80fe455cf3f1bb7d62ee[]

			response0.MatchesExample(@"GET publications/_mapping/field/title");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/get-field-mapping.asciidoc:125")]
		public void Line125()
		{
			// tag::ed3bdf4d6799b43526851e92b6a60c55[]
			var response0 = new SearchResponse<object>();
			// end::ed3bdf4d6799b43526851e92b6a60c55[]

			response0.MatchesExample(@"GET publications/_mapping/field/author.id,abstract,name");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/get-field-mapping.asciidoc:161")]
		public void Line161()
		{
			// tag::b61afb7ca29a11243232ffcc8b5a43cf[]
			var response0 = new SearchResponse<object>();
			// end::b61afb7ca29a11243232ffcc8b5a43cf[]

			response0.MatchesExample(@"GET publications/_mapping/field/a*");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/get-field-mapping.asciidoc:222")]
		public void Line222()
		{
			// tag::01b506b7f6e02d0f78280fd554b4a4db[]
			var response0 = new SearchResponse<object>();
			// end::01b506b7f6e02d0f78280fd554b4a4db[]

			response0.MatchesExample(@"GET /twitter,kimchy/_mapping/field/message");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/get-field-mapping.asciidoc:232")]
		public void Line232()
		{
			// tag::646d71869f1a18c5bede7759559bfc47[]
			var response0 = new SearchResponse<object>();
			// end::646d71869f1a18c5bede7759559bfc47[]

			response0.MatchesExample(@"GET /_all/_mapping/field/message");
		}

		[U(Skip = "Example not implemented")]
		[Description("indices/get-field-mapping.asciidoc:241")]
		public void Line241()
		{
			// tag::dbdd58cdeac9ef20b42ff73e4864e697[]
			var response0 = new SearchResponse<object>();
			// end::dbdd58cdeac9ef20b42ff73e4864e697[]

			response0.MatchesExample(@"GET /_all/_mapping/field/*.id");
		}
	}
}
