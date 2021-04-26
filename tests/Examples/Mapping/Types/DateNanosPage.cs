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

namespace Examples.Mapping.Types
{
	public class DateNanosPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/date_nanos.asciidoc:34")]
		public void Line34()
		{
			// tag::5e11eb4d328005434b19bbb9b11a3685[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();

			var response2 = new SearchResponse<object>();

			var response3 = new SearchResponse<object>();

			var response4 = new SearchResponse<object>();

			var response5 = new SearchResponse<object>();

			var response6 = new SearchResponse<object>();
			// end::5e11eb4d328005434b19bbb9b11a3685[]

			response0.MatchesExample(@"PUT my_index
			{
			  ""mappings"": {
			    ""properties"": {
			      ""date"": {
			        ""type"": ""date_nanos"" <1>
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"PUT my_index/_doc/1
			{ ""date"": ""2015-01-01"" } <2>");

			response2.MatchesExample(@"PUT my_index/_doc/2
			{ ""date"": ""2015-01-01T12:10:30.123456789Z"" } <3>");

			response3.MatchesExample(@"PUT my_index/_doc/3
			{ ""date"": 1420070400 } <4>");

			response4.MatchesExample(@"GET my_index/_search
			{
			  ""sort"": { ""date"": ""asc""} <5>
			}");

			response5.MatchesExample(@"GET my_index/_search
			{
			  ""script_fields"" : {
			    ""my_field"" : {
			      ""script"" : {
			        ""lang"" : ""painless"",
			        ""source"" : ""doc['date'].value.nano"" <6>
			      }
			    }
			  }
			}");

			response6.MatchesExample(@"GET my_index/_search
			{
			  ""docvalue_fields"" : [
			    {
			      ""field"" : ""date"",
			      ""format"": ""strict_date_time"" <7>
			    }
			  ]
			}");
		}
	}
}
