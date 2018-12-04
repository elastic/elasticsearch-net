using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface IDeleteRollupJobResponse : IAcknowledgedResponse { }

	public class DeleteRollupJobResponse : AcknowledgedResponseBase, IDeleteRollupJobResponse { }
}
