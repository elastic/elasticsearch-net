using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Sets the alternate handling for strength quaternary to be either shifted or non-ignorable.
	/// Which boils down to ignoring punctuation and whitespace.
	/// </summary>

	public enum IcuCollationCaseFirst
	{
		[EnumMember(Value = "lower")] Lower,
		[EnumMember(Value = "upper")] Upper
	}
}
