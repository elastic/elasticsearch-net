/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
