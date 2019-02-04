using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	/// <summary>
	/// A similarity with Bayesian smoothing using Dirichlet priors.
	/// </summary>
	public interface ILMDirichletSimilarity : ISimilarity
	{
		/// <summary>
		/// The mu parameter. Defaults to 2000.
		/// </summary>
		[DataMember(Name ="mu")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? Mu { get; set; }
	}

	/// <inheritdoc />
	public class LMDirichletSimilarity : ILMDirichletSimilarity
	{
		/// <inheritdoc />
		public int? Mu { get; set; }

		public string Type => "LMDirichlet";
	}

	/// <inheritdoc />
	public class LMDirichletSimilarityDescriptor
		: DescriptorBase<LMDirichletSimilarityDescriptor, ILMDirichletSimilarity>, ILMDirichletSimilarity
	{
		int? ILMDirichletSimilarity.Mu { get; set; }
		string ISimilarity.Type => "LMDirichlet";

		/// <inheritdoc />
		public LMDirichletSimilarityDescriptor Mu(int? mu) => Assign(a => a.Mu = mu);
	}
}
