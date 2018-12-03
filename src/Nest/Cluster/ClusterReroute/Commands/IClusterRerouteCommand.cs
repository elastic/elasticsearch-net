using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(ClusterRerouteCommandFormatter))]
	public interface IClusterRerouteCommand
	{
		[IgnoreDataMember]
		string Name { get; }
	}
}
