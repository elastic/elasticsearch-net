using System.Runtime.Serialization;

namespace Nest
{
	public class QueryBreakdown
	{
		[DataMember(Name ="advance")]
		public long Advance { get; internal set; }

		[DataMember(Name ="build_scorer")]
		public long BuildScorer { get; internal set; }

		[DataMember(Name ="create_weight")]
		public long CreateWeight { get; internal set; }

		[DataMember(Name ="match")]
		public long Match { get; internal set; }

		[DataMember(Name ="next_doc")]
		public long NextDoc { get; internal set; }

		[DataMember(Name ="score")]
		public long Score { get; internal set; }
	}
}
