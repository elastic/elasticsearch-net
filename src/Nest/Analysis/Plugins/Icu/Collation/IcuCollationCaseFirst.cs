using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// Controls which case is sorted first when case is not ignored for
	/// strength tertiary. The default depends on the collation.
	/// </summary>
	/// <remarks>
	/// Requires analysis-icu plugin to be installed
	/// </remarks>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum IcuCollationCaseFirst
	{
		[EnumMember(Value = "lower")] Lower,
		[EnumMember(Value = "upper")] Upper
	}
}
