using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DFRAfterEffect
	{
		/// <summary>
		/// Implementation used when there is no aftereffect.
		/// </summary>
		[EnumMember(Value = "no")]
		No,

		/// <summary>
		/// Model of the information gain based on the ratio of two Bernoulli processes.
		/// </summary>
		[EnumMember(Value = "b")]
		B,

		/// <summary>
		/// Model of the information gain based on Laplace's law of succession.
		/// </summary>
		[EnumMember(Value = "l")]
		L,

	}
}