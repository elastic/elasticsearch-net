using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest_5_2_0
{
	[JsonConverter(typeof(ReadAsTypeJsonConverter<RolloverIndexRequest>))]
	public partial interface IRolloverIndexRequest
	{
		[JsonProperty("conditions")]
		IRolloverConditions Conditions { get; set; }
	}

	public partial class RolloverIndexRequest
	{
		public IRolloverConditions Conditions { get; set; }
	}

	[DescriptorFor("IndicesRollover")]
	public partial class RolloverIndexDescriptor
	{
		IRolloverConditions IRolloverIndexRequest.Conditions { get; set; }

		public RolloverIndexDescriptor Conditions(Func<RolloverConditionsDescriptor, IRolloverConditions> selector) =>
			Assign(a => a.Conditions = selector?.Invoke(new RolloverConditionsDescriptor()));
	}
}
