using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public class Collector
	{
		[DataMember(Name ="children")]
		public IReadOnlyCollection<Collector> Children { get; internal set; } =
			EmptyReadOnly<Collector>.Collection;

		[DataMember(Name ="name")]
		public string Name { get; internal set; }

		[DataMember(Name ="reason")]
		public string Reason { get; internal set; }

		[DataMember(Name ="time_in_nanos")]
		public long TimeInNanoseconds { get; internal set; }
	}
}
