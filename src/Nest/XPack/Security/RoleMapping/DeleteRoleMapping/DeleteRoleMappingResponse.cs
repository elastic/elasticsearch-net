using System.Runtime.Serialization;

namespace Nest
{
	public interface IDeleteRoleMappingResponse : IResponse
	{
		[DataMember(Name ="found")]
		bool Found { get; }
	}

	public class DeleteRoleMappingResponse : ResponseBase, IDeleteRoleMappingResponse
	{
		public bool Found { get; internal set; }
	}
}
