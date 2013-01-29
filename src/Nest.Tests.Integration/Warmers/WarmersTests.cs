using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Warmers
{
	[TestFixture]
	public class WarmersTests : IntegrationTests
	{

		[Test]
		public void SimplePutAndGet()
		{
//			this._client.DeleTemplate("put-template-with-settings");
			var putResponse = this._client.PutWarmer<ElasticSearchProject>("warmer_simpleputandget", t => t
				.Query(q => q
				.Terms(p => p.Name, "value"))
			);
			Assert.IsTrue(putResponse.OK);

			var warmerResponse = this._client.GetWarmer<ElasticSearchProject>("warmer_simpleputandget");
			warmerResponse.Should().NotBeNull();
			warmerResponse.IsValid.Should().BeTrue();
			warmerResponse.Search.Should().NotBeNull();

		}

	}
}
