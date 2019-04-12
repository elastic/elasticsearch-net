using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAllocateAction : ILifecycleAction
	{
		[JsonProperty("number_of_replicas")]
		int? NumberOfReplicas { get; set; }

		[JsonProperty("include")]
		IDictionary<string, string> Include { get; set; }

		[JsonProperty("exclude")]
		IDictionary<string, string> Exclude { get; set; }

		[JsonProperty("require")]
		IDictionary<string, string> Require { get; set; }
	}

	public class AllocateAction : LifecycleActionBase, IAllocateAction
	{
		public AllocateAction() : base("allocate"){ }

		public int? NumberOfReplicas { get; set; }

		public IDictionary<string, string> Include { get; set; }

		public IDictionary<string, string> Exclude { get; set; }

		public IDictionary<string, string> Require { get; set; }
	}

	public class AllocateActionDescriptor : LifecycleActionDescriptorBase<AllocateActionDescriptor, IAllocateAction>, IAllocateAction
	{
		public AllocateActionDescriptor() : base("allocate") { }

		int? IAllocateAction.NumberOfReplicas { get; set; }

		IDictionary<string, string> IAllocateAction.Include { get; set; }

		IDictionary<string, string> IAllocateAction.Exclude { get; set; }

		IDictionary<string, string> IAllocateAction.Require { get; set; }

		public AllocateActionDescriptor NumberOfReplicas(int? numberOfReplicas)
			=> Assign(numberOfReplicas, (a, v) => a.NumberOfReplicas = numberOfReplicas);

		public AllocateActionDescriptor Include(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> includeSelector) =>
			Assign(includeSelector(new FluentDictionary<string, string>()), (a, v) => a.Include = v);

		public AllocateActionDescriptor Exclude(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> excludeSelector) =>
			Assign(excludeSelector(new FluentDictionary<string, string>()), (a, v) => a.Exclude = v);

		public AllocateActionDescriptor Require(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> requireSelector) =>
			Assign(requireSelector(new FluentDictionary<string, string>()), (a, v) => a.Require = v);
	}
}
