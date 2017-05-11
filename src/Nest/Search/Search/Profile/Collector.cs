using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class Collector
	{
		[JsonProperty("name")]
		public string Name { get; internal set; }

		[JsonProperty("reason")]
		public string Reason { get; internal set; }

		[JsonProperty("time_in_nanos")]
		public long TimeInNanoseconds { get; internal set; }

	[JsonProperty("children")]
		public IReadOnlyCollection<Collector> Children { get; internal set; } =
			EmptyReadOnly<Collector>.Collection;


	}
}
