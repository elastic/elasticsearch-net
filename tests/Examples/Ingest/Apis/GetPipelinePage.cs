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

namespace Examples.Ingest.Apis
{
	public class GetPipelinePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/get-pipeline.asciidoc:30")]
		public void Line30()
		{
			// tag::6a3a86ff58e5f20950d429cf2832c229[]
			var response0 = new SearchResponse<object>();
			// end::6a3a86ff58e5f20950d429cf2832c229[]

			response0.MatchesExample(@"GET /_ingest/pipeline/my-pipeline-id");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/get-pipeline.asciidoc:107")]
		public void Line107()
		{
			// tag::9f549bb400b6cc1523b00d60bc8fd8e1[]
			var response0 = new SearchResponse<object>();
			// end::9f549bb400b6cc1523b00d60bc8fd8e1[]

			response0.MatchesExample(@"GET /_ingest/pipeline/my-pipeline-id?filter_path=*.version");
		}
	}
}
