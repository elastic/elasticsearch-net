using System.Runtime.Serialization;

namespace Nest
{
	public class DeleteRoleMappingResponse : ResponseBase
	{
		[DataMember(Name ="found")]
		public bool Found { get; internal set; }
	}
}
