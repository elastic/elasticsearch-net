using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	//TODO find a way to combine with IWatchRequest

	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<Watch>))]
	public interface IWatch
	{
		[JsonProperty("actions")]
		[JsonConverter(typeof(ActionsJsonConverter))]
		Actions Actions { get; set; }

		[JsonProperty("condition")]
		ConditionContainer Condition { get; set; }

		[JsonProperty("input")]
		InputContainer Input { get; set; }

		[JsonProperty("metadata")]
		IDictionary<string, object> Metadata { get; set; }

		[JsonProperty("status")]
		WatchStatus Status { get; set; }

		[JsonProperty("throttle_period")]
		string ThrottlePeriod { get; set; }

		[JsonProperty("transform")]
		TransformContainer Transform { get; set; }

		[JsonProperty("trigger")]
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

	}
}
