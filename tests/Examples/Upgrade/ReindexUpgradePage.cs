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

namespace Examples.Upgrade
{
	public class ReindexUpgradePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("upgrade/reindex_upgrade.asciidoc:160")]
		public void Line160()
		{
			// tag::acd65c045139fef38ef5cd20c8c1cfc1[]
			var response0 = new SearchResponse<object>();
			// end::acd65c045139fef38ef5cd20c8c1cfc1[]

			response0.MatchesExample(@"POST _reindex
			{
			  ""source"": {
			    ""remote"": {
			      ""host"": ""http://oldhost:9200"",
			      ""username"": ""user"",
			      ""password"": ""pass""
			    },
			    ""index"": ""source"",
			    ""query"": {
			      ""match"": {
			        ""test"": ""data""
			      }
			    }
			  },
			  ""dest"": {
			    ""index"": ""dest""
			  }
			}");
		}
	}
}
