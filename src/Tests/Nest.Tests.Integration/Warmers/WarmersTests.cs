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
			var putResponse = this._client.PutWarmer("warmer_simpleputandget", wd => wd
				.Type<ElasticsearchProject>()
				.Search<ElasticsearchProject>(s => s
					.Query(q => q
						.Terms(p => p.Name, new [] {"strange-value"})
					)
				//.Filter(filter => filter)  // this is why there is a search descriptor
				)
			);
			Assert.IsTrue(putResponse.Acknowledged);

			var warmerResponse = this._client.GetWarmer("warmer_simpleputandget", wd => wd
				.Index<ElasticsearchProject>()
			   );

			warmerResponse.Should().NotBeNull();
			warmerResponse.IsValid.Should().BeTrue();
			warmerResponse.Indices.Should().NotBeNull();
			warmerResponse.Indices.Should().ContainKey(_settings.DefaultIndex);
			warmerResponse.Indices[_settings.DefaultIndex].Should().ContainKey("warmer_simpleputandget");
			var warmerMapping = warmerResponse.Indices[_settings.DefaultIndex]["warmer_simpleputandget"];
			warmerMapping.Name.Should().Be("warmer_simpleputandget");
			var typeName = _client.Infer.TypeName<ElasticsearchProject>();
			var typeNames = new ElasticInferrer(_settings).TypeNames(warmerMapping.Types);
			typeNames.Contains(typeName).Should().Be(true);
			//warmerMapping.Source.Should().Contain("\"strange-value\"");
		}

		[Test]
		public void PutWithEmptyTypes()
		{
			//this._client.DeleTemplate("put-template-with-settings");
			var putResponse = this._client.PutWarmer("warmer_putwithemptytypes", wd => wd
				.Index<ElasticsearchProject>()
				.Search<ElasticsearchProject>(s => s
					.Query(q => q
						.Terms(p => p.Name, new [] {"strange-value"})
					)
				//.Filter(filter => filter)  // this is why there is a search descriptor
				)
			);
			Assert.IsTrue(putResponse.Acknowledged);

			var warmerResponse = this._client.GetWarmer("warmer_putwithemptytypes", wd => wd
				.Index<ElasticsearchProject>()
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
				.Search<ElasticsearchProject>(s => s
					.Query(q => q
						.Terms(p => p.Name, new [] {"strange-value"})
					)
				)
			);
			Assert.IsTrue(putResponse.Acknowledged);

			var warmerResponse = this._client.GetWarmer("warmer_puttodefaultindex", wd => wd
				.Index<ElasticsearchProject>()
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
				.Search<ElasticsearchProject>(s => s
					.Query(q => q
						.Terms(p => p.Name, new [] {"strange-value"})
					)
				)
			);
			Assert.IsTrue(putResponse.Acknowledged);

			var deleteResponse = this._client.DeleteWarmer("warmer_delete", wd => wd
				.Index<ElasticsearchProject>()
			);
			Assert.IsTrue(deleteResponse.Acknowledged);
			
			this._client.Refresh(r => r.Index(ElasticsearchConfiguration.DefaultIndex));
			
			var warmerResponse = this._client.GetWarmer("warmer_delete", wd => wd
				.Index<ElasticsearchProject>()
			);

			var warmerResponse2 = this._client.GetWarmer("warmer_deleteasdkajsdkjahsdkahsdas", wd => wd
				.Index<ElasticsearchProject>()
			);
			warmerResponse.Should().NotBeNull();
			//TODO remove when this bug is fixed in elasticsearch
			Assert.Pass("1.0 GA has a bug that does not return a 404 for missing warmers see #5155");

			warmerResponse.IsValid.Should().BeFalse();
			warmerResponse.ConnectionStatus.HttpStatusCode.Should().Be(404);
		}

	}
}
