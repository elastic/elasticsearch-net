// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
