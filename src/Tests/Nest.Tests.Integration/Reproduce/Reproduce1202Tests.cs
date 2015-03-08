using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Reproduce
{
	[TestFixture]
	public class Reproduce1202Tests : IntegrationTests
	{
		[ElasticType(Name = "parent")]
		public class Round1Parent
		{
			[ElasticProperty(Name = "alternate_urls")]
			public List<Round1Child> AlternateUrls { get; set; }
		}

		public class Round1Child
		{
			[ElasticProperty(Index = FieldIndexOption.NotAnalyzed, Type = FieldType.String, Name = "url")]
			public string Url { get; set; }
		}

		[ElasticType(Name = "parent")]
		public class Round2Parent
		{
			[ElasticProperty(Name = "alternate_urls")]
			public List<Round2Child> AlternateUrls { get; set; }
		}

		public class Round2Child
		{
			[ElasticProperty(Index = FieldIndexOption.NotAnalyzed, Type = FieldType.String, Name = "url")]
			public string Url { get; set; }

			[ElasticProperty(Index = FieldIndexOption.NotAnalyzed, Type = FieldType.String, Name = "type")]
			public string Type { get; set; }
		}

		[Test]
		public void UpdateMappingWithNewFieldsShouldApplyIndexOption()
		{
			var index = ElasticsearchConfiguration.NewUniqueIndexName();
			var createResult = this.Client.CreateIndex(i => i
				.Index(index)
				.AddMapping<Round1Parent>(m => m.MapFromAttributes())
			);
			createResult.IsValid.Should().BeTrue();

			var getMapping = this.Client.GetMapping<Round1Parent>(m => m.Index(index));
			getMapping.Mapping.Name.Should().Be("parent");
			var urlMapping = getMapping.Mapping.Properties["alternate_urls"] as ObjectMapping;
			urlMapping.Should().NotBeNull();

			var urlStringMapping = urlMapping.Properties["url"] as StringMapping;
			urlStringMapping.Should().NotBeNull();

			urlStringMapping.Index.Should().Be(FieldIndexOption.NotAnalyzed);

			urlMapping.Properties.Should().NotContainKey("type");

			var updateMapping = this.Client.Map<Round2Parent>(m => m
				.Index(index)
				.MapFromAttributes()
			);
			updateMapping.IsValid.Should().BeTrue();

			getMapping = this.Client.GetMapping<Round1Parent>(m => m.Index(index));
			getMapping.Mapping.Name.Should().Be("parent");
			urlMapping = getMapping.Mapping.Properties["alternate_urls"] as ObjectMapping;
			urlMapping.Should().NotBeNull();

			urlStringMapping = urlMapping.Properties["url"] as StringMapping;
			urlStringMapping.Should().NotBeNull();
			urlStringMapping.Index.Should().Be(FieldIndexOption.NotAnalyzed);

			var urlTypeMapping = urlMapping.Properties["type"] as StringMapping;
			urlTypeMapping .Should().NotBeNull();
			urlTypeMapping .Index.Should().Be(FieldIndexOption.NotAnalyzed);


		}
	}
}
