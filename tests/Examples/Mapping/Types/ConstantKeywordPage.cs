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

namespace Examples.Mapping.Types
{
	public class ConstantKeywordPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/constant-keyword.asciidoc:14")]
		public void Line14()
		{
			// tag::6b0d492c0f50103fefeab385a7bebd01[]
			var response0 = new SearchResponse<object>();
			// end::6b0d492c0f50103fefeab385a7bebd01[]

			response0.MatchesExample(@"PUT logs-debug
			{
			  ""mappings"": {
			    ""properties"": {
			      ""@timestamp"": {
			        ""type"": ""date""
			      },
			      ""message"": {
			        ""type"": ""text""
			      },
			      ""level"": {
			        ""type"": ""constant_keyword"",
			        ""value"": ""debug""
			      }
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/constant-keyword.asciidoc:43")]
		public void Line43()
		{
			// tag::134384b8c63cfbd8d762fb01757bb3f9[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::134384b8c63cfbd8d762fb01757bb3f9[]

			response0.MatchesExample(@"POST logs-debug/_doc
			{
			  ""date"": ""2019-12-12"",
			  ""message"": ""Starting up Elasticsearch"",
			  ""level"": ""debug""
			}");

			response1.MatchesExample(@"POST logs-debug/_doc
			{
			  ""date"": ""2019-12-12"",
			  ""message"": ""Starting up Elasticsearch""
			}");
		}
	}
}
