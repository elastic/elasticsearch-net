using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface ICreateRollupJobResponse : IAcknowledgedResponse { }

	public class CreateRollupJobResponse : AcknowledgedResponseBase, ICreateRollupJobResponse { }
}
