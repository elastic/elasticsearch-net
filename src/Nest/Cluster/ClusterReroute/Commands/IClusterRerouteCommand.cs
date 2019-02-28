using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[JsonFormatter(typeof(ClusterRerouteCommandFormatter))]
	public interface IClusterRerouteCommand
	{
		[IgnoreDataMember]
		string Name { get; }
	}
}
