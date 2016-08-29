using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<RolloverConditions>))]
	public interface IRolloverConditions
	{
		[JsonProperty("max_age")]
		Time MaxAge { get; set; }

		[JsonProperty("max_docs")]
		int MaxDocs { get; set; }
	}

	public class RolloverConditions : IRolloverConditions
	{
		public Time MaxAge { get; set; }

		public int MaxDocs { get; set; }
	}

	public class RolloverConditionsDescriptor
		: DescriptorBase<RolloverConditionsDescriptor, IRolloverConditions>, IRolloverConditions
	{
		Time IRolloverConditions.MaxAge { get; set; }

		int IRolloverConditions.MaxDocs { get; set; }

		public RolloverConditionsDescriptor MaxAge(Time maxAge) => Assign(a => a.MaxAge = maxAge);

		public RolloverConditionsDescriptor MaxDocs(int maxDocs) => Assign(a => a.MaxDocs = maxDocs);
	}
}
