using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IIlmPutLifecycleRequest { }

	public partial class IlmPutLifecycleRequest
	{
		[JsonProperty("phases")]
		public Phases Phases { get; set; }
	}

	[DescriptorFor("IlmPutLifecycle")]
	public partial class IlmPutLifecycleDescriptor
	{
		public IlmPutLifecycleDescriptor Phases(Func<PhasesDescriptor, Phases> selector)
		{
			return null;
		}
	}

	public class Phases : IsADictionaryBase<string, Phase>
	{
		public void Add(string name, Phase phase) => BackingDictionary.Add(name, phase);
	}


	public class PhasesDescriptor : IDescriptor
	{
		private IDictionary<string, Phase> _payload;

		public PhasesDescriptor() { }

		public PhasesDescriptor(IDictionary<string, Phase> payload) => _payload = payload;

	//	IDictionary<string, Phase> ISimpleInput.Payload => _payload;

		public PhasesDescriptor Add(string key, Phase value)
		{
			if (_payload == null) _payload = new Dictionary<string, Phase>();
			_payload.Add(key, value);
			return this;
		}

		public PhasesDescriptor Remove(string key)
		{
			if (_payload == null) return this;

			_payload.Remove(key);
			return this;
		}
	}

//	public class LifecycleActionsDescriptor
//	{
//		public LifecycleActionsDescriptor Allocate(string name, Func<AllocateLifecycleActionDescriptor, IAllocateLifecycleAction> selector) =>
//			Assign(name, selector.InvokeOrDefault(new AllocateLifecycleActionDescriptor()));
//
//		public LifecycleActionsDescriptor Delete(string name, Func<DeleteLifecycleActionDescriptor, IDeleteLifecycleAction> selector) =>
//			Assign(name, selector.InvokeOrDefault(new DeleteLifecycleActionDescriptor()));
//
//		public LifecycleActionsDescriptor ForceMerge(string name, Func<ForceMergeLifecycleActionDescriptor, IForceMergeLifecycleAction> selector) =>
//			Assign(name, selector.InvokeOrDefault(new ForceMergeLifecycleActionDescriptor()));
//
//		public LifecycleActionsDescriptor Freeze(string name, Func<FreezeLifecycleActionDescriptor, IFreezeLifecycleAction> selector) =>
//			Assign(name, selector.InvokeOrDefault(new FreezeLifecycleActionDescriptor()));
//
//		public LifecycleActionsDescriptor ReadOnly(string name, Func<ReadOnlyLifecycleActionDescriptor, IReadOnlyLifecycleAction> selector) =>
//			Assign(name, selector.InvokeOrDefault(new ReadOnlyLifecycleActionDescriptor()));
//
//		public LifecycleActionsDescriptor Rollover(string name, Func<RolloverLifecycleActionDescriptor, IRolloverLifecycleAction> selector) =>
//			Assign(name, selector.InvokeOrDefault(new RolloverLifecycleActionDescriptor()));
//
//		public LifecycleActionsDescriptor SetPriority(string name, Func<SetPriorityLifecycleActionDescriptor, ISetPriorityLifecycleAction> selector) =>
//			Assign(name, selector.InvokeOrDefault(new SetPriorityLifecycleActionDescriptor()));
//
//		public LifecycleActionsDescriptor Shrink(string name, Func<ShrinkLifecycleActionDescriptor, IShrinkLifecycleAction> selector) =>
//			Assign(name, selector.InvokeOrDefault(new ShrinkLifecycleActionDescriptor()));
//
//		public LifecycleActionsDescriptor Unfollow(string name, Func<UnfollowLifecycleActionDescriptor, IUnfollowLifecycleAction> selector) =>
//			Assign(name, selector.InvokeOrDefault(new UnfollowLifecycleActionDescriptor()));
//	}
}
