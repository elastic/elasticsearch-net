using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Tests.Framework;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.ServerError
{

	public abstract class ServerErrorTestsBase
	{
		private IElasticClient HighLevelClient { get; }
		private IElasticLowLevelClient LowLevelClient { get; }

		protected ServerErrorTestsBase()
		{
			var settings = TestClient.GetFixedReturnSettings(ResponseJson, 500);
			this.LowLevelClient = new ElasticLowLevelClient(settings);
			this.HighLevelClient = new ElasticClient(settings);
		}
		protected virtual void AssertServerError()
		{
			LowLevelCall();
			HighLevelCall();
		}

		protected void HighLevelCall()
		{
			var response = this.HighLevelClient.Search<Project>(s => s);
			response.Should().NotBeNull();
			var serverError = response.ServerError;
			serverError.Should().NotBeNull();
			serverError.Status.Should().Be(response.ApiCall.HttpStatusCode);
			serverError.Error.Should().NotBeNull();
			serverError.Error.Headers.Should().NotBeNull();
			AssertResponseError("high level client", serverError.Error);
		}

		protected void LowLevelCall()
		{
			var response = this.LowLevelClient.Search<StringResponse>(PostData.Serializable(new { }));
			response.Should().NotBeNull();
			response.Body.Should().NotBeNullOrWhiteSpace();
			var hasServerError = response.TryGetServerError(out var serverError);
			hasServerError.Should().BeTrue("we're trying to deserialize a server error using the helper but it returned false");
			serverError.Should().NotBeNull();
			serverError.Status.Should().Be(response.ApiCall.HttpStatusCode);
			AssertResponseError("low level client", serverError.Error);
		}

		private string ResponseJson => string.Concat(@"{ ""error"": ", Json, @",  ""status"":500 }");

		protected abstract string Json { get;  }

		protected abstract void AssertResponseError(string origin, Error error);
	}

	public class ErrorWithRootCause : ServerErrorTestsBase
	{
		[U] protected override void AssertServerError() => base.AssertServerError();

		protected override string Json  =>@"{
			""root_cause"": [
			{
				""type"": ""parse_exception"",
				""reason"": ""failed to parse source for create index""
			}],
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
			error.RootCause.Should().NotBeEmpty(origin).And.HaveCount(1, origin);
			var rootCause = error.RootCause.First();
			rootCause.Should().NotBeNull(origin);
			rootCause.Type.Should().NotBeNullOrWhiteSpace(origin);
			rootCause.Reason.Should().NotBeNullOrWhiteSpace(origin);
			var causedBy = error.CausedBy;
			causedBy.Should().NotBeNull(origin);
			causedBy.Type.Should().NotBeNullOrWhiteSpace(origin);
			causedBy.Reason.Should().NotBeNullOrWhiteSpace(origin);
		}
	}
	public class ErrorWithMultipleRootCauses : ServerErrorTestsBase
	{
		[U] protected override void AssertServerError() => base.AssertServerError();

		protected override string Json  => @"{
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

	public class ErrorWithNullRootCauses : ServerErrorTestsBase
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
	public class ComplexError : ServerErrorTestsBase
	{
		[U] protected override void AssertServerError() => base.AssertServerError();

		protected override string Json  =>@"{
	""root_cause"" : [
	{
		""type"" : ""parse_exception"",
		""reason"" : ""failed to parse date field [-1m] with format [strict_date_optional_time||epoch_millis]""
	}],
	""type"" : ""search_phase_execution_exception"",
	""reason"" : ""all shards failed"",
	""phase"" : ""query"",
	""grouped"" : true,
	""failed_shards"" : [
	{
		""shard"" : 0,
		""index"" : ""project"",
		""node"" : ""Uo6PBln_QrmD8Y9o1NKdQw"",
		""reason"" : {
			""type"" : ""parse_exception"",
			""reason"" : ""failed to parse date field [-1m] with format [strict_date_optional_time||epoch_millis]"",
			""caused_by"" : {
				""type"" : ""illegal_argument_exception"",
				""reason"" : ""Parse failure at index [2] of [-1m]""
			}
		}
	}
	],
	""headers"" : {
		""WWW-Authenticate"" : ""Bearer: ..."",
		""x"" : ""y""
	},
	""caused_by"" : {
		""type"" : ""parse_exception"",
		""reason"" : ""failed to parse date field [-1m] with format [strict_date_optional_time||epoch_millis]"",
		""index"" : null,
		""resource.id"" : [""alias1"", ""alias2""],
		""script_stack"" : [""alias1"", ""alias2""],
		""unknown_prop"" : [""alias1"", ""alias2""],
		""caused_by"" : {
			""type"" : ""illegal_argument_exception"",
			""reason"" : ""Parse failure at index [2] of [-1m]""
		}
	},
	""license.expired.feature"" : ""ml"",
	""index"" : ""index"",
	""index_uuid"" : ""x9h1ks"",
	""unknown_prop"" : {},
	""unknown_prop2"" : false,
	""resource.type"" : ""aliases"",
	""resource.id"" : ""alias1"",
	""shard"" : ""1"",
	""line"" : 12,
	""col"" : 199,
	""bytes_wanted"" : 1298312,
	""bytes_limit"" : 8912031,
	""script_stack"" : ""x"",
	""script"" : ""some script"",
	""lang"" : ""c#""
}";

		protected override void AssertResponseError(string origin, Error error)
		{
			AssertCausedBy(origin, error);
			AssertCausedBy(origin, error.CausedBy);
			AssertCausedBy(origin, error.CausedBy.CausedBy);
			error.RootCause.Should().NotBeEmpty(origin);
			error.Headers.Should().HaveCount(2, origin);
			AssertMetadata(origin, error.Metadata);
			error.CausedBy.Metadata.Should().NotBeNull();
			error.CausedBy.Metadata.ScriptStack.Should().HaveCount(2);
			error.CausedBy.Metadata.ResourceId.Should().HaveCount(2);
		}

		private void AssertMetadata(string origin, ErrorCause.ErrorCauseMetadata errorMetadata)
		{
			errorMetadata.Should().NotBeNull(origin);
			errorMetadata.Grouped.Should().BeTrue(origin);
			errorMetadata.Phase.Should().Be("query", origin);
			errorMetadata.LicensedExpiredFeature.Should().Be("ml", origin);
			errorMetadata.Index.Should().Be("index", origin);
			errorMetadata.IndexUUID.Should().NotBeNullOrWhiteSpace(origin);
			errorMetadata.ResourceType.Should().NotBeNullOrWhiteSpace(origin);
			errorMetadata.ResourceId.Should().HaveCount(1, origin);
			errorMetadata.Shard.Should().Be(1, origin);
			errorMetadata.Line.Should().Be(12, origin);
			errorMetadata.Column.Should().Be(199, origin);
			errorMetadata.BytesWanted.Should().BeGreaterThan(1, origin);
			errorMetadata.BytesLimit.Should().BeGreaterThan(1, origin);
			errorMetadata.ScriptStack.Should().HaveCount(1, origin);
			errorMetadata.Script.Should().NotBeNullOrWhiteSpace(origin);
			errorMetadata.Language.Should().NotBeNullOrWhiteSpace(origin);
		}

		private static void AssertCausedBy(string origin, ErrorCause causedBy)
		{
			causedBy.Should().NotBeNull(origin);
			causedBy.Type.Should().NotBeNullOrWhiteSpace(origin);
			causedBy.Reason.Should().NotBeNullOrWhiteSpace(origin);
		}
	}
}
