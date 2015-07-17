using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Mapping
{
	[TestFixture]
	public class MapFromAttributeTests : IntegrationTests
	{
		class MapFromAttributeObject
		{
			public string Name { get; set; }
			public int Number { get; set; }
			[ElasticProperty(Type = FieldType.Nested, IncludeInParent = true)]
			public List<NestedObject> NestedObjects { get; set; }
			[ElasticProperty(Type = FieldType.Nested)]
			public List<NestedObject> NestedObjectsDontIncludeInParent { get; set; }
		}

		class NestedObject
		{
			public string Name { get; set; }
		}

		[Test]
		public void InlcudeInParent()
		{
			var indicesResponse = this.Client.Map<MapFromAttributeObject>(m => m.MapFromAttributes());

			indicesResponse.IsValid.Should().BeTrue();

			var typeMapping = this.Client.GetMapping<MapFromAttributeObject>(i => i.Type("mapfromattributeobject"));
			typeMapping.Should().NotBeNull();

			typeMapping.Mapping.Properties["nestedObjects"].Type.Name.Should().Be("nested");
			typeMapping.Mapping.Properties["nestedObjectsDontIncludeInParent"].Type.Name.Should().Be("nested");
			var nestedObject = typeMapping.Mapping.Properties["nestedObjects"] as NestedObjectMapping;
			var nestedObjectDontincludeInParent = typeMapping.Mapping.Properties["nestedObjectsDontIncludeInParent"] as NestedObjectMapping;
			nestedObject.Should().NotBeNull();
			nestedObjectDontincludeInParent.Should().NotBeNull();
			nestedObject.IncludeInParent.Should().BeTrue();
			nestedObjectDontincludeInParent.IncludeInParent.Should().NotHaveValue();
		}

		[Test]
		public void NumberNotAnalyzedShouldBeNotAnalyzed()
		{
			var index = ElasticsearchConfiguration.NewUniqueIndexName();
			var indicesResponse = this.Client.CreateIndex(c=>c
				.Index(index)
				.AddMapping<MapFromAttributeObject>(m=>m
					.MapFromAttributes()
					.Properties(prop=>prop
						.Number(n=>n.Name(p=>p.Number).Index(NonStringIndexOption.NotAnalyzed))
					)
				)
			
			);

			indicesResponse.IsValid.Should().BeTrue();

			var typeMapping = this.Client.GetMapping<MapFromAttributeObject>(i => i.Index(index));
			typeMapping.Should().NotBeNull();

			var numberMapping = typeMapping.Mapping.Properties["number"] as NumberMapping;
			numberMapping.Should().NotBeNull();
			//index == null because not analyzed is default
			numberMapping.Index.Should().NotBe(NonStringIndexOption.Analyzed).And.BeNull();
		}

	}
}
