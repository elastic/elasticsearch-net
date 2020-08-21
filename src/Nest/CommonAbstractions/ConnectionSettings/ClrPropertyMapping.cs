// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;

namespace Nest
{
	public abstract class ClrPropertyMappingBase<TDocument> : IClrPropertyMapping<TDocument>
		where TDocument : class
	{
		protected ClrPropertyMappingBase(Expression<Func<TDocument, object>> property) => Self.Property = property;

		protected IClrPropertyMapping<TDocument> Self => this;
		bool IClrPropertyMapping<TDocument>.Ignore { get; set; }
		string IClrPropertyMapping<TDocument>.NewName { get; set; }
		Expression<Func<TDocument, object>> IClrPropertyMapping<TDocument>.Property { get; set; }

		IPropertyMapping IClrPropertyMapping<TDocument>.ToPropertyMapping() => Self.Ignore
			? PropertyMapping.Ignored
			: new PropertyMapping { Name = Self.NewName };
	}

	public interface IClrPropertyMapping<TDocument> where TDocument : class
	{
		bool Ignore { get; set; }
		string NewName { get; set; }
		Expression<Func<TDocument, object>> Property { get; set; }

		IPropertyMapping ToPropertyMapping();
	}

	public class IgnoreClrPropertyMapping<TDocument> : ClrPropertyMappingBase<TDocument> where TDocument : class
	{
		public IgnoreClrPropertyMapping(Expression<Func<TDocument, object>> property) : base(property) => Self.Ignore = true;
	}

	public class RenameClrPropertyMapping<TDocument> : ClrPropertyMappingBase<TDocument> where TDocument : class
	{
		public RenameClrPropertyMapping(Expression<Func<TDocument, object>> property, string newName) : base(property)
		{
			newName.ThrowIfNull(nameof(newName));
			Self.NewName = newName;
		}
	}
}
