using System;
using Nest;

namespace Tests.Framework
{
	public abstract class UsageTestBase<TInterface, TDescriptor, TInitializer> : SerializationTestBase
		where TDescriptor : TInterface, new()
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected abstract TInitializer Initializer { get; }
		protected abstract Func<TDescriptor, TInterface> Fluent { get; }

		protected TInterface FluentInstance { get; private set; }

		protected UsageTestBase()
		{
			this.FluentInstance = this.Fluent(new TDescriptor());
		}

		[U] protected void SerializesInitializer() =>
			this.AssertSerializesAndRoundTrips<TInterface>(this.Initializer);

		[U] protected void SerializesFluent() =>
			this.AssertSerializesAndRoundTrips(this.FluentInstance);
	}

	public abstract class PromiseUsageTestBase<TInterface, TDescriptor, TInitializer> : SerializationTestBase
		where TDescriptor : IPromise<TInterface>, new()
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected abstract TInitializer Initializer { get; }
		protected abstract Func<TDescriptor, IPromise<TInterface>> Fluent { get; }

		protected TInterface FluentInstance { get; private set; }

		protected PromiseUsageTestBase()
		{
			this.FluentInstance = this.Fluent(new TDescriptor())?.Value;
		}

		[U] protected void SerializesInitializer() =>
			this.AssertSerializesAndRoundTrips<TInterface>(this.Initializer);

		[U] protected void SerializesFluent() =>
			this.AssertSerializesAndRoundTrips(this.FluentInstance);
	}
}
