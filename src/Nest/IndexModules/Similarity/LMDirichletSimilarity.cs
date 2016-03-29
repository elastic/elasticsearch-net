using Newtonsoft.Json;

namespace Nest
{

	public interface ILMDirichletSimilarity : ISimilarity
	{
		[JsonProperty("mu")]
		int? Mu { get; set; }
	}
	public class LMDirichletSimilarity : ILMDirichletSimilarity
	{
		public string Type => "LMDirichlet";

		public int? Mu { get; set; }
	}
	public class LMDirichletSimilarityDescriptor 
		: DescriptorBase<LMDirichletSimilarityDescriptor, ILMDirichletSimilarity>, ILMDirichletSimilarity
	{
		string ISimilarity.Type => "LMDirichlet";
		int? ILMDirichletSimilarity.Mu { get; set; }

		public LMDirichletSimilarityDescriptor Mu(int? mu) => Assign(a => a.Mu = mu);
	}
}
