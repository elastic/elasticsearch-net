using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
	public enum ConnectionScheme
	{
		[EnumMember(Value = "http")]
		Http,

		[EnumMember(Value = "https")]
		Https
	}
}
