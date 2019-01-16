﻿using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(PagerDutyContext))]
	public interface IPagerDutyContext
	{
		[DataMember(Name = "href")]
		string Href { get; set; }

		[DataMember(Name = "src")]
		string Src { get; set; }

		[DataMember(Name = "type")]
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

		public PagerDutyContextDescriptor Href(string href) => Assign(a => a.Href = href);

		public PagerDutyContextDescriptor Src(string src) => Assign(a => a.Src = src);
	}

	[StringEnum]
	public enum PagerDutyContextType
	{
		[EnumMember(Value = "link")]
		Link,

		[EnumMember(Value = "image")]
		Image
	}
}
