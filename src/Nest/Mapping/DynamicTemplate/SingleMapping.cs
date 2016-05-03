using System;

namespace Nest
{
	public class SingleMappingDescriptor<T> :
		DescriptorBase<SingleMappingDescriptor<T>, IPropertiesDescriptor<T, IProperty>>, IPropertiesDescriptor<T, IProperty>
		where T : class
	{
		public IProperty String(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			selector?.Invoke(new StringPropertyDescriptor<T>());

		public IProperty Number(Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			selector?.Invoke(new NumberPropertyDescriptor<T>());

		public IProperty Date(Func<DatePropertyDescriptor<T>, IDateProperty> selector) =>
			selector?.Invoke(new DatePropertyDescriptor<T>());

		public IProperty Boolean(Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector) =>
			selector?.Invoke(new BooleanPropertyDescriptor<T>());

		public IProperty Binary(Func<BinaryPropertyDescriptor<T>, IBinaryProperty> selector) =>
			selector?.Invoke(new BinaryPropertyDescriptor<T>());

		public IProperty Attachment(Func<AttachmentPropertyDescriptor<T>, IAttachmentProperty> selector) =>
			selector?.Invoke(new AttachmentPropertyDescriptor<T>());

		public IProperty Object<TChild>(Func<ObjectTypeDescriptor<T, TChild>, IObjectProperty> selector)
			where TChild : class => selector?.Invoke(new ObjectTypeDescriptor<T, TChild>());

		public IProperty Nested<TChild>(Func<NestedPropertyDescriptor<T, TChild>, INestedProperty> selector)
			where TChild : class => selector?.Invoke(new NestedPropertyDescriptor<T, TChild>());

		public IProperty Ip(Func<IpPropertyDescriptor<T>, IIpProperty> selector) =>
			selector?.Invoke(new IpPropertyDescriptor<T>());

		public IProperty GeoPoint(Func<GeoPointPropertyDescriptor<T>, IGeoPointProperty> selector) =>
			selector?.Invoke(new GeoPointPropertyDescriptor<T>());

		public IProperty GeoShape(Func<GeoShapePropertyDescriptor<T>, IGeoShapeProperty> selector) =>
			selector?.Invoke(new GeoShapePropertyDescriptor<T>());

		public IProperty Completion(Func<CompletionPropertyDescriptor<T>, ICompletionProperty> selector) =>
			selector?.Invoke(new CompletionPropertyDescriptor<T>());

		public IProperty Murmur3Hash(Func<Murmur3HashPropertyDescriptor<T>, IMurmur3HashProperty> selector) =>
			selector?.Invoke(new Murmur3HashPropertyDescriptor<T>());

		public IProperty TokenCount(Func<TokenCountPropertyDescriptor<T>, ITokenCountProperty> selector) =>
			selector?.Invoke(new TokenCountPropertyDescriptor<T>());

		public IProperty Generic(Func<GenericPropertyDescriptor<T>, IGenericProperty> selector) =>
			selector?.Invoke(new GenericPropertyDescriptor<T>());
	}
}
