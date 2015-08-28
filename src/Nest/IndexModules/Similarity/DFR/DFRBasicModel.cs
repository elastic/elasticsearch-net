using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DFRBasicModel
	{
		/// <summary>
		/// Limiting form of the Bose-Einstein model. The formula used in Lucene differs slightly from the one in the original paper: F is increased by tfn+1 and N is increased by F
		/// </summary>
		[EnumMember(Value = "be")]
		BE,

		/// <summary>
		/// Implements the approximation of the binomial model with the divergence for DFR. 
		/// The formula used in Lucene differs slightly from the one in the original paper: to avoid underflow for small values of N and F, N is increased by 1 and F is always increased by tfn+1.
		/// </summary>
		[EnumMember(Value = "d")]
		D,

		/// <summary>
		///Geometric as limiting form of the Bose-Einstein model. The formula used in Lucene differs slightly from the one in the original paper: F is increased by 1 and N is increased by F.
		/// </summary>
		[EnumMember(Value = "g")]
		G,

		/// <summary>
		/// An approximation of the I(ne) model.
		/// </summary>
		[EnumMember(Value = "if")]
		IF,

		/// <summary>
		/// The basic tf-idf model of randomness.
		/// </summary>
		[EnumMember(Value = "in")]
		IN,

		/// <summary>
		/// Tf-idf model of randomness, based on a mixture of Poisson and inverse document frequency.
		/// </summary>
		[EnumMember(Value = "ine")]
		INE,

		/// <summary>
		/// Implements the Poisson approximation for the binomial model for DFR.
		/// </summary>
		[EnumMember(Value = "p")]
		P
	}
}