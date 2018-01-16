using Newtonsoft.Json;

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
		[JsonProperty("mu")]
		int? Mu { get; set; }
	}

	/// <inheritdoc />
	public class LMDirichletSimilarity : ILMDirichletSimilarity
	{
		public string Type => "LMDirichlet";

		/// <inheritdoc />
		public int? Mu { get; set; }
	}

	/// <inheritdoc />
	public class LMDirichletSimilarityDescriptor
		: DescriptorBase<LMDirichletSimilarityDescriptor, ILMDirichletSimilarity>, ILMDirichletSimilarity
	{
		string ISimilarity.Type => "LMDirichlet";
		int? ILMDirichletSimilarity.Mu { get; set; }

		/// <inheritdoc />
		public LMDirichletSimilarityDescriptor Mu(int? mu) => Assign(a => a.Mu = mu);
	}
}
