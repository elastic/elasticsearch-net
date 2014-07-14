using System;
using System.Linq.Expressions;

namespace Nest
{
	public class MultiFieldMappingDescriptor<T> where T : class
	{
		internal MultiFieldMapping _Mapping = new MultiFieldMapping();

		public MultiFieldMappingDescriptor<T> Name(string name)
		{
			this._Mapping.Name = name;
			return this;
		}

        public MultiFieldMappingDescriptor<T> Path(MultiFieldMappingPath path)
        {
            this._Mapping.Path = path.Value;
            return this;
        }

		public MultiFieldMappingDescriptor<T> Name(Expression<Func<T, object>> objectPath)
		{
			this._Mapping.Name = objectPath;
			return this;
		}

		public MultiFieldMappingDescriptor<T> Fields(Func<CorePropertiesDescriptor<T>, CorePropertiesDescriptor<T>> fieldSelector)
		{
			fieldSelector.ThrowIfNull("fieldSelector");
			var properties = fieldSelector(new CorePropertiesDescriptor<T>());
			foreach (var p in properties.Properties)
			{
				var value = p.Value as IElasticCoreType;
				if (value == null)
					continue;
				
				_Mapping.Fields[p.Key] = value;
			}
			return this;
		}
		
	}

    
}