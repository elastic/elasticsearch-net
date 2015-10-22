using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Nest.Resolvers;
using System.Linq.Expressions;

namespace Nest
{
	public class ElasticInferrer
	{
		private readonly IConnectionSettingsValues _connectionSettings;

		private IdResolver IdResolver { get; set; }
		private IndexNameResolver IndexNameResolver { get; set; }
		private TypeNameResolver TypeNameResolver { get; set; }
		private FieldNameResolver FieldNameResolver { get; set; }

		public string DefaultIndex
		{
			get
			{
				var index = (this._connectionSettings == null) ? string.Empty : this._connectionSettings.DefaultIndex;
				return index.IsNullOrEmpty() ? "_all" : index;
			}
		}

		public ElasticInferrer(IConnectionSettingsValues connectionSettings)
		{
			this._connectionSettings = connectionSettings;
			this.IdResolver = new IdResolver(this._connectionSettings);
			this.IndexNameResolver = new IndexNameResolver(this._connectionSettings);
			this.TypeNameResolver = new TypeNameResolver(this._connectionSettings);
			this.FieldNameResolver = new FieldNameResolver(this._connectionSettings);
		}

		public string FieldName(FieldName field)
		{
			if (field.IsConditionless())
				return null;

			var name = !field.Name.IsNullOrEmpty()
				? field.Name
				: field.Expression != null
					? this.FieldNameResolver.Resolve(field.Expression)
					: field.Property != null
						? this.FieldNameResolver.Resolve(field.Property)
						: null;

			if (name == null)
				throw new ArgumentException("Could not resolve a field name");

			if (field != null && field.Boost.HasValue)
				name += "^" + field.Boost.Value.ToString(CultureInfo.InvariantCulture);

			return name;
		}

		public string FieldNames(IEnumerable<FieldName> fields)
		{
			if (!fields.HasAny() || fields.All(f=>f.IsConditionless())) return null;
			return string.Join(",", fields.Select(FieldName).Where(f => !f.IsNullOrEmpty()));
		}

		public string PropertyName(PropertyName property)
		{
			if (property.IsConditionless())
				return null;

			var name = !property.Name.IsNullOrEmpty()
				? property.Name
				: property.Expression != null
					? this.FieldNameResolver.Resolve(property.Expression)
					: property.Property != null
						? this.FieldNameResolver.Resolve(property.Property)
						: null;

			if (name == null)
				throw new ArgumentException("Could not resolve a property name");

			return name;
		}

		public string IndexName<T>() where T : class
		{
			return this.IndexName(typeof(T));
		}

		public string IndexName(Type type)
		{
			return this.IndexNameResolver.GetIndexForType(type);
		}

		public string IndexName(IndexName index)
		{
			if (index == null)
				return null;
			return index.Resolve(this._connectionSettings);
		}
		
		public string IndexNames(params IndexName[] indices)
		{
			if (indices == null) return null;
			return string.Join(",", indices.Select(i => this.IndexNameResolver.GetIndexForType(i)));
		}
		
		public string IndexNames(IEnumerable<IndexName> indices)
		{
			return !indices.HasAny() ? null : this.IndexNames(indices.ToArray());
		}

		public string Id<T>(T obj) where T : class
		{
			if (obj == null) return null;
			
			return this.IdResolver.GetIdFor(obj);
		}

		public string Id(Type objType, object obj)
		{
			if (obj == null) return null;
			
			return this.IdResolver.GetIdFor(objType, obj);
		}
		public string TypeName<T>() where T : class
		{
			return this.TypeName(typeof(T));
		}
		public string TypeName(Type t)
		{
			return t == null ? null : this.TypeNameResolver.GetTypeNameFor(t);
		}

		public string TypeNames(params TypeName[] typeNames)
		{
			return typeNames == null 
				? null 
				: string.Join(",", typeNames.Select(t => this.TypeNameResolver.GetTypeNameFor(t)));
		}

		public string TypeNames(IEnumerable<TypeName> typeNames)
		{
			return !typeNames.HasAny() ? null : this.TypeNames(typeNames.ToArray());
		}

		public string TypeName(TypeName type)
		{
			return type == null ? null : this.TypeNameResolver.GetTypeNameFor(type);
		}
	}
}
