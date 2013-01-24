using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Mapping
{
	[TestFixture]
	public class TemplateTests : IntegrationTests
	{

		[Test]
		public void SimplePutAndGet()
		{
			var templateMapping = new TemplateMapping { Template = "donotinfluencothertests-*" };
			var deleteResponse = this._client.DeleteTemplate("simple-put-and-get");

			var putResponse = this._client.PutTemplate("simple-put-and-get", templateMapping);
			Assert.IsTrue(putResponse.OK);

			var templateResponse = this._client.GetTemplate("simple-put-and-get");

			Assert.AreEqual("donotinfluencothertests-*", templateResponse.TemplateMapping.Template);
		}


		[Test]
		public void PutTemplateWithSettings()
		{
			var templateMapping = new TemplateMapping { Template = "donotinfluencothertests-*", Settings = new TemplateIndexSettings { NumberOfShards = 3, NumberOfReplicas = 2 } };

			this._client.DeleteTemplate("put-template-with-settings");
			var putResponse = this._client.PutTemplate("put-template-with-settings", templateMapping);
			Assert.IsTrue(putResponse.OK);

			var templateResponse = this._client.GetTemplate("put-template-with-settings");
			templateResponse.Should().NotBeNull();
			templateResponse.IsValid.Should().BeTrue();
			templateResponse.TemplateMapping.Should().NotBeNull();
			templateResponse.TemplateMapping.Mappings.Should().NotBeNull();

			var settings = templateResponse.TemplateMapping.Settings;
			settings.Should().NotBeNull();

			Assert.AreEqual(3, settings.NumberOfShards);
			Assert.AreEqual(2, settings.NumberOfReplicas);
		}

		[Test]
		public void PutTemplateWithMappings()
		{
			var dict = new FluentDictionary<string, RootObjectMapping>()
				.Add("mytype", new RootObjectMapping { AllFieldMapping = new AllFieldMapping { Enabled = false } });
			var templateMapping = new TemplateMapping { Template = "donotinfluencothertests-*", Mappings = dict };

			this._client.DeleteTemplate("put-template-with-mappings");
			var putResponse = this._client.PutTemplate("put-template-with-mappings", templateMapping);
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
	}
}
