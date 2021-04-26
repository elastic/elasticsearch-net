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

namespace Examples.HowTo
{
	public class SearchSpeedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:52")]
		public void Line52()
		{
			// tag::12facf3617a41551ce2f0c4d005cb1c7[]
			var response0 = new SearchResponse<object>();
			// end::12facf3617a41551ce2f0c4d005cb1c7[]

			response0.MatchesExample(@"PUT movies
			{
			  ""mappings"": {
			    ""properties"": {
			      ""name_and_plot"": {
			        ""type"": ""text""
			      },
			      ""name"": {
			        ""type"": ""text"",
			        ""copy_to"": ""name_and_plot""
			      },
			      ""plot"": {
			        ""type"": ""text"",
			        ""copy_to"": ""name_and_plot""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:86")]
		public void Line86()
		{
			// tag::a008f42379930edc354b4074e0a33344[]
			var response0 = new SearchResponse<object>();
			// end::a008f42379930edc354b4074e0a33344[]

			response0.MatchesExample(@"PUT index/_doc/1
			{
			  ""designation"": ""spoon"",
			  ""price"": 13
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:97")]
		public void Line97()
		{
			// tag::a0a7557bb7e2aff7918557cd648f41af[]
			var response0 = new SearchResponse<object>();
			// end::a0a7557bb7e2aff7918557cd648f41af[]

			response0.MatchesExample(@"GET index/_search
			{
			  ""aggs"": {
			    ""price_ranges"": {
			      ""range"": {
			        ""field"": ""price"",
			        ""ranges"": [
			          { ""to"": 10 },
			          { ""from"": 10, ""to"": 100 },
			          { ""from"": 100 }
			        ]
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:120")]
		public void Line120()
		{
			// tag::a4bae4d956bc0a663f42cfec36bf8e0b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::a4bae4d956bc0a663f42cfec36bf8e0b[]

			response0.MatchesExample(@"PUT index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""price_range"": {
			        ""type"": ""keyword""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT index/_doc/1
			{
			  ""designation"": ""spoon"",
			  ""price"": 13,
			  ""price_range"": ""10-100""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:144")]
		public void Line144()
		{
			// tag::7dedb148ff74912de81b8f8275f0d7f3[]
			var response0 = new SearchResponse<object>();
			// end::7dedb148ff74912de81b8f8275f0d7f3[]

			response0.MatchesExample(@"GET index/_search
			{
			  ""aggs"": {
			    ""price_ranges"": {
			      ""terms"": {
			        ""field"": ""price_range""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:188")]
		public void Line188()
		{
			// tag::34fff811cf60997f6df89928e5a41387[]
			var response0 = new SearchResponse<object>();
			// end::34fff811cf60997f6df89928e5a41387[]

			response0.MatchesExample(@"GET /my_test_scores/_search
			{
			  ""query"": {
			    ""term"": {
			      ""grad_year"": ""2020""
			    }
			  },
			  ""sort"": [
			    {
			      ""_script"": {
			        ""type"": ""number"",
			        ""script"": {
			          ""source"": ""doc['math_score'].value + doc['verbal_score'].value""
			        },
			        ""order"": ""desc""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:219")]
		public void Line219()
		{
			// tag::dda949d20d07a9edbe64cefc623df945[]
			var response0 = new SearchResponse<object>();
			// end::dda949d20d07a9edbe64cefc623df945[]

			response0.MatchesExample(@"PUT /my_test_scores/_mapping
			{
			  ""properties"": {
			    ""total_score"": {
			      ""type"": ""long""
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:236")]
		public void Line236()
		{
			// tag::295b3aaeb223612afdd991744dc9c873[]
			var response0 = new SearchResponse<object>();
			// end::295b3aaeb223612afdd991744dc9c873[]

			response0.MatchesExample(@"PUT _ingest/pipeline/my_test_scores_pipeline
			{
			  ""description"": ""Calculates the total test score"",
			  ""processors"": [
			    {
			      ""script"": {
			        ""source"": ""ctx.total_score = (ctx.math_score + ctx.verbal_score)""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:255")]
		public void Line255()
		{
			// tag::f70ff57c80cdbce3f1e7c63ee307c92d[]
			var response0 = new SearchResponse<object>();
			// end::f70ff57c80cdbce3f1e7c63ee307c92d[]

			response0.MatchesExample(@"POST /_reindex
			{
			  ""source"": {
			    ""index"": ""my_test_scores""
			  },
			  ""dest"": {
			    ""index"": ""my_test_scores_2"",
			    ""pipeline"": ""my_test_scores_pipeline""
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:272")]
		public void Line272()
		{
			// tag::161d6932c412a01a47ee0883b12056ca[]
			var response0 = new SearchResponse<object>();
			// end::161d6932c412a01a47ee0883b12056ca[]

			response0.MatchesExample(@"POST /my_test_scores_2/_doc/?pipeline=my_test_scores_pipeline
			{
			  ""student"": ""kimchy"",
			  ""grad_year"": ""2020"",
			  ""math_score"": 800,
			  ""verbal_score"": 800
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:288")]
		public void Line288()
		{
			// tag::aab42564d36970fe9e2f6eab560470a0[]
			var response0 = new SearchResponse<object>();
			// end::aab42564d36970fe9e2f6eab560470a0[]

			response0.MatchesExample(@"GET /my_test_scores_2/_search
			{
			  ""query"": {
			    ""term"": {
			      ""grad_year"": ""2020""
			    }
			  },
			  ""sort"": [
			    {
			      ""total_score"": {
			        ""order"": ""desc""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:337")]
		public void Line337()
		{
			// tag::102c7de25d13c87cf28839ada9f63c95[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::102c7de25d13c87cf28839ada9f63c95[]

			response0.MatchesExample(@"PUT index/_doc/1
			{
			  ""my_date"": ""2016-05-11T16:30:55.328Z""
			}");

			response1.MatchesExample(@"GET index/_search
			{
			  ""query"": {
			    ""constant_score"": {
			      ""filter"": {
			        ""range"": {
			          ""my_date"": {
			            ""gte"": ""now-1h"",
			            ""lte"": ""now""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:363")]
		public void Line363()
		{
			// tag::17dd67a66c49f7eb618dd17430e48dfa[]
			var response0 = new SearchResponse<object>();
			// end::17dd67a66c49f7eb618dd17430e48dfa[]

			response0.MatchesExample(@"GET index/_search
			{
			  ""query"": {
			    ""constant_score"": {
			      ""filter"": {
			        ""range"": {
			          ""my_date"": {
			            ""gte"": ""now-1h/m"",
			            ""lte"": ""now/m""
			          }
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:396")]
		public void Line396()
		{
			// tag::abc7a670a47516b58b6b07d7497b140c[]
			var response0 = new SearchResponse<object>();
			// end::abc7a670a47516b58b6b07d7497b140c[]

			response0.MatchesExample(@"GET index/_search
			{
			  ""query"": {
			    ""constant_score"": {
			      ""filter"": {
			        ""bool"": {
			          ""should"": [
			            {
			              ""range"": {
			                ""my_date"": {
			                  ""gte"": ""now-1h"",
			                  ""lte"": ""now-1h/m""
			                }
			              }
			            },
			            {
			              ""range"": {
			                ""my_date"": {
			                  ""gt"": ""now-1h/m"",
			                  ""lt"": ""now/m""
			                }
			              }
			            },
			            {
			              ""range"": {
			                ""my_date"": {
			                  ""gte"": ""now/m"",
			                  ""lte"": ""now""
			                }
			              }
			            }
			          ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:468")]
		public void Line468()
		{
			// tag::971c7a36ee79f2b3aa82c64ea338de70[]
			var response0 = new SearchResponse<object>();
			// end::971c7a36ee79f2b3aa82c64ea338de70[]

			response0.MatchesExample(@"PUT index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""foo"": {
			        ""type"": ""keyword"",
			        ""eager_global_ordinals"": true
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:599")]
		public void Line599()
		{
			// tag::9559de0c2190f99fcc344887fc7b232a[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::9559de0c2190f99fcc344887fc7b232a[]

			response0.MatchesExample(@"PUT bicycles
			{
			  ""mappings"": {
			    ""properties"": {
			      ""cycle_type"": {
			        ""type"": ""constant_keyword"",
			        ""value"": ""bicycle""
			      },
			      ""name"": {
			        ""type"": ""text""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT other_cycles
			{
			  ""mappings"": {
			    ""properties"": {
			      ""cycle_type"": {
			        ""type"": ""keyword""
			      },
			      ""name"": {
			        ""type"": ""text""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:637")]
		public void Line637()
		{
			// tag::14936b96cfb8ff999a833f615ba75495[]
			var response0 = new SearchResponse<object>();
			// end::14936b96cfb8ff999a833f615ba75495[]

			response0.MatchesExample(@"GET bicycles,other_cycles/_search
			{
			  ""query"": {
			    ""bool"": {
			      ""must"": {
			        ""match"": {
			          ""description"": ""dutch""
			        }
			      },
			      ""filter"": {
			        ""term"": {
			          ""cycle_type"": ""bicycle""
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("how-to/search-speed.asciidoc:662")]
		public void Line662()
		{
			// tag::9de10a59a5f56dd0906be627896cc789[]
			var response0 = new SearchResponse<object>();
			// end::9de10a59a5f56dd0906be627896cc789[]

			response0.MatchesExample(@"GET bicycles,other_cycles/_search
			{
			  ""query"": {
			    ""match"": {
			      ""description"": ""dutch""
			    }
			  }
			}");
		}
	}
}
