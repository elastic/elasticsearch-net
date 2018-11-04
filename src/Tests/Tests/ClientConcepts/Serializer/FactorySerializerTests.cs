using System;
using System.Collections.Generic;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Newtonsoft.Json;

namespace Tests.ClientConcepts.Serializer
{
	public class FactorySettingsTests
	{
		public IElasticClient CreateClient(string jsonResponse, Action<JsonSerializerSettings, IConnectionSettingsValues> settingsOverride)
		{
			var connection = new InMemoryConnection(Encoding.UTF8.GetBytes(jsonResponse));
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(connectionPool, connection, new MyCustomJsonFactory(settingsOverride))
				.DefaultIndex("default-index");
			var client = new ElasticClient(connectionSettings);
			return client;
		}

		[U] public void RespectsDateParseHandling()
		{
			var expectedDateString = "2015-02-06T23:45:05Z";
			var jsonResponse = $@"{{ ""_id"": ""1"", ""_source"": {{ ""dateString"": ""{expectedDateString}"" }}}}";
			var client = CreateClient(jsonResponse, (jsonSettings, nestSettings) => jsonSettings.DateParseHandling = DateParseHandling.None);

			var hit = client.Get<HasDateString>(1);
			hit.Should().NotBeNull();
			hit.Source.Should().NotBeNull();
			hit.Source.DateString.Should().Be(expectedDateString);
		}

		[U] public void RespectsMaxDepth()
		{
			var expectedDateString = "12-06-06 12:32:01";
			var jsonResponse = $@"{{ ""_id"": ""1"", ""_source"": {{ ""dateString"": ""{expectedDateString}"" }}}}";
			var client = CreateClient(jsonResponse, (jsonSettings, nestSettings) => jsonSettings.MaxDepth = 1);

			Action act = () => client.Get<HasDateString>(1);
			act.ShouldThrow<UnexpectedElasticsearchClientException>()
				.WithMessage("The reader's MaxDepth of 1 has been exceeded. Path '_source', line 1, position 26.");
		}

		[U] public void RespectsContractResolver()
		{
			var client = CreateClient("{}",
				(jsonSettings, nestSettings) => jsonSettings.ContractResolver = new MyCustomResolver(nestSettings, null));
			var serialized = client.Serializer.SerializeToString(new HasDateString { DateString = "1" }, SerializationFormatting.None);

			serialized.Should().Be($@"{{""DATESTRING"":""1""}}");
		}

		[U] public void DoesNotRespectDateTimeHandling()
		{
			var client = CreateClient("{}", (jsonSettings, nestSettings) => jsonSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat);
			var date = new DateTime(1999, 12, 30, 1, 2, 3);
			var serialized = client.Serializer.SerializeToString(new HasDateString { Date = date }, SerializationFormatting.None);

			serialized.Should().Be($@"{{""date"":""1999-12-30T01:02:03""}}");
		}

		public class MyCustomJsonFactory : ISerializerFactory
		{
			private readonly Action<JsonSerializerSettings, IConnectionSettingsValues> _settingsOverride;

			public MyCustomJsonFactory(Action<JsonSerializerSettings, IConnectionSettingsValues> settingsOverride) =>
				_settingsOverride = settingsOverride;

			public IElasticsearchSerializer Create(IConnectionSettingsValues settings) =>
				new LocalJsonNetSerializer(settings, _settingsOverride);

			public IElasticsearchSerializer CreateStateful(IConnectionSettingsValues settings, JsonConverter converter) =>
				new LocalJsonNetSerializer(settings, _settingsOverride, converter);
		}

		public class MyCustomResolver : ElasticContractResolver
		{
			public MyCustomResolver(IConnectionSettingsValues connectionSettings, IList<Func<Type, JsonConverter>> contractConverters) : base(
				connectionSettings, contractConverters) { }

			protected override string ResolvePropertyName(string fieldName) => fieldName.ToUpperInvariant();
		}

		private sealed class LocalJsonNetSerializer : JsonNetSerializer
		{
			public LocalJsonNetSerializer(IConnectionSettingsValues settings, Action<JsonSerializerSettings, IConnectionSettingsValues> s)
				: base(settings) => OverwriteDefaultSerializers(s);

			public LocalJsonNetSerializer(IConnectionSettingsValues settings, Action<JsonSerializerSettings, IConnectionSettingsValues> s,
				JsonConverter converter
			)
				: base(settings, converter) => OverwriteDefaultSerializers(s);
		}

		public class HasDateString
		{
			public DateTime? Date { get; set; }
			public string DateString { get; set; }
		}
	}
}
