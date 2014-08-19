using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest.Resolvers;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core.Get
{
	[TestFixture]
	public class GetFieldMappingTests : IntegrationTests
	{
		[Test]
		public void ReturnsErrorOnInvalidIndex()
		{
			var getFieldMappingResponse = this.Client.GetFieldMapping<ElasticsearchProject>(m => m
				.Index("this-index-does-not-exists")
				.Fields(Property.Path<ElasticsearchProject>(p=>p.LOC), "*a*")
			);

			getFieldMappingResponse.IsValid.Should().BeFalse();
			getFieldMappingResponse.ServerError.Status.Should().Be(404);
			getFieldMappingResponse.ServerError.ExceptionType.Should().Be("IndexMissingException");
		}
		
		[Test]
		public void GetFieldMapping_ReturnsProperties()
		{
			var indexName = this.Settings.Inferrer.IndexName<ElasticsearchProject>();
			var typeName = this.Settings.Inferrer.TypeName<ElasticsearchProject>();

			var getFieldMappingResponse = this.Client.GetFieldMapping<ElasticsearchProject>(m => m
				.Fields(Property.Path<ElasticsearchProject>(p=>p.LOC), "*a*")
			);
			getFieldMappingResponse.IsValid.Should().BeTrue();
			getFieldMappingResponse.Indices .Should().NotBeEmpty()
				.And.HaveCount(1).And.ContainKey(indexName);

			var indexMappings = getFieldMappingResponse.Indices[indexName];
			indexMappings.Should().NotBeNull();
			indexMappings.Mappings.Should().NotBeEmpty()
				.And.HaveCount(1).And.ContainKey(typeName);

			var typeMappings = indexMappings.Mappings[typeName];
			typeMappings.Should().NotBeEmpty().And.ContainKey("name");

			var nameFieldMapping = typeMappings["name"];
			nameFieldMapping.Should().NotBeNull();
			nameFieldMapping.Mapping.Should().NotBeEmpty().And.ContainKey("name");

			var nameMapping = nameFieldMapping.Mapping["name"] as MultiFieldMapping;
			nameMapping.Should().NotBeNull();
			nameMapping.Fields.Should().HaveCount(1);
			var sortField = nameMapping.Fields["sort"] as StringMapping;
			sortField.Should().NotBeNull();
			sortField.Index.Should().Be(FieldIndexOption.NotAnalyzed);
		}

		[Test]
		public void GetFieldMapping_Field_HelperSyntax()
		{
			var request = new GetFieldMappingRequest<ElasticsearchProject>(
				"name.sort", Property.Path<ElasticsearchProject>(p=>p.Name)
			);
			var mappingResponse = this.Client.GetFieldMapping(request);

			var sortField = mappingResponse.MappingFor<ElasticsearchProject>(p => p.Name.Suffix("sort")) as StringMapping;
			sortField.Should().NotBeNull();
			sortField.Index.Should().Be(FieldIndexOption.NotAnalyzed);
		}
		
		[Test]
		public void GetFieldMapping_Fields_HelperSyntax()
		{
			var request = new GetFieldMappingRequest<ElasticsearchProject>(
				"name.sort", Property.Path<ElasticsearchProject>(p=>p.Name)
			);
			var mappingResponse = this.Client.GetFieldMapping(request);

			var mappings = mappingResponse.MappingsFor<ElasticsearchProject>();
			mappings.Should().NotBeEmpty();

			var sortFieldMapping = mappings["name.sort"];
			sortFieldMapping.Should().NotBeNull();
			sortFieldMapping.Mapping.Should().NotBeEmpty();

			var sortField = sortFieldMapping.Mapping["sort"] as StringMapping;
			sortField.Should().NotBeNull();
			sortField.Index.Should().Be(FieldIndexOption.NotAnalyzed);
		}
		
		private class SpecialDto
		{
			public string Id { get; set; }
			public string Name { get; set; }
			public double Boost { get; set; }
			public string Analyzer { get; set; }
			public long Timestamp { get; set; }
		}


		[Test]
		public void SpecialFields_AreInspectable()
		{
			var map = this.Client.Map<SpecialDto>(m => m
				.SetParent<Person>() //makes no sense but i needed a type :)
				.AllField(a => a
					.Enabled(false)
					.IndexAnalyzer("default")
					.SearchAnalyzer("default")
					.StoreTermVectorPositions()
				)
				.IndexField(i=>i.Enabled(true).Store(true))
				.SizeField(i=>i.Enabled(false).Store(false))
				.IdField(i => i
					.Index("not_analyzed")
					.Path("myOtherId")
					.Store(false)
				)
				.SourceField(s => s
					.Enabled(false)
					.Compress()
					.CompressionThreshold("200b")
					.Excludes(new[] { "path1.*" })
					.Includes(new[] { "path2.*" })
				)
				.TypeField(t => t
					.Index(NonStringIndexOption.No)
					.Store()
				)
				.AnalyzerField(a => a
					.Path(p => p.Name)
					.Index()
				)
				.BoostField(b => b
					.Name(p => p.Boost)
					.NullValue(1.0)
				)
				.RoutingField(r => r
					.Path(p => p.Name)
					.Required()
				)
				.TimestampField(t => t
					.Enabled()
					.Path(p => p.Timestamp)
					.Format("yyyy")
				)
				.TtlField(t => t
					.Enable()
					.Default("1d")
				)
			);


			var request = new GetFieldMappingRequest<SpecialDto>("_*", "_analyzer");
			var fieldMappingResponse = this.Client.GetFieldMapping(request);

			var parentField = fieldMappingResponse.MappingFor<SpecialDto>("_parent") as ParentFieldMapping;
			parentField.Should().NotBeNull();
			parentField.Type.Name.Should().Be("person");

			var allField = fieldMappingResponse.MappingFor<SpecialDto>("_all") as AllFieldMapping;
			allField.Should().NotBeNull();
			allField.Enabled.Should().Be(false);
			allField.Analyzer.Should().Be("default");
			allField.StoreTermVectorPositions.Should().Be(true);

			var indexField = fieldMappingResponse.MappingFor<SpecialDto>("_index") as IndexFieldMapping;
			indexField.Should().NotBeNull();
			indexField.Enabled.Should().BeTrue();
			indexField.Store.Should().BeTrue();

			var typeField = fieldMappingResponse.MappingFor<SpecialDto>("_type") as TypeFieldMapping;
			typeField.Should().NotBeNull();
			typeField.Index.Should().Be(NonStringIndexOption.No);
			typeField.Store.Should().BeTrue();

			var routingField = fieldMappingResponse.MappingFor<SpecialDto>("_routing") as RoutingFieldMapping;
			routingField.Should().NotBeNull();
			routingField.Required.Should().BeTrue();
			routingField.Path.Should().Be("name");

			var timestampField = fieldMappingResponse.MappingFor<SpecialDto>("_timestamp") as TimestampFieldMapping;
			timestampField.Should().NotBeNull();
			timestampField.Format.Should().Be("yyyy");
			timestampField.Enabled.Should().BeTrue();
			timestampField.Path.Should().Be("timestamp");

			var ttlField = fieldMappingResponse.MappingFor<SpecialDto>("_ttl") as TtlFieldMapping;
			ttlField.Should().NotBeNull();
			ttlField.Enabled.Should().BeTrue();
			ttlField.Default.Should().Be("86400000");

			var sourceField = fieldMappingResponse.MappingFor<SpecialDto>("_source") as SourceFieldMapping;
			sourceField.Should().NotBeNull();
			sourceField.Enabled.Should().BeFalse();
			sourceField.Compress.Should().BeTrue();
			sourceField.CompressThreshold.Should().Be("200b");
			sourceField.Includes.Should().BeEquivalentTo(new [] {"path2.*"});
			sourceField.Excludes.Should().BeEquivalentTo(new [] {"path1.*"});


			var analyzerField = fieldMappingResponse.MappingFor<SpecialDto>("_analyzer") as AnalyzerFieldMapping;
			analyzerField.Should().BeNull("If this tests fails it means https://github.com/elasticsearch/elasticsearch/issues/7237 is fixed!");
			var boostField = fieldMappingResponse.MappingFor<SpecialDto>("_boost") as BoostFieldMapping;
			boostField.Should().BeNull("If this tests fails it means https://github.com/elasticsearch/elasticsearch/issues/7237 is fixed!");
			
		}
	}
}
