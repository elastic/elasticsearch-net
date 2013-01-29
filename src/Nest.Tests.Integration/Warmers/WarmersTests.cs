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

			//var templateResponse = this._client.GetTemplate("put-template-with-settings");
			//templateResponse.Should().NotBeNull();
			//templateResponse.IsValid.Should().BeTrue();
			//templateResponse.TemplateMapping.Should().NotBeNull();
			//templateResponse.TemplateMapping.Mappings.Should().NotBeNull();
			
			//var settings = templateResponse.TemplateMapping.Settings;
			//templateResponse.TemplateMapping.Order.Should().Be(42);

			//settings.Should().NotBeNull();
		}

	}
}
