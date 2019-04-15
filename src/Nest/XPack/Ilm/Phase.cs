using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPhase
	{
		[JsonProperty("min_age")]
		Time MinimumAge { get; set; }

		[JsonProperty("actions")]
		List<ILifecycleAction> Actions { get; set; }
	}

	public class Phase : IPhase
	{
		public Time MinimumAge { get; set; }

		public List<ILifecycleAction> Actions { get; set; }
	}
}
