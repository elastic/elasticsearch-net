using Newtonsoft.Json;

namespace Nest
{

	public interface ILMJelinekMercerSimilarity : ISimilarity
	{
		[JsonProperty("lambda")]
		double? Lambda { get; set; }
	}

	public class LMJelinekMercerSimilarity : ILMJelinekMercerSimilarity
	{
		public string Type => "LMJelinekMercer";

		public double? Lambda { get; set; }
	}

	public class LMJelinekMercerSimilarityDescriptor 
		: DescriptorBase<LMJelinekMercerSimilarityDescriptor, ILMJelinekMercerSimilarity>, ILMJelinekMercerSimilarity
	{
		string ISimilarity.Type => "LMJelinekMercer";
		double? ILMJelinekMercerSimilarity.Lambda { get; set; }

		public LMJelinekMercerSimilarityDescriptor Lamdba(double? lamda) => Assign(a => a.Lambda = lamda);
	}
}
