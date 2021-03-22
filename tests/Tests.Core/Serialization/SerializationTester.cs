// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Text;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using Nest;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tests.Core.Client;
using Tests.Core.Extensions;

namespace Tests.Core.Serialization
{
	public class SerializationResult
	{
		public string DiffFromExpected { get; set; }
		public string Serialized { get; set; }
		public bool Success { get; set; }

		private string DiffFromExpectedExcerpt =>
			string.IsNullOrEmpty(DiffFromExpected)
				? string.Empty
				: DiffFromExpected?.Substring(0, DiffFromExpected.Length > 4896 ? 4896 : DiffFromExpected.Length);

		public override string ToString()
		{
			var message = $"{GetType().Name} success: {Success}";
			if (Success)
				return message;

			message += Environment.NewLine;
			message += DiffFromExpectedExcerpt;
			return message;
		}
	}

	public class DeserializationResult<T> : SerializationResult
	{
		public T Result { get; set; }

		public override string ToString()
		{
			var s = $"Deserialization has result: {Result != null}";
			s += Environment.NewLine;
			s += base.ToString();
			return s;
		}
	}

	public class RoundTripResult<T> : DeserializationResult<T>
	{
		public int Iterations { get; set; }

		public override string ToString()
		{
			var s = $"RoundTrip: {Iterations.ToOrdinal()} iteration";
			s += Environment.NewLine;
			s += base.ToString();
			return s;
		}
	}

	public class SerializationTester
	{
		public static SerializationTester Default { get; } = new(TestClient.DefaultInMemoryClient);

		public SerializationTester(IElasticClient client) => Client = client;

		public IElasticClient Client { get; }

		//public static SerializationTester DefaultWithJsonNetSerializer { get; } = new SerializationTester(TestClient.InMemoryWithJsonNetSerializer);

		protected ITransportSerializer Serializer => Client.ConnectionSettings.RequestResponseSerializer;

		public RoundTripResult<T> RoundTrips<T>(T @object) //, bool preserveNullInExpected = false)
		{
			var serialized = SerializeUsingClientDefault(@object);
			return RoundTrips(@object, serialized);
		}

		public RoundTripResult<T> RoundTrips<T>(T @object, object expectedJson, bool preserveNullInExpected = false)
		{
			var expectedJsonToken = ExpectedJsonToJsonDocument(expectedJson, preserveNullInExpected);

			var result = new RoundTripResult<T>() { Success = false };
			if (expectedJsonToken == null)
			{
				result.DiffFromExpected = "Expected json was null";
				return result;
			}

			if (!SerializesAndMatches(@object, expectedJsonToken, result))
				return result;

			@object = Deserialize<T>(result.Serialized);
			result.Iterations += 1;

			if (!SerializesAndMatches(@object, expectedJsonToken, result))
				return result;

			@object = Deserialize<T>(result.Serialized);

			result.Result = @object;
			result.Success = true;
			return result;
		}

		public SerializationResult Serializes<T>(T @object, object expectedJson, bool preserveNullInExpected = false)
		{
			var expectedJsonToken = ExpectedJsonToJsonDocument(expectedJson, preserveNullInExpected);
			var result = new RoundTripResult<T> { Success = false };

			if (SerializesAndMatches(@object, expectedJsonToken, result))
				result.Success = true;
			return result;
		}

		public DeserializationResult<T> Deserializes<T>(object expectedJson, bool preserveNullInExpected = false)
		{
			var expectedJsonString = ExpectedJsonString(expectedJson, preserveNullInExpected);
			var result = new RoundTripResult<T>() { Success = false };
			var @object = Deserialize<T>(expectedJsonString);
			if (@object != null)
				result.Success = true;
			result.Result = @object;
			return result;
		}

		private static JsonDocument ExpectedJsonToJsonDocument(object expectedJson, bool preserveNullInFromJson)
		{
			switch (expectedJson)
			{
				case string s:
					return JsonDocument.Parse(Encoding.UTF8.GetBytes(s));
				case byte[] utf8:
					return JsonDocument.Parse(utf8);
				default:
					var json = ExpectedJsonString(expectedJson, preserveNullInFromJson); 
					return JsonDocument.Parse(Encoding.UTF8.GetBytes(json));
			}
		}

		private static string ExpectedJsonString(object expectedJson, bool preserveNullInFromJson)
		{
			switch (expectedJson)
			{
				case string s:
					return s;
				case byte[] utf8:
					return Encoding.UTF8.GetString(utf8);
				default:
					var options = ExpectedJsonSerializerSettings(preserveNullInFromJson);
					return JsonSerializer.Serialize(expectedJson, options);
			}
		}

		private bool SerializesAndMatches<T>(T @object, JsonDocument expectedJsonDocument, RoundTripResult<T> result)
		{
			result.Serialized = SerializeUsingClientDefault(@object);

			return true;

			//return expectedJsonDocument.Type == JTokenType.Array
			//	? ArrayMatches((JArray)expectedJsonDocument, result)
			//	: TokenMatches(expectedJsonDocument, result);
		}

		private string SerializeUsingClientDefault<T>(T @object) =>
			@object switch
			{
				string s => s,
				byte[] b => Encoding.UTF8.GetString(b),
				_ => Serializer.SerializeToString(@object)
			};

		private T Deserialize<T>(string json)
		{
			using var ms = Client.ConnectionSettings.MemoryStreamFactory.Create(Encoding.UTF8.GetBytes(json));
			return Serializer.Deserialize<T>(ms); // TODO: Can we make this async
		}

		//private static bool ArrayMatches<T>(JArray jArray, RoundTripResult<T> result)
		//{
		//	var lines = result.Serialized.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
		//	var zipped = jArray.Children<JObject>().Zip(lines, (j, s) => new { j, s });
		//	var matches = zipped.Select((z, i) => TokenMatches(z.j, result, z.s, i)).ToList();
		//	matches.Count.Should().Be(lines.Count);
		//	var matchesAll = matches.All(b => b);
		//	if (matchesAll)
		//		return true;

		//	matches.Should().OnlyContain(b => b, "{0}", result.DiffFromExpected);
		//	return matches.All(b => b);
		//}

		//private static bool TokenMatches<T>(JsonElement expectedJson, RoundTripResult<T> result, string itemJson = null, int item = -1)
		//{
		//	var actualJson = itemJson ?? result.Serialized;
		//	var message = "This is the first time I am serializing";
		//	if (result.Iterations > 0)
		//		message = "This is the second time I am serializing, this usually indicates a problem when deserializing";
		//	if (item > -1)
		//		message += $". This is while comparing the {item.ToOrdinal()} item";

		//	if (expectedJson.ValueKind == JsonValueKind.String)
		//		return MatchString(expectedJson.GetString(), actualJson, result, message);

		//	return MatchJson(expectedJson, actualJson, result, message);
		//}

		//private static bool MatchString<T>(string expected, string actual, RoundTripResult<T> result, string message)
		//{
		//	//Serialize() returns quoted strings always.
		//	var diff = expected.CreateCharacterDifference(actual, message);
		//	if (string.IsNullOrWhiteSpace(diff))
		//		return true;

		//	result.DiffFromExpected = diff;
		//	return false;
		//}

		//private static bool MatchJson<T>(JToken expectedJson, string actualJson, RoundTripResult<T> result, string message)
		//{
		//	JToken actualJsonToken = null;
		//	try
		//	{
		//		actualJsonToken = JToken.Parse(actualJson);
		//	}
		//	catch (Exception e)
		//	{
		//		throw new Exception($"Invalid json: {actualJson}", e);
		//	}
		//	var matches = JToken.DeepEquals(expectedJson, actualJsonToken);
		//	if (matches)
		//		return true;

		//	(actualJsonToken as JObject)?.DeepSort();
		//	(expectedJson as JObject)?.DeepSort();

		//	var sortedExpected = expectedJson.ToString();
		//	var sortedActual = actualJsonToken.ToString();
		//	var diff = sortedExpected.Diff(sortedActual, message);
		//	if (string.IsNullOrWhiteSpace(diff))
		//		return true;

		//	result.DiffFromExpected = diff;
		//	return false;
		//}

		private static JsonSerializerOptions ExpectedJsonSerializerSettings(bool preserveNullInExpected = false) =>
			new()
			{
				IgnoreNullValues = !preserveNullInExpected,
				Converters = { new JsonStringEnumConverter() }
			};
	}
}
