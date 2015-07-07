using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Nest.Resolvers;

namespace Nest
{
	public static class Infer
	{
		public static IndexName Index<T>()
		{
			return typeof(T);
		}
		public static IndexName Index(Type t)
		{
			return t;
		}
		public static TypeName Type<T>()
		{
			return typeof(T);
		}
		public static TypeName Type(Type t)
		{
			return t;
		}
	}

	public class ElasticInferrer
	{
		private readonly IConnectionSettingsValues _connectionSettings;

		private IdResolver IdResolver { get; set; }
		private IndexNameResolver IndexNameResolver { get; set; }
		private TypeNameResolver TypeNameResolver { get; set; }
		private PropertyNameResolver PropertyNameResolver { get; set; }
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
			this.PropertyNameResolver = new PropertyNameResolver(this._connectionSettings);
		}

		public string PropertyPath(PropertyPath marker)
		{
			if (marker.IsConditionless())
				return null;
			var name = !marker.Name.IsNullOrEmpty() ? marker.Name : this.PropertyNameResolver.Resolve(marker.Expression);
			if (marker.Boost.HasValue)
				name += "^" + marker.Boost.Value.ToString(CultureInfo.InvariantCulture);

			return name;
		}

		public string PropertyPaths(IEnumerable<PropertyPath> fields)
		{
			if (!fields.HasAny() || fields.All(f=>f.IsConditionless())) return null;
			return string.Join(",", fields.Select(PropertyPath).Where(f => !f.IsNullOrEmpty()));
		}

		public string PropertyPath(MemberInfo member)
		{
			return member == null ? null : this.PropertyNameResolver.Resolve(member);
		}

		public string PropertyName(PropertyName marker)
		{
			if (marker.IsConditionless())
				return null;
			return !marker.Name.IsNullOrEmpty() 
				? marker.Name 
				: marker.Expression != null 
					? this.PropertyNameResolver.ResolveToLastToken(marker.Expression)
					: this.TypeName(marker.Type);
		}
		
		public string PropertyNames(IEnumerable<PropertyName> fields)
		{
			if (!fields.HasAny() || fields.All(f=>f.IsConditionless())) return null;
			return string.Join(",", fields.Select(PropertyName).Where(f => !f.IsNullOrEmpty()));
		}
		public string PropertyName(MemberInfo member)
		{
			return member == null ? null : this.PropertyNameResolver.ResolveToLastToken(member);
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
