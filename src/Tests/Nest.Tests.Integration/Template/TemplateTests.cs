using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Template
{
	[TestFixture]
	public class TemplateTests : IntegrationTests
	{

		[Test]
		public void SimplePutAndGet()
		{
		    this._client.DeleteTemplate("put-template-with-settings");
			var putResponse = this._client.PutTemplate("put-template-with-settings", t => t
				.Template("donotinfluencothertests-*")
				.Order(42)
			);
			Assert.IsTrue(putResponse.OK);

			var templateResponse = this._client.GetTemplate("put-template-with-settings");
			templateResponse.Should().NotBeNull();
			templateResponse.IsValid.Should().BeTrue();
			templateResponse.TemplateMapping.Should().NotBeNull();
			templateResponse.TemplateMapping.Mappings.Should().NotBeNull();
			
			var settings = templateResponse.TemplateMapping.Settings;
			templateResponse.TemplateMapping.Order.Should().Be(42);

			settings.Should().NotBeNull();
		}


		[Test]
		public void PutTemplateWithSettings()
		{
			this._client.DeleteTemplate("put-template-with-settings");
			var putResponse = this._client.PutTemplate("put-template-with-settings", t=>t
				.Template("donotinfluencothertests-*")
				.Settings(s=>s
					.Add("index.number_of_shards", 3)
					.Add("index.number_of_replicas", 2)
				)
			);
			Assert.IsTrue(putResponse.OK);

			var templateResponse = this._client.GetTemplate("put-template-with-settings");
			templateResponse.Should().NotBeNull();
			templateResponse.IsValid.Should().BeTrue();
			templateResponse.TemplateMapping.Should().NotBeNull();
			templateResponse.TemplateMapping.Mappings.Should().NotBeNull();

			var settings = templateResponse.TemplateMapping.Settings;
			settings.Should().NotBeNull();

			Assert.AreEqual("3", settings["index.number_of_shards"]);
			Assert.AreEqual("2", settings["index.number_of_replicas"]);
		}

		[Test]
		public void PutTemplateWithMappings()
		{
			this._client.DeleteTemplate("put-template-with-mappings");
			var putResponse = this._client.PutTemplate("put-template-with-mappings",t => t
				.Template("donotinfluencothertests")
				.AddMapping<dynamic>(s=>s
					.Type("mytype")
					.DisableAllField()
				)
			);
			Assert.IsTrue(putResponse.OK);

			var templateResponse = this._client.GetTemplate("put-template-with-mappings");
			templateResponse.Should().NotBeNull();
			templateResponse.IsValid.Should().BeTrue();
			templateResponse.TemplateMapping.Should().NotBeNull();
			templateResponse.TemplateMapping.Mappings.Should().NotBeNull().And.NotBeEmpty();

			var mappings = templateResponse.TemplateMapping.Mappings;

			Assert.IsTrue(mappings.ContainsKey("mytype"), "put-template-with-mappings template should have a `mytype` mapping");
			Assert.NotNull(mappings["mytype"].AllFieldMapping, "`mytype` mapping should contain the _all field mapping");
			Assert.AreEqual(false, mappings["mytype"].AllFieldMapping.Enabled, "_all { enabled } should be set to false");
		}

		[Test]
		public void PutTemplateWithWarmers()
		{
			this._client.DeleteTemplate("put-template-with-warmers");
			var putResponse = this._client.PutTemplate("put-template-with-warmers", t => t
				.Template("donotinfluencothertests2")
				.AddWarmer<ElasticsearchProject>(w => w
					.WarmerName("matchall")
					.Type("elasticsearchprojects")
					.Search(s=>s
						.MatchAll()
					)
				)
			);
			Assert.IsTrue(putResponse.OK);

			var templateResponse = this._client.GetTemplate("put-template-with-warmers"); 
			templateResponse.Should().NotBeNull();
			templateResponse.IsValid.Should().BeTrue();
			templateResponse.TemplateMapping.Should().NotBeNull();
			//possible elasticsearch bug https://github.com/elasticsearch/elasticsearch/issues/2868
			//templateResponse.TemplateMapping.Warmers.Should().NotBeNull().And.NotBeEmpty();
		}
	}
}
