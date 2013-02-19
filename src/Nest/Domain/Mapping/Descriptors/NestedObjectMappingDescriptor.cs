using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
    public class NestedObjectMappingDescriptor<TParent, TChild> 
		where TParent : class
		where TChild : class

    {
		internal NestedObjectMapping _Mapping { get; set; }
		internal string _TypeName { get; set; }

		public NestedObjectMappingDescriptor()
		{
			this._TypeName = new TypeNameResolver().GetTypeNameFor<TChild>();
			this._Mapping = new NestedObjectMapping() { };
        }
		public NestedObjectMappingDescriptor<TParent, TChild> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public NestedObjectMappingDescriptor<TParent, TChild> Name(Expression<Func<TParent, TChild>> objectPath)
		{
			var name = new PropertyNameResolver().ResolveToLastToken(objectPath);
			this._Mapping.Name = name;
			return this;
		}

		/// <summary>
		/// Convenience method to map from most of the object from the attributes/properties.
		/// Later calls on the fluent interface can override whatever is set is by this call. 
		/// This helps mapping all the ints as ints, floats as floats etcetera withouth having to be overly verbose in your fluent mapping
		/// </summary>
		/// <returns></returns>
		public NestedObjectMappingDescriptor<TParent, TChild> MapFromAttributes(int maxRecursion = 0)
		{
			var writer = new TypeMappingWriter(typeof(TChild), this._TypeName, maxRecursion);
			var mapping = writer.NestedObjectMappingFromAttributes();
			if (mapping == null)
				return this;
			if (this._Mapping.Properties == null)
				this._Mapping.Properties = new Dictionary<string, IElasticType>();

			var properties = mapping.Properties;
			foreach (var p in properties)
			{
				this._Mapping.Properties[p.Key] = p.Value;
			}
			return this;
		}

		public NestedObjectMappingDescriptor<TParent, TChild> Dynamic(bool dynamic = true)
		{
			this._Mapping.Dynamic = dynamic;
			return this;
		}
		public NestedObjectMappingDescriptor<TParent, TChild> Enabled(bool enabled = true)
		{
			this._Mapping.Enabled = enabled;
			return this;
		}
		public NestedObjectMappingDescriptor<TParent, TChild> IncludeInParent(bool includeInParent = true)
		{
			this._Mapping.IncludeInParent = includeInParent;
			return this;
		}
		public NestedObjectMappingDescriptor<TParent, TChild> IncludeInRoot(bool includeInRoot = true)
		{
			this._Mapping.IncludeInRoot = includeInRoot;
			return this;
		}
		public NestedObjectMappingDescriptor<TParent, TChild> IncludeInAll(bool includeInAll = true)
		{
			this._Mapping.IncludeInAll = includeInAll;
			return this;
		}
		public NestedObjectMappingDescriptor<TParent, TChild> Path(string path)
		{
			this._Mapping.Path = path;
			return this;
		}

		public NestedObjectMappingDescriptor<TParent, TChild> Properties(Func<PropertiesDescriptor<TChild>, PropertiesDescriptor<TChild>> propertiesSelector)
		{
			propertiesSelector.ThrowIfNull("propertiesSelector");
			var properties = propertiesSelector(new PropertiesDescriptor<TChild>());
			if (this._Mapping.Properties == null)
				this._Mapping.Properties = new Dictionary<string, IElasticType>();

			foreach (var t in properties._Deletes)
			{
				_Mapping.Properties.Remove(t);
			}
			foreach (var p in properties.Properties)
			{
				_Mapping.Properties[p.Key] = p.Value;
			}
			return this;
		}
		
    }
}