using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Tests.Framework;
using FluentAssertions;

namespace Tests.ClientConcepts.ServerError
{
	public class ErrorWithNullRootCausesTests : ServerErrorTestsBase
	{
		[U] protected override void AssertServerError() => base.AssertServerError();

		protected override string Json  =>@"{
			""root_cause"": null,
			""type"": ""parse_exception"",
			""reason"": ""failed to parse source for create index"",
			""caused_by"": {
				""type"": ""json_parse_exception"",
				""reason"": ""Unexpected character ('\""' (code 34)): was expecting a colon to separate field name and value\n at [Source: [B@1231dcb3; line: 6, column: 10]""
			}
		}";

		protected override void AssertResponseError(string origin, Error error)
		{
			error.Type.Should().NotBeNullOrWhiteSpace(origin);
			error.Reason.Should().NotBeNullOrWhiteSpace(origin);
			error.RootCause.Should().BeNull(origin);
			var causedBy = error.CausedBy;
			causedBy.Should().NotBeNull(origin);
			causedBy.Type.Should().NotBeNullOrWhiteSpace(origin);
			causedBy.Reason.Should().NotBeNullOrWhiteSpace(origin);
		}
	}
}
