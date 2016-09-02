using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IShrinkIndexResponse : IAcknowledgedResponse
	{
		[JsonProperty("shards_acknowledged")]
		bool ShardsAcknowledged { get; }
	}

	public class ShrinkIndexResponse : AcknowledgedResponseBase, IShrinkIndexResponse
	{
		public bool ShardsAcknowledged { get; internal set; }
	}
}
