using System.Runtime.Serialization;

namespace Nest
{
	public class DeleteExpiredDataResponse : ResponseBase
	{
		[DataMember(Name ="deleted")]
		public bool Deleted { get; internal set; }
	}
}
