using System.Runtime.Serialization;

namespace Nest
{
	public class DeleteUserResponse : ResponseBase
	{
		[DataMember(Name ="found")]
		public bool Found { get; internal set; }
	}
}
