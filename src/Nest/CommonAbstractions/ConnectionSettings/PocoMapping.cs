using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Nest
{
	public interface IPocoMapping
	{
		Type ClrType { get; }

		/// <summary>
		/// When specified dictates the default Elasticsearch index name for <see cref="ClrType"/>
		/// </summary>
		string IndexName { get; set; }

		/// <summary>
		/// When specified dictates the default Elasticsearch type name for <see cref="ClrType"/>
		/// </summary>
		string TypeName { get; set; }

		/// <summary>
		/// When specified dictates the relation name for <see cref="ClrType"/> to resolve to.
		/// </summary>
		string RelationName { get; set; }

	}

	public interface IPocoMapping<T> : IPocoMapping where T : class
	{
		/// <summary>
		/// Allows you to set a default Id property on <typeparamref name="T" /> that NEST will evaluate
		/// </summary>
		Expression<Func<T, object>> IdProperty { get; set; }

		/// <summary>
		/// When specified allows you to ignore or rename certain properties of clr type <typeparamref name="T" />
		/// </summary>
		IList<IPocoPropertyMapping<T>> Properties { get; set; }
	}

	public class PocoMapping : IPocoMapping
	{

		public Type ClrType { get; }
		public PocoMapping(Type type) => ClrType = type;

		/// <summary>
		/// When specified dictates the default Elasticsearch index name for <see cref="ClrType"/>
		/// </summary>
		public string IndexName { get; set; }

		/// <summary>
		/// When specified dictates the default Elasticsearch type name for <see cref="ClrType"/>
		/// </summary>
		public string TypeName { get; set; }

		/// <summary>
		/// When specified dictates the relation name for <see cref="ClrType"/> to resolve to.
		/// </summary>
		public string RelationName { get; set; }

	}
	public class PocoMapping<T> : PocoMapping, IPocoMapping<T> where T : class
	{
		public PocoMapping() : base(typeof(T)) { }

		/// <summary>
		/// Allows you to set a default Id property on <typeparamref name="T" /> that NEST will evaluate
		/// </summary>
		public Expression<Func<T, object>> IdProperty { get; set; }

		/// <summary>
		/// When specified allows you to ignore or rename certain properties of clr type <typeparamref name="T" />
		/// </summary>
		public IList<IPocoPropertyMapping<T>> Properties { get; set; }

	}

	public class PocoMappingDescriptor<T> : DescriptorBase<PocoMappingDescriptor<T>,IPocoMapping<T>> , IPocoMapping<T>
		where T : class
	{
		Type IPocoMapping.ClrType { get; } = typeof (T);
		string IPocoMapping.IndexName { get; set; }
		string IPocoMapping.TypeName { get; set; }
		string IPocoMapping.RelationName { get; set; }
		Expression<Func<T, object>> IPocoMapping<T>.IdProperty { get; set; }
		IList<IPocoPropertyMapping<T>> IPocoMapping<T>.Properties { get; set; } = new List<IPocoPropertyMapping<T>>();

		/// <summary>
		/// When specified dictates the default Elasticsearch index name for <typeparamref name="T"/>
		/// </summary>
		public PocoMappingDescriptor<T> IndexName(string indexName) => Assign(a => a.IndexName = indexName);

		/// <summary>
		/// When specified dictates the default Elasticsearch type name for <typeparamref name="T" />
		/// </summary>
		public PocoMappingDescriptor<T> TypeName(string typeName) => Assign(a => a.TypeName = typeName);

		/// <summary>
		/// When specified dictates the relation name for <typeparamref name="T" /> to resolve to.
		/// </summary>
		public PocoMappingDescriptor<T> RelationName(string relationName) => Assign(a => a.RelationName = relationName);

		/// <summary>
		/// Allows you to set a default Id property on <typeparamref name="T" /> that NEST will evaluate
		/// </summary>
		public PocoMappingDescriptor<T> IdProperty(Expression<Func<T, object>> property) => Assign(a => a.IdProperty = property);

		/// <summary>
		/// When specified allows you to ignore <param name="property"></param> on clr type <typeparamref name="T" />
		/// </summary>
		public PocoMappingDescriptor<T> Ignore(Expression<Func<T, object>> property) =>
			Assign(a => a.Properties.Add(new IgnorePropertyMapping<T>(property)));

		/// <summary>
		/// When specified allows you to rename <param name="property"></param> on clr type <typeparamref name="T" />
		/// </summary>
		public PocoMappingDescriptor<T> Rename(Expression<Func<T, object>> property, string newName) =>
			Assign(a => a.Properties.Add(new RenamePropertyMapping<T>(property, newName)));

	}

	public class PocoMappingDescriptor : DescriptorBase<PocoMappingDescriptor,IPocoMapping> , IPocoMapping
	{
		private readonly Type _type;
		Type IPocoMapping.ClrType => _type;
		string IPocoMapping.IndexName { get; set; }
		string IPocoMapping.TypeName { get; set; }
		string IPocoMapping.RelationName { get; set; }
		public PocoMappingDescriptor(Type type) => _type = type;

		/// <summary>
		/// When specified dictates the default Elasticsearch index name for <typeparamref name="T"/>
		/// </summary>
		public PocoMappingDescriptor IndexName(string indexName) => Assign(a => a.IndexName = indexName);

		/// <summary>
		/// When specified dictates the default Elasticsearch type name for <typeparamref name="T" />
		/// </summary>
		public PocoMappingDescriptor TypeName(string typeName) => Assign(a => a.TypeName = typeName);

		/// <summary>
		/// When specified dictates the relation name for <typeparamref name="T" /> to resolve to.
		/// </summary>
		public PocoMappingDescriptor RelationName(string relationName) => Assign(a => a.RelationName = relationName);
	}

	public interface IPocoPropertyMapping<T> where T : class
	{
		Expression<Func<T, object>> Property { get; set; }
		bool Ignore { get; set; }
		string NewName { get; set; }
		IPropertyMapping ToPropertyMapping();
	}

	public abstract class PocoPropertyMappingBase<T> : IPocoPropertyMapping<T>
		where T : class
	{
		protected IPocoPropertyMapping<T> Self => this;

		Expression<Func<T, object>> IPocoPropertyMapping<T>.Property { get; set; }

		bool IPocoPropertyMapping<T>.Ignore { get; set; }

		string IPocoPropertyMapping<T>.NewName { get; set; }

		protected PocoPropertyMappingBase(Expression<Func<T, object>> property)
		{
			Self.Property = property;
		}

		IPropertyMapping IPocoPropertyMapping<T>.ToPropertyMapping() => Self.Ignore ? PropertyMapping.Ignored : new PropertyMapping {Name = Self.NewName};
	}

	public class IgnorePropertyMapping<T> : PocoPropertyMappingBase<T> where T : class
	{
		public IgnorePropertyMapping(Expression<Func<T, object>> property) : base(property)
		{
			Self.Ignore = true;
		}
	}

	public class RenamePropertyMapping<T> : PocoPropertyMappingBase<T> where T : class
	{
		public RenamePropertyMapping(Expression<Func<T, object>> property, string newName) : base(property)
		{
			newName.ThrowIfNull(nameof(newName));
			Self.NewName = newName;
		}
	}



}
