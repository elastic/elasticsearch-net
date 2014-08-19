using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public abstract class CoreMappingDescriptorBase<T> where T : class
	{
		protected virtual void Fields(Func<CorePropertiesDescriptor<T>, CorePropertiesDescriptor<T>> fieldSelector, CoreMappingBase mapping)
		{
			fieldSelector.ThrowIfNull("fieldSelector");
			
			if (mapping.Fields == null)
				mapping.Fields = new Dictionary<PropertyNameMarker, IElasticCoreType>();

			var properties = fieldSelector(new CorePropertiesDescriptor<T>());
			
			foreach (var p in properties.Properties)
			{
				var value = p.Value as IElasticCoreType;
				if (value == null)
					continue;

				mapping.Fields[p.Key] = value;
			}
		}
	}
}
