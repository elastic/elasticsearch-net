using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface ICreateRollupJobResponse : IAcknowledgedResponse { }

	public class CreateRollupJobResponse : AcknowledgedResponseBase, ICreateRollupJobResponse { }
}
