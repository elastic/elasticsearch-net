// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Analysis.Tokenfilters
{
	public class SynonymTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/synonym-tokenfilter.asciidoc:12")]
		public void Line12()
		{
			// tag::09f74df1d07d84ee133ce90f7832e712[]
			var response0 = new SearchResponse<object>();
			// end::09f74df1d07d84ee133ce90f7832e712[]

			response0.MatchesExample(@"PUT /test_index
			{
			    ""settings"": {
			        ""index"" : {
			            ""analysis"" : {
			                ""analyzer"" : {
			                    ""synonym"" : {
			                        ""tokenizer"" : ""whitespace"",
			                        ""filter"" : [""synonym""]
			                    }
			                },
			                ""filter"" : {
			                    ""synonym"" : {
			                        ""type"" : ""synonym"",
			                        ""synonyms_path"" : ""analysis/synonym.txt""
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/synonym-tokenfilter.asciidoc:51")]
		public void Line51()
		{
			// tag::bcc57126b24c408b5d944928b6f08c94[]
			var response0 = new SearchResponse<object>();
			// end::bcc57126b24c408b5d944928b6f08c94[]

			response0.MatchesExample(@"PUT /test_index
			{
			    ""settings"": {
			        ""index"" : {
			            ""analysis"" : {
			                ""analyzer"" : {
			                    ""synonym"" : {
			                        ""tokenizer"" : ""standard"",
			                        ""filter"" : [""my_stop"", ""synonym""]
			                    }
			                },
			                ""filter"" : {
			                    ""my_stop"": {
			                        ""type"" : ""stop"",
			                        ""stopwords"": [""bar""]
			                    },
			                    ""synonym"" : {
			                        ""type"" : ""synonym"",
			                        ""lenient"": true,
			                        ""synonyms"" : [""foo, bar => baz""]
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/synonym-tokenfilter.asciidoc:112")]
		public void Line112()
		{
			// tag::9fb5e28535f396ab2eb8bc710eebc1e6[]
			var response0 = new SearchResponse<object>();
			// end::9fb5e28535f396ab2eb8bc710eebc1e6[]

			response0.MatchesExample(@"PUT /test_index
			{
			    ""settings"": {
			        ""index"" : {
			            ""analysis"" : {
			                ""filter"" : {
			                    ""synonym"" : {
			                        ""type"" : ""synonym"",
			                        ""synonyms"" : [
			                            ""i-pod, i pod => ipod"",
			                            ""universe, cosmos""
			                        ]
			                    }
			                }
			            }
			        }
			    }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/synonym-tokenfilter.asciidoc:143")]
		public void Line143()
		{
			// tag::0c0f37e409459dcd40d29ea684db4706[]
			var response0 = new SearchResponse<object>();
			// end::0c0f37e409459dcd40d29ea684db4706[]

			response0.MatchesExample(@"PUT /test_index
			{
			    ""settings"": {
			        ""index"" : {
			            ""analysis"" : {
			                ""filter"" : {
			                    ""synonym"" : {
			                        ""type"" : ""synonym"",
			                        ""format"" : ""wordnet"",
			                        ""synonyms"" : [
			                            ""s(100000001,1,'abstain',v,1,0)."",
			                            ""s(100000001,2,'refrain',v,1,0)."",
			                            ""s(100000001,3,'desist',v,1,0).""
			                        ]
			                    }
			                }
			            }
			        }
			    }
			}");
		}
	}
}
