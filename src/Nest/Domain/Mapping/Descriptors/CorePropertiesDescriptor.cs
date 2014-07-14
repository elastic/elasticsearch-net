using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class CorePropertiesDescriptor<T> where T : class
	{
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		public IDictionary<PropertyNameMarker, IElasticType> Properties { get; private set; }
		internal IList<string> _Deletes = new List<string>();

		public CorePropertiesDescriptor()
		{
			this.Properties = new Dictionary<PropertyNameMarker, IElasticType>();
		}

		public CorePropertiesDescriptor<T> Remove(string name)
		{
			this._Deletes.Add(name);
			return this;
		}

		public CorePropertiesDescriptor<T> String(Func<StringMappingDescriptor<T>, StringMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new StringMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for string mapping");
			this.Properties.Add(d._Mapping.Name, d._Mapping);
			return this;
		}

		public CorePropertiesDescriptor<T> Number(Func<NumberMappingDescriptor<T>, NumberMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new NumberMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for number mapping");
			this.Properties.Add(d._Mapping.Name, d._Mapping);
			return this;
		}

		public CorePropertiesDescriptor<T> Date(Func<DateMappingDescriptor<T>, DateMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new DateMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for date mapping");
			this.Properties.Add(d._Mapping.Name, d._Mapping);
			return this;
		}

		public CorePropertiesDescriptor<T> Boolean(Func<BooleanMappingDescriptor<T>, BooleanMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new BooleanMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for boolean mapping");
			this.Properties.Add(d._Mapping.Name, d._Mapping);
			return this;
		}

		public CorePropertiesDescriptor<T> Binary(Func<BinaryMappingDescriptor<T>, BinaryMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new BinaryMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for binary mapping");
			this.Properties.Add(d._Mapping.Name, d._Mapping);
			return this;
		}

		public CorePropertiesDescriptor<T> Generic(Func<GenericMappingDescriptor<T>, GenericMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new GenericMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsConditionless())
				throw new Exception("Could not get field name for generic mapping");
			this.Properties.Add(d._Mapping.Name, d._Mapping);
			return this;
		}

		//Reminder if you are adding a new mapping type, may one appear in the future
		//Add them to PropertiesDescriptor, CorePropertiesDescriptor (if its a new core type), SingleMappingDescriptor
	}
}