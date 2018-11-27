using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IClearCachedRolesResponse : IResponse
	{
		[DataMember(Name ="cluster_name")]
		string ClusterName { get; }

		[DataMember(Name ="nodes")]
		IReadOnlyDictionary<string, SecurityNode> Nodes { get; }
	}

	public class ClearCachedRolesResponse : ResponseBase, IClearCachedRolesResponse
	{
		public string ClusterName { get; internal set; }
		public IReadOnlyDictionary<string, SecurityNode> Nodes { get; internal set; } = EmptyReadOnly<string, SecurityNode>.Dictionary;
	}
}
