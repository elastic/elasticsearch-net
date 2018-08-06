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
			this.FluentInstance = this.Fluent(new TDescriptor());

		[U] protected void SerializesInitializer()
		{
			if (this.TestObjectInitializer) this.RoundTripsOrSerializes<TInterface>(this.Initializer);
		}

		[U] protected void SerializesFluent() => this.RoundTripsOrSerializes(this.FluentInstance);
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
			this.FluentInstance = this.Fluent(new TDescriptor())?.Value;

		[U] protected void SerializesInitializer() => this.Tester.RoundTrips<TInterface>(this.Initializer, this.ExpectJson);

		[U] protected void SerializesFluent() => this.Tester.RoundTrips(this.FluentInstance, this.ExpectJson);
	}
}
