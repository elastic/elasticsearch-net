// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;
using FluentAssertions;

namespace Tests.ClientConcepts.ServerError
{
	public class StringErrorTests : ServerErrorTestsBase
	{
		protected override string Json => @"""alias [x] is missing""";

		[U] protected override void AssertServerError() => base.AssertServerError();

		protected override void AssertResponseError(string origin, Error error)
		{
			error.Type.Should().BeNullOrEmpty(origin);
			error.Reason.Should().NotBeNullOrWhiteSpace(origin).And.Contain("is missing");
			error.RootCause.Should().BeNull(origin);
		}
	}

	public class TempErrorTests : ServerErrorTestsBase
	{
		protected override string Json =>
			@"{""root_cause"":[{""type"":""index_not_found_exception"",""reason"":""no such index"",""index_uuid"":""_na_"",""index"":""non-existent-index""}],""type"":""index_not_found_exception"",""reason"":""no such index"",""index_uuid"":""_na_"",""index"":""non-existent-index""}";

		[U] protected override void AssertServerError() => base.AssertServerError();

		protected override void AssertResponseError(string origin, Error error)
		{
			error.Type.Should().NotBeNullOrEmpty(origin);
			error.Reason.Should().NotBeNullOrWhiteSpace(origin);
			error.RootCause.Should().NotBeNull(origin).And.HaveCount(1);
		}
	}
}
