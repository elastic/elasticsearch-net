using System.Runtime.Serialization;

namespace Nest
{
	public interface IStopWatcherResponse : IAcknowledgedResponse { }

	public class StopWatcherResponse : AcknowledgedResponseBase, IStopWatcherResponse { }
}
