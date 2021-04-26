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

namespace Examples.Licensing
{
	public class UpdateLicensePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("licensing/update-license.asciidoc:61")]
		public void Line61()
		{
			// tag::85f2839beeb71edb66988e5c82188be0[]
			var response0 = new SearchResponse<object>();
			// end::85f2839beeb71edb66988e5c82188be0[]

			response0.MatchesExample(@"PUT _license
			{
			  ""licenses"": [
			    {
			      ""uid"":""893361dc-9749-4997-93cb-802e3d7fa4xx"",
			      ""type"":""basic"",
			      ""issue_date_in_millis"":1411948800000,
			      ""expiry_date_in_millis"":1914278399999,
			      ""max_nodes"":1,
			      ""issued_to"":""issuedTo"",
			      ""issuer"":""issuer"",
			      ""signature"":""xx""
			    }
			    ]
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("licensing/update-license.asciidoc:137")]
		public void Line137()
		{
			// tag::46b1c1f6e0c86528be84c373eeb8d425[]
			var response0 = new SearchResponse<object>();
			// end::46b1c1f6e0c86528be84c373eeb8d425[]

			response0.MatchesExample(@"PUT _license?acknowledge=true
			{
			  ""licenses"": [
			    {
			      ""uid"":""893361dc-9749-4997-93cb-802e3d7fa4xx"",
			      ""type"":""basic"",
			      ""issue_date_in_millis"":1411948800000,
			      ""expiry_date_in_millis"":1914278399999,
			      ""max_nodes"":1,
			      ""issued_to"":""issuedTo"",
			      ""issuer"":""issuer"",
			      ""signature"":""xx""
			    }
			    ]
			}");
		}
	}
}
