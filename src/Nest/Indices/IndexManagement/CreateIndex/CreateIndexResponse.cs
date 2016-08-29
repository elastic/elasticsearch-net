using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ICreateIndexResponse : IAcknowledgedResponse
	{
		[JsonProperty("shards_acknowledged")]
		bool ShardsAcknowledged { get; }
	}

	public class CreateIndexResponse : AcknowledgedResponseBase, ICreateIndexResponse
	{
		public bool ShardsAcknowledged { get; set; }
	}
}
