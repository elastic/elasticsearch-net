using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IIlmPutLifecycleRequest { }

	public partial class IlmPutLifecycleRequest
	{
		[JsonProperty("policy")]
		public Policy Policy { get; set; }
	}

	[DescriptorFor("IlmPutLifecycle")]
	public partial class IlmPutLifecycleDescriptor
	{
		public IlmPutLifecycleDescriptor Policy(Func<PolicyDescriptor, object> selector) => null;
	}

	public class PolicyDescriptor : IDescriptor
	{
		public PhasesDescriptor Phases(Func<PhasesDescriptor, object> func) => null;
	}

	public class PhasesDescriptor : IDescriptor
	{
		public PhasesDescriptor Warm(Func<PhaseDescriptor, PhaseDescriptor> func) => null;
		public PhasesDescriptor Hot(Func<PhaseDescriptor, PhaseDescriptor> func) => null;
		public PhasesDescriptor Cold(Func<PhaseDescriptor, PhaseDescriptor> func) => null;
		public PhasesDescriptor Delete(Func<PhaseDescriptor, PhaseDescriptor> func) => null;
	}

	public class PhaseDescriptor : IDescriptor
	{
		public PhaseDescriptor MinimumAge(string p0) => null;

		public PhaseDescriptor Actions(Func<LifecycleActionsDescriptor, object> func) => null;
	}

	public class LifecycleActionsDescriptor : IsADictionaryDescriptorBase<LifecycleActionsDescriptor, IsADictionaryBase<string, ILifecycleAction>, string, ILifecycleAction>
	{
		public LifecycleActionsDescriptor(IsADictionaryBase<string, ILifecycleAction> instance) : base(instance) { }

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
