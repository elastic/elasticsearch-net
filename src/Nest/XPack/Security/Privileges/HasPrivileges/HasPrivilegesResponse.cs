using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[JsonConverter(typeof(HasPrivilegesResponseJsonConverter))]
	public interface IHasPrivilegesResponse : IResponse
	{
		[JsonProperty("username")]
		string Username { get; }

		[JsonProperty("has_all_requested")]
		bool HasAllRequested { get; }

		[JsonProperty("cluster")]
		IReadOnlyDictionary<string, bool> Clusters { get; }

		[JsonProperty("index")]
		IReadOnlyCollection<ResourcePrivileges> Indices { get; }

		[JsonProperty("application")]
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

	internal class HasPrivilegesResponseJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => true;
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var response = new HasPrivilegesResponse();
			var jsonObject = JObject.Load(reader);

			response.Username = jsonObject.Property("username").Value.Value<string>();
			response.HasAllRequested = jsonObject.Property("has_all_requested").Value.Value<bool>();

			response.Clusters = jsonObject.Property("cluster")
				.Value.Value<JObject>()
				.Properties()
				.ToDictionary(c => c.Name, c => c.Value.Value<bool>());

			response.Indices = jsonObject.Property("index")
										 .Value.Value<JObject>()
										 .Properties()
										 .Select(o => new ResourcePrivileges
										 {
											 Resource = o.Name,
											 Privileges = o.Value.Value<JObject>()
												 .Properties()
												 .ToDictionary(p => p.Name, p => p.Value.Value<bool>())
										 }).ToList().AsReadOnly();

			var apps = new Dictionary<string, IReadOnlyCollection<ResourcePrivileges>>();
			foreach (var applicationProp in jsonObject.Property("application").Value.Value<JObject>())
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
			response.Applications = apps;

			return response;
		}
	}
}
