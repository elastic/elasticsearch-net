using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PagerDutyContext>))]
	public interface IPagerDutyContext
	{
		[JsonProperty("type")]
		PagerDutyContextType Type { get; set; }

		[JsonProperty("href")]
		string Href { get; set; }

		[JsonProperty("src")]
		string Src { get; set; }
	}

	public class PagerDutyContext : IPagerDutyContext
	{
		public PagerDutyContext(PagerDutyContextType type)
		{
			this.Type = type;
		}

		internal PagerDutyContext() { }

		public PagerDutyContextType Type { get; set; }

		public string Href { get; set; }

		public string Src { get; set; }
	}

	public class PagerDutyContextDescriptor : DescriptorBase<PagerDutyContextDescriptor, IPagerDutyContext>, IPagerDutyContext
	{
		PagerDutyContextType IPagerDutyContext.Type { get; set; }
		string IPagerDutyContext.Href { get; set; }
		string IPagerDutyContext.Src { get; set; }

		public PagerDutyContextDescriptor(PagerDutyContextType type)
		{
			Self.Type = type;
		}

		public PagerDutyContextDescriptor Href(string href) => Assign(a => a.Href = href);

		public PagerDutyContextDescriptor Src(string src) => Assign(a => a.Src = src);
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
