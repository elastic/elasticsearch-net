using System.Runtime.Serialization;

namespace Nest
{
	/// <summary>
	/// Sets the alternate handling for strength quaternary to be either shifted or non-ignorable.
	/// Which boils down to ignoring punctuation and whitespace.
	/// </summary>
	public enum IcuCollationAlternate
	{
		[EnumMember(Value = "shifted")] Shifted,
		[EnumMember(Value = "non-ignorable")] NonIgnorable
	}
}
