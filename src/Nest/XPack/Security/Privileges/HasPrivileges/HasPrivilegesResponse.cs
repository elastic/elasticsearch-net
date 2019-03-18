using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	public interface IHasPrivilegesResponse : IResponse
	{
		[JsonProperty("username")]
		string Username { get; }

		[JsonProperty("has_all_requested")]
		bool HasAllRequested { get; }

		[JsonProperty("cluster")]
		IReadOnlyDictionary<string, bool> Clusters { get; }

		[JsonProperty("index")]
		[JsonConverter(typeof(IndicesPrivilegesResponseJsonConverter))]
		IReadOnlyCollection<ResourcePrivileges> Indices { get; }

		[JsonProperty("application")]
		[JsonConverter(typeof(ApplicationsPrivilegesResponseJsonConverter))]
		IReadOnlyDictionary<string, IReadOnlyCollection<ResourcePrivileges>> Applications { get; }
	}

	public class HasPrivilegesResponse : ResponseBase, IHasPrivilegesResponse
	{
		public string Username { get; internal set;  }
		public bool HasAllRequested { get; internal set; }

		public IReadOnlyDictionary<string, bool> Clusters { get; internal set; } = EmptyReadOnly<string, bool>.Dictionary;

		public IReadOnlyCollection<ResourcePrivileges> Indices { get; internal set; } = EmptyReadOnly<ResourcePrivileges>.Collection;

		public IReadOnlyDictionary<string, IReadOnlyCollection<ResourcePrivileges>> Applications { get; internal set; } =
			EmptyReadOnly<string, IReadOnlyCollection<ResourcePrivileges>>.Dictionary;
	}

	public class ResourcePrivileges
	{
		public string Resource { get; internal set; }

		public IReadOnlyDictionary<string, bool> Privileges { get; internal set; } = EmptyReadOnly<string, bool>.Dictionary;
	}

	internal class IndicesPrivilegesResponseJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jsonObject = JObject.Load(reader);
			return jsonObject.Properties()
							 .Select(o => new ResourcePrivileges
							 {
								 Resource = o.Name,
								 Privileges = o.Value.Value<JObject>()
									 .Properties()
									 .ToDictionary(p => p.Name, p => p.Value.Value<bool>())
							 }).ToList().AsReadOnly();
		}
	}

	internal class ApplicationsPrivilegesResponseJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var jsonObject = JObject.Load(reader);
			var apps = new Dictionary<string, IReadOnlyCollection<ResourcePrivileges>>();
			foreach (var applicationProp in jsonObject)
			{
				apps.Add(applicationProp.Key, applicationProp.Value.Value<JObject>()
					.Properties()
					.Select(o => new ResourcePrivileges
					{
						Resource = o.Name,
						Privileges = o.Value.Value<JObject>()
							.Properties()
							.ToDictionary(p => p.Name, p => p.Value.Value<bool>())
					}).ToList().AsReadOnly());
			}
			return apps;
		}
	}
}
