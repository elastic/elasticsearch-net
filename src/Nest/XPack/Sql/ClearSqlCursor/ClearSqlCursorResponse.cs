using System.Runtime.Serialization;

namespace Nest
{
	public interface IClearSqlCursorResponse : IResponse
	{
		[DataMember(Name ="succeeded")]
		bool Succeeded { get; }
	}

	public class ClearSqlCursorResponse : ResponseBase, IClearSqlCursorResponse
	{
		public bool Succeeded { get; internal set; }
	}
}
