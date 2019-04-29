using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPhase
	{
		[JsonProperty("min_age")]
		Time MinimumAge { get; set; }

		[JsonProperty("actions")]
		ILifecycleActions Actions { get; set; }
	}

	public class Phase : IPhase
	{
		public Time MinimumAge { get; set; }

		public ILifecycleActions Actions { get; set; }
	}

	public class PhaseDescriptor : DescriptorBase<PhaseDescriptor, IPhase>, IPhase
	{
		Time IPhase.MinimumAge { get; set; }

		ILifecycleActions IPhase.Actions { get; set; }

		public PhaseDescriptor MinimumAge(string minimumAge) => Assign(minimumAge, (a, v) => a.MinimumAge = v);

		public PhaseDescriptor Actions(Func<LifecycleActionsDescriptor, IPromise<ILifecycleActions>> selector) =>
			Assign(selector, (a, v) => a.Actions = v?.Invoke(new LifecycleActionsDescriptor())?.Value);
	}

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

	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<LifecycleActions, string, ILifecycleAction>))]
	public interface ILifecycleActions : IIsADictionary<string, ILifecycleAction> { }

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
