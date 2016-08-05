using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Tests.Framework;
using Elasticsearch.Net;
using FluentAssertions;
using Xunit;
using Nest;
using Newtonsoft.Json;
using System.Text;

namespace Tests.ClientConcepts.Serializer
{

	/// <summary>
	/// Here we get into a bind because our constructor runs too late
	/// </summary>
	public class CustomConstrcutorSerializerSettingsTests : SerializationTestBase
	{
		public class MyCystomResolver : ElasticContractResolver
		{
			public MyCystomResolver(IConnectionSettingsValues connectionSettings, IList<Func<Type, JsonConverter>> contractConverters) : base(connectionSettings, contractConverters)
			{
			}

			protected override string ResolvePropertyName(string fieldName)
			{
				return fieldName.ToUpperInvariant();
			}
		}

		private sealed class LocalJsonNetSerializer : JsonNetSerializer
		{
			private Action<JsonSerializerSettings, IConnectionSettingsValues> _settingsOverride;

			public LocalJsonNetSerializer(
				IConnectionSettingsValues settings,
				Action<JsonSerializerSettings, IConnectionSettingsValues> settingsOverride
			) : base(settings)
			{
				this._settingsOverride = settingsOverride;
			}

#pragma warning disable CS0672 // Member overrides obsolete member
			protected override void ModifyJsonSerializerSettings(JsonSerializerSettings settings)
#pragma warning restore CS0672 // Member overrides obsolete member
			{
				this._settingsOverride.Should().BeNull("the value assigned in our custom constructor has not been assigned yet");
			}
		}

		public class HasDateString
		{
			public string DateString { get; set; }
			public DateTime? Date { get; set; }
		}

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
			var client = this.CreateClient("{}",
				(jsonSettings, nestSettings)=> jsonSettings.ContractResolver = new MyCystomResolver(nestSettings, null));
			var serialized = client.Serializer.SerializeToString(new HasDateString { DateString = "1" }, SerializationFormatting.None);

			serialized.Should().NotBe($@"{{""DATESTRING"":""1""}}");
		}
	}
}
