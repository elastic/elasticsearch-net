using Nest;
using Tests.Core.Extensions;

namespace Tests.Core.Serialization
{
	public abstract class ExpectJsonTestBase
	{
		protected SerializationTester Tester { get; }

		protected ExpectJsonTestBase(IElasticClient client) => this.Tester = new SerializationTester(client);

		protected abstract object ExpectJson { get; }

		//TODO Validate all overrides for false whether they truly do not support deserialization
		protected virtual bool SupportsDeserialization => true;
		protected virtual bool IncludeNullInExpected => true;

		protected void RoundTripsOrSerializes<T>(T @object)
		{
			if (@object == null) return;
			if (this.ExpectJson == null) return;

			if (this.SupportsDeserialization) this.Tester.AssertRoundTrip<T>(@object, this.ExpectJson, preserveNullInExpected: this.IncludeNullInExpected);
			else this.Tester.AssertSerialize(@object, this.ExpectJson, preserveNullInExpected: this.IncludeNullInExpected);
		}

	}
}
