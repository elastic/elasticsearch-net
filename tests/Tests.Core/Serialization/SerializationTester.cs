using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using Nest;
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
		public SerializationTester(IElasticClient client) => Client = client;
		// TODO: This needs a fair bit of cleanup and refactoring. Code is hacked for initial testing using STJ

		public static SerializationTester Default { get; } = new(TestClient.DefaultInMemoryClient);

		public IElasticClient Client { get; }

		//public static SerializationTester DefaultWithJsonNetSerializer { get; } = new SerializationTester(TestClient.InMemoryWithJsonNetSerializer);

		protected ITransportSerializer Serializer => Client.ElasticsearchClientSettings.RequestResponseSerializer;

		public RoundTripResult<T> RoundTrips<T>(T @object) //, bool preserveNullInExpected = false)
		{
			var serialized = SerializeUsingClientDefault(@object);
			return RoundTrips(@object, serialized);
		}

		public RoundTripResult<T> RoundTrips<T>(T @object, object expectedJson, bool preserveNullInExpected = false)
		{
			using var expectedJsonToken = ExpectedJsonToJsonDocument(expectedJson, preserveNullInExpected);

			var result = new RoundTripResult<T>() {Success = false};
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
			var result = new RoundTripResult<T> {Success = false};

			if (SerializesAndMatches(@object, expectedJsonToken, result))
				result.Success = true;
			return result;
		}

		public DeserializationResult<T> Deserializes<T>(object expectedJson, bool preserveNullInExpected = false)
		{
			var expectedJsonString = ExpectedJsonString(expectedJson, preserveNullInExpected);
			var result = new RoundTripResult<T>() {Success = false};
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


			return TokenMatches(expectedJsonDocument, result);

			//return expectedJsonDocument.RootElement.ValueKind == JsonValueKind.Array
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
			using var ms = Client.ElasticsearchClientSettings.MemoryStreamFactory.Create(Encoding.UTF8.GetBytes(json));
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

		private static bool TokenMatches<T>(JsonDocument expectedJson, RoundTripResult<T> result,
			string itemJson = null, int item = -1)
		{
			var actualJson = itemJson ?? result.Serialized;
			var message = "This is the first time I am serializing";
			if (result.Iterations > 0)
				message =
					"This is the second time I am serializing, this usually indicates a problem when deserializing";
			if (item > -1)
				message += $". This is while comparing the {item.ToOrdinal()} item";

			//if (expectedJson.RootElement.ValueKind == JsonValueKind.String)
			//	return MatchString(expectedJson.GetString(), actualJson, result, message);

			return MatchJson(expectedJson, actualJson, result, message);
		}

		private static bool MatchString<T>(string expected, string actual, RoundTripResult<T> result, string message)
		{
			//Serialize() returns quoted strings always.
			var diff = expected.CreateCharacterDifference(actual, message);
			if (string.IsNullOrWhiteSpace(diff))
				return true;

			result.DiffFromExpected = diff;
			return false;
		}

		private static bool MatchJson<T>(JsonDocument expectedJson, string actualJson, RoundTripResult<T> result,
			string message)
		{
			var comparer = new JsonElementComparer(); // private field

			using var doc2 = JsonDocument.Parse(actualJson);

			return comparer.Equals(expectedJson.RootElement, doc2.RootElement);
		}

		private static JsonSerializerOptions ExpectedJsonSerializerSettings(bool preserveNullInExpected = false) =>
			new() {IgnoreNullValues = !preserveNullInExpected, Converters = {new JsonStringEnumConverter()}};

		/// <summary>
		///     TEMP: Borrowed from
		///     https://stackoverflow.com/questions/60580743/what-is-equivalent-in-jtoken-deepequal-in-system-text-json
		/// </summary>
		public class JsonElementComparer : IEqualityComparer<JsonElement>
		{
			public JsonElementComparer() : this(-1) { }

			public JsonElementComparer(int maxHashDepth) => MaxHashDepth = maxHashDepth;

			private int MaxHashDepth { get; }

			public bool Equals(JsonElement x, JsonElement y)
			{
				if (x.ValueKind != y.ValueKind)
					return false;
				switch (x.ValueKind)
				{
					case JsonValueKind.Null:
					case JsonValueKind.True:
					case JsonValueKind.False:
					case JsonValueKind.Undefined:
						return true;

					// Compare the raw values of numbers, and the text of strings.
					// Note this means that 0.0 will differ from 0.00 -- which may be correct as deserializing either to `decimal` will result in subtly different results.
					// Newtonsoft's JValue.Compare(JTokenType valueType, object? objA, object? objB) has logic for detecting "equivalent" values, 
					// you may want to examine it to see if anything there is required here.
					// https://github.com/JamesNK/Newtonsoft.Json/blob/master/Src/Newtonsoft.Json/Linq/JValue.cs#L246
					case JsonValueKind.Number:
						return x.GetRawText() == y.GetRawText();

					case JsonValueKind.String:
						return
							x.GetString() ==
							y.GetString(); // Do not use GetRawText() here, it does not automatically resolve JSON escape sequences to their corresponding characters.

					case JsonValueKind.Array:
						return x.EnumerateArray().SequenceEqual(y.EnumerateArray(), this);

					case JsonValueKind.Object:
					{
						// Surprisingly, JsonDocument fully supports duplicate property names.
						// I.e. it's perfectly happy to parse {"Value":"a", "Value" : "b"} and will store both
						// key/value pairs inside the document!
						// A close reading of https://tools.ietf.org/html/rfc8259#section-4 seems to indicate that
						// such objects are allowed but not recommended, and when they arise, interpretation of 
						// identically-named properties is order-dependent.  
						// So stably sorting by name then comparing values seems the way to go.
						var xPropertiesUnsorted = x.EnumerateObject().ToList();
						var yPropertiesUnsorted = y.EnumerateObject().ToList();
						if (xPropertiesUnsorted.Count != yPropertiesUnsorted.Count)
							return false;
						var xProperties = xPropertiesUnsorted.OrderBy(p => p.Name, StringComparer.Ordinal);
						var yProperties = yPropertiesUnsorted.OrderBy(p => p.Name, StringComparer.Ordinal);
						foreach (var (px, py) in xProperties.Zip(yProperties, (n, c) => (n, c)))
						{
							if (px.Name != py.Name)
								return false;
							if (!Equals(px.Value, py.Value))
								return false;
						}

						return true;
					}

					default:
						throw new JsonException($"Unknown JsonValueKind {x.ValueKind}");
				}
			}

			public int GetHashCode(JsonElement obj)
			{
				var
					hash = new HashCode(); // New in .Net core: https://docs.microsoft.com/en-us/dotnet/api/system.hashcode
				ComputeHashCode(obj, ref hash, 0);
				return hash.ToHashCode();
			}

			private void ComputeHashCode(JsonElement obj, ref HashCode hash, int depth)
			{
				hash.Add(obj.ValueKind);

				switch (obj.ValueKind)
				{
					case JsonValueKind.Null:
					case JsonValueKind.True:
					case JsonValueKind.False:
					case JsonValueKind.Undefined:
						break;

					case JsonValueKind.Number:
						hash.Add(obj.GetRawText());
						break;

					case JsonValueKind.String:
						hash.Add(obj.GetString());
						break;

					case JsonValueKind.Array:
						if (depth != MaxHashDepth)
						{
							foreach (var item in obj.EnumerateArray())
								ComputeHashCode(item, ref hash, depth + 1);
						}
						else
							hash.Add(obj.GetArrayLength());

						break;

					case JsonValueKind.Object:
						foreach (var property in obj.EnumerateObject().OrderBy(p => p.Name, StringComparer.Ordinal))
						{
							hash.Add(property.Name);
							if (depth != MaxHashDepth)
								ComputeHashCode(property.Value, ref hash, depth + 1);
						}

						break;

					default:
						throw new JsonException($"Unknown JsonValueKind {obj.ValueKind}");
				}
			}
		}
	}
}
