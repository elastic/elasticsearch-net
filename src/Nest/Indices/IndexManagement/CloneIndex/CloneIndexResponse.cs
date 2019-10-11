using System.Runtime.Serialization;

namespace Nest
{
	public class CloneIndexResponse : AcknowledgedResponseBase
	{
		[DataMember(Name = "shards_acknowledged")]
		public bool ShardsAcknowledged { get; set; }

		/// <summary>
		/// The target index created
		/// </summary>
		[DataMember(Name = "index")]
		public string Index { get; set; }
	}
}
