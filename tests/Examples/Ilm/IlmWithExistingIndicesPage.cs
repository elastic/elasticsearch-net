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

namespace Examples.Ilm
{
	public class IlmWithExistingIndicesPage : ExampleBase
	{

		[U(Skip = "Example not implemented")]
		[Description("ilm/ilm-with-existing-indices.asciidoc:75")]
		public void Line75()
		{
			// tag::5df1ed33b5fcf3b9d85c20d100780d43[]
			var response0 = new SearchResponse<object>();
			// end::5df1ed33b5fcf3b9d85c20d100780d43[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""transient"": {
			    ""indices.lifecycle.poll_interval"": ""1m"" \<1>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/ilm-with-existing-indices.asciidoc:158")]
		public void Line158()
		{
			// tag::afadb6bb7d0fa5a4531708af1ea8f9f8[]
			var response0 = new SearchResponse<object>();
			// end::afadb6bb7d0fa5a4531708af1ea8f9f8[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""index"": ""mylogs-*"" <1>
			  },
			  ""dest"": {
			    ""index"": ""mylogs"", <2>
			    ""op_type"": ""create"" <3>
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("ilm/ilm-with-existing-indices.asciidoc:184")]
		public void Line184()
		{
			// tag::227e19aecb349f31e74898384322ae01[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::227e19aecb349f31e74898384322ae01[]

			response0.MatchesExample(@"PUT _cluster/settings
			{
			  ""transient"": {
			    ""indices.lifecycle.poll_interval"": null
			  }
			}");

			response1.MatchesExample(@"");
		}
	}
}
