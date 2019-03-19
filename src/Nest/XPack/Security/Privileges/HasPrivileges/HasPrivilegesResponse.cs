using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using static Nest.IndicesPrivilegesResponseJsonConverter;

namespace Nest
{
	public interface IHasPrivilegesResponse : IResponse
	{
		[JsonProperty("application")]
		[JsonConverter(typeof(ApplicationsPrivilegesResponseJsonConverter))]
		IReadOnlyDictionary<string, IReadOnlyCollection<ResourcePrivileges>> Applications { get; }

		[JsonProperty("cluster")]
		IReadOnlyDictionary<string, bool> Clusters { get; }

		[JsonProperty("has_all_requested")]
		bool HasAllRequested { get; }

		[JsonProperty("index")]
		[JsonConverter(typeof(IndicesPrivilegesResponseJsonConverter))]
		IReadOnlyCollection<ResourcePrivileges> Indices { get; }

		[JsonProperty("username")]
		string Username { get; }
	}

	public class HasPrivilegesResponse : ResponseBase, IHasPrivilegesResponse
	{
		public IReadOnlyDictionary<string, IReadOnlyCollection<ResourcePrivileges>> Applications { get; internal set; } =
			EmptyReadOnly<string, IReadOnlyCollection<ResourcePrivileges>>.Dictionary;

		public IReadOnlyDictionary<string, bool> Clusters { get; internal set; } = EmptyReadOnly<string, bool>.Dictionary;
		public bool HasAllRequested { get; internal set; }

		public IReadOnlyCollection<ResourcePrivileges> Indices { get; internal set; } = EmptyReadOnly<ResourcePrivileges>.Collection;
		public string Username { get; internal set; }
	}

	public class ResourcePrivileges
	{
		public IReadOnlyDictionary<string, bool> Privileges { get; internal set; } = EmptyReadOnly<string, bool>.Dictionary;
		public string Resource { get; internal set; }
	}

	internal class IndicesPrivilegesResponseJsonConverter : JsonConverter
	{
		public override bool CanWrite { get; } = false;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) =>
			ReadResourcePrivileges(reader, objectType, existingValue, serializer);

		internal static IReadOnlyCollection<ResourcePrivileges> ReadResourcePrivileges(JsonReader reader, Type objectType, object existingValue,
			JsonSerializer serializer
		)
		{
			if (reader.TokenType != JsonToken.StartObject)
				return null;

			var privileges = new List<ResourcePrivileges>();
			while (reader.Read() && reader.TokenType != JsonToken.EndObject)
			{
				var resource = (string)reader.Value;
				reader.Read();
				var resourcePrivileges = serializer.Deserialize<IReadOnlyDictionary<string, bool>>(reader);
				privileges.Add(new ResourcePrivileges { Resource = resource, Privileges = resourcePrivileges });
			}

			return privileges;
		}
	}

	internal class ApplicationsPrivilegesResponseJsonConverter : JsonConverter
	{
		public override bool CanWrite { get; } = false;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject)
				return null;

			var apps = new Dictionary<string, IReadOnlyCollection<ResourcePrivileges>>();
			while (reader.Read() && reader.TokenType != JsonToken.EndObject)
			{
				var app = (string)reader.Value;
				reader.Read();
				var privileges = ReadResourcePrivileges(reader, objectType, existingValue, serializer);
				apps.Add(app, privileges);
			}
			
			return apps;
		}
	}
}
