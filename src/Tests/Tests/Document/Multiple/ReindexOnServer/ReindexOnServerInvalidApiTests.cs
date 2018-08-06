using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Document.Multiple.ReindexOnServer
{
	[SkipVersion("<2.3.0", "")]
	public class ReindexOnServerInvalidApiTests : ReindexOnServerApiTests
	{
		public ReindexOnServerInvalidApiTests(IntrusiveOperationCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 500;

		//bad painless script
		protected override string PainlessScript { get; } = "if ctx._source.flag == 'bar') {ctx._source.remove('flag')}";

		protected override void ExpectResponse(IReindexOnServerResponse response)
		{
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(500);
			response.ServerError.Error.Should().NotBeNull();
			response.ServerError.Error.RootCause.Should().NotBeNullOrEmpty();
			response.ServerError.Error.RootCause.First().Reason.Should().Contain("compil");
			response.ServerError.Error.RootCause.First().Type.Should().Be("script_exception");
		}

	}
}
