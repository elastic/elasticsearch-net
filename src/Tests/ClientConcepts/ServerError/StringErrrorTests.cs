using Elastic.Xunit.XunitPlumbing;
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
	//

	public class TempErrrorTests : ServerErrorTestsBase
	{
		[U] protected override void AssertServerError() => base.AssertServerError();

		protected override string Json => @"{""root_cause"":[{""type"":""index_not_found_exception"",""reason"":""no such index"",""index_uuid"":""_na_"",""index"":""non-existent-index""}],""type"":""index_not_found_exception"",""reason"":""no such index"",""index_uuid"":""_na_"",""index"":""non-existent-index""}";

		protected override void AssertResponseError(string origin, Error error)
		{
			error.Type.Should().NotBeNullOrEmpty(origin);
			error.Reason.Should().NotBeNullOrWhiteSpace(origin);
			error.RootCause.Should().NotBeNull(origin).And.HaveCount(1);
		}
	}
}
