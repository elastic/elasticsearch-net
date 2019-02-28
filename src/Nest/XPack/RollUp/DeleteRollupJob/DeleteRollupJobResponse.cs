using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IDeleteRollupJobResponse : IAcknowledgedResponse { }

	public class DeleteRollupJobResponse : AcknowledgedResponseBase, IDeleteRollupJobResponse { }
}
