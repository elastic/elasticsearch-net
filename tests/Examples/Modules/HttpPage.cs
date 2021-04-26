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

namespace Examples.Modules
{
	public class HttpPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("modules/http.asciidoc:137")]
		public void Line137()
		{
			// tag::45df8177c5f8a3cc4e36867742e8250c[]
			var response0 = new SearchResponse<object>();
			// end::45df8177c5f8a3cc4e36867742e8250c[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			   ""transient"" : {
			      ""logger.org.elasticsearch.http.HttpTracer"" : ""TRACE""
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/http.asciidoc:150")]
		public void Line150()
		{
			// tag::fa4e5b5cd144dd03cd507ffa9dec5b7e[]
			var response0 = new SearchResponse<object>();
			// end::fa4e5b5cd144dd03cd507ffa9dec5b7e[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			   ""transient"" : {
			      ""http.tracer.include"" : ""*"",
			      ""http.tracer.exclude"" : """"
			   }
			}");
		}
	}
}
