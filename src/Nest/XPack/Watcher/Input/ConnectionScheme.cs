using System.Runtime.Serialization;


namespace Nest
{

	public enum ConnectionScheme
	{
		[EnumMember(Value = "http")]
		Http,

		[EnumMember(Value = "https")]
		Https
	}
}
