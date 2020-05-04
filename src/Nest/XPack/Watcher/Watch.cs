// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAsAttribute(typeof(Watch))]
	public interface IWatch
	{
		[DataMember(Name ="actions")]
		Actions Actions { get; set; }

		[DataMember(Name ="condition")]
		ConditionContainer Condition { get; set; }

		[DataMember(Name ="input")]
		InputContainer Input { get; set; }

		[DataMember(Name ="metadata")]
		IDictionary<string, object> Metadata { get; set; }

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
			Assign(actions, (a, v) => a.Actions = v?.Invoke(new ActionsDescriptor())?.Value);

		/// <inheritdoc cref="IWatch.Condition" />
		public WatchDescriptor Condition(Func<ConditionDescriptor, ConditionContainer> selector) =>
			Assign(selector, (a, v) => a.Condition = v.InvokeOrDefault(new ConditionDescriptor()));

		/// <inheritdoc cref="IWatch.Input" />
		public WatchDescriptor Input(Func<InputDescriptor, InputContainer> selector) =>
			Assign(selector, (a, v) => a.Input = v.InvokeOrDefault(new InputDescriptor()));

		/// <inheritdoc cref="IWatch.Metadata" />
		public WatchDescriptor Metadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> paramsDictionary) =>
			Assign(paramsDictionary, (a, v) => a.Metadata = v(new FluentDictionary<string, object>()));

		/// <inheritdoc cref="IWatch.Metadata" />
		public WatchDescriptor Metadata(Dictionary<string, object> paramsDictionary) =>
			Assign(paramsDictionary, (a, v) => a.Metadata = v);

		/// <inheritdoc cref="IWatch.ThrottlePeriod" />
		public WatchDescriptor ThrottlePeriod(string throttlePeriod) => Assign(throttlePeriod, (a, v) => a.ThrottlePeriod = v);

		/// <inheritdoc cref="IWatch.Transform" />
		public WatchDescriptor Transform(Func<TransformDescriptor, TransformContainer> selector) =>
			Assign(selector, (a, v) => a.Transform = v.InvokeOrDefault(new TransformDescriptor()));

		/// <inheritdoc cref="IWatch.Trigger" />
		public WatchDescriptor Trigger(Func<TriggerDescriptor, TriggerContainer> selector) =>
			Assign(selector, (a, v) => a.Trigger = v.InvokeOrDefault(new TriggerDescriptor()));

	}
}
