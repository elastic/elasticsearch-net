using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;

namespace Nest
{
	public interface IClrTypeMapping<T> where T : class
	{
		Type Type { get; } 

		/// <summary>
		/// When specified dictates the default Elasticsearch index name for <typeparamref name="T"/> 
		/// </summary>
		string IndexName { get; set; }

		/// <summary>
		/// When specified dictates the default Elasticsearch type name for <typeparamref name="T" />
		/// </summary>
		string TypeName { get; set; }

		/// <summary>
		/// Allows you to set a default Id property on <typeparamref name="T" /> that NEST will evaluate 
		/// </summary>
		Expression<Func<T, object>> IdProperty { get; set; }
		
		/// <summary>
		/// When specified allows you to ignore or rename certain properties of clr type <typeparamref name="T" />
		/// </summary>
		IList<IClrTypePropertyMapping<T>> Properties { get; set; }
	}

	public class ClrTypeMapping<T> : IClrTypeMapping<T> where T : class
	{
		public Type Type { get; } = typeof (T);

		/// <summary>
		/// When specified dictates the default Elasticsearch index name for <typeparamref name="T"/> 
		/// </summary>
		public string IndexName { get; set; }

		/// <summary>
		/// When specified dictates the default Elasticsearch type name for <typeparamref name="T" />
		/// </summary>
		public string TypeName { get; set; }

		/// <summary>
		/// Allows you to set a default Id property on <typeparamref name="T" /> that NEST will evaluate 
		/// </summary>
		public Expression<Func<T, object>> IdProperty { get; set; }

		public IList<IClrTypePropertyMapping<T>> Properties { get; set; }
	}

	public class ClrTypeMappingDescriptor<T> : DescriptorBase<ClrTypeMappingDescriptor<T>,IClrTypeMapping<T>> , IClrTypeMapping<T>
		where T : class
	{
		Type IClrTypeMapping<T>.Type { get; } = typeof (T);
		string IClrTypeMapping<T>.IndexName { get; set; }
		string IClrTypeMapping<T>.TypeName { get; set; }
		Expression<Func<T, object>> IClrTypeMapping<T>.IdProperty { get; set; }
		IList<IClrTypePropertyMapping<T>> IClrTypeMapping<T>.Properties { get; set; } = new List<IClrTypePropertyMapping<T>>();

		/// <summary>
		/// When specified dictates the default Elasticsearch index name for <typeparamref name="T"/> 
		/// </summary>
		public ClrTypeMappingDescriptor<T> IndexName(string indexName) => Assign(a => a.IndexName = indexName);

		/// <summary>
		/// When specified dictates the default Elasticsearch type name for <typeparamref name="T" />
		/// </summary>
		public ClrTypeMappingDescriptor<T> TypeName(string typeName) => Assign(a => a.TypeName = typeName);

		/// <summary>
		/// Allows you to set a default Id property on <typeparamref name="T" /> that NEST will evaluate 
		/// </summary>
		public ClrTypeMappingDescriptor<T> IdProperty(Expression<Func<T, object>> property) => Assign(a => a.IdProperty = property);

		/// <summary>
		/// When specified allows you to ignore <param name="property"></param> on clr type <typeparamref name="T" />
		/// </summary>
		public ClrTypeMappingDescriptor<T> Ignore(Expression<Func<T, object>> property) => 
			Assign(a => a.Properties.Add(new IgnorePropertyMapping<T>(property)));

		/// <summary>
		/// When specified allows you to rename <param name="property"></param> on clr type <typeparamref name="T" />
		/// </summary>
		public ClrTypeMappingDescriptor<T> Rename(Expression<Func<T, object>> property, string newName) => 
			Assign(a => a.Properties.Add(new RenamePropertyMapping<T>(property, newName)));

	}

	public interface IClrTypePropertyMapping<T> where T : class
	{
		Expression<Func<T, object>> Property { get; set; }
		bool Ignore { get; set; }
		string NewName { get; set; }
		IPropertyMapping ToPropertyMapping();
	}

	public abstract class ClrPropertyMappingBase<T> : IClrTypePropertyMapping<T>
		where T : class
	{
		protected IClrTypePropertyMapping<T> Self => this;

		Expression<Func<T, object>> IClrTypePropertyMapping<T>.Property { get; set; }

		bool IClrTypePropertyMapping<T>.Ignore { get; set; }

		string IClrTypePropertyMapping<T>.NewName { get; set; }

		protected ClrPropertyMappingBase(Expression<Func<T, object>> property)
		{
			Self.Property = property;
		}

		IPropertyMapping IClrTypePropertyMapping<T>.ToPropertyMapping() => Self.Ignore ? PropertyMapping.Ignored : new PropertyMapping {Name = Self.NewName};
	}

	public class IgnorePropertyMapping<T> : ClrPropertyMappingBase<T> where T : class
	{
		public IgnorePropertyMapping(Expression<Func<T, object>> property) : base(property)
		{
			Self.Ignore = true;
		}
	}

	public class RenamePropertyMapping<T> : ClrPropertyMappingBase<T> where T : class
	{
		public RenamePropertyMapping(Expression<Func<T, object>> property, string newName) : base(property)
		{
			newName.ThrowIfNull(nameof(newName));
			Self.NewName = newName;
		}
	}



}
