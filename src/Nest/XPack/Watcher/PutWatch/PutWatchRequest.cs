// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A PutWatch request
	/// </summary>
	[MapsApi("watcher.put_watch.json")]
	public partial interface IPutWatchRequest
	{
		/// <summary>
		/// The actions that will be run if the condition matches
		/// </summary>
		[DataMember(Name = "actions")]
		Actions Actions { get; set; }

		/// <summary>
		/// Defines if the actions should be run
		/// </summary>
		[DataMember(Name = "condition")]
		ConditionContainer Condition { get; set; }

		/// <summary>
		/// Defines the input that loads the data for the watch
		/// </summary>
		[DataMember(Name = "input")]
		InputContainer Input { get; set; }

		/// <summary>
		/// Metadata that will be copied into the history entries
		/// </summary>
		[DataMember(Name = "metadata")]
		[JsonFormatter(typeof(VerbatimDictionaryInterfaceKeysFormatter<string, object>))]
		IDictionary<string, object> Metadata { get; set; }

		/// <summary>
		/// The minimum time between actions being run. Defaults to 5 seconds.
		/// </summary>
		/// <remarks>
		/// Default can be changed in the config file with the setting <code>xpack.watcher.throttle.period.default_period</code>.
		/// </remarks>
		[DataMember(Name = "throttle_period")]
		string ThrottlePeriod { get; set; }

		/// <summary>
		/// Processes and changes the payload in the watch execution context to prepare it for the actions.
		/// </summary>
		[DataMember(Name = "transform")]
		TransformContainer Transform { get; set; }

		/// <summary>
		/// Defines when the watch should run
		/// </summary>
		[DataMember(Name = "trigger")]
		TriggerContainer Trigger { get; set; }
	}

	/// <inheritdoc cref="IPutWatchRequest" />
	public partial class PutWatchRequest
	{
		/// <inheritdoc />
		public Actions Actions { get; set; }

		/// <inheritdoc />
		public ConditionContainer Condition { get; set; }

		/// <inheritdoc />
		public InputContainer Input { get; set; }

		/// <inheritdoc />
		public IDictionary<string, object> Metadata { get; set; }

		/// <inheritdoc />
		public string ThrottlePeriod { get; set; }

		/// <inheritdoc />
		public TransformContainer Transform { get; set; }

		/// <inheritdoc />
		public TriggerContainer Trigger { get; set; }
	}

	/// <inheritdoc cref="IPutWatchRequest" />
	public partial class PutWatchDescriptor
	{
		Actions IPutWatchRequest.Actions { get; set; }
		ConditionContainer IPutWatchRequest.Condition { get; set; }
		InputContainer IPutWatchRequest.Input { get; set; }
		IDictionary<string, object> IPutWatchRequest.Metadata { get; set; }
		string IPutWatchRequest.ThrottlePeriod { get; set; }
		TransformContainer IPutWatchRequest.Transform { get; set; }
		TriggerContainer IPutWatchRequest.Trigger { get; set; }

		/// <inheritdoc cref="IPutWatchRequest.Actions" />
		public PutWatchDescriptor Actions(Func<ActionsDescriptor, IPromise<Actions>> actions) =>
			Assign(actions, (a, v) => a.Actions = v?.Invoke(new ActionsDescriptor())?.Value);

		/// <inheritdoc cref="IPutWatchRequest.Condition" />
		public PutWatchDescriptor Condition(Func<ConditionDescriptor, ConditionContainer> selector) =>
			Assign(selector.InvokeOrDefault(new ConditionDescriptor()), (a, v) => a.Condition = v);

		/// <inheritdoc cref="IPutWatchRequest.Input" />
		public PutWatchDescriptor Input(Func<InputDescriptor, InputContainer> selector) =>
			Assign(selector.InvokeOrDefault(new InputDescriptor()), (a, v) => a.Input = v);

		/// <inheritdoc cref="IPutWatchRequest.Metadata" />
		public PutWatchDescriptor Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary) =>
			Assign(paramsDictionary, (a, v) => a.Metadata = v?.Invoke(new FluentDictionary<string, object>()));

		/// <inheritdoc cref="IPutWatchRequest.Metadata" />
		public PutWatchDescriptor Metadata(Dictionary<string, object> paramsDictionary) =>
			Assign(paramsDictionary, (a, v) => a.Metadata = v);

		/// <inheritdoc cref="IPutWatchRequest.ThrottlePeriod" />
		public PutWatchDescriptor ThrottlePeriod(string throttlePeriod) => Assign(throttlePeriod, (a, v) => a.ThrottlePeriod = v);

		/// <inheritdoc cref="IPutWatchRequest.Transform" />
		public PutWatchDescriptor Transform(Func<TransformDescriptor, TransformContainer> selector) =>
			Assign(selector.InvokeOrDefault(new TransformDescriptor()), (a, v) => a.Transform = v);

		/// <inheritdoc cref="IPutWatchRequest.Trigger" />
		public PutWatchDescriptor Trigger(Func<TriggerDescriptor, TriggerContainer> selector) =>
			Assign(selector.InvokeOrDefault(new TriggerDescriptor()), (a, v) => a.Trigger = v);
	}
}
