// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch;
using Tests.Core.Extensions;
using FluentAssertions;
using Elastic.Transport.Products.Elasticsearch;

namespace Tests.Core.Serialization
{
	public class JsonRoundTripper : RoundTripperBase
	{
		private readonly object _expectedJson;
		private bool _noRoundTrip;

		internal JsonRoundTripper(object expectedJson,
			Func<ElasticsearchClientSettings, ElasticsearchClientSettings> settingsModifier = null,
			ElasticsearchClientSettings.SourceSerializerFactory sourceSerializerFactory = null,
			IPropertyMappingProvider propertyMappingProvider = null,
			bool preserveNullInExpected = false
		)
			: base(settingsModifier, sourceSerializerFactory, propertyMappingProvider, preserveNullInExpected) => _expectedJson = expectedJson;

		public JsonRoundTripper NoRoundTrip()
		{
			_noRoundTrip = true;
			return this;
		}

		public T DeserializesTo<T>(Action<string, T> assert = null)
		{
			var origin = $"{nameof(JsonRoundTripper)}.{nameof(DeserializesTo)}";
			var deserializationResult = Tester.Deserializes<T>(_expectedJson, PreserveNullInExpected);
			deserializationResult.ShouldBeValid($"{origin} did not deserialize into {typeof(T).Name}");
			assert?.Invoke("first deserialization", deserializationResult.Result);
			if (_noRoundTrip)
				return deserializationResult.Result;

			var serializationResult = Tester.Serializes(deserializationResult.Result, _expectedJson, PreserveNullInExpected);
			deserializationResult.ShouldBeValid($"{origin} first serialization");
			assert?.Invoke("first deserialization", deserializationResult.Result);

			deserializationResult = Tester.Deserializes<T>(serializationResult.Serialized, PreserveNullInExpected);
			deserializationResult.ShouldBeValid($"{origin} did not deserialize into {typeof(T).Name} a 2nd time");
			assert?.Invoke("second deserialization", deserializationResult.Result);
			return deserializationResult.Result;
		}

		public void FromRequest(IElasticsearchResponse response) => ToSerializeTo(response.ApiCall.RequestBodyInBytes);

		public void FromRequest<T>(Func<ElasticsearchClient, T> call) where T : IElasticsearchResponse => FromRequest(call(Tester.Client));

		public void FromResponse(IElasticsearchResponse response) => ToSerializeTo(response.ApiCall.ResponseBodyInBytes);

		public void FromResponse<T>(Func<ElasticsearchClient, T> call) where T : IElasticsearchResponse => FromResponse(call(Tester.Client));

		private void ToSerializeTo(byte[] json)
		{
			var result = Tester.Serializes(json, _expectedJson, PreserveNullInExpected);
			result.ShouldBeValid();
		}

		public JsonRoundTripperContinuation<T> WhenSerializing<T>(T actual)
		{
			switch ((object)actual)
			{
				case string _:
					throw new Exception($"{nameof(WhenSerializing)} was passed a string but it only expects objects");
				case byte[] _:
					throw new Exception($"{nameof(WhenSerializing)} was passed a byte[] but it only expects objects");
			}
			var result = Tester.RoundTrips(actual, _expectedJson, PreserveNullInExpected);
			result.Success.Should().BeTrue(result.ToString());
			return new JsonRoundTripperContinuation<T>(result.Serialized, result.Result, Tester);
		}

		public JsonRoundTripper WhenInferringIdOn<T>(T project) where T : class
		{
			Tester.Client.Infer.Id(project).Should().Be((string)_expectedJson);
			return this;
		}

		public JsonRoundTripper WhenInferringRoutingOn<T>(T project) where T : class
		{
			Tester.Client.Infer.Routing(project).Should().Be((string)_expectedJson);
			return this;
		}

		public JsonRoundTripper ForField(Field field)
		{
			Tester.Client.Infer.Field(field).Should().Be((string)_expectedJson);
			return this;
		}

		//public JsonRoundTripper AsPropertiesOf<T>(T document) where T : class
		//{
		//	var client = Tester.Client;
		//	var settings = client.ElasticsearchClientSettings;

		//	var jo = JObject.Parse(client.RequestResponseSerializer.SerializeToString(document, settings.MemoryStreamFactory));
		//	var serializedProperties = jo.Properties().Select(p => p.Name);
		//	if (!(_expectedJson is IEnumerable<string> sut))
		//		throw new ArgumentException("Can not call AsPropertiesOf if sut is not IEnumerable<string>");

		//	sut.Should().BeEquivalentTo(serializedProperties);
		//	return this;
		//}

		public class JsonRoundTripperContinuation<T>
		{
			private readonly object _expectedJson;

			internal JsonRoundTripperContinuation(object expectedJson, T result, SerializationTester tester)
			{
				Tester = tester;
				Result = result;
				_expectedJson = expectedJson;
			}

			private T Result { get; set; }
			private SerializationTester Tester { get; }

			public JsonRoundTripperContinuation<T> WhenSerializing(T actual)
			{
				var result = Tester.RoundTrips(actual, _expectedJson);
				result.ShouldBeValid();
				Result = result.Result;
				return this;
			}

			public JsonRoundTripperContinuation<TDifferent> WhenSerializing<TDifferent>(TDifferent actual)
			{
				var result = Tester.RoundTrips(actual, _expectedJson);
				result.ShouldBeValid();
				return new JsonRoundTripperContinuation<TDifferent>(result.Serialized, result.Result, Tester);
			}

			public JsonRoundTripperContinuation<T> AssertSubject(Action<T> assert)
			{
				assert(Result);
				return this;
			}
		}
	}
}
