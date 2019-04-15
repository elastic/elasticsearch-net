using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[JsonConverter(typeof(LifecycleActionJsonConverter))]
	public interface ILifecycleAction {}

	internal class LifecycleActionJsonConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var o = JObject.Load(reader);

			var typeProperty = o.Property("name");
			if (typeProperty == null) return null;

			var typePropertyValue = typeProperty.Value.ToString();
			switch (typePropertyValue.ToLowerInvariant())
			{
				case "allocate": return o.ToObject<AllocateLifecycleAction>(ElasticContractResolver.Empty);
				case "delete": return o.ToObject<DeleteLifecycleAction>(ElasticContractResolver.Empty);
				case "forcemerge": return o.ToObject<ForceMergeLifecycleAction>(ElasticContractResolver.Empty);
				case "freeze": return o.ToObject<FreezeLifecycleAction>(ElasticContractResolver.Empty);
				case "readonly": return o.ToObject<ReadOnlyLifecycleAction>(ElasticContractResolver.Empty);
				case "rollover": return o.ToObject<RolloverLifecycleAction>(ElasticContractResolver.Empty);
				case "set_priority": return o.ToObject<SetPriorityLifecycleAction>(ElasticContractResolver.Empty);
				case "shrink": return o.ToObject<ShrinkLifecycleAction>(ElasticContractResolver.Empty);
				case "unfollow": return o.ToObject<UnfollowLifecycleAction>(ElasticContractResolver.Empty);
				default:
					throw new JsonSerializationException($"Cannot deserialize detector for '{typePropertyValue.ToLowerInvariant()}'");
			}
		}

		public override bool CanConvert(Type objectType) => true;
	}
}
