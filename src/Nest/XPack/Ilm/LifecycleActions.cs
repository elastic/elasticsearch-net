using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(LifecycleActionsJsonConverter))]
	public interface ILifecycleActions : IIsADictionary<string, ILifecycleAction> { }

	public class LifecycleActions : IsADictionaryBase<string, ILifecycleAction>, ILifecycleActions
	{
		public LifecycleActions() { }

		public LifecycleActions(IDictionary<string, ILifecycleAction> container) : base(container) { }

		public LifecycleActions(Dictionary<string, ILifecycleAction> container)
			: base(container.Select(kv => kv).ToDictionary(kv => kv.Key, kv => kv.Value)) { }

		public void Add(IAllocateLifecycleAction action) => BackingDictionary.Add("allocate", action);
		public void Add(IDeleteLifecycleAction action) => BackingDictionary.Add("delete", action);
		public void Add(IForceMergeLifecycleAction action) => BackingDictionary.Add("forcemerge", action);
		public void Add(IFreezeLifecycleAction action) => BackingDictionary.Add("freeze", action);
		public void Add(IReadOnlyLifecycleAction action) => BackingDictionary.Add("readonly", action);
		public void Add(IRolloverLifecycleAction action) => BackingDictionary.Add("rollover", action);
		public void Add(ISetPriorityLifecycleAction action) => BackingDictionary.Add("setpriority", action);
		public void Add(IShrinkLifecycleAction action) => BackingDictionary.Add("shrink", action);
		public void Add(IUnfollowLifecycleAction action) => BackingDictionary.Add("unfollow", action);
	}

	internal class LifecycleActionsJsonConverter : JsonConverter
	{
		public override bool CanWrite => false;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject) return null;

			var lifecycles = new Dictionary<string, ILifecycleAction>();
			while (reader.Read())
			{
				if (reader.TokenType == JsonToken.PropertyName)
				{
					var type = (string)reader.Value;
					ILifecycleAction lifecycleAction;

					switch (type)
					{
						case "allocate":
							reader.Read();
							lifecycleAction = serializer.Deserialize<AllocateLifecycleAction>(reader);
							break;
						case "delete":
							reader.Read();
							lifecycleAction = serializer.Deserialize<DeleteLifecycleAction>(reader);
							break;
						case "forcemerge":
							reader.Read();
							lifecycleAction = serializer.Deserialize<ForceMergeLifecycleAction>(reader);
							break;
						case "freeze":
							reader.Read();
							lifecycleAction = serializer.Deserialize<FreezeLifecycleAction>(reader);
							break;
						case "readonly":
							reader.Read();
							lifecycleAction = serializer.Deserialize<ReadOnlyLifecycleAction>(reader);
							break;
						case "rollover":
							reader.Read();
							lifecycleAction = serializer.Deserialize<RolloverLifecycleAction>(reader);
							break;
						case "set_priority":
							reader.Read();
							lifecycleAction = serializer.Deserialize<SetPriorityLifecycleAction>(reader);
							break;
						case "shrink":
							reader.Read();
							lifecycleAction = serializer.Deserialize<ShrinkLifecycleAction>(reader);
							break;
						case "unfollow":
							reader.Read();
							lifecycleAction = serializer.Deserialize<UnfollowLifecycleAction>(reader);
							break;
						default:
							throw new JsonSerializationException($"Cannot deserialize detector for '{type}'");
					}

					lifecycles.Add(type, lifecycleAction);
				}
				else if (reader.TokenType == JsonToken.EndObject) break;
			}

			return new LifecycleActions(lifecycles);
		}
	}

	public class LifecycleActionsDescriptor : IsADictionaryDescriptorBase<LifecycleActionsDescriptor, LifecycleActions, string, ILifecycleAction>
	{
		public LifecycleActionsDescriptor() : base(new LifecycleActions()) { }

		public LifecycleActionsDescriptor Allocate(Func<AllocateLifecycleActionDescriptor, IAllocateLifecycleAction> selector) =>
			Assign("allocate", selector.InvokeOrDefault(new AllocateLifecycleActionDescriptor()));

		public LifecycleActionsDescriptor Delete(Func<DeleteLifecycleActionDescriptor, IDeleteLifecycleAction> selector) =>
			Assign("delete", selector.InvokeOrDefault(new DeleteLifecycleActionDescriptor()));

		public LifecycleActionsDescriptor ForceMerge(Func<ForceMergeLifecycleActionDescriptor, IForceMergeLifecycleAction> selector) =>
			Assign("forcemerge", selector.InvokeOrDefault(new ForceMergeLifecycleActionDescriptor()));

		public LifecycleActionsDescriptor Freeze(Func<FreezeLifecycleActionDescriptor, IFreezeLifecycleAction> selector) =>
			Assign("freeze", selector.InvokeOrDefault(new FreezeLifecycleActionDescriptor()));

		public LifecycleActionsDescriptor ReadOnly(Func<ReadOnlyLifecycleActionDescriptor, IReadOnlyLifecycleAction> selector) =>
			Assign("readonly", selector.InvokeOrDefault(new ReadOnlyLifecycleActionDescriptor()));

		public LifecycleActionsDescriptor Rollover(Func<RolloverLifecycleActionDescriptor, IRolloverLifecycleAction> selector) =>
			Assign("rollover", selector.InvokeOrDefault(new RolloverLifecycleActionDescriptor()));

		public LifecycleActionsDescriptor SetPriority(Func<SetPriorityLifecycleActionDescriptor, ISetPriorityLifecycleAction> selector) =>
			Assign("setpriority", selector.InvokeOrDefault(new SetPriorityLifecycleActionDescriptor()));

		public LifecycleActionsDescriptor Shrink(Func<ShrinkLifecycleActionDescriptor, IShrinkLifecycleAction> selector) =>
			Assign("shrink", selector.InvokeOrDefault(new ShrinkLifecycleActionDescriptor()));

		public LifecycleActionsDescriptor Unfollow(Func<UnfollowLifecycleActionDescriptor, IUnfollowLifecycleAction> selector) =>
			Assign("unfollow", selector.InvokeOrDefault(new UnfollowLifecycleActionDescriptor()));
	}
}
