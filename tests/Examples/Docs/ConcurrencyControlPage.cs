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
	public class ConcurrencyControlPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("docs/concurrency-control.asciidoc:24")]
		public void Line24()
		{
			// tag::cffc8b207f354beb6d76c8d334cab677[]
			var response0 = new SearchResponse<object>();
			// end::cffc8b207f354beb6d76c8d334cab677[]

			response0.MatchesExample(@"PUT products/_doc/1567
			{
			    ""product"" : ""r2d2"",
			    ""details"" : ""A resourceful astromech droid""
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/concurrency-control.asciidoc:60")]
		public void Line60()
		{
			// tag::278d5bfa1a01f91d5c84679ef1bca390[]
			var response0 = new SearchResponse<object>();
			// end::278d5bfa1a01f91d5c84679ef1bca390[]

			response0.MatchesExample(@"GET products/_doc/1567");
		}

		[U(Skip = "Example not implemented")]
		[Description("docs/concurrency-control.asciidoc:100")]
		public void Line100()
		{
			// tag::ac24941027452bdafe82b4bd7edf9000[]
			var response0 = new SearchResponse<object>();
			// end::ac24941027452bdafe82b4bd7edf9000[]

			response0.MatchesExample(@"PUT products/_doc/1567?if_seq_no=362&if_primary_term=2
			{
			    ""product"" : ""r2d2"",
			    ""details"" : ""A resourceful astromech droid"",
			    ""tags"": [""droid""]
			}");
		}
	}
}
