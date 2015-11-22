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
			var client = this.Client();
			this.FluentInstance = this.Fluent(new TDescriptor());
		}

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings settings) => settings; 
		protected virtual IElasticClient Client() => TestClient.GetClient(ConnectionSettings); 

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
			var client = this.Client();
			this.FluentInstance = this.Fluent(new TDescriptor())?.Value;
		}

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings settings) => settings; 
		protected virtual IElasticClient Client() => TestClient.GetClient(ConnectionSettings); 

		[U] protected void SerializesInitializer() => 
			this.AssertSerializesAndRoundTrips<TInterface>(this.Initializer);

		[U] protected void SerializesFluent() => 
			this.AssertSerializesAndRoundTrips(this.FluentInstance);
	}
}