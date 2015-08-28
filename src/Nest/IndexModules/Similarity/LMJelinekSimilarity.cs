using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{

	public interface ILMJelinekSimilarity : ISimilarity
	{
		[JsonProperty("lambda")]
		double? Lambda { get; set; }
	}

	public class LMJelinekSimilarity : ILMJelinekSimilarity
	{
		public string Type => "LMJelinekMercer";

		public double? Lambda { get; set; }
	}

	public class LMJelinekSimilarityDescriptor 
		: DescriptorBase<LMJelinekSimilarityDescriptor, ILMJelinekSimilarity>, ILMJelinekSimilarity
	{
		string ISimilarity.Type => "LMJelinekMercer";
		double? ILMJelinekSimilarity.Lambda { get; set; }

		public LMJelinekSimilarityDescriptor Lamdba(double? lamda) => Assign(a => a.Lambda = lamda);
	}
}
