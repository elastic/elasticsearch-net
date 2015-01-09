using Elasticsearch.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.ObjectInitializer.Aliases
{
	[TestFixture]
	public class GetAliasesRequestTests : BaseJsonTests
	{
		private readonly IElasticsearchResponse _status;

		public GetAliasesRequestTests()
		{
			var request = new GetAliasesRequest()
			{
				Alias = "my-alias"  
			};
			var response = this._client.GetAliases(request);
			this._status = response.ConnectionStatus;
		}

		[Test]
		public void Url()
		{
			this._status.RequestUrl.Should().EndWith("/_aliases/my-alias");
			this._status.RequestMethod.Should().Be("GET");
		}
	}
}
