using System;
using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.Mapping.Types
{
	public abstract class TypeMappingTestBase<T>
			where T : class
	{
		protected abstract object AttributeBasedValues { get; }
		protected abstract object CodeBasedValues { get; }

		protected abstract Func<PropertiesDescriptor<T>, IPromise<IProperties>> FluentProperties { get; }

		[U]
		protected virtual void AttributeBasedSerializes() =>
			Expect(AttributeBasedValues)
				.WhenSerializing(new PutMappingDescriptor<T>().AutoMap() as IPutMappingRequest);

		[U]
		protected virtual void CodeBasedSerializes() =>
			Expect(CodeBasedValues)
				.WhenSerializing(new PutMappingDescriptor<T>().AutoMap().Properties(FluentProperties) as IPutMappingRequest);
	}
}
