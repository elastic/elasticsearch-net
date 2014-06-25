using System.Runtime.Serialization;

namespace Nest
{
	public enum NonStringIndexOption
	{
		[EnumMember(Value = "no")]
		No,
		[EnumMember(Value = "analyzed")]
		Analyzed,
		[EnumMember(Value = "not_analyzed")]
		NotAnalyzed 
	}
}
