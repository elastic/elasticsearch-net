using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Tests.Framework.Integration;

namespace Tests.Framework
{
	public abstract class SerializationTestBase
	{
		protected SerializationTestBase() => SetupSerialization();
		protected SerializationTestBase(EndpointUsage usage) { }

		protected virtual object ExpectJson { get; } = null;
		protected virtual bool SupportsDeserialization { get; set; } = true;
		protected virtual bool IncludeNullInExpected => true;

		protected DateTime FixedDate => new DateTime(2015, 06, 06, 12, 01, 02, 123);

		protected JToken ExpectedJsonJObject;

		protected Func<ConnectionSettings, ConnectionSettings> ConnectionSettingsModifier { get; set; }
		protected IPropertyMappingProvider PropertyMappingProvider { get; set; }
		protected ConnectionSettings.SourceSerializerFactory SourceSerializerFactory { get; set; }

		protected static readonly JsonSerializerSettings NullValueSettings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Include};

		protected virtual JsonSerializerSettings JsonSettingsExpected => new JsonSerializerSettings
		{
			ContractResolver = new DefaultContractResolver {NamingStrategy = new DefaultNamingStrategy()},
			NullValueHandling = IncludeNullInExpected ? NullValueHandling.Include : NullValueHandling.Ignore,
			//copied here because anonymyzing geocoordinates is too tedious
			Converters = new List<JsonConverter> { new TestGeoCoordinateJsonConverter() }
		};

		protected IElasticsearchSerializer RequestResponseSerializer => Client.ConnectionSettings.RequestResponseSerializer;

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
					_client = ConnectionSettingsModifier == null && SourceSerializerFactory == null && this.PropertyMappingProvider == null
						? TestClient.DefaultInMemoryClient
						: TestClient.GetInMemoryClientWithSourceSerializer(
							ConnectionSettingsModifier, SourceSerializerFactory, PropertyMappingProvider);
				}
				return _client;
			}
		}

		protected TObject Deserialize<TObject>(string json) =>
			RequestResponseSerializer.Deserialize<TObject>(new MemoryStream(Encoding.UTF8.GetBytes(json)));

		protected string Serialize<TObject>(TObject o)
		{
			var bytes = RequestResponseSerializer.SerializeToBytes(o);
			return Encoding.UTF8.GetString(bytes);
		}

		protected void SetupSerialization()
		{
			var o = this.ExpectJson;
			if (o == null) return;

			var expectedJsonString = JsonConvert.SerializeObject(o, Formatting.None, JsonSettingsExpected);
			this.ExpectedJsonJObject = JToken.Parse(expectedJsonString);

			if (string.IsNullOrEmpty(expectedJsonString))
				throw new ArgumentNullException(nameof(expectedJsonString));
		}

		private bool SerializesAndMatches(object o, int iteration, out string serialized)
		{
			if (this.ExpectedJsonJObject.Type != JTokenType.Array)
				return ActualMatches(o, this.ExpectedJsonJObject, iteration, out serialized);

			var jArray = this.ExpectedJsonJObject as JArray;
			serialized = this.Serialize(o);
			var lines = serialized.Split(new [] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
			var zipped = jArray.Children<JObject>().Zip(lines, (j, s) => new {j, s});
			var matches = zipped.Select((z, i) => TokenMatches(z.j, iteration, z.s, i)).ToList();
			matches.Should().OnlyContain(b => b);
			matches.Count.Should().Be(lines.Count);
			return matches.All(b => b);
		}

		private bool ActualMatches(object o, JToken expectedJson, int iteration, out string serialized)
		{
			serialized = o is string s? s : this.Serialize(o);
			return TokenMatches(expectedJson, iteration, serialized);
		}

		private static bool TokenMatches(JToken expectedJson, int iteration, string actual, int item = -1)
		{
			var actualJson = JToken.Parse(actual);
			var matches = JToken.DeepEquals(expectedJson, actualJson);
			if (matches) return true;

			(actualJson as JObject)?.DeepSort();
			(expectedJson as JObject)?.DeepSort();

			var sortedExpected = expectedJson.ToString();
			var sortedActual = actualJson.ToString();

			var message = "This is the first time I am serializing";
			if (iteration > 0)
				message = "This is the second time I am serializing, this usually indicates a problem when deserializing";

			if (item > -1) message += $". This is while comparing the {item.ToOrdinal()} item";

			sortedExpected.Diff(sortedActual, message);
			return false;
		}

		protected T AssertSerializesAndRoundTrips<T>(T o)
		{
			if (this.ExpectedJsonJObject == null) return default(T);

			var iteration = 0;
			//first serialize to string and assert it looks like this.ExpectedJson
			if (!this.SerializesAndMatches(o, iteration, out var serialized)) return default(T);

			if (!this.SupportsDeserialization) return default(T);

			//deserialize serialized json back again
			var oAgain = this.Deserialize<T>(serialized);
			//now use deserialized `o` and serialize again making sure
			//it still looks like this.ExpectedJson
			this.SerializesAndMatches(oAgain, ++iteration,out serialized);
			return oAgain;
		}

		protected object Dependant(object builtin, object source) => TestClient.Configuration.Random.SourceSerializer ? source : builtin;
	}
}
