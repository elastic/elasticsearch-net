using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IRestartWatcherResponse : IAcknowledgedResponse { }

	public class RestartWatcherResponse : AcknowledgedResponseBase, IRestartWatcherResponse { }
}
