using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IStartWatcherResponse : IAcknowledgedResponse { }

	public class StartWatcherResponse : AcknowledgedResponseBase, IStartWatcherResponse { }
}
