using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[DataContract]
	[ReadAsAttribute(typeof(Watch))]
	public class IWatch
	{
		[DataMember(Name ="actions")]
		public Actions Actions { get; internal set; }

		[DataMember(Name ="condition")]
		ConditionContainer Condition { get; set; }

		[DataMember(Name ="input")]
		InputContainer Input { get; set; }

		[DataMember(Name ="metadata")]
		IDictionary<string, object> Meta { get; set; }

		[DataMember(Name ="status")]
		WatchStatus Status { get; set; }

		[DataMember(Name ="throttle_period")]
		string ThrottlePeriod { get; set; }

		[DataMember(Name ="transform")]
		TransformContainer Transform { get; set; }

		[DataMember(Name = "trigger")]
		TriggerContainer Trigger { get; set; }

	}

	public class Watch : IWatch
	{
		WatchStatus IWatch.Status { get; set; } //only used on the response

		public Actions Actions { get; set; }

		public ConditionContainer Condition { get; set; }

		public InputContainer Input { get; set; }

		public IDictionary<string, object> Metadata { get; set; }

		public WatchStatus Status { get; set; }

		public string ThrottlePeriod { get; set; }

		public TransformContainer Transform { get; set; }

		public TriggerContainer Trigger { get; set; }
	}

	public class WatchDescriptor : DescriptorBase<WatchDescriptor, IWatch>, IWatch
	{
		Actions IWatch.Actions { get; set; }
		ConditionContainer IWatch.Condition { get; set; }
		InputContainer IWatch.Input { get; set; }
		IDictionary<string, object> IWatch.Metadata { get; set; }
		string IWatch.ThrottlePeriod { get; set; }
		TransformContainer IWatch.Transform { get; set; }
		TriggerContainer IWatch.Trigger { get; set; }
		WatchStatus IWatch.Status { get; set; } //only used on the response

		/// <inheritdoc cref="IWatch.Actions" />
		public WatchDescriptor Actions(Func<ActionsDescriptor, IPromise<Actions>> actions) =>
			Assign(a => a.Actions = actions?.Invoke(new ActionsDescriptor())?.Value);

		/// <inheritdoc cref="IWatch.Condition" />
		public WatchDescriptor Condition(Func<ConditionDescriptor, ConditionContainer> selector) =>
			Assign(a => a.Condition = selector.InvokeOrDefault(new ConditionDescriptor()));

		/// <inheritdoc cref="IWatch.Input" />
		public WatchDescriptor Input(Func<InputDescriptor, InputContainer> selector) =>
			Assign(a => a.Input = selector.InvokeOrDefault(new InputDescriptor()));

		/// <inheritdoc cref="IWatch.Metadata" />
		public WatchDescriptor Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary) =>
			Assign(a => a.Metadata = paramsDictionary(new FluentDictionary<string, object>()));

		/// <inheritdoc cref="IWatch.Metadata" />
		public WatchDescriptor Metadata(Dictionary<string, object> paramsDictionary) =>
			Assign(a => a.Metadata = paramsDictionary);

		/// <inheritdoc cref="IWatch.ThrottlePeriod" />
		public WatchDescriptor ThrottlePeriod(string throttlePeriod) => Assign(a => a.ThrottlePeriod = throttlePeriod);

		/// <inheritdoc cref="IWatch.Transform" />
		public WatchDescriptor Transform(Func<TransformDescriptor, TransformContainer> selector) =>
			Assign(a => a.Transform = selector.InvokeOrDefault(new TransformDescriptor()));

		/// <inheritdoc cref="IWatch.Trigger" />
		public WatchDescriptor Trigger(Func<TriggerDescriptor, TriggerContainer> selector) =>
			Assign(a => a.Trigger = selector.InvokeOrDefault(new TriggerDescriptor()));

		[DataMember(Name ="trigger")]
		public ITriggerContainer Trigger { get; internal set; }
	}
}
