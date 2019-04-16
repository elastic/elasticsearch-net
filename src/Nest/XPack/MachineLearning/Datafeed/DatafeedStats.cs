using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class DatafeedStats
	{
		[DataMember(Name ="assignment_explanation")]
		public string AssignmentExplanation { get; internal set; }

		[DataMember(Name ="datafeed_id")]
		public string DatafeedId { get; internal set; }

		[DataMember(Name ="node")]
		public DiscoveryNode Node { get; internal set; }

		[DataMember(Name ="state")]
		public DatafeedState State { get; internal set; }
	}
}
