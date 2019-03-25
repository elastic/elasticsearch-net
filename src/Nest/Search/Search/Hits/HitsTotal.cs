
using System.Runtime.Serialization;

namespace Nest
{
	public class HitsTotal
	{
		//TODO 7.0: Still need to map relation
//		[JsonProperty("max_score")]
//		public double MaxScore { get; internal set; }

		[DataMember(Name = "value")]
		public long? Value { get; internal set; }

	}
}
