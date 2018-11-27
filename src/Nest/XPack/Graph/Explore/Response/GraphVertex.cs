using System.Runtime.Serialization;

namespace Nest
{
	public class GraphVertex
	{
		[DataMember(Name ="depth")]
		public long Depth { get; internal set; }

		[DataMember(Name ="field")]
		public string Field { get; internal set; }

		[DataMember(Name ="term")]
		public string Term { get; internal set; }

		[DataMember(Name ="weight")]
		public double Weight { get; internal set; }
	}
}
