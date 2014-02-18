using System;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Mapping
{
	[TestFixture]
	public class MapTests : IntegrationTests
	{
		private void TestMapping(RootObjectMapping typeMapping)
		{
			Assert.NotNull(typeMapping);
			Assert.AreEqual("string", typeMapping.Properties["content"].Type.Name);
			Assert.AreEqual("string", typeMapping.Properties["country"].Type.Name);
			Assert.AreEqual("double", typeMapping.Properties["doubleValue"].Type.Name);
			Assert.AreEqual("long", typeMapping.Properties["longValue"].Type.Name);
			Assert.AreEqual("boolean", typeMapping.Properties["boolValue"].Type.Name);
			Assert.AreEqual("integer", typeMapping.Properties["intValues"].Type.Name);
			Assert.AreEqual("float", typeMapping.Properties["floatValues"].Type.Name);
			Assert.AreEqual("multi_field", typeMapping.Properties["name"].Type.Name);
			Assert.AreEqual("date", typeMapping.Properties["startedOn"].Type.Name);
			Assert.AreEqual("long", typeMapping.Properties["stupidIntIWantAsLong"].Type.Name);
			Assert.AreEqual("float", typeMapping.Properties["floatValue"].Type.Name);
			Assert.AreEqual("integer", typeMapping.Properties["id"].Type.Name);
			Assert.AreEqual("multi_field", typeMapping.Properties["loc"].Type.Name);
			var mapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(mapping);
			Assert.AreEqual(FieldIndexOption.not_analyzed, mapping.Index);
			//Assert.AreEqual("elasticsearchprojects", typeMapping.Parent.Type);

			Assert.AreEqual("geo_point", typeMapping.Properties["origin"].Type.Name);

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
			var index = ElasticsearchConfiguration.NewUniqueIndexName();
			var x = this._client.CreateIndex(index, s => s
				.AddMapping<ElasticsearchProject>(m => m.MapFromAttributes())
			);
			Assert.IsTrue(x.Acknowledged, x.ConnectionStatus.ToString());

			var typeMapping = this._client.GetMapping(i => i.Index(index).Type("elasticsearchprojects"));
			typeMapping.Should().NotBeNull();
			TestMapping(typeMapping.Mapping);
		}




		[Test]
		public void SimpleMapByAttributesUsingType()
		{
			var index = ElasticsearchConfiguration.NewUniqueIndexName();
			var x = this._client.CreateIndex(index, s => s
				.AddMapping<ElasticsearchProject>(a=>a.MapFromAttributes())
			);
			Assert.IsTrue(x.Acknowledged, x.ConnectionStatus.ToString());
			var xx = this._client.Map<object>(m=>m.Type(typeof(ElasticsearchProject)).Index(index));
			Assert.IsTrue(xx.OK);

			var typeMapping = this._client.GetMapping(i => i.Index(index).Type("elasticsearchprojects"));
			typeMapping.Should().NotBeNull();
			TestMapping(typeMapping.Mapping);
		}

		[Test]
		public void GetMapping()
		{
			var typeMapping = this._client.GetMapping(i => i.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects"));
			typeMapping.Should().NotBeNull();
			TestMapping(typeMapping.Mapping);
		}

		[Test]
		public void GetMappingOnNonExistingIndexType()
		{
			Assert.DoesNotThrow(() =>
			{
				var typeMapping = this._client.GetMapping(i=>i.Index("asfasfasfasfasf").Type("asdasdasdasdasdasdasdasd"));
				typeMapping.IsValid.Should().BeFalse();
				Assert.Null(typeMapping.Mapping);
			});

		}

		[Test]
		public void DynamicMap()
		{
			var index = ElasticsearchConfiguration.NewUniqueIndexName();
			var x = this._client.CreateIndex(index, s => s);
			Assert.IsTrue(x.Acknowledged, x.ConnectionStatus.ToString());
			var typeMappingResult = this._client.GetMapping(gm=>gm.Index(ElasticsearchConfiguration.DefaultIndex).Type("elasticsearchprojects"));
			typeMappingResult.IsValid.Should().BeTrue();
			var typeMapping = typeMappingResult.Mapping;
			var mapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(mapping);
			mapping.Boost = 3;
			this._client.Map<object>(m=>m.InitializeUsing(typeMapping).Index(index).Type("elasticsearchprojects2"));

			typeMapping = this._client.GetMapping(gm=>gm.Index(index).Type("elasticsearchprojects2")).Mapping;
			var countryMapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(countryMapping);
			Assert.AreEqual(3, countryMapping.Boost);
		}

		[Test]
		public void GetMissingMap()
		{
			Assert.DoesNotThrow(() => {
				var typeMapping = this._client.GetMapping(gm => gm.Index("asdasdasdsada").Type("elasticsearchprojects2")).Mapping;
			});
		}

	}
}
