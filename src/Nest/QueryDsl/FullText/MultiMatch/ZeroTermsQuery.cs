using System.Runtime.Serialization;
using System.Runtime.Serialization;


namespace Nest
{

	public enum ZeroTermsQuery
	{
		[EnumMember(Value = "all")]
		All,

		[EnumMember(Value = "none")]
		None
	}
}
