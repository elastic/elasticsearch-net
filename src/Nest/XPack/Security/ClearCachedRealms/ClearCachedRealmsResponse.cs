using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	public interface IClearCachedRealmsResponse : IResponse
	{
		[DataMember(Name ="cluster_name")]
		string ClusterName { get; }

		[DataMember(Name ="nodes")]
		IReadOnlyDictionary<string, SecurityNode> Nodes { get; }
	}

	public class ClearCachedRealmsResponse : ResponseBase, IClearCachedRealmsResponse
	{
		public string ClusterName { get; internal set; }
		public IReadOnlyDictionary<string, SecurityNode> Nodes { get; internal set; } = EmptyReadOnly<string, SecurityNode>.Dictionary;
	}
}
