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
	/// <summary>
	/// Here we get into a bind because our constructor runs too late
	/// </summary>
	public class CustomConstructorSerializerSettingsTests
	{
		public IElasticClient CreateClient(string jsonResponse, Action<JsonSerializerSettings, IConnectionSettingsValues> settingsOverride)
		{
			var connection = new InMemoryConnection(Encoding.UTF8.GetBytes(jsonResponse));
			var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
#pragma warning disable CS0618 // Type or member is obsolete
			var connectionSettings = new ConnectionSettings(connectionPool, connection, settings =>
				new LocalJsonNetSerializer(settings.DefaultIndex("default-index"), settingsOverride));
#pragma warning restore CS0618 // Type or member is obsolete
			var client = new ElasticClient(connectionSettings);
			return client;
		}

		[U] public void RespectsContractResolver()
		{
			var client = CreateClient("{}",
				(jsonSettings, nestSettings) => jsonSettings.ContractResolver = new MyCystomResolver(nestSettings, null));
			var serialized = client.Serializer.SerializeToString(new HasDateString { DateString = "1" }, SerializationFormatting.None);

			serialized.Should().NotBe($@"{{""DATESTRING"":""1""}}");
		}

		public class MyCystomResolver : ElasticContractResolver
		{
			public MyCystomResolver(IConnectionSettingsValues connectionSettings, IList<Func<Type, JsonConverter>> contractConverters) : base(
				connectionSettings, contractConverters) { }

			protected override string ResolvePropertyName(string fieldName) => fieldName.ToUpperInvariant();
		}

		private sealed class LocalJsonNetSerializer : JsonNetSerializer
		{
			//TODO this is unused so what exactly are we testing below?
			private Action<JsonSerializerSettings, IConnectionSettingsValues> _settingsOverride;

			public LocalJsonNetSerializer(
				IConnectionSettingsValues settings,
				Action<JsonSerializerSettings, IConnectionSettingsValues> settingsOverride
			) : base(settings) => _settingsOverride = settingsOverride;
		}

		public class HasDateString
		{
			public DateTime? Date { get; set; }
			public string DateString { get; set; }
		}
	}
}
