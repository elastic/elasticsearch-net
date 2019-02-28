using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Forward (default) for LTR and reverse for RTL
	/// </summary>
	[StringEnum]
	public enum IcuTransformDirection
	{
		/// <summary>LTR</summary>
		[EnumMember(Value = "forward")]
		Forward,

		/// <summary> RTL</summary>
		[EnumMember(Value = "reverse")]
		Reverse,
	}
}
