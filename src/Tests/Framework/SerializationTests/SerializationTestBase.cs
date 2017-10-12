using System;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Framework
{
	public abstract class SerializationTestBase
	{
		protected virtual bool NoClientSerialize { get; }
		protected virtual object ExpectJson { get; }
		protected virtual bool SupportsDeserialization { get; set; } = true;

		protected DateTime FixedDate => new DateTime(2015, 06, 06, 12, 01, 02, 123);
		protected string _expectedJsonString;
		protected JToken _expectedJsonJObject;

		protected Func<ConnectionSettings, ConnectionSettings> _connectionSettingsModifier = null;
		protected Func<ConnectionSettings, IElasticsearchSerializer> _serializerFactory;
		private static readonly JsonSerializerSettings NullValueSettings = new JsonSerializerSettings {NullValueHandling = NullValueHandling.Include};

		protected IElasticsearchSerializer Serializer => Client.Serializer;

		protected virtual IElasticClient Client =>
			_connectionSettingsModifier == null && _serializerFactory == null
			? TestClient.DefaultInMemoryClient
			: TestClient.GetInMemoryClientWithSerializerFactory(_connectionSettingsModifier, _serializerFactory);

		protected SerializationTestBase()
		{
			SetupSerialization();
		}

		protected SerializationTestBase(ClusterBase cluster)
		{
		}

		protected TObject Deserialize<TObject>(string json) =>
			Serializer.Deserialize<TObject>(new MemoryStream(Encoding.UTF8.GetBytes(json)));

		protected string Serialize<TObject>(TObject o)
		{
			var bytes = Serializer.SerializeToBytes(o);
			return Encoding.UTF8.GetString(bytes);
		}

		protected void SetupSerialization()
		{
			var o = this.ExpectJson;
			if (o == null) return;

			this._expectedJsonString = this.NoClientSerialize
				? JsonConvert.SerializeObject(o, Formatting.None, NullValueSettings)
				: this.Serialize(o);
			this._expectedJsonJObject = JToken.Parse(this._expectedJsonString);

			if (string.IsNullOrEmpty(this._expectedJsonString))
				throw new ArgumentNullException(nameof(this._expectedJsonString));
		}

		private bool SerializesAndMatches(object o, int iteration, out string serialized)
		{
			if (this._expectedJsonJObject.Type != JTokenType.Array)
				return ActualMatches(o, this._expectedJsonJObject, this._expectedJsonString, iteration, out serialized);

			var jArray = this._expectedJsonJObject as JArray;
			serialized = this.Serialize(o);
			var lines = serialized.Split(new [] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
			var zipped = jArray.Children<JObject>().Zip(lines, (j, s) => new {j, s});
			var matches = zipped.Select((z, i) => this.TokenMatches(z.j, this.Serialize(z.j), iteration, z.s, i)).ToList();
			matches.Should().OnlyContain(b => b);
			matches.Count.Should().Be(lines.Count);
			return matches.All(b => b);
		}

		private bool ActualMatches(object o, JToken expectedJson, string expectedString, int iteration, out string serialized)
		{
			serialized = o is string? (string)o : this.Serialize(o);
			return TokenMatches(expectedJson, expectedString, iteration, serialized);
		}

		private bool TokenMatches(JToken expectedJson, string expectedString,int iteration, string actual, int item = -1)
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
			if (string.IsNullOrEmpty(this._expectedJsonString)) return default(T);

			int iteration = 0;
			//first serialize to string and assert it looks like this.ExpectedJson
			string serialized;
			if (!this.SerializesAndMatches(o, iteration, out serialized)) return default(T);

			if (!this.SupportsDeserialization) return default(T);

			//deserialize serialized json back again
			var oAgain = this.Deserialize<T>(serialized);
			//now use deserialized `o` and serialize again making sure
			//it still looks like this.ExpectedJson
			this.SerializesAndMatches(oAgain, ++iteration,out serialized);
			return oAgain;
		}
	}
}
