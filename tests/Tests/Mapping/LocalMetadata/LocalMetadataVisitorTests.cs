// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Mapping.Types.Core.Text;

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

			var visitor = new LocalMetadataVisitor();
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

			var visitor = new LocalMetadataVisitor();
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
			var propertyDescriptor = descriptor as IProperty;

			if (propertyDescriptor == null)
				return descriptor;

			if (propertyDescriptor.LocalMetadata == null)
				propertyDescriptor.LocalMetadata = new Dictionary<string, object>();

			propertyDescriptor.LocalMetadata.Add("Test", "TestValue");

			return descriptor;
		}
	}

	public class LocalMetadataVisitor : NoopMappingVisitor
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
