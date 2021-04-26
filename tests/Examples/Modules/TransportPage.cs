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
	public class TransportPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("modules/transport.asciidoc:155")]
		public void Line155()
		{
			// tag::939e79dee613238f9512fb9cbf0be816[]
			var response0 = new SearchResponse<object>();
			// end::939e79dee613238f9512fb9cbf0be816[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			   ""transient"" : {
			      ""logger.org.elasticsearch.transport.TransportService.tracer"" : ""TRACE""
			   }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("modules/transport.asciidoc:168")]
		public void Line168()
		{
			// tag::cecbbd7b4ec1bf82fd84ae96099febcc[]
			var response0 = new SearchResponse<object>();
			// end::cecbbd7b4ec1bf82fd84ae96099febcc[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			   ""transient"" : {
			      ""transport.tracer.include"" : ""*"",
			      ""transport.tracer.exclude"" : ""internal:coordination/fault_detection/*""
			   }
			}");
		}
	}
}
