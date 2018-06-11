using System;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework;
using static Tests.Framework.RoundTripper;
using Tests.Framework.Integration;

namespace Tests.Mapping.Types
{

	public abstract class AttributeTestsBase<T>
			where T : class
	{
		protected abstract object ExpectJson { get; }


		[U]
		protected virtual void Serializes() =>
			Expect(ExpectJson)
				.WhenSerializing(new PutMappingDescriptor<T>().AutoMap() as IPutMappingRequest);
	}
}
