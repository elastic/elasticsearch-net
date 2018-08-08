using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Tests.Core.Serialization;
using static Tests.Core.Serialization.SerializationTestHelper;

namespace Tests.ClientConcepts.ServerError
{
	public class ServerErrorTests
	{
		[U]
		public void CanDeserializeServerError()
		{
			var serverErrorJson = @"{
			   ""error"": {
				  ""root_cause"": [
					 {
						""type"": ""parse_exception"",
						""reason"": ""failed to parse source for create index""
					 }
				  ],
				  ""type"": ""parse_exception"",
				  ""reason"": ""failed to parse source for create index"",
				  ""caused_by"": {
					 ""type"": ""json_parse_exception"",
					 ""reason"": ""Unexpected character ('\""' (code 34)): was expecting a colon to separate field name and value\n at [Source: [B@1231dcb3; line: 6, column: 10]""
				  }
			   },
			   ""status"": 400
			}";

			var serverError = Expect(serverErrorJson).NoRoundTrip().DeserializesTo<Elasticsearch.Net.ServerError>();

			serverError.Should().NotBeNull();
			serverError.Status.Should().Be(400);

			serverError.Error.Should().NotBeNull();
			serverError.Error.RootCause.Count.Should().Be(1);
			serverError.Error.CausedBy.Should().NotBeNull();
		}
	}
}
