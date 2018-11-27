using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IStartWatcherResponse : IAcknowledgedResponse { }

	public class StartWatcherResponse : AcknowledgedResponseBase, IStartWatcherResponse { }
}
