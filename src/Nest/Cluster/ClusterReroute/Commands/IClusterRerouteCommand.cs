using System.Runtime.Serialization;

namespace Nest
{
	[ContractJsonConverter(typeof(ClusterRerouteCommandJsonConverter))]
	public interface IClusterRerouteCommand
	{
		[IgnoreDataMember]
		string Name { get; }
	}
}
