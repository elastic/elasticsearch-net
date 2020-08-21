// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Linq;
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Document.Multiple.ReindexOnServer
{
	[SkipVersion("<2.3.0", "")]
	public class ReindexOnServerInvalidApiTests : ReindexOnServerApiTests
	{
		public ReindexOnServerInvalidApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 400;

		//bad painless script - missing opening ( in front of ctx.
		protected override string PainlessScript { get; } = "if ctx._source.flag == 'bar') {ctx._source.remove('flag')}";

		protected override void ExpectResponse(ReindexOnServerResponse response)
		{
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(400);
			response.ServerError.Error.Should().NotBeNull();
			response.ServerError.Error.RootCause.Should().NotBeNullOrEmpty();
			response.ServerError.Error.RootCause.First().Reason.Should().Contain("compile");
			response.ServerError.Error.RootCause.First().Type.Should().Be("script_exception");
		}

		// https://youtrack.jetbrains.com/issue/RIDER-19912
		[I] public override async Task ReturnsExpectedStatusCode() => await base.ReturnsExpectedResponse();

		[I] public override async Task ReturnsExpectedIsValid() => await base.ReturnsExpectedIsValid();

		[I] public override async Task ReturnsExpectedResponse() => await base.ReturnsExpectedResponse();

	}
}
