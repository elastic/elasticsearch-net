using System;
using Elastic.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.Client;
using Tests.Core.Serialization;

namespace Tests.Framework
{
	public abstract class UsageTestBase<TInterface, TDescriptor, TInitializer> : ExpectJsonTestBase
		where TDescriptor : TInterface, new()
		where TInitializer : class, TInterface
		where TInterface : class
	{
		private TInterface FluentInstance { get; }
		protected abstract TInitializer Initializer { get; }
		protected abstract Func<TDescriptor, TInterface> Fluent { get; }

		protected virtual bool TestObjectInitializer => true;

		protected UsageTestBase() : base(TestClient.DefaultInMemoryClient) =>
			FluentInstance = Fluent(new TDescriptor());

		[U] protected void SerializesInitializer()
		{
			if (TestObjectInitializer) RoundTripsOrSerializes<TInterface>(Initializer);
		}

		[U] protected void SerializesFluent() => RoundTripsOrSerializes(FluentInstance);
	}

	public abstract class PromiseUsageTestBase<TInterface, TDescriptor, TInitializer> : ExpectJsonTestBase
		where TDescriptor : IPromise<TInterface>, new()
		where TInitializer : class, TInterface
		where TInterface : class
	{
		private TInterface FluentInstance { get; }
		protected abstract TInitializer Initializer { get; }
		protected abstract Func<TDescriptor, IPromise<TInterface>> Fluent { get; }

		protected PromiseUsageTestBase() : base(TestClient.DefaultInMemoryClient) =>
			FluentInstance = Fluent(new TDescriptor())?.Value;

		[U] protected void SerializesInitializer() => Tester.RoundTrips<TInterface>(Initializer, ExpectJson);

		[U] protected void SerializesFluent() => Tester.RoundTrips(FluentInstance, ExpectJson);
	}
}
