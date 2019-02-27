using System;

namespace Nest
{
	public class MappingsDescriptor
	{
		public ITypeMapping Map<T>(Func<TypeMappingDescriptor<T>, ITypeMapping> selector) where T : class =>
			selector?.Invoke(new TypeMappingDescriptor<T>());
	}
}
