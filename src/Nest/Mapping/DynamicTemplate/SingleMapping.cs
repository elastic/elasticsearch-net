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
		public IElasticType String(Func<StringTypeDescriptor<T>, StringTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new StringTypeDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get string mapping");
			return d;
		}

		public IElasticType Number(Func<NumberTypeDescriptor<T>, NumberTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new NumberTypeDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get number mapping");
			return d;
		}

		public IElasticType Date(Func<DateTypeDescriptor<T>, DateTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new DateTypeDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get date mapping");
			return d;
		}

		public IElasticType Boolean(Func<BooleanTypeDescriptor<T>, BooleanTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new BooleanTypeDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get boolean mapping");
			return d;
		}

		public IElasticType Binary(Func<BinaryTypeDescriptor<T>, BinaryTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new BinaryTypeDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get binary mapping");
			return d;
		}
		public IElasticType Attachment(Func<AttachmentTypeDescriptor<T>, AttachmentTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new AttachmentTypeDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get attachment mapping");
			return d;
		}

		public IElasticType Object<TChild>(Func<ObjectTypeDescriptor<T, TChild>, ObjectTypeDescriptor<T, TChild>> selector)
			where TChild : class
		{
			selector.ThrowIfNull("selector");
			var d = selector(new ObjectTypeDescriptor<T, TChild>(this._connectionSettings));
			if (d == null)
				throw new Exception("Could not get object mapping");
			return d._Mapping;
		}
		
		public IElasticType NestedObject<TChild>(Func<NestedObjectTypeDescriptor<T, TChild>, NestedObjectTypeDescriptor<T, TChild>> selector)
			where TChild : class
		{
			selector.ThrowIfNull("selector");
			var d = selector(new NestedObjectTypeDescriptor<T, TChild>(this._connectionSettings));
			if (d == null)
				throw new Exception("Could not get nested object mapping");
			return d._Mapping;
		}
		
		public IElasticType Ip(Func<IpTypeDescriptor<T>, IpTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new IpTypeDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get IP mapping");
			return d;
		}

		public IElasticType GeoPoint(Func<GeoPointTypeDescriptor<T>, GeoPointTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new GeoPointTypeDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get geo point mapping");
			return d._Mapping;
		}

		public IElasticType GeoShape(Func<GeoShapeTypeDescriptor<T>, GeoShapeTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new GeoShapeTypeDescriptor<T>());
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

        public IElasticType Completion(Func<CompletionTypeDescriptor<T>, CompletionTypeDescriptor<T>> selector)
        {
            selector.ThrowIfNull("selector");
            var d = selector(new CompletionTypeDescriptor<T>());
            if (d == null)
                throw new Exception("Could not get completion mapping");
			return d;
        }

		public IElasticType Murmur3Hash(Func<Murmur3HashTypeDescriptor<T>, Murmur3HashTypeDescriptor<T>> selector)
		{
			selector.ThrowIfNull("selector");
			var d = selector(new Murmur3HashTypeDescriptor<T>());
			if (d == null)
				throw new Exception("Could not get murmur hash mapping");
			return d._Mapping;
		}
	}
}