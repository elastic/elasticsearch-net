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
	public class ScriptPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/script.asciidoc:50")]
		public void Line50()
		{
			// tag::197d87fdb4aeccf3d9a08ae485c12306[]
			var response0 = new SearchResponse<object>();
			// end::197d87fdb4aeccf3d9a08ae485c12306[]

			response0.MatchesExample(@"PUT _ingest/pipeline/my_index
			{
			    ""description"": ""use index:my_index"",
			    ""processors"": [
			      {
			        ""script"": {
			          ""source"": """"""
			            ctx._index = 'my_index';
			          """"""
			        }
			      }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/processors/script.asciidoc:69")]
		public void Line69()
		{
			// tag::cdc55ad88de55999fe2d79fd4781918b[]
			var response0 = new SearchResponse<object>();
			// end::cdc55ad88de55999fe2d79fd4781918b[]

			response0.MatchesExample(@"PUT any_index/_doc/1?pipeline=my_index
			{
			  ""message"": ""text""
			}");
		}
	}
}
