// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using System.ComponentModel;

namespace Examples.Ingest.Apis
{
	public class DeletePipelinePage : ExampleBase
	{
		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/delete-pipeline.asciidoc:28")]
		public void Line28()
		{
			// tag::dff61a76d5ef9ca8cbe59a416269a84b[]
			var response0 = new SearchResponse<object>();
			// end::dff61a76d5ef9ca8cbe59a416269a84b[]

			response0.MatchesExample(@"DELETE /_ingest/pipeline/my-pipeline-id");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/delete-pipeline.asciidoc:67")]
		public void Line67()
		{
			// tag::a7cf31f4b907e4c00132aca75f55790c[]
			var response0 = new SearchResponse<object>();
			// end::a7cf31f4b907e4c00132aca75f55790c[]

			response0.MatchesExample(@"DELETE /_ingest/pipeline/pipeline-one");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/delete-pipeline.asciidoc:76")]
		public void Line76()
		{
			// tag::c6b5c695a9b757b5e7325345b206bde5[]
			var response0 = new SearchResponse<object>();
			// end::c6b5c695a9b757b5e7325345b206bde5[]

			response0.MatchesExample(@"DELETE /_ingest/pipeline/pipeline-*");
		}

		[U(Skip = "Example not implemented")]
		[Description("ingest/apis/delete-pipeline.asciidoc:85")]
		public void Line85()
		{
			// tag::11e772ff5dbb73408ae30a1a367a0d9b[]
			var response0 = new SearchResponse<object>();
			// end::11e772ff5dbb73408ae30a1a367a0d9b[]

			response0.MatchesExample(@"DELETE /_ingest/pipeline/*");
		}
	}
}
