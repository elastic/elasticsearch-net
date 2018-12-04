using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ReadAs(typeof(HttpInputRequestResult))]
	public class HttpInputRequestResult : HttpInputRequest { }
}
