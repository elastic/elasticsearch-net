using System.Runtime.Serialization;

namespace Nest
{
	public interface IDeleteRoleResponse : IResponse
	{
		[DataMember(Name ="found")]
		bool Found { get; }
	}

	public class DeleteRoleResponse : ResponseBase, IDeleteRoleResponse
	{
		public bool Found { get; internal set; }
	}
}
