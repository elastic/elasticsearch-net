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

namespace Tests.Framework
{
	public class RoundTripper : SerializationTestBase
	{
		protected override bool NoClientSerializeOfExpected => true;
		protected override object ExpectJson { get; }

		internal RoundTripper(object expected,
			Func<ConnectionSettings, ConnectionSettings> settings = null,
			IElasticsearchSerializer sourceSerializer = null,
			IPropertyMappingProvider propertyMappingProvider = null)
		{
			this.ExpectJson = expected;
			this._connectionSettingsModifier = settings;

			this._expectedJsonString = JsonConvert.SerializeObject(expected, NullValueSettings);
			this._expectedJsonJObject = JToken.Parse(this._expectedJsonString);
			this._sourceSerializer = sourceSerializer;
			this._propertyMappingProvider = propertyMappingProvider;
		}

		public virtual void DeserializesTo<T>(Action<string, T> assert)
		{
			var json = (this.ExpectJson is string)
				? (string) ExpectJson
				: JsonConvert.SerializeObject(this.ExpectJson, NullValueSettings);

			T sut;
			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
				sut = this.Client.Serializer.Deserialize<T>(stream);
			sut.Should().NotBeNull();
			assert("first deserialization", sut);

			var serialized = this.Client.Serializer.SerializeToString(sut);
			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(serialized)))
				sut = this.Client.Serializer.Deserialize<T>(stream);
			sut.Should().NotBeNull();
			assert("second deserialization", sut);
		}

		public void FromRequest(IResponse response) => ToSerializeTo(response.ApiCall.RequestBodyInBytes);
		public void FromRequest<T>(Func<IElasticClient, T> call) where T : IResponse => FromRequest(call(this.Client));
		public void FromResponse(IResponse response) => ToSerializeTo(response.ApiCall.ResponseBodyInBytes);
		public void FromResponse<T>(Func<IElasticClient, T> call) where T : IResponse => FromResponse(call(this.Client));
		public void ToSerializeTo(byte[] json) => ToSerializeTo(Encoding.UTF8.GetString(json));
		public void ToSerializeTo(string json)
		{
			if (this._expectedJsonJObject.Type != JTokenType.Array)
				CompareToken(json, JToken.FromObject(this.ExpectJson));
			else
				CompareMultiJson(json);
		}

		private void CompareMultiJson(string json)
		{
			var jArray = this._expectedJsonJObject as JArray;
			var lines = json.Split(new [] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
			var zipped = jArray.Children<JObject>().Zip(lines, (j, s) => new {j, s}).ToList();
			foreach(var t in zipped)
				CompareToken(t.s, t.j);
			zipped.Count.Should().Be(lines.Count);
		}

		private void CompareToken(string json, JToken expected)
		{
			var actual = JToken.Parse(json);
			var sameJson = JToken.DeepEquals(expected, actual);
			if (sameJson) return;
			expected.ToString().Diff(actual.ToString(), "Expected serialization differs:");
		}

		public virtual RoundTripper<T> WhenSerializing<T>(T actual)
		{
			var sut = this.AssertSerializesAndRoundTrips(actual);
			return new RoundTripper<T>(this.ExpectJson, sut);
		}

		public RoundTripper WhenInferringIdOn<T>(T project) where T : class
		{
			this.Client.Infer.Id<T>(project).Should().Be((string)this.ExpectJson);
			return this;
		}

		public RoundTripper ForField(Field field)
		{
			this.Client.Infer.Field(field).Should().Be((string)this.ExpectJson);
			return this;
		}

		public RoundTripper AsPropertiesOf<T>(T document) where T : class
		{
			var jo = JObject.Parse(this.Serialize(document));
			var serializedProperties = jo.Properties().Select(p => p.Name);
			var sut = this.ExpectJson as IEnumerable<string>;
			if (sut == null) throw new ArgumentException("Can not call AsPropertiesOf if sut is not IEnumerable<string>");

			sut.Should().BeEquivalentTo(serializedProperties);
			return this;
		}

	    public RoundTripper NoRoundTrip()
	    {
	        this.SupportsDeserialization = false;
	        return this;
	    }

		public static IntermediateChangedSettings WithConnectionSettings(Func<ConnectionSettings, ConnectionSettings> settings) =>
			new IntermediateChangedSettings(settings);

		public static IntermediateChangedSettings WithSourceSerializer(IElasticsearchSerializer serializer) =>
			new IntermediateChangedSettings(s=>s.EnableDebugMode()).WithSourceSerializer(serializer);

		public static RoundTripper Expect(object expected) =>  new RoundTripper(expected);

	}

	public class IntermediateChangedSettings
	{
		private readonly Func<ConnectionSettings, ConnectionSettings> _connectionSettingsModifier;
		private IElasticsearchSerializer _sourceSerializer;
		private IPropertyMappingProvider _propertyMappingProvider;

		internal IntermediateChangedSettings(Func<ConnectionSettings, ConnectionSettings> settings)
		{
			this._connectionSettingsModifier = settings;
		}
		public IntermediateChangedSettings WithSourceSerializer(IElasticsearchSerializer sourceSerializer)
		{
			this._sourceSerializer = sourceSerializer;
			return this;
		}
		public IntermediateChangedSettings WithPropertyMappingProvider(IPropertyMappingProvider propertyMappingProvider)
		{
			this._propertyMappingProvider = propertyMappingProvider;
			return this;
		}

		public RoundTripper Expect(object expected) =>
			new RoundTripper(expected, _connectionSettingsModifier, this._sourceSerializer, this._propertyMappingProvider);
	}

	public class RoundTripper<T> : RoundTripper
	{
		protected T Sut { get; set;  }

		internal RoundTripper(object expected, T sut) : base(expected)
		{
			this.Sut = sut;
		}

		public RoundTripper<T> WhenSerializing(T actual)
		{
			Sut = this.AssertSerializesAndRoundTrips(actual);
			return this;
		}

		public RoundTripper<T> Result(Action<T> assert)
		{
			assert(this.Sut);
			return this;
		}

		public RoundTripper<T> Result<TOther>(Action<TOther> assert)
			where TOther : T
		{
			assert((TOther)this.Sut);
			return this;
		}
	}
}
