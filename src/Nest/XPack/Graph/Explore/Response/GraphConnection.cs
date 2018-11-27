using System.Runtime.Serialization;

namespace Nest
{
	public class GraphConnection
	{
		[DataMember(Name ="doc_count")]
		public long DocumentCount { get; internal set; }

		[DataMember(Name ="source")]
		public long Source { get; internal set; }

		[DataMember(Name ="target")]
		public long Target { get; internal set; }

		[DataMember(Name ="weight")]
		public double Weight { get; internal set; }
	}
}
