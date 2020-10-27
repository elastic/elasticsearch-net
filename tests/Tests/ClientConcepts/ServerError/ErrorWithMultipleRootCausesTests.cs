// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch.Failures;
using FluentAssertions;

namespace Tests.ClientConcepts.ServerError
{
	public class ErrorWithMultipleRootCausesTests : ServerErrorTestsBase
	{
		protected override string Json => @"{
			""root_cause"": [
			{
				""type"": ""parse_exception1"",
				""reason"": ""failed to parse source for create index"",
				""stack_trace"" : ""[Failed to parse int parameter [size] with value [boom]]; nested: IllegalArgumentException""

			}, {
				""type"": ""parse_exception2"",
				""reason"": ""failed to parse source for create index"",
				""stack_trace"" : ""[Failed to parse int parameter [size] with value [boom]]; nested: IllegalArgumentException""
			}],
			""stack_trace"" : ""[Failed to parse int parameter [size] with value [boom]]; nested: IllegalArgumentException"",
			""type"": ""parse_exception"",
			""reason"": ""failed to parse source for create index"",
			""caused_by"": {
				""type"": ""json_parse_exception"",
				""stack_trace"" : ""[Failed to parse int parameter [size] with value [boom]]; nested: IllegalArgumentException"",
				""reason"": ""Unexpected character ('\""' (code 34)): was expecting a colon to separate field name and value\n at [Source: [B@1231dcb3; line: 6, column: 10]""
			}
		}";

		[U] protected override void AssertServerError() => base.AssertServerError();

		protected override void AssertResponseError(string origin, Error error)
		{
			error.Type.Should().NotBeNullOrWhiteSpace(origin);
			error.Reason.Should().NotBeNullOrWhiteSpace(origin);
			error.StackTrace.Should().NotBeNullOrWhiteSpace(origin);
			error.RootCause.Should().NotBeEmpty(origin).And.HaveCount(2, origin);
			foreach (var rootCause in error.RootCause)
			{
				rootCause.Should().NotBeNull(origin);
				rootCause.Type.Should().NotBeNullOrWhiteSpace(origin);
				rootCause.Reason.Should().NotBeNullOrWhiteSpace(origin);
				rootCause.StackTrace.Should().NotBeNullOrWhiteSpace(origin);
			}
			var causedBy = error.CausedBy;
			causedBy.Should().NotBeNull(origin);
			causedBy.Type.Should().NotBeNullOrWhiteSpace(origin);
			causedBy.Reason.Should().NotBeNullOrWhiteSpace(origin);
			causedBy.StackTrace.Should().NotBeNullOrWhiteSpace(origin);
		}
	}
}
