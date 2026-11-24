using Elastic.Clients.Elasticsearch.Core.Bulk;
using Elastic.Clients.Elasticsearch.Serialization;
using FluentAssertions;
using Xunit;

namespace Tests.Bulk;

public sealed class CreateResponseItemTests
{
	[Fact]
	public void ToString_WhenSuccessful_ReturnsExpectedText()
	{
		CreateResponseItem createResponseItem = new(JsonConstructorSentinel.Instance)
		{
			Id = "id",
			Index = "index",
			Status = 200,
			Result = "created"
		};

		var actual = createResponseItem.ToString();

		actual.Should().Be("Index: index, Operation: create, Id: id, Status: 200 (created)");
	}

	[Fact]
	public void ToString_WhenNotSuccessful_ReturnsExpectedText()
	{
		CreateResponseItem createResponseItem = new(JsonConstructorSentinel.Instance)
		{
			Index = "index",
			Status = 400,
			Error = new Elastic.Clients.Elasticsearch.ErrorCause()
			{
				Type = "error",
				Reason = "reason"
			}
		};

		var actual = createResponseItem.ToString();

		actual.Should().Be("Index: index, Operation: create, Status: 400, Error: error, Reason: reason");
	}
}
