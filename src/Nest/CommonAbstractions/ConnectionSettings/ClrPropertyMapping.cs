using System;
using System.Linq.Expressions;

namespace Nest
{
	public abstract class ClrPropertyMappingBase<TDocument> : IClrPropertyMapping<TDocument>
		where TDocument : class
	{
		Expression<Func<TDocument, object>> IClrPropertyMapping<TDocument>.Property { get; set; }
		bool IClrPropertyMapping<TDocument>.Ignore { get; set; }
		string IClrPropertyMapping<TDocument>.NewName { get; set; }

		protected IClrPropertyMapping<TDocument> Self => this;

		protected ClrPropertyMappingBase(Expression<Func<TDocument, object>> property) => Self.Property = property;

		IPropertyMapping IClrPropertyMapping<TDocument>.ToPropertyMapping() => Self.Ignore
			? PropertyMapping.Ignored
			: new PropertyMapping {Name = Self.NewName};
	}

	public interface IClrPropertyMapping<TDocument> where TDocument : class
	{
		Expression<Func<TDocument, object>> Property { get; set; }
		bool Ignore { get; set; }
		string NewName { get; set; }
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
