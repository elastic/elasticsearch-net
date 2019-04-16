using System.Runtime.Serialization;

namespace Nest
{
	public interface IDeleteExpiredDataResponse : IResponse
	{
		[DataMember(Name ="deleted")]
		bool Deleted { get; }
	}

	public class DeleteExpiredDataResponse : ResponseBase, IDeleteExpiredDataResponse
	{
		public bool Deleted { get; internal set; }
	}
}
