using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public class ExistsResponse : ResponseBase
	{
		public bool Exists => ApiCall != null && ApiCall.Success && ApiCall.HttpStatusCode == 200;
	}
}
