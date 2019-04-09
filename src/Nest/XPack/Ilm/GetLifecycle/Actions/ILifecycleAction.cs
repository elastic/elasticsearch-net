using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	[JsonConverter(typeof(LifecycleActionJsonConverter))]
	public interface ILifecycleAction
	{
		[JsonProperty("name")]
		string Name { get; set; }
	}

	public abstract class LifecycleActionBase : ILifecycleAction
	{
		public string Name { get; set; }

		internal LifecycleActionBase() { }

		protected LifecycleActionBase(string name) => ((ILifecycleAction)this).Name = name;
	}

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
				case "allocate": return o.ToObject<AllocateAction>(ElasticContractResolver.Empty);
				case "delete": return o.ToObject<DeleteAction>(ElasticContractResolver.Empty);
				case "forcemerge": return o.ToObject<ForceMergeAction>(ElasticContractResolver.Empty);
				case "freeze": return o.ToObject<FreezeAction>(ElasticContractResolver.Empty);
				case "readonly": return o.ToObject<ReadOnlyAction>(ElasticContractResolver.Empty);
				case "rollover": return o.ToObject<RolloverAction>(ElasticContractResolver.Empty);
				case "set_priority": return o.ToObject<SetPriorityAction>(ElasticContractResolver.Empty);
				case "shrink": return o.ToObject<ShrinkAction>(ElasticContractResolver.Empty);
				case "unfollow": return o.ToObject<UnfollowAction>(ElasticContractResolver.Empty);
				default:
					throw new JsonSerializationException($"Cannot deserialize detector for '{typePropertyValue.ToLowerInvariant()}'");
			}
		}

		public override bool CanConvert(Type objectType) => objectType == typeof(LifecycleActionBase);
	}
}
