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
	public class PutPipelinePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/put-pipeline.asciidoc:11")]
		public void Line11()
		{
			// tag::e7e28812b86c5257bf48931d131409f0[]
			var response0 = new SearchResponse<object>();
			// end::e7e28812b86c5257bf48931d131409f0[]

			response0.MatchesExample(@"PUT _ingest/pipeline/my-pipeline-id
			{
			  ""description"" : ""describe pipeline"",
			  ""processors"" : [
			    {
			      ""set"" : {
			        ""field"": ""foo"",
			        ""value"": ""bar""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/put-pipeline.asciidoc:93")]
		public void Line93()
		{
			// tag::3467d11d772321353951461b756e3ffb[]
			var response0 = new SearchResponse<object>();
			// end::3467d11d772321353951461b756e3ffb[]

			response0.MatchesExample(@"PUT /_ingest/pipeline/my-pipeline-id
			{
			  ""description"" : ""describe pipeline"",
			  ""version"" : 123,
			  ""processors"" : [
			    {
			      ""set"" : {
			        ""field"": ""foo"",
			        ""value"": ""bar""
			      }
			    }
			  ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/put-pipeline.asciidoc:113")]
		public void Line113()
		{
			// tag::ca42bbe58bdd127eb042389699d4a45c[]
			var response0 = new SearchResponse<object>();
			// end::ca42bbe58bdd127eb042389699d4a45c[]

			response0.MatchesExample(@"PUT /_ingest/pipeline/my-pipeline-id
			{
			  ""description"" : ""describe pipeline"",
			  ""processors"" : [
			    {
			      ""set"" : {
			        ""field"": ""foo"",
			        ""value"": ""bar""
			      }
			    }
			  ]
			}");
		}
	}
}
