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
		TypeNameResolver typeNameResolver = new TypeNameResolver();
		[Test]
		public void SimplePutAndGet()
		{
			var putResponse = this._client.PutWarmer(wd => wd
				.WarmerName("warmer_simpleputandget")
				.Type<ElasticSearchProject>()
				.Search<ElasticSearchProject>(s => s
					.Query(q => q
						.Terms(p => p.Name, "strange-value")
					)
					//.Filter(filter => filter)  // this is why there is a search descriptor
				)
			);
			Assert.IsTrue(putResponse.OK);

			var warmerResponse = this._client.GetWarmer(wd => wd
				.IndexFromType<ElasticSearchProject>()
				.WarmerName("warmer_simpleputandget"));

			warmerResponse.Should().NotBeNull();
			warmerResponse.IsValid.Should().BeTrue();
			warmerResponse.Indices.Should().NotBeNull();
			warmerResponse.Indices.Should().ContainKey(_settings.DefaultIndex);
			warmerResponse.Indices[_settings.DefaultIndex].Should().ContainKey("warmer_simpleputandget");
			var warmerMapping = warmerResponse.Indices[_settings.DefaultIndex]["warmer_simpleputandget"];
			warmerMapping.Name.Should().Be("warmer_simpleputandget");
			var typeName = typeNameResolver.GetTypeNameFor<ElasticSearchProject>().Resolve(_settings);
			warmerMapping.Types.Select(s => s.Resolve(_settings)).Contains(typeName).Should().Be(true);
			warmerMapping.Source.Should().Contain("\"strange-value\"");
		}

		[Test]
		public void PutWithEmptyTypes()
		{
//			this._client.DeleTemplate("put-template-with-settings");
			var putResponse = this._client.PutWarmer(wd => wd
				.WarmerName("warmer_putwithemptytypes")
				.IndexFromType<ElasticSearchProject>()
				.Search<ElasticSearchProject>(s => s
					.Query(q => q
						.Terms(p => p.Name, "strange-value")
					)
				//.Filter(filter => filter)  // this is why there is a search descriptor
				)
			);
			Assert.IsTrue(putResponse.OK);

			var warmerResponse = this._client.GetWarmer(wd => wd
				.IndexFromType<ElasticSearchProject>()
				.WarmerName("warmer_putwithemptytypes"));

			warmerResponse.Should().NotBeNull();
			warmerResponse.IsValid.Should().BeTrue();
			warmerResponse.Indices.Should().NotBeNull();
			warmerResponse.Indices.Should().ContainKey(_settings.DefaultIndex);
			warmerResponse.Indices[_settings.DefaultIndex].Should().ContainKey("warmer_putwithemptytypes");
			var warmerMapping = warmerResponse.Indices[_settings.DefaultIndex]["warmer_putwithemptytypes"];
			warmerMapping.Name.Should().Be("warmer_putwithemptytypes");
			warmerMapping.Types.Should().BeEmpty();
			warmerMapping.Source.Should().Contain("\"strange-value\"");
		}

		[Test]
		public void PutToDefaultIndex()
		{
			//			this._client.DeleTemplate("put-template-with-settings");
			var putResponse = this._client.PutWarmer(wd => wd
				.WarmerName("warmer_puttodefaultindex")
				.Search<ElasticSearchProject>(s => s
					.Query(q => q
						.Terms(p => p.Name, "strange-value")
					)
				//.Filter(filter => filter)  // this is why there is a search descriptor
				)
			);
			Assert.IsTrue(putResponse.OK);

			var warmerResponse = this._client.GetWarmer(wd => wd
				.IndexFromType<ElasticSearchProject>()
				.WarmerName("warmer_puttodefaultindex"));

			warmerResponse.Should().NotBeNull();
			warmerResponse.IsValid.Should().BeTrue();
			warmerResponse.Indices.Should().NotBeNull();
			warmerResponse.Indices.Should().ContainKey(_settings.DefaultIndex);
			warmerResponse.Indices[_settings.DefaultIndex].Should().ContainKey("warmer_puttodefaultindex");
			var warmerMapping = warmerResponse.Indices[_settings.DefaultIndex]["warmer_puttodefaultindex"];
			warmerMapping.Name.Should().Be("warmer_puttodefaultindex");
			warmerMapping.Types.Should().BeEmpty();
			warmerMapping.Source.Should().Contain("\"strange-value\"");
		}



		[Test]
		public void Delete()
		{
			//			this._client.DeleTemplate("put-template-with-settings");
			var putResponse = this._client.PutWarmer(wd => wd
				.WarmerName("warmer_delete")
				.Search<ElasticSearchProject>(s => s
					.Query(q => q
						.Terms(p => p.Name, "strange-value")
					)
				//.Filter(filter => filter)  // this is why there is a search descriptor
				)
			);
			Assert.IsTrue(putResponse.OK);

			var deleteResponse = this._client.DeleteWarmer(wd => wd
				.IndexFromType<ElasticSearchProject>()
				.WarmerName("warmer_delete"));

			Assert.IsTrue(deleteResponse.OK);

			var warmerResponse = this._client.GetWarmer(wd => wd
				.IndexFromType<ElasticSearchProject>()
				.WarmerName("warmer_delete"));

			warmerResponse.Should().NotBeNull();
			warmerResponse.IsValid.Should().BeFalse();
			warmerResponse.ConnectionStatus.Error.HttpStatusCode.Should().Be(HttpStatusCode.NotFound);
		}

	}
}
