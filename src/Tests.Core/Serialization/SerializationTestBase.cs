using System;
using Nest;
using Tests.Framework.ManagedElasticsearch;

namespace Tests.Framework
{
	public abstract class SerializationTestBase
	{
		public SerializationTester Tester { get; }

		protected SerializationTestBase(IElasticClient client) => this.Tester = new SerializationTester(client);

		protected virtual object ExpectJson { get; } = null;
		//TODO Validate all overrides for false whether they truly do not support deserialization
		protected virtual bool SupportsDeserialization => true;
		protected virtual bool IncludeNullInExpected => true;

		protected DateTime FixedDate => new DateTime(2015, 06, 06, 12, 01, 02, 123);

		protected Func<ConnectionSettings, ConnectionSettings> ConnectionSettingsModifier { get; set; }
		protected IPropertyMappingProvider PropertyMappingProvider { get; set; }
		protected ConnectionSettings.SourceSerializerFactory SourceSerializerFactory { get; set; }

		private readonly object _clientLock = new object();
		private volatile IElasticClient _client;

		public virtual IElasticClient Client
		{
			get
			{
				if (_client != null) return _client;
				lock (_clientLock)
				{
					if (_client != null) return _client;
					if (ConnectionSettingsModifier == null && SourceSerializerFactory == null && this.PropertyMappingProvider == null)
						_client = TestClient.DefaultInMemoryClient;
					else
					{
						ConnectionSettings settings = new AlwaysInMemoryConnectionSettings(sourceSerializerFactory: SourceSerializerFactory, propertyMappingProvider: PropertyMappingProvider);
						if (ConnectionSettingsModifier != null) settings = ConnectionSettingsModifier(settings);
						// ReSharper disable once PossibleMultipleWriteAccessInDoubleCheckLocking
						_client = new ElasticClient(settings);
					}
				}
				return _client;
			}
		}
		protected void RoundTripsOrSerializes<T>(T @object)
		{
			if (@object == null) return;
			if (this.ExpectJson == null) return;
			if (this.SupportsDeserialization) this.Tester.AssertRoundTrip<T>(@object, this.ExpectJson, preserveNullInExpected: IncludeNullInExpected);
			this.Tester.AssertSerialize(@object, this.ExpectJson, preserveNullInExpected: IncludeNullInExpected);
		}

		protected object Dependant(object builtin, object source) => TestClient.Configuration.Random.SourceSerializer ? source : builtin;
	}
}
