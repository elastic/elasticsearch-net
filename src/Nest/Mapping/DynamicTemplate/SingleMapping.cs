using System;
using System.Collections.Generic;

namespace Nest
{
	public class SingleMappingDescriptor<T> 
		: IPropertiesDescriptor<T, IElasticType>
		where T : class
	{
		public IElasticType String(Func<StringTypeDescriptor<T>, IStringType> selector) =>
			selector?.Invoke(new StringTypeDescriptor<T>());

		public IElasticType Number(Func<NumberTypeDescriptor<T>, INumberType> selector) =>
			selector?.Invoke(new NumberTypeDescriptor<T>());

		public IElasticType Date(Func<DateTypeDescriptor<T>, IDateType> selector) =>
			selector?.Invoke(new DateTypeDescriptor<T>());

		public IElasticType Boolean(Func<BooleanTypeDescriptor<T>, IBooleanType> selector) =>
			selector?.Invoke(new BooleanTypeDescriptor<T>());

		public IElasticType Binary(Func<BinaryTypeDescriptor<T>, IBinaryType> selector) =>
			selector?.Invoke(new BinaryTypeDescriptor<T>());

		public IElasticType Attachment(Func<AttachmentTypeDescriptor<T>, IAttachmentType> selector) =>
			selector?.Invoke(new AttachmentTypeDescriptor<T>());

		public IElasticType Object<TChild>(Func<ObjectTypeDescriptor<T, TChild>, IObjectType> selector)
			where TChild : class => selector?.Invoke(new ObjectTypeDescriptor<T, TChild>());

		public IElasticType Nested<TChild>(Func<NestedObjectTypeDescriptor<T, TChild>, INestedType> selector)
			where TChild : class => selector?.Invoke(new NestedObjectTypeDescriptor<T, TChild>());

		public IElasticType Ip(Func<IpTypeDescriptor<T>, IIpType> selector) =>
			selector?.Invoke(new IpTypeDescriptor<T>());

		public IElasticType GeoPoint(Func<GeoPointTypeDescriptor<T>, IGeoPointType> selector) =>
			selector?.Invoke(new GeoPointTypeDescriptor<T>());

		public IElasticType GeoShape(Func<GeoShapeTypeDescriptor<T>, IGeoShapeType> selector) =>
			selector?.Invoke(new GeoShapeTypeDescriptor<T>());

		public IElasticType Completion(Func<CompletionTypeDescriptor<T>, ICompletionType> selector) =>
			selector?.Invoke(new CompletionTypeDescriptor<T>());

		public IElasticType Murmur3Hash(Func<Murmur3HashTypeDescriptor<T>, IMurmur3HashType> selector) =>
			selector?.Invoke(new Murmur3HashTypeDescriptor<T>());

		public IElasticType TokenCount(Func<TokenCountTypeDescriptor<T>, ITokenCountType> selector) =>
			selector?.Invoke(new TokenCountTypeDescriptor<T>());
	}
}