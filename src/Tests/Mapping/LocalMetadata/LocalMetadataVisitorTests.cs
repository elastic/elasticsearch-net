using System;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Mapping.Types.Core.Text;
using Xunit;
using Tests.Framework;
using Tests.Framework.MockData;
using Tests.Mapping.LocalMetadata.Extensions;

namespace Tests.Mapping.LocalMetadata
{
	public class LocalMetadataVisitorTests
	{
		[U]
		public void CanAssignAndAccessLocalMetadataInitializer()
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

		[U]
		public void CanAssignAndAccessLocalMetadataFluent()
		{
			var descriptor = new TypeMappingDescriptor<TextTest>().Properties(p => p
				.Text(t => t
				.Name(o => o.Full)
				.Norms()
				.LocalMetadata(m => m
					.Add("Test", "TestValue")
				)
			)) as ITypeMapping;

			var visitor = new LocalMatadataVisitor();
			var walker = new MappingWalker(visitor);
			walker.Accept(descriptor.Properties);

			visitor.MetadataCount.Should().Be(1);
		}
	}
}

namespace Tests.Mapping.LocalMetadata.Extensions
{
	public static class TestLocalMetadataMappingExtensions
	{
		public static TDescriptor AddTestLocalMetadata<TDescriptor>(this TDescriptor descriptor)
				where TDescriptor : IDescriptor
		{
			var propertyDescriptor = descriptor as IProperty;

			if (propertyDescriptor == null)
				return descriptor;

			if (propertyDescriptor.LocalMetadata == null)
				propertyDescriptor.LocalMetadata = new Dictionary<string, object>();

			propertyDescriptor.LocalMetadata.Add("Test", "TestValue");

			return descriptor;
		}
	}

	public class LocalMatadataVisitor : NoopMappingVisitor
	{
		public int MetadataCount { get; set; }

		public override void Visit(ITextProperty property)
		{
			property.Should().NotBeNull();
			property.LocalMetadata.Should().NotBeNull();

			MetadataCount += property.LocalMetadata.Count;

			property.LocalMetadata.Should().Contain("Test", "TestValue");
		}
	}
}
