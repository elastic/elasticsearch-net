using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IDeleteRollupJobResponse : IAcknowledgedResponse { }

	public class DeleteRollupJobResponse : AcknowledgedResponseBase, IDeleteRollupJobResponse { }
}
