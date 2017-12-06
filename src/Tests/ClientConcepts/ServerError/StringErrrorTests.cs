using Elasticsearch.Net;
using FluentAssertions;
using Tests.Framework;

namespace Tests.ClientConcepts.ServerError
{
	public class StringErrrorTests : ServerErrorTestsBase
	{
		[U] protected override void AssertServerError() => base.AssertServerError();

		protected override string Json => @"""alias [x] is missing""";

		protected override void AssertResponseError(string origin, Error error)
		{
			error.Type.Should().BeNullOrEmpty(origin);
			error.Reason.Should().NotBeNullOrWhiteSpace(origin).And.Contain("is missing");
			error.RootCause.Should().BeNull(origin);
		}
	}
}
