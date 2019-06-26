using System.Runtime.Serialization;

namespace Nest
{
	public class DeleteRoleResponse : ResponseBase
	{
		[DataMember(Name ="found")]
		public bool Found { get; internal set; }
		
	}
}
