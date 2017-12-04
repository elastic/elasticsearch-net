using Tests.Framework;
using FluentAssertions;

namespace Tests.ClientConcepts.ServerError
{
	public class ServerErrorTests : SerializationTestBase
	{
		[U]
		public void CanDeserializeServerError()
		{
			var serverErrorJson = @"{
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

			var serverError = this.Deserialize<Elasticsearch.Net.Error>(serverErrorJson);

			serverError.Should().NotBeNull();

			serverError.Should().NotBeNull();
			serverError.RootCause.Count.Should().Be(1);
			serverError.CausedBy.Should().NotBeNull();
		}
	}
}
