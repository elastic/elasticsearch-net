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
	public class FlattenedPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("mapping/types/flattened.asciidoc:38")]
		public void Line38()
		{
			// tag::8aa74aee3dcf4b34028e4c5e1c1ed27b[]
			var response0 = new SearchResponse<object>();

			var response1 = new SearchResponse<object>();
			// end::8aa74aee3dcf4b34028e4c5e1c1ed27b[]

			response0.MatchesExample(@"PUT bug_reports
			{
			  ""mappings"": {
			    ""properties"": {
			      ""title"": {
			        ""type"": ""text""
			      },
			      ""labels"": {
			        ""type"": ""flattened""
			      }
			    }
			  }
			}");

			response1.MatchesExample(@"POST bug_reports/_doc/1
			{
			  ""title"": ""Results are not sorted correctly."",
			  ""labels"": {
			    ""priority"": ""urgent"",
			    ""release"": [""v1.2.5"", ""v1.3.0""],
			    ""timestamp"": {
			      ""created"": 1541458026,
			      ""closed"": 1541457010
			    }
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/flattened.asciidoc:76")]
		public void Line76()
		{
			// tag::169b39bb889ecd47541bed3e48725488[]
			var response0 = new SearchResponse<object>();
			// end::169b39bb889ecd47541bed3e48725488[]

			response0.MatchesExample(@"POST bug_reports/_search
			{
			  ""query"": {
			    ""term"": {""labels"": ""urgent""}
			  }
			}");
		}

		[U(Skip = "Example not implemented")]
		[Description("mapping/types/flattened.asciidoc:88")]
		public void Line88()
		{
			// tag::2f4a55dfeba8851b306ef9c1b216ef54[]
			var response0 = new SearchResponse<object>();
			// end::2f4a55dfeba8851b306ef9c1b216ef54[]

			response0.MatchesExample(@"POST bug_reports/_search
			{
			  ""query"": {
			    ""term"": {""labels.release"": ""v1.3.0""}
			  }
			}");
		}
	}
}
