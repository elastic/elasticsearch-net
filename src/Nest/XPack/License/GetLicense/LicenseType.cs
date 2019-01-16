using System.Runtime.Serialization;


namespace Nest
{
	[StringEnum]
	public enum LicenseType
	{
		[EnumMember(Value = "missing")]
		Missing,

		[EnumMember(Value = "trial")]
		Trial,

		[EnumMember(Value = "basic")]
		Basic,

		[EnumMember(Value = "standard")]
		Standard,

		[EnumMember(Value = "dev")] //bwc
		Dev,

		[EnumMember(Value = "silver")] //bwc
		Silver,

		[EnumMember(Value = "gold")]
		Gold,

		[EnumMember(Value = "platinum")]
		Platinum
	}
}
