using System;
using System.Collections.Generic;

namespace Nest
{
	public class SingleMappingDescriptor<T> where T : class
	{
		private readonly IConnectionSettingsValues _connectionSettings;

		public SingleMappingDescriptor(IConnectionSettingsValues connectionSettings)
		{
			this._connectionSettings = connectionSettings;
		}
		public IElasticType String(Func<StringMappingDescriptor<T>, StringMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new StringMappingDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get string mapping");
			return d._Mapping;
		}

		public IElasticType Number(Func<NumberMappingDescriptor<T>, NumberMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new NumberMappingDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get number mapping");
			return d._Mapping;
		}

		public IElasticType Date(Func<DateMappingDescriptor<T>, DateMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new DateMappingDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get date mapping");
			return d._Mapping;
		}

		public IElasticType Boolean(Func<BooleanMappingDescriptor<T>, BooleanMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new BooleanMappingDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get boolean mapping");
			return d._Mapping;
		}

		public IElasticType Binary(Func<BinaryMappingDescriptor<T>, BinaryMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new BinaryMappingDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get binary mapping");
			return d._Mapping;
		}
		public IElasticType Attachment(Func<AttachmentMappingDescriptor<T>, AttachmentMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new AttachmentMappingDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get attachment mapping");
			return d._Mapping;
		}

		public IElasticType Object<TChild>(Func<ObjectMappingDescriptor<T, TChild>, ObjectMappingDescriptor<T, TChild>> selector)
			where TChild : class
		{
			selector.ThrowIfNull("selector");
			var d = selector(new ObjectMappingDescriptor<T, TChild>(this._connectionSettings));
			if (d == null)
				throw new Exception("Could not get object mapping");
			return d._Mapping;
		}
		
		public IElasticType NestedObject<TChild>(Func<NestedObjectMappingDescriptor<T, TChild>, NestedObjectMappingDescriptor<T, TChild>> selector)
			where TChild : class
		{
			selector.ThrowIfNull("selector");
			var d = selector(new NestedObjectMappingDescriptor<T, TChild>(this._connectionSettings));
			if (d == null)
				throw new Exception("Could not get nested object mapping");
			return d._Mapping;
		}
		
		public IElasticType MultiField(Func<MultiFieldMappingDescriptor<T>, MultiFieldMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new MultiFieldMappingDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get multifield mapping");
			return d._Mapping;
		}
		public IElasticType IP(Func<IPMappingDescriptor<T>, IPMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new IPMappingDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get IP mapping");
			return d._Mapping;
		}

		public IElasticType GeoPoint(Func<GeoPointMappingDescriptor<T>, GeoPointMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new GeoPointMappingDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get geo point mapping");
			return d._Mapping;
		}

		public IElasticType GeoShape(Func<GeoShapeMappingDescriptor<T>, GeoShapeMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new GeoShapeMappingDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get geo shape mapping");
			return d._Mapping;
		}

		public IElasticType Generic(Func<GenericMappingDescriptor<T>, GenericMappingDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new GenericMappingDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get generic mapping");
			return d._Mapping;
		}

        public IElasticType Completion(Func<CompletionMappingDescriptor<T>, CompletionMappingDescriptor<T>> selector)
        {
            selector.ThrowIfNull("selector");
            var d = selector(new CompletionMappingDescriptor<T>());
            if (d == null)
                throw new Exception("Could not get completion mapping");
            return d._Mapping;
        }

		//Reminder if you are adding a new mapping type, may one appear in the future
		//Add them to PropertiesDescriptor, CorePropertiesDescriptor (if its a new core type), SingleMappingDescriptor
	}
}