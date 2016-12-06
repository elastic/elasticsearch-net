using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	//only used by GetRoleResponse thus private setters and IReadOnlyCollection
	public class XPackRole
	{
		[JsonProperty("cluster")]
		public IReadOnlyCollection<string> Cluster { get; private set; } = EmptyReadOnly<string>.Collection;

		[JsonProperty("run_as")]
		public IReadOnlyCollection<string> RunAs { get; private set; }= EmptyReadOnly<string>.Collection;

		[JsonProperty("indices")]
		public IReadOnlyCollection<IIndicesPrivileges> Indices { get; private set; } = EmptyReadOnly<IIndicesPrivileges>.Collection;

		[JsonProperty("metadata")]
		public IReadOnlyDictionary<string, object> Metadata { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;
	}
}
