using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<PagerDutyContext>))]
	public interface IPagerDutyContext
	{
		[JsonProperty("href")]
		string Href { get; set; }

		[JsonProperty("src")]
		string Src { get; set; }

		[JsonProperty("type")]
		PagerDutyContextType Type { get; set; }
	}

	public class PagerDutyContext : IPagerDutyContext
	{
		public PagerDutyContext(PagerDutyContextType type) => Type = type;

		internal PagerDutyContext() { }

		public string Href { get; set; }

		public string Src { get; set; }

		public PagerDutyContextType Type { get; set; }
	}

	public class PagerDutyContextDescriptor : DescriptorBase<PagerDutyContextDescriptor, IPagerDutyContext>, IPagerDutyContext
	{
		public PagerDutyContextDescriptor(PagerDutyContextType type) => Self.Type = type;

		string IPagerDutyContext.Href { get; set; }
		string IPagerDutyContext.Src { get; set; }
		PagerDutyContextType IPagerDutyContext.Type { get; set; }

		public PagerDutyContextDescriptor Href(string href) => Assign(href, (a, v) => a.Href = v);

		public PagerDutyContextDescriptor Src(string src) => Assign(src, (a, v) => a.Src = v);
	}

	[JsonConverter(typeof(StringEnumConverter))]
	public enum PagerDutyContextType
	{
		[EnumMember(Value = "link")]
		Link,

		[EnumMember(Value = "image")]
		Image
	}
}
