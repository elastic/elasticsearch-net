using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public class Role
	{
		[JsonProperty("cluster")]
		public IEnumerable<string> Cluster { get; set; }

		[JsonProperty("run_as")]
		public IEnumerable<string> RunAs { get; set; }

		[JsonProperty("indices")]
		public IEnumerable<IIndicesPrivileges> Indices { get; set; }

	}
}
