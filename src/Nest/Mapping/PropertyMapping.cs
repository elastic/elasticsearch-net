// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Nest
{
	public class PropertyMappingDescriptor<TDocument> : DescriptorBase<PropertyMappingDescriptor<TDocument>, IDescriptor>
		where TDocument : class
	{
		internal IList<IClrPropertyMapping<TDocument>> Mappings { get; } = new List<IClrPropertyMapping<TDocument>>();

		public PropertyMappingDescriptor<TDocument> PropertyName(Expression<Func<TDocument, object>> property, string field)
		{
			property.ThrowIfNull(nameof(property));
			field.ThrowIfNullOrEmpty(nameof(field));
			Mappings.Add(new RenameClrPropertyMapping<TDocument>(property, field));
			return this;
		}

		public PropertyMappingDescriptor<TDocument> Ignore(Expression<Func<TDocument, object>> property)
		{
			property.ThrowIfNull(nameof(property));
			Mappings.Add(new IgnoreClrPropertyMapping<TDocument>(property));
			return this;
		}
	}
}
