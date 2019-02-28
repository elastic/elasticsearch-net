using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IRestartWatcherResponse : IAcknowledgedResponse { }

	public class RestartWatcherResponse : AcknowledgedResponseBase, IRestartWatcherResponse { }
}
