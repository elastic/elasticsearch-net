using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject]
	[ExactContractJsonConverter(typeof(ReadAsTypeJsonConverter<PagerDutyAction>))]
	public interface IPagerDutyAction : IAction, IPagerDutyEvent { }

	public class PagerDutyAction : ActionBase, IPagerDutyAction
	{
		public override ActionType ActionType => ActionType.PagerDuty;

		public string Account { get; set; }

		public string Description { get; set; }

		public PagerDutyEventType? EventType { get; set; }

		public string IncidentKey { get; set; }

		public string Client { get; set; }

		public string ClientUrl { get; set; }

		public bool? AttachPayload { get; set; }

		public IEnumerable<IPagerDutyContext> Context { get; set; }

		public PagerDutyAction(string name) : base(name) {}
	}

	public class PagerDutyActionDescriptor : ActionsDescriptorBase<PagerDutyActionDescriptor, IPagerDutyAction>, IPagerDutyAction
	{
		protected override ActionType ActionType => ActionType.PagerDuty;

		string IPagerDutyEvent.Account { get; set; }
		string IPagerDutyEvent.Description { get; set; }
		PagerDutyEventType? IPagerDutyEvent.EventType { get; set; }
		string IPagerDutyEvent.IncidentKey { get; set; }
		string IPagerDutyEvent.Client { get; set; }
		string IPagerDutyEvent.ClientUrl { get; set; }
		bool? IPagerDutyEvent.AttachPayload { get; set; }
		IEnumerable<IPagerDutyContext> IPagerDutyEvent.Context { get; set; }

		public PagerDutyActionDescriptor(string name) : base(name)
		{
		}

		public PagerDutyActionDescriptor Account(string account) => Assign(a => a.Account = account);

		public PagerDutyActionDescriptor Description(string description) => Assign(a => a.Description = description);

		public PagerDutyActionDescriptor EventType(PagerDutyEventType? eventType) => Assign(a => a.EventType = eventType);

		public PagerDutyActionDescriptor IncidentKey(string incidentKey) => Assign(a => a.IncidentKey = incidentKey);

		public PagerDutyActionDescriptor Client(string client) => Assign(a => a.Client = client);

		public PagerDutyActionDescriptor ClientUrl(string url) => Assign(a => a.ClientUrl = url);

		public PagerDutyActionDescriptor AttachPayload(bool? attach = true) => Assign(a => a.AttachPayload = attach);

		public PagerDutyActionDescriptor Context(Func<PagerDutyContextsDescriptor, IPromise<IList<IPagerDutyContext>>> selector) =>
			Assign(a => a.Context = selector?.Invoke(new PagerDutyContextsDescriptor())?.Value);
	}

	public class PagerDutyContextsDescriptor
	: DescriptorPromiseBase<PagerDutyContextsDescriptor, IList<IPagerDutyContext>>
	{
		public PagerDutyContextsDescriptor() : base(new List<IPagerDutyContext>()) { }

		public PagerDutyContextsDescriptor Context(PagerDutyContextType type, Func<PagerDutyContextDescriptor, IPagerDutyContext> selector) =>
			this.Assign(a => a.AddIfNotNull(selector?.Invoke(new PagerDutyContextDescriptor(type))));
	}

	[JsonConverter(typeof(StringEnumConverter))]
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
