using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.MockData;

namespace Tests.Framework
{
	public class RoundTripper
	{
		private readonly IElasticClient _client;
		protected readonly SerializationTester _serializationTester;
		protected readonly object _objectUnderTest;
		private bool _noRoundTrip = false;
		private bool PreserveNullInExpected { get; }

		internal RoundTripper(object objectUnderTest,
			Func<ConnectionSettings, ConnectionSettings> settingsModifier = null,
			ConnectionSettings.SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null,
			bool preserveNullInExpected = false)
		{
			this.PreserveNullInExpected = preserveNullInExpected;
			if (settingsModifier == null && sourceSerializerFactory == null && propertyMappingProvider == null)
				_client = TestClient.DefaultInMemoryClient;
			else
			{
				ConnectionSettings settings =
					new AlwaysInMemoryConnectionSettings(sourceSerializerFactory: sourceSerializerFactory, propertyMappingProvider: propertyMappingProvider)
						.ApplyDomainSettings();

				if (settingsModifier != null) settings = settingsModifier(settings);
				// ReSharper disable once PossibleMultipleWriteAccessInDoubleCheckLocking
				_client = new ElasticClient(settings);
			}
			this._serializationTester = new SerializationTester(_client);

			this._objectUnderTest = objectUnderTest;
		}
		public T DeserializesTo<T>()
		{
			var origin = $"{nameof(RoundTripper)}.{nameof(RoundTripper.DeserializesTo)}";
			var deserializationResult = this._serializationTester.Deserializes<T>(this._objectUnderTest, this.PreserveNullInExpected);
			deserializationResult.ShouldBeValid($"{origin} did not deserialize into {typeof(T).Name}");
			if (this._noRoundTrip)
				return deserializationResult.Result;

			var serializationResult = this._serializationTester.Serializes(deserializationResult.Result, this._objectUnderTest, this.PreserveNullInExpected);
			deserializationResult.ShouldBeValid($"{origin} first serialization");

			deserializationResult = this._serializationTester.Deserializes<T>(serializationResult.Serialized, this.PreserveNullInExpected);
			deserializationResult.ShouldBeValid($"{origin} did not deserialize into {typeof(T).Name} a 2nd time");

			return deserializationResult.Result;
		}

		public void DeserializesTo<T>(Action<string, T> assert)
		{
			var origin = $"{nameof(RoundTripper)}.{nameof(RoundTripper.DeserializesTo)}";
			var deserializationResult = this._serializationTester.Deserializes<T>(this._objectUnderTest, this.PreserveNullInExpected);
			deserializationResult.ShouldBeValid($"{origin} did not deserialize into {typeof(T).Name}");
			assert("first deserialization", deserializationResult.Result);

			var serializationResult = this._serializationTester.Serializes(deserializationResult.Result, this._objectUnderTest, this.PreserveNullInExpected);
			deserializationResult.ShouldBeValid($"{origin} first serialization");
			assert("first deserialization", deserializationResult.Result);

			deserializationResult = this._serializationTester.Deserializes<T>(serializationResult.Serialized, this.PreserveNullInExpected);
			deserializationResult.ShouldBeValid($"{origin} did not deserialize into {typeof(T).Name} a 2nd time");
			assert("seond deserialization", deserializationResult.Result);
		}

		public void FromRequest(IResponse response) => ToSerializeTo(response.ApiCall.RequestBodyInBytes);
		public void FromRequest<T>(Func<IElasticClient, T> call) where T : IResponse => FromRequest(call(this._client));
		public void FromResponse(IResponse response) => ToSerializeTo(response.ApiCall.ResponseBodyInBytes);
		public void FromResponse<T>(Func<IElasticClient, T> call) where T : IResponse => FromResponse(call(this._client));
		private void ToSerializeTo(byte[] json)
		{
			var result = this._serializationTester.Serializes(json, this._objectUnderTest, this.PreserveNullInExpected);
			result.ShouldBeValid();
		}

		public virtual RoundTripper<T> WhenSerializing<T>(T actual)
		{
			switch ((object)actual)
			{
				case string s: throw new Exception($"{nameof(WhenSerializing)} was passed a string but it only expects objects");
				case byte[] b: throw new Exception($"{nameof(WhenSerializing)} was passed a byte[] but it only expects objects");
			}
			var result = this._serializationTester.RoundTrips(actual, this._objectUnderTest, this.PreserveNullInExpected);
			result.Success.Should().BeTrue(result.ToString());
			return new RoundTripper<T>(result.Serialized, result.Result);
		}

		public RoundTripper WhenInferringIdOn<T>(T project) where T : class
		{
			this._client.Infer.Id(project).Should().Be((string)this._objectUnderTest);
			return this;
		}
		public RoundTripper WhenInferringRoutingOn<T>(T project) where T : class
		{
			this._client.Infer.Routing<T>(project).Should().Be((string)this._objectUnderTest);
			return this;
		}

		public RoundTripper ForField(Field field)
		{
			this._client.Infer.Field(field).Should().Be((string)this._objectUnderTest);
			return this;
		}

		public RoundTripper AsPropertiesOf<T>(T document) where T : class
		{
			var jo = JObject.Parse(this._client.RequestResponseSerializer.SerializeToString(document));
			var serializedProperties = jo.Properties().Select(p => p.Name);
			if (!(this._objectUnderTest is IEnumerable<string> sut))
				throw new ArgumentException("Can not call AsPropertiesOf if sut is not IEnumerable<string>");

			sut.Should().BeEquivalentTo(serializedProperties);
			return this;
		}

	    public RoundTripper NoRoundTrip()
	    {
	        this._noRoundTrip = true;
	        return this;
	    }

		public static IntermediateChangedSettings WithConnectionSettings(Func<ConnectionSettings, ConnectionSettings> settings) =>
			new IntermediateChangedSettings(settings);

		public static IntermediateChangedSettings WithSourceSerializer(ConnectionSettings.SourceSerializerFactory factory) =>
			new IntermediateChangedSettings(s=>s.EnableDebugMode()).WithSourceSerializer(factory);

		public static RoundTripper Expect(object expected, bool preserveNullInExpected = false) => new RoundTripper(expected, preserveNullInExpected: preserveNullInExpected);

		public static ObjectRoundTripper<T> Object<T>(T expected) => new ObjectRoundTripper<T>(expected);
	}

	public class ObjectRoundTripper<T>
	{
		private readonly T _object;

		public ObjectRoundTripper(T @object) => _object = @object;

		public T RoundTrips() => SerializationTester.Default.AssertRoundTrip(_object);
		public T RoundTrips(object expectedJson) => SerializationTester.Default.AssertRoundTrip(_object, expectedJson);
	}

	public class IntermediateChangedSettings
	{
		private readonly Func<ConnectionSettings, ConnectionSettings> _connectionSettingsModifier;
		private ConnectionSettings.SourceSerializerFactory _sourceSerializerFactory;
		private IPropertyMappingProvider _propertyMappingProvider;

		internal IntermediateChangedSettings(Func<ConnectionSettings, ConnectionSettings> settings)
		{
			this._connectionSettingsModifier = settings;
		}
		public IntermediateChangedSettings WithSourceSerializer(ConnectionSettings.SourceSerializerFactory factory)
		{
			this._sourceSerializerFactory = factory;
			return this;
		}
		public IntermediateChangedSettings WithPropertyMappingProvider(IPropertyMappingProvider propertyMappingProvider)
		{
			this._propertyMappingProvider = propertyMappingProvider;
			return this;
		}

		public RoundTripper Expect(object expected, bool preserveNullInExpected = false) =>
			new RoundTripper(expected, _connectionSettingsModifier, this._sourceSerializerFactory, this._propertyMappingProvider, preserveNullInExpected);
	}

	public class RoundTripper<T> : RoundTripper
	{
		protected T Sut { get; set;  }

		internal RoundTripper(object objectUnderTest, T sut) : base(objectUnderTest)
		{
			this.Sut = sut;
		}

		public RoundTripper<T> WhenSerializing(T actual)
		{
			var result = this._serializationTester.RoundTrips(actual, this._objectUnderTest);
			result.ShouldBeValid();
			Sut = result.Result;
			return this;
		}

		public RoundTripper<T> AssertSubject(Action<T> assert)
		{
			assert(this.Sut);
			return this;
		}
	}
}
