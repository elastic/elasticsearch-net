using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Core.Client.Settings;
using Tests.Core.Extensions;
using Tests.Domain.Extensions;

namespace Tests.Core.Serialization
{
	public abstract class RoundTripperBase
	{
		protected SerializationTester Tester { get; }

		protected bool PreserveNullInExpected { get; }

		internal RoundTripperBase(
			Func<ConnectionSettings, ConnectionSettings> settingsModifier = null,
			ConnectionSettings.SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null,
			bool preserveNullInExpected = false)
		{
			this.PreserveNullInExpected = preserveNullInExpected;
			if (settingsModifier == null && sourceSerializerFactory == null && propertyMappingProvider == null)
				this.Tester = SerializationTester.Default;
			else
			{
				var settings =
					new AlwaysInMemoryConnectionSettings(sourceSerializerFactory: sourceSerializerFactory, propertyMappingProvider: propertyMappingProvider)
						.ApplyDomainSettings();

				if (settingsModifier != null) settings = settingsModifier(settings);
				this.Tester = new SerializationTester(new ElasticClient(settings));
			}
		}
	}

	public class ObjectRoundTripper<T> : RoundTripperBase
	{
		internal ObjectRoundTripper(T @object,
			Func<ConnectionSettings, ConnectionSettings> settingsModifier = null,
			ConnectionSettings.SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null)
			: base(settingsModifier, sourceSerializerFactory, propertyMappingProvider, preserveNullInExpected: false)
		{
			_object = @object;
		}

		private readonly T _object;

		public T RoundTrips() => this.Tester.AssertRoundTrip(_object);
		public T RoundTrips(object expectedJson) => this.Tester.AssertRoundTrip(_object, expectedJson);
	}

	public class JsonRoundTripper : RoundTripperBase
	{
		internal JsonRoundTripper(object expectedJson,
			Func<ConnectionSettings, ConnectionSettings> settingsModifier = null,
			ConnectionSettings.SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null,
			bool preserveNullInExpected = false)
			: base(settingsModifier, sourceSerializerFactory, propertyMappingProvider, preserveNullInExpected)
		{
			this._expectedJson = expectedJson;
		}

		private readonly object _expectedJson;
		private bool _noRoundTrip;

	    public JsonRoundTripper NoRoundTrip()
	    {
	        this._noRoundTrip = true;
	        return this;
	    }

		public T DeserializesTo<T>(Action<string, T> assert = null)
		{
			var origin = $"{nameof(JsonRoundTripper)}.{nameof(this.DeserializesTo)}";
			var deserializationResult = this.Tester.Deserializes<T>(this._expectedJson, this.PreserveNullInExpected);
			deserializationResult.ShouldBeValid($"{origin} did not deserialize into {typeof(T).Name}");
			assert?.Invoke("first deserialization", deserializationResult.Result);
			if (this._noRoundTrip)
				return deserializationResult.Result;

			var serializationResult = this.Tester.Serializes(deserializationResult.Result, this._expectedJson, this.PreserveNullInExpected);
			deserializationResult.ShouldBeValid($"{origin} first serialization");
			assert?.Invoke("first deserialization", deserializationResult.Result);

			deserializationResult = this.Tester.Deserializes<T>(serializationResult.Serialized, this.PreserveNullInExpected);
			deserializationResult.ShouldBeValid($"{origin} did not deserialize into {typeof(T).Name} a 2nd time");
			assert?.Invoke("seond deserialization", deserializationResult.Result);
			return deserializationResult.Result;
		}

		public void FromRequest(IResponse response) => this.ToSerializeTo(response.ApiCall.RequestBodyInBytes);
		public void FromRequest<T>(Func<IElasticClient, T> call) where T : IResponse => this.FromRequest(call(this.Tester.Client));

		public void FromResponse(IResponse response) => this.ToSerializeTo(response.ApiCall.ResponseBodyInBytes);
		public void FromResponse<T>(Func<IElasticClient, T> call) where T : IResponse => this.FromResponse(call(this.Tester.Client));

		private void ToSerializeTo(byte[] json)
		{
			var result = this.Tester.Serializes(json, this._expectedJson, this.PreserveNullInExpected);
			result.ShouldBeValid();
		}

		public JsonRoundTripperContinuation<T> WhenSerializing<T>(T actual)
		{
			switch ((object)actual)
			{
				case string s: throw new Exception($"{nameof(this.WhenSerializing)} was passed a string but it only expects objects");
				case byte[] b: throw new Exception($"{nameof(this.WhenSerializing)} was passed a byte[] but it only expects objects");
			}
			var result = this.Tester.RoundTrips(actual, this._expectedJson, this.PreserveNullInExpected);
			result.Success.Should().BeTrue(result.ToString());
			return new JsonRoundTripperContinuation<T>(result.Serialized, result.Result, this.Tester);
		}

		public JsonRoundTripper WhenInferringIdOn<T>(T project) where T : class
		{
			this.Tester.Client.Infer.Id(project).Should().Be((string)this._expectedJson);
			return this;
		}
		public JsonRoundTripper WhenInferringRoutingOn<T>(T project) where T : class
		{
			this.Tester.Client.Infer.Routing<T>(project).Should().Be((string)this._expectedJson);
			return this;
		}

		public JsonRoundTripper ForField(Field field)
		{
			this.Tester.Client.Infer.Field(field).Should().Be((string)this._expectedJson);
			return this;
		}

		public JsonRoundTripper AsPropertiesOf<T>(T document) where T : class
		{
			var jo = JObject.Parse(this.Tester.Client.RequestResponseSerializer.SerializeToString(document));
			var serializedProperties = jo.Properties().Select(p => p.Name);
			if (!(this._expectedJson is IEnumerable<string> sut))
				throw new ArgumentException("Can not call AsPropertiesOf if sut is not IEnumerable<string>");

			sut.Should().BeEquivalentTo(serializedProperties);
			return this;
		}

		public class JsonRoundTripperContinuation<T>
		{
			internal JsonRoundTripperContinuation(object expectedJson, T result, SerializationTester tester)
			{
				this.Tester = tester;
				this.Result = result;
				this._expectedJson = expectedJson;
			}

			private readonly object _expectedJson;
			private T Result { get; set; }
			private SerializationTester Tester { get; }

			public JsonRoundTripperContinuation<T> WhenSerializing(T actual)
			{
				var result = this.Tester.RoundTrips(actual, this._expectedJson);
				result.ShouldBeValid();
				this.Result = result.Result;
				return this;
			}

			public JsonRoundTripperContinuation<TDifferent> WhenSerializing<TDifferent>(TDifferent actual)
			{
				var result = this.Tester.RoundTrips<TDifferent>(actual, expectedJson: this._expectedJson);
				result.ShouldBeValid();
				return new JsonRoundTripperContinuation<TDifferent>(result.Serialized, result.Result, this.Tester);
			}

			public JsonRoundTripperContinuation<T> AssertSubject(Action<T> assert)
			{
				assert(this.Result);
				return this;
			}
		}
	}
}
