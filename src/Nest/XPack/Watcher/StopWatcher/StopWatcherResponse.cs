using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IStopWatcherResponse : IAcknowledgedResponse { }

	public class StopWatcherResponse : AcknowledgedResponseBase, IStopWatcherResponse { }
}
