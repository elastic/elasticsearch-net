using System;
using System.Collections.Generic;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Internal;
using Elasticsearch.Net.Utf8Json.Resolvers;

namespace Nest
{
	[JsonFormatter(typeof(LifecycleActionsJsonFormatter))]
	public interface ILifecycleActions : IIsADictionary<string, ILifecycleAction> { }

	public class LifecycleActions : IsADictionaryBase<string, ILifecycleAction>, ILifecycleActions
	{
		public LifecycleActions() { }

		public LifecycleActions(IDictionary<string, ILifecycleAction> container) : base(container) { }

		public void Add(IAllocateLifecycleAction action) => BackingDictionary.Add("allocate", action);

		public void Add(IDeleteLifecycleAction action) => BackingDictionary.Add("delete", action);

		public void Add(IForceMergeLifecycleAction action) => BackingDictionary.Add("forcemerge", action);

		public void Add(IFreezeLifecycleAction action) => BackingDictionary.Add("freeze", action);

		public void Add(IReadOnlyLifecycleAction action) => BackingDictionary.Add("readonly", action);

		public void Add(IRolloverLifecycleAction action) => BackingDictionary.Add("rollover", action);

		public void Add(ISetPriorityLifecycleAction action) => BackingDictionary.Add("set_priority", action);

		public void Add(IShrinkLifecycleAction action) => BackingDictionary.Add("shrink", action);

		public void Add(IUnfollowLifecycleAction action) => BackingDictionary.Add("unfollow", action);
	}

	internal class LifecycleActionsJsonFormatter : IJsonFormatter<ILifecycleActions>
	{
		private static readonly AutomataDictionary LifeCycleActions = new AutomataDictionary
		{
			{ "allocate", 0 },
			{ "delete", 1 },
			{ "forcemerge", 2 },
			{ "freeze", 3 },
			{ "readonly", 4 },
			{ "rollover", 5 },
			{ "set_priority", 6 },
			{ "shrink", 7 },
			{ "unfollow", 8 },
		};

		public ILifecycleActions Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.BeginObject)
			{
				reader.ReadNextBlock();
				return null;
			}

			var lifecycles = new Dictionary<string, ILifecycleAction>();
			var count = 0;
			while (reader.ReadIsInObject(ref count))
			{
				var type = reader.ReadPropertyNameSegmentRaw();
				ILifecycleAction lifecycleAction = null;

				if (LifeCycleActions.TryGetValue(type, out var value))
				{
					switch (value)
					{
						case 0:
							lifecycleAction = formatterResolver.GetFormatter<AllocateLifecycleAction>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 1:
							lifecycleAction = formatterResolver.GetFormatter<DeleteLifecycleAction>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 2:
							lifecycleAction = formatterResolver.GetFormatter<ForceMergeLifecycleAction>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 3:
							lifecycleAction = formatterResolver.GetFormatter<FreezeLifecycleAction>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 4:
							lifecycleAction = formatterResolver.GetFormatter<ReadOnlyLifecycleAction>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 5:
							lifecycleAction = formatterResolver.GetFormatter<RolloverLifecycleAction>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 6:
							lifecycleAction = formatterResolver.GetFormatter<SetPriorityLifecycleAction>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 7:
							lifecycleAction = formatterResolver.GetFormatter<ShrinkLifecycleAction>()
								.Deserialize(ref reader, formatterResolver);
							break;
						case 8:
							lifecycleAction = formatterResolver.GetFormatter<UnfollowLifecycleAction>()
								.Deserialize(ref reader, formatterResolver);
							break;
					}

					lifecycles.Add(type.Utf8String(), lifecycleAction);
				}
				else
					reader.ReadNextBlock();
			}

			return new LifecycleActions(lifecycles);
		}

		public void Serialize(ref JsonWriter writer, ILifecycleActions value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteBeginObject();
			var count = 0;
			foreach (var action in value)
			{
				if (count > 0)
					writer.WriteValueSeparator();

				writer.WritePropertyName(action.Key);

				switch (action.Key)
				{
					case "allocate":
						Serialize<IAllocateLifecycleAction>(ref writer, action.Value, formatterResolver);
						break;
					case "delete":
						Serialize<IDeleteLifecycleAction>(ref writer, action.Value, formatterResolver);
						break;
					case "forcemerge":
						Serialize<IForceMergeLifecycleAction>(ref writer, action.Value, formatterResolver);
						break;
					case "freeze":
						Serialize<IFreezeLifecycleAction>(ref writer, action.Value, formatterResolver);
						break;
					case "readonly":
						Serialize<IReadOnlyLifecycleAction>(ref writer, action.Value, formatterResolver);
						break;
					case "rollover":
						Serialize<IRolloverLifecycleAction>(ref writer, action.Value, formatterResolver);
						break;
					case "set_priority":
						Serialize<ISetPriorityLifecycleAction>(ref writer, action.Value, formatterResolver);
						break;
					case "shrink":
						Serialize<IShrinkLifecycleAction>(ref writer, action.Value, formatterResolver);
						break;
					case "unfollow":
						Serialize<IUnfollowLifecycleAction>(ref writer, action.Value, formatterResolver);
						break;
					default:
						DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<ILifecycleAction>()
							.Serialize(ref writer, action.Value, formatterResolver);
						break;
				}
			}
			writer.WriteEndObject();
		}

		private void Serialize<TLifecycleAction>(ref JsonWriter writer, ILifecycleAction value, IJsonFormatterResolver formatterResolver)
			where TLifecycleAction : ILifecycleAction =>
			formatterResolver.GetFormatter<TLifecycleAction>().Serialize(ref writer, (TLifecycleAction)value, formatterResolver);
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
			Assign("set_priority", selector.InvokeOrDefault(new SetPriorityLifecycleActionDescriptor()));

		public LifecycleActionsDescriptor Shrink(Func<ShrinkLifecycleActionDescriptor, IShrinkLifecycleAction> selector) =>
			Assign("shrink", selector.InvokeOrDefault(new ShrinkLifecycleActionDescriptor()));

		public LifecycleActionsDescriptor Unfollow(Func<UnfollowLifecycleActionDescriptor, IUnfollowLifecycleAction> selector) =>
			Assign("unfollow", selector.InvokeOrDefault(new UnfollowLifecycleActionDescriptor()));
	}
}
