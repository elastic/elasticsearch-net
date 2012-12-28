using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest.Resolvers;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
    public class PropertiesDescriptor<T> where T : class
    {
		
		public IDictionary<string, IElasticType> Properties { get; set; }

		public PropertiesDescriptor()
		{
			this.Properties = new Dictionary<string, IElasticType>();
        }

		public PropertiesDescriptor<T> String(Func<StringMappingDescriptor<T>, StringMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new StringMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsNullOrEmpty())
				throw new Exception("Could not get field name for string mapping");
			this.Properties.Add(d._Mapping.Name, d._Mapping);
			return this;
		}
   }

	public class StringMappingDescriptor<T>
	{
		internal StringMapping _Mapping = new StringMapping();

		public StringMappingDescriptor<T> ForField(string name)
		{
			this._Mapping.Name = name;
			return this;
		}
		public StringMappingDescriptor<T> ForField(Expression<Func<T, object>> objectPath)
		{
			var name = new PropertyNameResolver().ResolveToLastToken(objectPath);
			this._Mapping.Name = name;
			return this;
		}

		public StringMappingDescriptor<T> IndexName(string indexName)
		{
			this._Mapping.IndexName = indexName;
			return this;
		}

	}
}