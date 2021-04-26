/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Docs
{
	public class TermvectorsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("docs/termvectors.asciidoc:10")]
		public void Line10()
		{
			// tag::4c9b1db368186091c1a660bcd52890b8[]
			var response0 = new SearchResponse<object>();
			// end::4c9b1db368186091c1a660bcd52890b8[]

			response0.MatchesExample(@"GET /twitter/_termvectors/1");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/termvectors.asciidoc:30")]
		public void Line30()
		{
			// tag::a15ca7faa8ba282679396de3c7b90485[]
			var response0 = new SearchResponse<object>();
			// end::a15ca7faa8ba282679396de3c7b90485[]

			response0.MatchesExample(@"GET /twitter/_termvectors/1?fields=message");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/termvectors.asciidoc:171")]
		public void Line171()
		{
			// tag::587dd0c1aebbc1d93190bf117959cb73[]
			var response0 = new SearchResponse<object>();
			// end::587dd0c1aebbc1d93190bf117959cb73[]

			response0.MatchesExample(@"PUT /twitter
			{ ""mappings"": {
			    ""properties"": {
			      ""text"": {
			        ""type"": ""text"",
			        ""term_vector"": ""with_positions_offsets_payloads"",
			        ""store"" : true,
			        ""analyzer"" : ""fulltext_analyzer""
			       },
			       ""fullname"": {
			        ""type"": ""text"",
			        ""term_vector"": ""with_positions_offsets_payloads"",
			        ""analyzer"" : ""fulltext_analyzer""
			      }
			    }
			  },
			  ""settings"" : {
			    ""index"" : {
			      ""number_of_shards"" : 1,
			      ""number_of_replicas"" : 0
			    },
			    ""analysis"": {
			      ""analyzer"": {
			        ""fulltext_analyzer"": {
			          ""type"": ""custom"",
			          ""tokenizer"": ""whitespace"",
			          ""filter"": [
			            ""lowercase"",
			            ""type_as_payload""
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/termvectors.asciidoc:212")]
		public void Line212()
		{
			// tag::c75bd2b34c51aecf55ece4137612d4c7[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::c75bd2b34c51aecf55ece4137612d4c7[]

			response0.MatchesExample(@"PUT /twitter/_doc/1
			{
			  ""fullname"" : ""John Doe"",
			  ""text"" : ""twitter test test test ""
			}");

			response1.MatchesExample(@"PUT /twitter/_doc/2?refresh=wait_for
			{
			  ""fullname"" : ""Jane Doe"",
			  ""text"" : ""Another twitter test ...""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/termvectors.asciidoc:231")]
		public void Line231()
		{
			// tag::8dfecbb38a81fb5b42f63d6fe9bf9278[]
			var response0 = new SearchResponse<object>();
			// end::8dfecbb38a81fb5b42f63d6fe9bf9278[]

			response0.MatchesExample(@"GET /twitter/_termvectors/1
			{
			  ""fields"" : [""text""],
			  ""offsets"" : true,
			  ""payloads"" : true,
			  ""positions"" : true,
			  ""term_statistics"" : true,
			  ""field_statistics"" : true
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/termvectors.asciidoc:317")]
		public void Line317()
		{
			// tag::487d12bb3e3036c4493dcbe43191b6f0[]
			var response0 = new SearchResponse<object>();
			// end::487d12bb3e3036c4493dcbe43191b6f0[]

			response0.MatchesExample(@"GET /twitter/_termvectors/1
			{
			  ""fields"" : [""text"", ""some_field_without_term_vectors""],
			  ""offsets"" : true,
			  ""positions"" : true,
			  ""term_statistics"" : true,
			  ""field_statistics"" : true
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/termvectors.asciidoc:340")]
		public void Line340()
		{
			// tag::1f580df38ae517800d0c62d9648ebcb9[]
			var response0 = new SearchResponse<object>();
			// end::1f580df38ae517800d0c62d9648ebcb9[]

			response0.MatchesExample(@"GET /twitter/_termvectors
			{
			  ""doc"" : {
			    ""fullname"" : ""John Doe"",
			    ""text"" : ""twitter test test test""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/termvectors.asciidoc:361")]
		public void Line361()
		{
			// tag::8304a9c1ae8d0329b66ba57fb8263485[]
			var response0 = new SearchResponse<object>();
			// end::8304a9c1ae8d0329b66ba57fb8263485[]

			response0.MatchesExample(@"GET /twitter/_termvectors
			{
			  ""doc"" : {
			    ""fullname"" : ""John Doe"",
			    ""text"" : ""twitter test test test""
			  },
			  ""fields"": [""fullname""],
			  ""per_field_analyzer"" : {
			    ""fullname"": ""keyword""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/termvectors.asciidoc:425")]
		public void Line425()
		{
			// tag::ef3b210782fe58df252d0e805b8ef644[]
			var response0 = new SearchResponse<object>();
			// end::ef3b210782fe58df252d0e805b8ef644[]

			response0.MatchesExample(@"GET /imdb/_termvectors
			{
			    ""doc"": {
			      ""plot"": ""When wealthy industrialist Tony Stark is forced to build an armored suit after a life-threatening incident, he ultimately decides to use its technology to fight against evil.""
			    },
			    ""term_statistics"" : true,
			    ""field_statistics"" : true,
			    ""positions"": false,
			    ""offsets"": false,
			    ""filter"" : {
			      ""max_num_terms"" : 3,
			      ""min_term_freq"" : 1,
			      ""min_doc_freq"" : 1
			    }
			}");
		}
	}
}
