using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class ClusterRerouteCommandJsonConverter : JsonConverter
	{
		public override bool CanWrite => true;
		public override bool CanRead => true;
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			IClusterRerouteCommand command = null;
			var o = JObject.Load(reader);
			var child = o.Children<JProperty>().FirstOrDefault();
			var v = child?.Children<JObject>().FirstOrDefault();
			if (v == null) return null;
			switch (child.Name)
			{
				case "allocate_replica":
					command = v.ToObject<AllocateReplicaClusterRerouteCommand>(ElasticContractResolver.Empty);
					break;
				case "allocate_empty_primary":
					command = v.ToObject<AllocateEmptyPrimaryRerouteCommand>(ElasticContractResolver.Empty);
					break;
				case "allocate_stale_primary":
					command = v.ToObject<AllocateStalePrimaryRerouteCommand>(ElasticContractResolver.Empty);
					break;
				case "move":
					command = v.ToObject<MoveClusterRerouteCommand>(ElasticContractResolver.Empty);
					break;
				case "cancel":
					command = v.ToObject<CancelClusterRerouteCommand>(ElasticContractResolver.Empty);
					break;
			}
			return command;


		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var c = value as IClusterRerouteCommand;
			if (c == null) return;

			var properties = value.GetType().GetCachedObjectProperties();
			if (properties.Count == 0) return;
			writer.WriteStartObject();
			writer.WritePropertyName(c.Name);
			writer.WriteStartObject();
			foreach (var p in properties)
			{
				if (p.Ignored) continue;
				var vv = p.ValueProvider.GetValue(value);
				if (vv == null) continue;
				writer.WritePropertyName(p.PropertyName);
				serializer.Serialize(writer, vv);
			}
			writer.WriteEndObject();
			writer.WriteEndObject();
		}
	}
}
