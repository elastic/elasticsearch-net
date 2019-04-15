using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IAllocateLifecycleAction : ILifecycleAction
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

	public class AllocateLifecycleAction : IAllocateLifecycleAction
	{
		public int? NumberOfReplicas { get; set; }

		public IDictionary<string, string> Include { get; set; }

		public IDictionary<string, string> Exclude { get; set; }

		public IDictionary<string, string> Require { get; set; }
	}

	public class AllocateLifecycleActionDescriptor : DescriptorBase<AllocateLifecycleActionDescriptor, IAllocateLifecycleAction>, IAllocateLifecycleAction
	{
		int? IAllocateLifecycleAction.NumberOfReplicas { get; set; }

		IDictionary<string, string> IAllocateLifecycleAction.Include { get; set; }

		IDictionary<string, string> IAllocateLifecycleAction.Exclude { get; set; }

		IDictionary<string, string> IAllocateLifecycleAction.Require { get; set; }

		public AllocateLifecycleActionDescriptor NumberOfReplicas(int? numberOfReplicas)
			=> Assign(numberOfReplicas, (a, v) => a.NumberOfReplicas = numberOfReplicas);

		public AllocateLifecycleActionDescriptor Include(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> includeSelector) =>
			Assign(includeSelector(new FluentDictionary<string, string>()), (a, v) => a.Include = v);

		public AllocateLifecycleActionDescriptor Exclude(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> excludeSelector) =>
			Assign(excludeSelector(new FluentDictionary<string, string>()), (a, v) => a.Exclude = v);

		public AllocateLifecycleActionDescriptor Require(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> requireSelector) =>
			Assign(requireSelector(new FluentDictionary<string, string>()), (a, v) => a.Require = v);
	}
}
