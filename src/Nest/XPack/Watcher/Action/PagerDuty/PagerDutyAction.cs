// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(PagerDutyAction))]
	public interface IPagerDutyAction : IAction, IPagerDutyEvent { }

	public class PagerDutyAction : ActionBase, IPagerDutyAction
	{
		public PagerDutyAction(string name) : base(name) { }

		public string Account { get; set; }
		public override ActionType ActionType => ActionType.PagerDuty;

		public bool? AttachPayload { get; set; }

		public string Client { get; set; }

		public string ClientUrl { get; set; }

		public IEnumerable<IPagerDutyContext> Context { get; set; }

		public string Description { get; set; }

		public PagerDutyEventType? EventType { get; set; }

		public string IncidentKey { get; set; }
	}

	public class PagerDutyActionDescriptor : ActionsDescriptorBase<PagerDutyActionDescriptor, IPagerDutyAction>, IPagerDutyAction
	{
		public PagerDutyActionDescriptor(string name) : base(name) { }

		protected override ActionType ActionType => ActionType.PagerDuty;

		string IPagerDutyEvent.Account { get; set; }
		bool? IPagerDutyEvent.AttachPayload { get; set; }
		string IPagerDutyEvent.Client { get; set; }
		string IPagerDutyEvent.ClientUrl { get; set; }
		IEnumerable<IPagerDutyContext> IPagerDutyEvent.Context { get; set; }
		string IPagerDutyEvent.Description { get; set; }
		PagerDutyEventType? IPagerDutyEvent.EventType { get; set; }
		string IPagerDutyEvent.IncidentKey { get; set; }

		public PagerDutyActionDescriptor Account(string account) => Assign(account, (a, v) => a.Account = v);

		public PagerDutyActionDescriptor Description(string description) => Assign(description, (a, v) => a.Description = v);

		public PagerDutyActionDescriptor EventType(PagerDutyEventType? eventType) => Assign(eventType, (a, v) => a.EventType = v);

		public PagerDutyActionDescriptor IncidentKey(string incidentKey) => Assign(incidentKey, (a, v) => a.IncidentKey = v);

		public PagerDutyActionDescriptor Client(string client) => Assign(client, (a, v) => a.Client = v);

		public PagerDutyActionDescriptor ClientUrl(string url) => Assign(url, (a, v) => a.ClientUrl = v);

		public PagerDutyActionDescriptor AttachPayload(bool? attach = true) => Assign(attach, (a, v) => a.AttachPayload = v);

		public PagerDutyActionDescriptor Context(Func<PagerDutyContextsDescriptor, IPromise<IList<IPagerDutyContext>>> selector) =>
			Assign(selector, (a, v) => a.Context = v?.Invoke(new PagerDutyContextsDescriptor())?.Value);
	}

	public class PagerDutyContextsDescriptor
		: DescriptorPromiseBase<PagerDutyContextsDescriptor, IList<IPagerDutyContext>>
	{
		public PagerDutyContextsDescriptor() : base(new List<IPagerDutyContext>()) { }

		public PagerDutyContextsDescriptor Context(PagerDutyContextType type, Func<PagerDutyContextDescriptor, IPagerDutyContext> selector) =>
			Assign(selector?.Invoke(new PagerDutyContextDescriptor(type)), (a, v) => a.AddIfNotNull(v));
	}

	[StringEnum]
	public enum PagerDutyEventType
	{
		[EnumMember(Value = "trigger")]
		Trigger,

		[EnumMember(Value = "resolve")]
		Resolve,

		[EnumMember(Value = "acknowledge")]
		Acknowledge
	}
}
