using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum GeoStrategy
	{
		/// <summary>
		/// supports all shape types
		/// </summary>
		[EnumMember(Value = "recursive")]
		Recursive,

		/// <summary>
		/// supports point types only
		/// </summary>
		[EnumMember(Value = "term")]
		Term
	}
}
