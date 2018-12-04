using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface ICreateRollupJobResponse : IAcknowledgedResponse { }

	public class CreateRollupJobResponse : AcknowledgedResponseBase, ICreateRollupJobResponse { }
}
