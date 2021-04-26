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

namespace Examples.Setup.Sysconfig
{
	public class FileDescriptorsPage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("setup/sysconfig/file-descriptors.asciidoc:29")]
		public void Line29()
		{
			// tag::c5bc577ff92f889225b0d2617adcb48c[]
			var response0 = new SearchResponse<object>();
			// end::c5bc577ff92f889225b0d2617adcb48c[]

			response0.MatchesExample(@"GET _nodes/stats/process?filter_path=**.max_file_descriptors");
		}
	}
}
