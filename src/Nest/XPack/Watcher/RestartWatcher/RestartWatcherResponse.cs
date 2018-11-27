using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IRestartWatcherResponse : IAcknowledgedResponse { }

	public class RestartWatcherResponse : AcknowledgedResponseBase, IRestartWatcherResponse { }
}
