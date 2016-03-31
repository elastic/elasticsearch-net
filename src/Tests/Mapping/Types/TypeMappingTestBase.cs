using System;
using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;

namespace Tests.Mapping.Types
{
	public abstract class TypeMappingTestBase<T>
			where T : class
	{
		protected abstract object ExpectJson { get; }

		protected virtual object ExpectJsonFluentOnly { get; }

		protected virtual bool AutoMap => false;

		protected abstract Func<PropertiesDescriptor<T>, IPromise<IProperties>> FluentProperties { get; }

		protected virtual Func<PropertiesDescriptor<T>, IPromise<IProperties>> FluentOnlyProperties { get; }

		[U]
		protected virtual void AttributeBasedSerializes() =>
			Expect(ExpectJson)
				.WhenSerializing(new PutMappingDescriptor<T>().AutoMap() as IPutMappingRequest);

		[U]
		protected virtual void CodeBasedSerializes()
		{
			var descriptor = new PutMappingDescriptor<T>().Properties(FluentProperties);
			if (this.AutoMap) descriptor.AutoMap();
			Expect(ExpectJson)
				.WhenSerializing(descriptor as IPutMappingRequest);

			if (ExpectJsonFluentOnly != null)
			{
				Expect(ExpectJsonFluentOnly)
					.WhenSerializing(new PutMappingDescriptor<T>().Properties(FluentOnlyProperties) as IPutMappingRequest);
			}
		}
	}
}
