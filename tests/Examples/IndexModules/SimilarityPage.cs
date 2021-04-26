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

namespace Examples.IndexModules
{
	public class SimilarityPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("index-modules/similarity.asciidoc:22")]
		public void Line22()
		{
			// tag::2342a56279106ea643026df657bf7f88[]
			var response0 = new SearchResponse<object>();
			// end::2342a56279106ea643026df657bf7f88[]

			response0.MatchesExample(@"PUT /index
			{
			  ""settings"": {
			    ""index"": {
			      ""similarity"": {
			        ""my_similarity"": {
			          ""type"": ""DFR"",
			          ""basic_model"": ""g"",
			          ""after_effect"": ""l"",
			          ""normalization"": ""h2"",
			          ""normalization.h2.c"": ""3.0""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/similarity.asciidoc:45")]
		public void Line45()
		{
			// tag::528e5f1c345c3769248cc6889e8cf552[]
			var response0 = new SearchResponse<object>();
			// end::528e5f1c345c3769248cc6889e8cf552[]

			response0.MatchesExample(@"PUT /index/_mapping
			{
			  ""properties"" : {
			    ""title"" : { ""type"" : ""text"", ""similarity"" : ""my_similarity"" }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/similarity.asciidoc:192")]
		public void Line192()
		{
			// tag::dfa16b7300d225e013f23625f44c087b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();
			// end::dfa16b7300d225e013f23625f44c087b[]

			response0.MatchesExample(@"PUT /index
			{
			  ""settings"": {
			    ""number_of_shards"": 1,
			    ""similarity"": {
			      ""scripted_tfidf"": {
			        ""type"": ""scripted"",
			        ""script"": {
			          ""source"": ""double tf = Math.sqrt(doc.freq); double idf = Math.log((field.docCount+1.0)/(term.docFreq+1.0)) + 1.0; double norm = 1/Math.sqrt(doc.length); return query.boost * tf * idf * norm;""
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""field"": {
			        ""type"": ""text"",
			        ""similarity"": ""scripted_tfidf""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT /index/_doc/1
			{
			  ""field"": ""foo bar foo""
			}");

			response2.MatchesExample(@"PUT /index/_doc/2
			{
			  ""field"": ""bar baz""
			}");

			response3.MatchesExample(@"POST /index/_refresh");

			response4.MatchesExample(@"GET /index/_search?explain=true
			{
			  ""query"": {
			    ""query_string"": {
			      ""query"": ""foo^1.7"",
			      ""default_field"": ""field""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/similarity.asciidoc:357")]
		public void Line357()
		{
			// tag::5f8fb5513d4f725434db2f517ad4298f[]
			var response0 = new SearchResponse<object>();
			// end::5f8fb5513d4f725434db2f517ad4298f[]

			response0.MatchesExample(@"PUT /index
			{
			  ""settings"": {
			    ""number_of_shards"": 1,
			    ""similarity"": {
			      ""scripted_tfidf"": {
			        ""type"": ""scripted"",
			        ""weight_script"": {
			          ""source"": ""double idf = Math.log((field.docCount+1.0)/(term.docFreq+1.0)) + 1.0; return query.boost * idf;""
			        },
			        ""script"": {
			          ""source"": ""double tf = Math.sqrt(doc.freq); double norm = 1/Math.sqrt(doc.length); return weight * tf * norm;""
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""field"": {
			        ""type"": ""text"",
			        ""similarity"": ""scripted_tfidf""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/similarity.asciidoc:520")]
		public void Line520()
		{
			// tag::553d79817bb1333970e99507c37a159a[]
			var response0 = new SearchResponse<object>();
			// end::553d79817bb1333970e99507c37a159a[]

			response0.MatchesExample(@"PUT /index
			{
			  ""settings"": {
			    ""index"": {
			      ""similarity"": {
			        ""default"": {
			          ""type"": ""boolean""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("index-modules/similarity.asciidoc:540")]
		public void Line540()
		{
			// tag::48de51de87a8ad9fd8b8db1ca25b85c1[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::48de51de87a8ad9fd8b8db1ca25b85c1[]

			response0.MatchesExample(@"POST /index/_close");

			response1.MatchesExample(@"PUT /index/_settings
			{
			  ""index"": {
			    ""similarity"": {
			      ""default"": {
			        ""type"": ""boolean""
			      }
			    }
			  }
			}");

			response2.MatchesExample(@"POST /index/_open");
		}
	}
}
