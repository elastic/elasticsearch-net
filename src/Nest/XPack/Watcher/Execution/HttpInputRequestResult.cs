using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	[ExactContractJsonConverter(typeof(ReadAsTypeJsonConverter<HttpInputRequestResult>))]
	public class HttpInputRequestResult : HttpInputRequest { }
}
