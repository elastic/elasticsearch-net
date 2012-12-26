using System;
using System.Collections.Generic;
using Nest.Resolvers;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
    public class TypeMappingDescriptor<T> where T : class
    {
		internal TypeMapping _TypeMapping { get; set; }
		internal string _Name { get; set; }

		public TypeMappingDescriptor()
		{
			this._Name = new TypeNameResolver().GetTypeNameFor<T>();
			this._TypeMapping = new TypeMapping(this._Name);
        }

		/// <summary>
		/// Convenience method to map from most of the object from the attributes/properties.
		/// Later calls on the fluent interface can override whatever is set is by this call. 
		/// This helps mapping all the ints as ints, floats as floats etcetera withouth having to be overly terse in your fluent mapping
		/// </summary>
		/// <returns></returns>
		public TypeMappingDescriptor<T> MapFromAttributes(int maxRecursion = 0)
		{
			var writer = new TypeMappingWriter(typeof(T), this._Name, maxRecursion);
			var json = writer.MapFromAttributes();
			this._TypeMapping = JsonConvert.DeserializeObject<TypeMapping>(json);
			return this;
		}

		/// <summary>
		/// Explicitly set the typename otherwise it will infer the typename on its own (lowercase and pluralized).
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public TypeMappingDescriptor<T> TypeName(string name)
		{
			this._Name = name;
			this._TypeMapping.Name = name;
			return this;
		}
		public TypeMappingDescriptor<T> IdMapping(Func<IdMapping, IdMapping> idMapper)
		{
			idMapper.ThrowIfNull("idMapper");
			this._TypeMapping.IdMapping = idMapper(new IdMapping());
			return this;
		}

		public TypeMappingDescriptor<T> SourceMapping(Func<SourceMapping, SourceMapping> sourceMapper)
		{
			sourceMapper.ThrowIfNull("sourceMapper");
			this._TypeMapping.SourceMapping = sourceMapper(new SourceMapping());
			return this;
		}

    }
}