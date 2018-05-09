using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<PagerDutyEvent>))]
	public interface IPagerDutyEvent
	{
		[JsonProperty("account")]
		string Account { get; set; }

		[JsonProperty("description")]
		string Description { get; set; }

		[JsonProperty("event_type")]
		PagerDutyEventType? EventType { get; set; }

		[JsonProperty("incident_key")]
		string IncidentKey { get; set; }

		[JsonProperty("client")]
		string Client { get; set; }

		[JsonProperty("client_url")]
		string ClientUrl { get; set; }

		[JsonProperty("attach_payload")]
		bool? AttachPayload { get; set; }

		[JsonProperty("context")]
		IEnumerable<IPagerDutyContext> Context { get; set; }
	}

	public class PagerDutyEvent : IPagerDutyEvent
	{
		public string Account { get; set; }

		public string Description { get; set; }

		public PagerDutyEventType? EventType { get; set; }

		public string IncidentKey { get; set; }

		public string Client { get; set; }

		public string ClientUrl { get; set; }

		public bool? AttachPayload { get; set; }

		public IEnumerable<IPagerDutyContext> Context { get; set; }
	}
}
