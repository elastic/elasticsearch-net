using System;
using System.Collections.Generic;

namespace Nest
{
	public class SingleMappingDescriptor<T> 
		: IPropertiesDescriptor<T, IElasticsearchProperty>
		where T : class
	{
		public IElasticsearchProperty String(Func<StringPropertyDescriptor<T>, IStringProperty> selector) =>
			selector?.Invoke(new StringPropertyDescriptor<T>());

		public IElasticsearchProperty Number(Func<NumberPropertyDescriptor<T>, INumberProperty> selector) =>
			selector?.Invoke(new NumberPropertyDescriptor<T>());

		public IElasticsearchProperty Date(Func<DatePropertyDescriptor<T>, IDateProperty> selector) =>
			selector?.Invoke(new DatePropertyDescriptor<T>());

		public IElasticsearchProperty Boolean(Func<BooleanPropertyDescriptor<T>, IBooleanProperty> selector) =>
			selector?.Invoke(new BooleanPropertyDescriptor<T>());

		public IElasticsearchProperty Binary(Func<BinaryPropertyDescriptor<T>, IBinaryProperty> selector) =>
			selector?.Invoke(new BinaryPropertyDescriptor<T>());

		public IElasticsearchProperty Attachment(Func<AttachmentPropertyDescriptor<T>, IAttachmentProperty> selector) =>
			selector?.Invoke(new AttachmentPropertyDescriptor<T>());

		public IElasticsearchProperty Object<TChild>(Func<ObjectTypeDescriptor<T, TChild>, IObjectProperty> selector)
			where TChild : class => selector?.Invoke(new ObjectTypeDescriptor<T, TChild>());

		public IElasticsearchProperty Nested<TChild>(Func<NestedPropertyDescriptor<T, TChild>, INestedProperty> selector)
			where TChild : class => selector?.Invoke(new NestedPropertyDescriptor<T, TChild>());

		public IElasticsearchProperty Ip(Func<IpPropertyDescriptor<T>, IIpProperty> selector) =>
			selector?.Invoke(new IpPropertyDescriptor<T>());

		public IElasticsearchProperty GeoPoint(Func<GeoPointPropertyDescriptor<T>, IGeoPointProperty> selector) =>
			selector?.Invoke(new GeoPointPropertyDescriptor<T>());

		public IElasticsearchProperty GeoShape(Func<GeoShapePropertyDescriptor<T>, IGeoShapeProperty> selector) =>
			selector?.Invoke(new GeoShapePropertyDescriptor<T>());

		public IElasticsearchProperty Completion(Func<CompletionPropertyDescriptor<T>, ICompletionProperty> selector) =>
			selector?.Invoke(new CompletionPropertyDescriptor<T>());

		public IElasticsearchProperty Murmur3Hash(Func<Murmur3HashPropertyDescriptor<T>, IMurmur3HashProperty> selector) =>
			selector?.Invoke(new Murmur3HashPropertyDescriptor<T>());

		public IElasticsearchProperty TokenCount(Func<TokenCountPropertyDescriptor<T>, ITokenCountProperty> selector) =>
			selector?.Invoke(new TokenCountPropertyDescriptor<T>());
	}
}