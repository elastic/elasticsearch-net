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

namespace Examples.Search.Suggesters
{
	public class PhraseSuggestPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/phrase-suggest.asciidoc:25")]
		public void Line25()
		{
			// tag::5566cff431570f522e1fc5475b2ed875[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();
			// end::5566cff431570f522e1fc5475b2ed875[]

			response0.MatchesExample(@"PUT test
			{
			  ""settings"": {
			    ""index"": {
			      ""number_of_shards"": 1,
			      ""analysis"": {
			        ""analyzer"": {
			          ""trigram"": {
			            ""type"": ""custom"",
			            ""tokenizer"": ""standard"",
			            ""filter"": [""lowercase"",""shingle""]
			          },
			          ""reverse"": {
			            ""type"": ""custom"",
			            ""tokenizer"": ""standard"",
			            ""filter"": [""lowercase"",""reverse""]
			          }
			        },
			        ""filter"": {
			          ""shingle"": {
			            ""type"": ""shingle"",
			            ""min_shingle_size"": 2,
			            ""max_shingle_size"": 3
			          }
			        }
			      }
			    }
			  },
			  ""mappings"": {
			    ""properties"": {
			      ""title"": {
			        ""type"": ""text"",
			        ""fields"": {
			          ""trigram"": {
			            ""type"": ""text"",
			            ""analyzer"": ""trigram""
			          },
			          ""reverse"": {
			            ""type"": ""text"",
			            ""analyzer"": ""reverse""
			          }
			        }
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST test/_doc?refresh=true
			{""title"": ""noble warriors""}");

			response2.MatchesExample(@"POST test/_doc?refresh=true
			{""title"": ""nobel prize""}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/phrase-suggest.asciidoc:83")]
		public void Line83()
		{
			// tag::3b162509ed14eda44a9681cd1108fa39[]
			var response0 = new SearchResponse<object>();
			// end::3b162509ed14eda44a9681cd1108fa39[]

			response0.MatchesExample(@"POST test/_search
			{
			  ""suggest"": {
			    ""text"": ""noble prize"",
			    ""simple_phrase"": {
			      ""phrase"": {
			        ""field"": ""title.trigram"",
			        ""size"": 1,
			        ""gram_size"": 3,
			        ""direct_generator"": [ {
			          ""field"": ""title.trigram"",
			          ""suggest_mode"": ""always""
			        } ],
			        ""highlight"": {
			          ""pre_tag"": ""<em>"",
			          ""post_tag"": ""</em>""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/phrase-suggest.asciidoc:224")]
		public void Line224()
		{
			// tag::89a6b24618cafd60de1702a5b9f28a8d[]
			var response0 = new SearchResponse<object>();
			// end::89a6b24618cafd60de1702a5b9f28a8d[]

			response0.MatchesExample(@"POST test/_search
			{
			  ""suggest"": {
			    ""text"" : ""noble prize"",
			    ""simple_phrase"" : {
			      ""phrase"" : {
			        ""field"" :  ""title.trigram"",
			        ""size"" :   1,
			        ""direct_generator"" : [ {
			          ""field"" :            ""title.trigram"",
			          ""suggest_mode"" :     ""always"",
			          ""min_word_length"" :  1
			        } ],
			        ""collate"": {
			          ""query"": { \<1>
			            ""source"" : {
			              ""match"": {
			                ""{{field_name}}"" : ""{{suggestion}}"" \<2>
			              }
			            }
			          },
			          ""params"": {""field_name"" : ""title""}, \<3>
			          ""prune"": true \<4>
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/phrase-suggest.asciidoc:293")]
		public void Line293()
		{
			// tag::203c3bb334384bdfb11ff1101ccfba25[]
			var response0 = new SearchResponse<object>();
			// end::203c3bb334384bdfb11ff1101ccfba25[]

			response0.MatchesExample(@"POST test/_search
			{
			  ""suggest"": {
			    ""text"" : ""obel prize"",
			    ""simple_phrase"" : {
			      ""phrase"" : {
			        ""field"" : ""title.trigram"",
			        ""size"" : 1,
			        ""smoothing"" : {
			          ""laplace"" : {
			            ""alpha"" : 0.7
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("search/suggesters/phrase-suggest.asciidoc:413")]
		public void Line413()
		{
			// tag::eb6d62f1d855a8e8fe9eab2656d47504[]
			var response0 = new SearchResponse<object>();
			// end::eb6d62f1d855a8e8fe9eab2656d47504[]

			response0.MatchesExample(@"POST test/_search
			{
			  ""suggest"": {
			    ""text"" : ""obel prize"",
			    ""simple_phrase"" : {
			      ""phrase"" : {
			        ""field"" : ""title.trigram"",
			        ""size"" : 1,
			        ""direct_generator"" : [ {
			          ""field"" : ""title.trigram"",
			          ""suggest_mode"" : ""always""
			        }, {
			          ""field"" : ""title.reverse"",
			          ""suggest_mode"" : ""always"",
			          ""pre_filter"" : ""reverse"",
			          ""post_filter"" : ""reverse""
			        } ]
			      }
			    }
			  }
			}");
		}
	}
}
