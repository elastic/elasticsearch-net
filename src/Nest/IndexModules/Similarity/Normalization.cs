using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Normalization
	{
		/// <summary>
		/// Implementation used when there is no normalization. 
		/// </summary>
		[EnumMember(Value = "no")]
		No,

		/// <summary>
		/// Normalization model that assumes a uniform distribution of the term frequency. 
		/// </summary>
		[EnumMember(Value = "h1")]
		H1,
		/// <summary>
		///  Normalization model in which the term frequency is inversely related to the length.
		/// </summary>
		[EnumMember(Value = "h2")]
		H2,
		/// <summary>
		/// Dirichlet Priors normalization
		/// </summary>
		[EnumMember(Value = "h3")]
		H3,
		/// <summary>
		/// Pareto-Zipf Normalization
		/// </summary>
		[EnumMember(Value = "z")]
		Z,
	}
}