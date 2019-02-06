using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	//only used by GetRoleResponse thus private setters and IReadOnlyCollection
	public class XPackRole
	{
		[DataMember(Name = "cluster")]
		public IReadOnlyCollection<string> Cluster { get; private set; } = EmptyReadOnly<string>.Collection;

		[DataMember(Name = "indices")]
		public IReadOnlyCollection<IIndicesPrivileges> Indices { get; private set; } = EmptyReadOnly<IIndicesPrivileges>.Collection;

		[DataMember(Name = "metadata")]
		public IReadOnlyDictionary<string, object> Metadata { get; internal set; } = EmptyReadOnly<string, object>.Dictionary;

		[DataMember(Name = "run_as")]
		public IReadOnlyCollection<string> RunAs { get; private set; } = EmptyReadOnly<string>.Collection;
	}
}
