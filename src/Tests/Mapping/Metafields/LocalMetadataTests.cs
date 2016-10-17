using System;
using System.Collections.Generic;
using FluentAssertions;
using Nest;
using Tests.Mapping.Types.Core.Text;
using Xunit;

namespace Tests.Mapping.Metafields
{
	public class LocalMetadataTests
	{
		[Fact]
		public void CanAssignAndAccessLocalMetadata()
		{
			var descriptor = new TypeMappingDescriptor<TextTest>().Properties(p => p
				.Text(t => t
					.Name(o => o.Full)
					.Norms()
					.AddTestLocalMetadata()
				)) as ITypeMapping;

			var visitor = new LocalMatadataVisitor();
			var walker = new MappingWalker(visitor);
			walker.Accept(descriptor.Properties);

			visitor.MetadataCount.Should().Be(1);
		}
	}

	public static class TestLocalMetadataMappingExtensions
	{
		public static TDescriptor AddTestLocalMetadata<TDescriptor>(this TDescriptor descriptor)
				where TDescriptor : IDescriptor
		{
			var descriptorWithLocalMetadata = descriptor as IPropertyWithLocalMetadata;

			if (descriptorWithLocalMetadata == null)
				return descriptor;

			if (descriptorWithLocalMetadata.LocalMetadata == null)
				descriptorWithLocalMetadata.LocalMetadata = new Dictionary<string, object>();

			descriptorWithLocalMetadata.LocalMetadata.Add("Test", "TestValue");

			return descriptor;
		}
	}

	public class LocalMatadataVisitor : NoopMappingVisitor
	{
		public int MetadataCount { get; set; }

		public override void Visit(ITextProperty property)
		{
			var propertyWithLocalMetadata = property as IPropertyWithLocalMetadata;
			propertyWithLocalMetadata.Should().NotBeNull();
			propertyWithLocalMetadata.LocalMetadata.Should().NotBeNull();

			MetadataCount += propertyWithLocalMetadata.LocalMetadata.Count;

			propertyWithLocalMetadata.LocalMetadata.Should().Contain("Test", "TestValue");
		}
	}
}
