using FluentAssertions;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System.Net;
using System.Linq;

namespace Nest.Tests.Integration.Warmers
{
	[TestFixture]
	public class WarmersTests : IntegrationTests
	{
		[Test]
		public void SimplePutAndGet()
		{
			var putResponse = this._client.PutWarmer("warmer_simpleputandget", wd => wd
				.Type<ElasticSearchProject>()
				.Search<ElasticSearchProject>(s => s
					.Query(q => q
						.Terms(p => p.Name, "strange-value")
					)
				//.Filter(filter => filter)  // this is why there is a search descriptor
				)
			);
			Assert.IsTrue(putResponse.OK);

			var warmerResponse = this._client.GetWarmer("warmer_simpleputandget", wd => wd
				.Index<ElasticSearchProject>()
			   );

			warmerResponse.Should().NotBeNull();
			warmerResponse.IsValid.Should().BeTrue();
			warmerResponse.Indices.Should().NotBeNull();
			warmerResponse.Indices.Should().ContainKey(_settings.DefaultIndex);
			warmerResponse.Indices[_settings.DefaultIndex].Should().ContainKey("warmer_simpleputandget");
			var warmerMapping = warmerResponse.Indices[_settings.DefaultIndex]["warmer_simpleputandget"];
			warmerMapping.Name.Should().Be("warmer_simpleputandget");
			var typeName = _client.Infer.TypeName<ElasticSearchProject>();
			warmerMapping.Types.Select(s => s.Resolve(_settings)).Contains(typeName).Should().Be(true);
			//warmerMapping.Source.Should().Contain("\"strange-value\"");
		}

		[Test]
		public void PutWithEmptyTypes()
		{
			//			this._client.DeleTemplate("put-template-with-settings");
			var putResponse = this._client.PutWarmer("warmer_putwithemptytypes", wd => wd
				.Index<ElasticSearchProject>()
				.Search<ElasticSearchProject>(s => s
					.Query(q => q
						.Terms(p => p.Name, "strange-value")
					)
				//.Filter(filter => filter)  // this is why there is a search descriptor
				)
			);
			Assert.IsTrue(putResponse.OK);

			var warmerResponse = this._client.GetWarmer("warmer_putwithemptytypes", wd => wd
				.Index<ElasticSearchProject>()
			);
			warmerResponse.Should().NotBeNull();
			warmerResponse.IsValid.Should().BeTrue();
			warmerResponse.Indices.Should().NotBeNull();
			warmerResponse.Indices.Should().ContainKey(_settings.DefaultIndex);
			warmerResponse.Indices[_settings.DefaultIndex].Should().ContainKey("warmer_putwithemptytypes");
			var warmerMapping = warmerResponse.Indices[_settings.DefaultIndex]["warmer_putwithemptytypes"];
			warmerMapping.Name.Should().Be("warmer_putwithemptytypes");
			warmerMapping.Types.Should().BeEmpty();
			//warmerMapping.Source.Should().Contain("\"strange-value\"");
		}

		[Test]
		public void PutToDefaultIndex()
		{
			//			this._client.DeleTemplate("put-template-with-settings");
			var putResponse = this._client.PutWarmer("warmer_puttodefaultindex", wd => wd
				.Search<ElasticSearchProject>(s => s
					.Query(q => q
						.Terms(p => p.Name, "strange-value")
					)
				)
			);
			Assert.IsTrue(putResponse.OK);

			var warmerResponse = this._client.GetWarmer("warmer_puttodefaultindex", wd => wd
				.Index<ElasticSearchProject>()
			);
			warmerResponse.Should().NotBeNull();
			warmerResponse.IsValid.Should().BeTrue();
			warmerResponse.Indices.Should().NotBeNull();
			warmerResponse.Indices.Should().ContainKey(_settings.DefaultIndex);
			warmerResponse.Indices[_settings.DefaultIndex].Should().ContainKey("warmer_puttodefaultindex");
			var warmerMapping = warmerResponse.Indices[_settings.DefaultIndex]["warmer_puttodefaultindex"];
			warmerMapping.Name.Should().Be("warmer_puttodefaultindex");
			warmerMapping.Types.Should().BeEmpty();
			//warmerMapping.Source.Should().Contain("\"strange-value\"");
		}



		[Test]
		public void Delete()
		{
			//			this._client.DeleTemplate("put-template-with-settings");
			var putResponse = this._client.PutWarmer("warmer_delete", wd => wd
				.AllIndices()
				.Search<ElasticSearchProject>(s => s
					.Query(q => q
						.Terms(p => p.Name, "strange-value")
					)
				)
			);
			Assert.IsTrue(putResponse.OK);

			var deleteResponse = this._client.DeleteWarmer("warmer_delete", wd => wd
				.Index<ElasticSearchProject>()
			);
			Assert.IsTrue(deleteResponse.OK);

			var warmerResponse = this._client.GetWarmer("warmer_delete", wd => wd
				.Index<ElasticSearchProject>()
			);
			warmerResponse.Should().NotBeNull();
			warmerResponse.IsValid.Should().BeFalse();
			warmerResponse.ConnectionStatus.Error.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
		}

	}
}
