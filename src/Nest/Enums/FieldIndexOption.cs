using System.Runtime.Serialization;

namespace Nest
{
	public enum FieldIndexOption
	{
		[EnumMember(Value = "analyzed")]
		Analyzed,
		[EnumMember(Value = "not_analyzed")]
		NotAnalyzed,
		[EnumMember(Value = "no")]
		No
	}
}
