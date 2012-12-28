using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Mapping
{
	[TestFixture]
	public class MapTests : BaseElasticSearchTests
	{
		private void TestMapping(MapRootObject typeMapping)
		{
			Assert.NotNull(typeMapping);
			Assert.AreEqual("string", typeMapping.Properties["content"].Type);
			Assert.AreEqual("string", typeMapping.Properties["country"].Type);
			Assert.AreEqual("double", typeMapping.Properties["doubleValue"].Type);
			Assert.AreEqual("long", typeMapping.Properties["longValue"].Type);
			Assert.AreEqual("boolean", typeMapping.Properties["boolValue"].Type);
			Assert.AreEqual("integer", typeMapping.Properties["intValues"].Type);
			Assert.AreEqual("float", typeMapping.Properties["floatValues"].Type);
			Assert.AreEqual("multi_field", typeMapping.Properties["name"].Type);
			Assert.AreEqual("date", typeMapping.Properties["startedOn"].Type);
			Assert.AreEqual("long", typeMapping.Properties["stupidIntIWantAsLong"].Type);
			Assert.AreEqual("float", typeMapping.Properties["floatValue"].Type);
			Assert.AreEqual("integer", typeMapping.Properties["id"].Type);
			Assert.AreEqual("multi_field", typeMapping.Properties["loc"].Type);
			var mapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(mapping);
			Assert.AreEqual("not_analyzed", mapping.Index);
			//Assert.AreEqual("elasticsearchprojects", typeMapping.Parent.Type);

			Assert.AreEqual("geo_point", typeMapping.Properties["origin"].Type);

			//Assert.IsTrue(typeMapping.Properties["origin"].Dynamic);
			//Assert.AreEqual("double", typeMapping.Properties["origin"].Properties["lat"].Type);
			//Assert.AreEqual("double", typeMapping.Properties["origin"].Properties["lon"].Type);

			//Assert.IsTrue(typeMapping.Properties["followers"].Dynamic);
			//Assert.AreEqual("long", typeMapping.Properties["followers"].Properties["age"].Type);
			//Assert.AreEqual("date", typeMapping.Properties["followers"].Properties["dateOfBirth"].Type);
			//Assert.AreEqual("string", typeMapping.Properties["followers"].Properties["email"].Type);
			//Assert.AreEqual("string", typeMapping.Properties["followers"].Properties["firstName"].Type);
			//Assert.AreEqual("long", typeMapping.Properties["followers"].Properties["id"].Type);
			//Assert.AreEqual("string", typeMapping.Properties["followers"].Properties["lastName"].Type);

			//Assert.IsTrue(typeMapping.Properties["followers"].Properties["placeOfBirth"].Dynamic);
			//Assert.AreEqual("double", typeMapping.Properties["followers"].Properties["placeOfBirth"].Properties["lat"].Type);
			//Assert.AreEqual("double", typeMapping.Properties["followers"].Properties["placeOfBirth"].Properties["lon"].Type);
		}

		[Test]
		public void SimpleMapByAttributes()
		{
			this.ConnectedClient.DeleteMapping<ElasticSearchProject>();
			this.ConnectedClient.DeleteMapping<ElasticSearchProject>(Test.Default.DefaultIndex + "_clone");
			var x = this.ConnectedClient.MapFromAttributes<ElasticSearchProject>();
			Assert.IsTrue(x.OK);

			var typeMapping = this.ConnectedClient.GetMapping(Test.Default.DefaultIndex, "elasticsearchprojects");
			TestMapping(typeMapping);
		}




		[Test]
		public void SimpleMapByAttributesUsingType()
		{
			var t = typeof(ElasticSearchProject);
			this.ConnectedClient.DeleteMapping(t);
			this.ConnectedClient.DeleteMapping(t, Test.Default.DefaultIndex + "_clone");
			var x = this.ConnectedClient.MapFromAttributes(t);
			Assert.IsTrue(x.OK);

			var typeMapping = this.ConnectedClient.GetMapping(Test.Default.DefaultIndex, "elasticsearchprojects");
			TestMapping(typeMapping);
		}


		public void FluentMapping()
		{
			//TODO: Waiting to pull in nordbergm's excellent work on mapping 
			/*var map = Map<ElasticSearchProject>
				.Type(new ElasticType() 
				{
					
				}).AddField(p=>p.Content, Field.Analyzer("").)*/
		}

		[Test]
		public void GetMapping()
		{
			var typeMapping = this.ConnectedClient.GetMapping(Test.Default.DefaultIndex + "_clone", "elasticsearchprojects");
			this.TestMapping(typeMapping);
		}

	[Test]
	public void GetMappingOnNonExistingIndexType()
	{
	  Assert.DoesNotThrow(() =>
	  {
		var typeMapping = this.ConnectedClient.GetMapping("asfasfasfasfasf", "asdasdasdasdasdasdasdasd");
		Assert.Null(typeMapping);
	  });
	  
	}

		[Test]
		public void DynamicMap()
		{
			var typeMapping = this.ConnectedClient.GetMapping(Test.Default.DefaultIndex + "_clone", "elasticsearchprojects");
			var mapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(mapping);
			mapping.Boost = 3;
			typeMapping.Name = "elasticsearchprojects2";
			this.ConnectedClient.Map(typeMapping, Test.Default.DefaultIndex + "_clone");

			typeMapping = this.ConnectedClient.GetMapping(Test.Default.DefaultIndex + "_clone",
												  "elasticsearchprojects2");
			var countryMapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(countryMapping);
			Assert.AreEqual(3, countryMapping.Boost);
		}
	}
}
