using System.Runtime.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace Nest
{

	public enum DynamicMapping
	{
		/// <summary>
		/// If new unmapped fields are passed, the whole document will not be added/updated
		/// </summary>
		[EnumMember(Value = "strict")]
		Strict
	}
}
