using System.Runtime.Serialization;

namespace Nest
{
	public interface IExistsResponse : IResponse
	{
		bool Exists { get; }
	}

	[DataContract]
	public class ExistsResponse : ResponseBase, IExistsResponse
	{
		public bool Exists => ApiCall != null && ApiCall.Success && ApiCall.HttpStatusCode == 200;
	}
}
