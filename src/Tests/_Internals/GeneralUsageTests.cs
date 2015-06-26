using System;
using Xunit;

namespace Nest.Tests.Literate
{
	public abstract class GeneralUsageTests<TInterface, TDescriptor, TInitializer> : SerializationTests
		where TDescriptor : TInterface, new()
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected abstract TInitializer Initializer { get; }
		protected abstract Func<TDescriptor, TInterface> Fluent { get; }

		protected TInterface FluentInstance { get; private set; }

		public GeneralUsageTests()
		{
			var client = this.Client();
			this.FluentInstance = this.Fluent(new TDescriptor());
		}

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings settings) => settings; 
		protected virtual IElasticClient Client() => TestClient.GetClient(ConnectionSettings); 

		[Fact] protected void SerializesInitializer() => this.AssertSerializesAndRoundTrips(this.Initializer);

		[Fact] protected void SerializesFluent() => this.AssertSerializesAndRoundTrips(this.FluentInstance);
	}
}