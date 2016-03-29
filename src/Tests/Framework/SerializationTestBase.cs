using System;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;

namespace Tests.Framework
{
	public abstract class SerializationTestBase
	{
		protected virtual object ExpectJson { get; }
		protected virtual bool SupportsDeserialization => true;

		protected string _expectedJsonString;
		protected JToken _expectedJsonJObject;
		protected Func<ConnectionSettings, ConnectionSettings> _connectionSettingsModifier = null;
		protected Func<ConnectionSettings, IElasticsearchSerializer> _serializerFactory;

		protected SerializationTestBase()
		{
			SetupSerialization();
		}

		protected void SetupSerialization()
		{
			var o = this.ExpectJson;
			if (o == null) return;

			this._expectedJsonString = this.Serialize(o);
			this._expectedJsonJObject = JToken.Parse(this._expectedJsonString);

			if (string.IsNullOrEmpty(this._expectedJsonString))
				throw new ArgumentNullException(nameof(this._expectedJsonString));
		}

		protected DateTime FixedDate => new DateTime(2015, 06, 06, 12, 01, 02, 123);

		protected void ShouldBeEquivalentTo(string serialized) =>
			serialized.Should().BeEquivalentTo(_expectedJsonString);

		private bool SerializesAndMatches(object o, int iteration, out string serialized)
		{
			//multi
			if (this._expectedJsonJObject.Type == JTokenType.Array)
			{
				var jArray = this._expectedJsonJObject as JArray;
				serialized = this.Serialize(o);
				var lines = serialized.Split(new [] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
				var zipped = jArray.Children<JObject>().Zip(lines, (j, s) => new {j, s});
				var matches = zipped.Select(z => this.TokenMatches(z.j, this.Serialize(z.j), iteration, z.s)).ToList();
				matches.Should().OnlyContain(b => b);
				matches.Count.Should().Be(lines.Count);
				return matches.All(b => b);

			}
			return ActualMatches(o, this._expectedJsonJObject, this._expectedJsonString, iteration, out serialized);
		}

		private bool ActualMatches(object o, JToken expectedJson, string expectedString, int iteration, out string serialized)
		{
			serialized = this.Serialize(o);
			return TokenMatches(expectedJson, expectedString, iteration, serialized);
		}

		private bool TokenMatches(JToken expectedJson, string expectedString,int iteration, string actual)
		{
			var actualJson = JToken.Parse(actual);
			var matches = JToken.DeepEquals(expectedJson, actualJson);
			if (matches) return true;

			Sort(actualJson as JObject);
			Sort(expectedJson as JObject);

			var sortedExpected = expectedJson.ToString();
			var sortedActual = actualJson.ToString();

			var message = "This is the first time I am serializing";
			if (iteration > 0)
				message = "This is the second time I am serializing, this usually indicates a problem when deserializing";

			sortedExpected.Diff(sortedActual, message);
			return false;
		}

		private void Sort(JObject jObj)
		{
			if (jObj == null) return;

			var props = jObj.Properties().ToList();
			foreach (var prop in props)
			{
				prop.Remove();
			}

			foreach (var prop in props.OrderBy(p => p.Name))
			{
				jObj.Add(prop);
				var o = prop.Value as JObject;
				if (o != null)
					Sort(o);
			}
		}

		protected TObject Deserialize<TObject>(string json) =>
			GetSerializer().Deserialize<TObject>(new MemoryStream(Encoding.UTF8.GetBytes(json)));

		protected string Serialize<TObject>(TObject o)
		{
			var bytes = GetSerializer().SerializeToBytes(o);
			return Encoding.UTF8.GetString(bytes);
		}

		protected IElasticsearchSerializer GetSerializer() => GetClient().Serializer;

		protected IElasticClient GetClient() => TestClient.GetInMemoryClient(_connectionSettingsModifier, _serializerFactory);

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
