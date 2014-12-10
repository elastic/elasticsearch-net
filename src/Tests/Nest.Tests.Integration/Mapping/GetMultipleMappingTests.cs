using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Mapping
{
	[TestFixture]
	public class GetMultipleMappingTests : IntegrationTests
	{
		private void TestElasticsearchProjectMapping(RootObjectMapping typeMapping)
		{
			Assert.NotNull(typeMapping);
			Assert.AreEqual("string", typeMapping.Properties["content"].Type.Name);
			Assert.AreEqual("string", typeMapping.Properties["country"].Type.Name);
			Assert.AreEqual("double", typeMapping.Properties["doubleValue"].Type.Name);
			Assert.AreEqual("long", typeMapping.Properties["longValue"].Type.Name);
			Assert.AreEqual("boolean", typeMapping.Properties["boolValue"].Type.Name);
			Assert.AreEqual("integer", typeMapping.Properties["intValues"].Type.Name);
			Assert.AreEqual("float", typeMapping.Properties["floatValues"].Type.Name);
			Assert.AreEqual("string", typeMapping.Properties["name"].Type.Name);
			Assert.AreEqual("date", typeMapping.Properties["startedOn"].Type.Name);
			Assert.AreEqual("long", typeMapping.Properties["stupidIntIWantAsLong"].Type.Name);
			Assert.AreEqual("float", typeMapping.Properties["floatValue"].Type.Name);
			Assert.AreEqual("integer", typeMapping.Properties["id"].Type.Name);
			Assert.AreEqual("integer", typeMapping.Properties["loc"].Type.Name);
			Assert.AreEqual("geo_point", typeMapping.Properties["origin"].Type.Name);
			Assert.AreEqual("object", typeMapping.Properties["product"].Type.Name);

			var productMapping = typeMapping.Properties["product"] as ObjectMapping;
			Assert.NotNull(productMapping);
			Assert.AreEqual("string", productMapping.Properties["name"].Type.Name);
			Assert.AreEqual("string", productMapping.Properties["id"].Type.Name);

			var countryMapping = typeMapping.Properties["country"] as StringMapping;
			Assert.NotNull(countryMapping);
			Assert.AreEqual(FieldIndexOption.NotAnalyzed, countryMapping.Index);
		}

		private void TestPersonMapping(RootObjectMapping typeMapping)
		{
			Assert.NotNull(typeMapping);
			Assert.AreEqual("string", typeMapping.Properties["email"].Type.Name);
			var firstNameMapping = typeMapping.Properties["email"] as StringMapping;
			firstNameMapping.Should().NotBeNull();
			firstNameMapping.Index.Should().Be(FieldIndexOption.NotAnalyzed);
		}

		[Test]
		public void GetSameMappingFromTwoDifferentIndices()
		{
			var indices = new[]
			{
				ElasticsearchConfiguration.NewUniqueIndexName(),
				ElasticsearchConfiguration.NewUniqueIndexName()
			};

			var x = this.Client.CreateIndex(indices.First(), s => s
				.AddMapping<ElasticsearchProject>(m => m.MapFromAttributes())
			);
			Assert.IsTrue(x.Acknowledged, x.ConnectionStatus.ToString());

			x = this.Client.CreateIndex(indices.Last(), s => s
				.AddMapping<ElasticsearchProject>(m => m.MapFromAttributes())
			);
			Assert.IsTrue(x.Acknowledged, x.ConnectionStatus.ToString());

			var response = this.Client.GetMapping<ElasticsearchProject>(i => i
				.Index(string.Join(",", indices))
				.Type("elasticsearchprojects")
			);
			response.Should().NotBeNull();
			response.Mappings.Should().NotBeEmpty()
				.And.HaveCount(2);
			foreach (var indexMapping in response.Mappings)
			{
				var indexName = indexMapping.Key;
				indices.Should().Contain(indexName);
				var mappings = indexMapping.Value;
				mappings.Should().NotBeEmpty().And.HaveCount(1);
				foreach (var mapping in mappings)
				{
					mapping.TypeName.Should().Be("elasticsearchprojects");
					TestElasticsearchProjectMapping(mapping.Mapping);
				}
			}


		}

		[Test]
		public void GetDifferentMappingsFromMultipleIndices()
		{
			var indices = new[]
			{
				ElasticsearchConfiguration.NewUniqueIndexName(),
				ElasticsearchConfiguration.NewUniqueIndexName()
			};

			var x = this.Client.CreateIndex(indices.First(), s => s
				.AddMapping<ElasticsearchProject>(m => m.MapFromAttributes())
				.AddMapping<Person>(m => m.MapFromAttributes())
			);
			Assert.IsTrue(x.Acknowledged, x.ConnectionStatus.ToString());

			x = this.Client.CreateIndex(indices.Last(), s => s
				.AddMapping<ElasticsearchProject>(m => m.MapFromAttributes())
				.AddMapping<MockData.Domain.CustomGeoLocation>(m => m.MapFromAttributes())
			);
			Assert.IsTrue(x.Acknowledged, x.ConnectionStatus.ToString());

			var response = this.Client.GetMapping(new GetMappingRequest(string.Join(",", indices), "*"));
			response.Should().NotBeNull();
			response.Mappings.Should().NotBeEmpty()
				.And.HaveCount(2);
			foreach (var indexMapping in response.Mappings)
			{
				var indexName = indexMapping.Key;
				indices.Should().Contain(indexName);
				var mappings = indexMapping.Value;
				mappings.Should().NotBeEmpty().And.HaveCount(2);
				mappings.Should().Contain(m => m.TypeName == "elasticsearchprojects")
					.And.Contain(m=>m.TypeName == (indexName == indices.First() ? "person" : "geolocation"));
				foreach (var mapping in mappings)
				{
					switch (mapping.TypeName)
					{
						case "elasticsearchprojects":
							TestElasticsearchProjectMapping(mapping.Mapping);
							break;
						case "person":
							TestPersonMapping(mapping.Mapping);
							break;
						case "geolocation":
							break;
						default:
							Assert.Fail("Unexpected mapping found {0}", mapping.TypeName);
							break;
					}
				}
			}


		}

	}
}
