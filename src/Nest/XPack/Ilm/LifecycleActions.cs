// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest.Utf8Json;
namespace Nest
{
	[JsonFormatter(typeof(LifecycleActionsJsonFormatter))]
	public interface ILifecycleActions : IIsADictionary<string, ILifecycleAction> { }

	public class LifecycleActions : IsADictionaryBase<string, ILifecycleAction>, ILifecycleActions
	{
		public LifecycleActions() { }

		public LifecycleActions(IDictionary<string, ILifecycleAction> container) : base(container) { }

		/// <inheritdoc cref="IAllocateLifecycleAction"/>
		public void Add(IAllocateLifecycleAction action) => BackingDictionary.Add("allocate", action);

		/// <inheritdoc cref="IDeleteLifecycleAction"/>
		public void Add(IDeleteLifecycleAction action) => BackingDictionary.Add("delete", action);

		/// <inheritdoc cref="IForceMergeLifecycleAction"/>
		public void Add(IForceMergeLifecycleAction action) => BackingDictionary.Add("forcemerge", action);

		/// <inheritdoc cref="IFreezeLifecycleAction"/>
		public void Add(IFreezeLifecycleAction action) => BackingDictionary.Add("freeze", action);

		/// <inheritdoc cref="IReadOnlyLifecycleAction"/>
		public void Add(IReadOnlyLifecycleAction action) => BackingDictionary.Add("readonly", action);

		/// <inheritdoc cref="IRolloverLifecycleAction"/>
		public void Add(IRolloverLifecycleAction action) => BackingDictionary.Add("rollover", action);

		/// <inheritdoc cref="ISetPriorityLifecycleAction"/>
		public void Add(ISetPriorityLifecycleAction action) => BackingDictionary.Add("set_priority", action);

		/// <inheritdoc cref="IShrinkLifecycleAction"/>
		public void Add(IShrinkLifecycleAction action) => BackingDictionary.Add("shrink", action);

		/// <inheritdoc cref="IUnfollowLifecycleAction"/>
		public void Add(IUnfollowLifecycleAction action) => BackingDictionary.Add("unfollow", action);

		/// <inheritdoc cref="IWaitForSnapshotLifecycleAction"/>
		public void Add(IWaitForSnapshotLifecycleAction action) => BackingDictionary.Add("wait_for_snapshot", action);
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
			{ "wait_for_snapshot", 9 },
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
						case 9:
							lifecycleAction = formatterResolver.GetFormatter<WaitForSnapshotLifecycleAction>()
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
					case "wait_for_snapshot":
						Serialize<IWaitForSnapshotLifecycleAction>(ref writer, action.Value, formatterResolver);
						break;
					default:
						DynamicObjectResolver.ExcludeNullCamelCase.GetFormatter<ILifecycleAction>()
							.Serialize(ref writer, action.Value, formatterResolver);
						break;
				}

				count++;
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

		/// <inheritdoc cref="IAllocateLifecycleAction"/>
		public LifecycleActionsDescriptor Allocate(Func<AllocateLifecycleActionDescriptor, IAllocateLifecycleAction> selector) =>
			Assign("allocate", selector.InvokeOrDefault(new AllocateLifecycleActionDescriptor()));

		/// <inheritdoc cref="IDeleteLifecycleAction"/>
		public LifecycleActionsDescriptor Delete(Func<DeleteLifecycleActionDescriptor, IDeleteLifecycleAction> selector) =>
			Assign("delete", selector.InvokeOrDefault(new DeleteLifecycleActionDescriptor()));

		/// <inheritdoc cref="IForceMergeLifecycleAction"/>
		public LifecycleActionsDescriptor ForceMerge(Func<ForceMergeLifecycleActionDescriptor, IForceMergeLifecycleAction> selector) =>
			Assign("forcemerge", selector.InvokeOrDefault(new ForceMergeLifecycleActionDescriptor()));

		/// <inheritdoc cref="IFreezeLifecycleAction"/>
		public LifecycleActionsDescriptor Freeze(Func<FreezeLifecycleActionDescriptor, IFreezeLifecycleAction> selector) =>
			Assign("freeze", selector.InvokeOrDefault(new FreezeLifecycleActionDescriptor()));

		/// <inheritdoc cref="IReadOnlyLifecycleAction"/>
		public LifecycleActionsDescriptor ReadOnly(Func<ReadOnlyLifecycleActionDescriptor, IReadOnlyLifecycleAction> selector) =>
			Assign("readonly", selector.InvokeOrDefault(new ReadOnlyLifecycleActionDescriptor()));

		/// <inheritdoc cref="IRolloverLifecycleAction"/>
		public LifecycleActionsDescriptor Rollover(Func<RolloverLifecycleActionDescriptor, IRolloverLifecycleAction> selector) =>
			Assign("rollover", selector.InvokeOrDefault(new RolloverLifecycleActionDescriptor()));

		/// <inheritdoc cref="ISetPriorityLifecycleAction"/>
		public LifecycleActionsDescriptor SetPriority(Func<SetPriorityLifecycleActionDescriptor, ISetPriorityLifecycleAction> selector) =>
			Assign("set_priority", selector.InvokeOrDefault(new SetPriorityLifecycleActionDescriptor()));

		/// <inheritdoc cref="IShrinkLifecycleAction"/>
		public LifecycleActionsDescriptor Shrink(Func<ShrinkLifecycleActionDescriptor, IShrinkLifecycleAction> selector) =>
			Assign("shrink", selector.InvokeOrDefault(new ShrinkLifecycleActionDescriptor()));

		/// <inheritdoc cref="IUnfollowLifecycleAction"/>
		public LifecycleActionsDescriptor Unfollow(Func<UnfollowLifecycleActionDescriptor, IUnfollowLifecycleAction> selector) =>
			Assign("unfollow", selector.InvokeOrDefault(new UnfollowLifecycleActionDescriptor()));

		/// <inheritdoc cref="IWaitForSnapshotLifecycleAction"/>
		public LifecycleActionsDescriptor WaitForSnapshot(Func<WaitForSnapshotLifecycleActionDescriptor, IWaitForSnapshotLifecycleAction> selector) =>
			Assign("wait_for_snapshot", selector.InvokeOrDefault(new WaitForSnapshotLifecycleActionDescriptor()));
	}
}
