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

namespace Examples.Ingest.Processors
{
	public class SetPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/set.asciidoc:33")]
		public void Line33()
		{
			// tag::366b29ef910f12c7fbced35f39000953[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::366b29ef910f12c7fbced35f39000953[]

			response0.MatchesExample(@"PUT _ingest/pipeline/set_os
			{
			  ""description"": ""sets the value of host.os.name from the field os"",
			  ""processors"": [
			    {
			      ""set"": {
			        ""field"": ""host.os.name"",
			        ""value"": ""{{os}}""
			      }
			    }
			  ]
			}");

			response1.MatchesExample(@"POST _ingest/pipeline/set_os/_simulate
			{
			  ""docs"": [
			    {
			      ""_source"": {
			        ""os"": ""Ubuntu""
			      }
			    }
			  ]
			}");
		}
	}
}
