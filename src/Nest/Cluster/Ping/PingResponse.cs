using System.Runtime.Serialization;

namespace Nest
{
	public interface IPingResponse : IResponse { }

	[DataContract]
	public class PingResponse : ResponseBase, IPingResponse { }
}
