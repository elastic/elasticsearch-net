using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nest.Resolvers.Writers;

namespace Nest
{
	public class PropertiesDescriptor<T> where T : class
	{
		public IDictionary<string, IElasticType> Properties { get; private set; }
		internal IList<string> _Deletes = new List<string>();

		public PropertiesDescriptor()
		{
			this.Properties = new Dictionary<string, IElasticType>();
		}

		public PropertiesDescriptor<T> Remove(string name)
		{
			this._Deletes.Add(name);
			return this;
		}

		public PropertiesDescriptor<T> String(Func<StringMappingDescriptor<T>, StringMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new StringMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsNullOrEmpty())
				throw new Exception("Could not get field name for string mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> Number(Func<NumberMappingDescriptor<T>, NumberMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new NumberMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsNullOrEmpty())
				throw new Exception("Could not get field name for number mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> Date(Func<DateMappingDescriptor<T>, DateMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new DateMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsNullOrEmpty())
				throw new Exception("Could not get field name for date mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> Boolean(Func<BooleanMappingDescriptor<T>, BooleanMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new BooleanMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsNullOrEmpty())
				throw new Exception("Could not get field name for boolean mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> Binary(Func<BinaryMappingDescriptor<T>, BinaryMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new BinaryMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsNullOrEmpty())
				throw new Exception("Could not get field name for binary mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}
		public PropertiesDescriptor<T> Attachment(Func<AttachmentMappingDescriptor<T>, AttachmentMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new AttachmentMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsNullOrEmpty())
				throw new Exception("Could not get field name for attachment mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> Object<TChild>(Func<ObjectMappingDescriptor<T, TChild>, ObjectMappingDescriptor<T, TChild>> selector)
			where TChild : class
		{
			selector.ThrowIfNull("selector");
			var d = selector(new ObjectMappingDescriptor<T, TChild>());
			if (d == null || d._Mapping.Name.IsNullOrEmpty())
				throw new Exception("Could not get field name for object mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}
		
		public PropertiesDescriptor<T> NestedObject<TChild>(Func<NestedObjectMappingDescriptor<T, TChild>, NestedObjectMappingDescriptor<T, TChild>> selector)
			where TChild : class
		{
			selector.ThrowIfNull("selector");
			var d = selector(new NestedObjectMappingDescriptor<T, TChild>());
			if (d == null || d._Mapping.Name.IsNullOrEmpty())
				throw new Exception("Could not get field name for nested sobject mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}
		
		public PropertiesDescriptor<T> MultiField(Func<MultiFieldMappingDescriptor<T>, MultiFieldMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new MultiFieldMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsNullOrEmpty())
				throw new Exception("Could not get field name for multifield mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}
		public PropertiesDescriptor<T> IP(Func<IPMappingDescriptor<T>, IPMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new IPMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsNullOrEmpty())
				throw new Exception("Could not get field name for IP mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> GeoPoint(Func<GeoPointMappingDescriptor<T>, GeoPointMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new GeoPointMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsNullOrEmpty())
				throw new Exception("Could not get field name for geo point mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}

		public PropertiesDescriptor<T> GeoShape(Func<GeoShapeMappingDescriptor<T>, GeoShapeMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new GeoShapeMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsNullOrEmpty())
				throw new Exception("Could not get field name for geo shape mapping");
			this.Properties[d._Mapping.Name] = d._Mapping;
			return this;
		}
		public PropertiesDescriptor<T> Generic(Func<GenericMappingDescriptor<T>, GenericMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new GenericMappingDescriptor<T>());
			if (d == null || d._Mapping.Name.IsNullOrEmpty())
				throw new Exception("Could not get field name for generic mapping");
			this.Properties.Add(d._Mapping.Name, d._Mapping);
			return this;
		}

		//Reminder if you are adding a new mapping type, may one appear in the future
		//Add them to PropertiesDescriptor, CorePropertiesDescriptor (if its a new core type), SingleMappingDescriptor
	}
}