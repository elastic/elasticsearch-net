using Elastic.Xunit.XunitPlumbing;
using Nest;
using static Tests.Core.Serialization.SerializationTestHelper;

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
