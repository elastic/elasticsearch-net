using System.Runtime.Serialization;

namespace Nest
{
	public class ClearSqlCursorResponse : ResponseBase
	{
		[DataMember(Name ="succeeded")]
		public bool Succeeded { get; internal set; }
	}
}
