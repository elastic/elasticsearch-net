using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization;

namespace Nest
{
	public class SearchProfile
	{
		[DataMember(Name ="collector")]
		public IReadOnlyCollection<Collector> Collector { get; internal set; } =
			EmptyReadOnly<Collector>.Collection;

		[DataMember(Name ="query")]
		public IReadOnlyCollection<QueryProfile> Query { get; internal set; } =
			EmptyReadOnly<QueryProfile>.Collection;

		[DataMember(Name ="rewrite_time")]
		public long RewriteTime { get; internal set; }
	}
}
