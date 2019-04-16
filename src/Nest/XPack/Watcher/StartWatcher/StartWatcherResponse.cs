using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IStartWatcherResponse : IAcknowledgedResponse { }

	public class StartWatcherResponse : AcknowledgedResponseBase, IStartWatcherResponse { }
}
