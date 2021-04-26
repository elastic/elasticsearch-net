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
using System.ComponentModel;
using Nest;

namespace Examples.Analysis.Tokenfilters
{
	public class WordDelimiterGraphTokenfilterPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/word-delimiter-graph-tokenfilter.asciidoc:47")]
		public void Line47()
		{
			// tag::ffcf80e1094aa2d774f56f6b0bc54827[]
			var response0 = new SearchResponse<object>();
			// end::ffcf80e1094aa2d774f56f6b0bc54827[]

			response0.MatchesExample(@"GET /_analyze
			{
			  ""tokenizer"": ""keyword"",
			  ""filter"": [ ""word_delimiter_graph"" ],
			  ""text"": ""Neil's-Super-Duper-XL500--42+AutoCoder""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/word-delimiter-graph-tokenfilter.asciidoc:137")]
		public void Line137()
		{
			// tag::4620e88aa70944db528af43fead2b9ab[]
			var response0 = new SearchResponse<object>();
			// end::4620e88aa70944db528af43fead2b9ab[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""keyword"",
			          ""filter"": [ ""word_delimiter_graph"" ]
			        }
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("analysis/tokenfilters/word-delimiter-graph-tokenfilter.asciidoc:404")]
		public void Line404()
		{
			// tag::aee2ef858cd6bcc75ef97563cbe5ca5f[]
			var response0 = new SearchResponse<object>();
			// end::aee2ef858cd6bcc75ef97563cbe5ca5f[]

			response0.MatchesExample(@"PUT /my_index
			{
			  ""settings"": {
			    ""analysis"": {
			      ""analyzer"": {
			        ""my_analyzer"": {
			          ""tokenizer"": ""keyword"",
			          ""filter"": [ ""my_custom_word_delimiter_graph_filter"" ]
			        }
			      },
			      ""filter"": {
			        ""my_custom_word_delimiter_graph_filter"": {
			          ""type"": ""word_delimiter_graph"",
			          ""type_table"": [ ""- => ALPHA"" ],
			          ""split_on_case_change"": false,
			          ""split_on_numerics"": false,
			          ""stem_english_possessive"": true
			        }
			      }
			    }
			  }
			}");
		}
	}
}
