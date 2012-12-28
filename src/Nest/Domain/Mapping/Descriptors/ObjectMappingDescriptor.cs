using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
    public class ObjectMappingDescriptor<TParent, TChild> 
		where TParent : class
		where TChild : class

    {
		internal ObjectMapping _Mapping { get; set; }
		internal string _TypeName { get; set; }

		public ObjectMappingDescriptor()
		{
			this._TypeName = new TypeNameResolver().GetTypeNameFor<TChild>();
			this._Mapping = new ObjectMapping() { Name = this._TypeName };
        }
		public ObjectMappingDescriptor<TParent, TChild> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public ObjectMappingDescriptor<TParent, TChild> Name(Expression<Func<TParent, TChild>> objectPath)
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
		public ObjectMappingDescriptor<TParent, TChild> MapFromAttributes(int maxRecursion = 0)
		{
			var writer = new TypeMappingWriter(typeof(TChild), this._TypeName, maxRecursion);
			this._Mapping = writer.RootObjectMappingFromAttributes();

			return this;
		}

		public ObjectMappingDescriptor<TParent, TChild> Dynamic(bool dynamic = true)
		{
			this._Mapping.Dynamic = dynamic;
			return this;
		}
		public ObjectMappingDescriptor<TParent, TChild> Enabled(bool enabled = true)
		{
			this._Mapping.Enabled = enabled;
			return this;
		}
		public ObjectMappingDescriptor<TParent, TChild> IncludeInAll(bool includeInAll = true)
		{
			this._Mapping.IncludeInAll = includeInAll;
			return this;
		}
		public ObjectMappingDescriptor<TParent, TChild> Path(string path)
		{
			this._Mapping.Path = path;
			return this;
		}

		public ObjectMappingDescriptor<TParent, TChild> Properties(Func<PropertiesDescriptor<TChild>, PropertiesDescriptor<TChild>> propertiesSelector)
		{
			propertiesSelector.ThrowIfNull("propertiesSelector");
			//todo merge with _ObjectMapping 
			return this;
		}
		
    }
}