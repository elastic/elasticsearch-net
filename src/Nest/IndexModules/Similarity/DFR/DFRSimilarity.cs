using Newtonsoft.Json;

namespace Nest
{

	/// <summary>
	/// Implements the divergence from randomness (DFR) framework introduced in Gianni Amati and Cornelis Joost Van Rijsbergen. 2002. 
	/// Probabilistic models of information retrieval based on measuring the divergence from randomness. ACM Trans. Inf. Syst. 20, 4 (October 2002), 357-389.
	/// The DFR scoring formula is composed of three separate components: the basic model, the aftereffect and an additional normalization component, 
	/// represented by the classes BasicModel, AfterEffect and Normalization, respectively.The names of these classes were chosen to match the names of their counterparts in the Terrier IR engine.
	/// </summary>
	public interface IDFRSimilarity : ISimilarity
	{
		[JsonProperty("basic_model")]
		DFRBasicModel? BasicModel { get; set; }

		[JsonProperty("after_effect")]
		DFRAfterEffect? AfterEffect { get; set; }

		[JsonProperty("normalization")]
		Normalization? Normalization { get; set; }

		/// <summary>
		/// Normalization model that assumes a uniform distribution of the term frequency. 
		/// </summary>
		[JsonProperty("normalization.h1.c")]
		double? NormalizationH1C { get; set; }

		/// <summary>
		///  Normalization model in which the term frequency is inversely related to the length.
		/// </summary>
		[JsonProperty("normalization.h2.c")]
		double? NormalizationH2C { get; set; }

		/// <summary>
		///  Dirichlet Priors normalization
		/// </summary>
		[JsonProperty("normalization.h3.c")]
		double? NormalizationH3C { get; set; }

		/// <summary>
		/// Pareto-Zipf Normalization
		/// </summary>
		[JsonProperty("normalization.z.z")]
		double? NormalizationZZ { get; set; }
	}
	public class DFRSimilarity : IDFRSimilarity
	{
		public string Type => "DFR";

		public DFRBasicModel? BasicModel { get; set; }

		public DFRAfterEffect? AfterEffect { get; set; }

		public Normalization? Normalization { get; set; }

		public double? NormalizationH1C { get; set; }

		public double? NormalizationH2C { get; set; }

		public double? NormalizationH3C { get; set; }

		public double? NormalizationZZ { get; set; }
	}

	public class DFRSimilarityDescriptor
		: DescriptorBase<DFRSimilarityDescriptor, IDFRSimilarity>, IDFRSimilarity
	{
		string ISimilarity.Type => "DFR";
		DFRBasicModel? IDFRSimilarity.BasicModel { get; set; }
		DFRAfterEffect? IDFRSimilarity.AfterEffect { get; set; }
		Normalization? IDFRSimilarity.Normalization { get; set; }
		double? IDFRSimilarity.NormalizationH1C { get; set; }
		double? IDFRSimilarity.NormalizationH2C { get; set; }
		double? IDFRSimilarity.NormalizationH3C { get; set; }
		double? IDFRSimilarity.NormalizationZZ { get; set; }

		public DFRSimilarityDescriptor BasicModel(DFRBasicModel? model) => Assign(a => a.BasicModel = model);
		public DFRSimilarityDescriptor AfterEffect(DFRAfterEffect? afterEffect) => Assign(a => a.AfterEffect = afterEffect);
		public DFRSimilarityDescriptor NoNormalization() => Assign(a => a.Normalization = Normalization.No);

		/// <summary>
		/// Normalization model that assumes a uniform distribution of the term frequency. 
		/// </summary>
		/// <param name="c">hyper-parameter that controls the term frequency normalization with respect to the document length.</param>
		public DFRSimilarityDescriptor NormalizationH1(double? c) => Assign(a =>
		{
			a.Normalization = Normalization.H1;
			a.NormalizationH1C = c;
		});

		/// <summary>
		/// Normalization model in which the term frequency is inversely related to the length.
		/// </summary>
		/// <param name="c">hyper-parameter that controls the term frequency normalization with respect to the document length.</param>
		public DFRSimilarityDescriptor NormalizationH2(double? c) => Assign(a =>
		{
			a.Normalization = Normalization.H2;
			a.NormalizationH1C = c;
		});

		/// <summary>
		/// Dirichlet Priors normalization
		/// </summary>
		/// <param name="mu">smoothing parameter μ.</param>
		public DFRSimilarityDescriptor NormalizationH3(double? mu) => Assign(a =>
		{
			a.Normalization = Normalization.H3;
			a.NormalizationH1C = mu;
		});

		/// <summary>
		/// Pareto-Zipf Normalization
		/// </summary>
		/// <param name="mu">represents A/(A+1) where A measures the specificity of the language..</param>
		public DFRSimilarityDescriptor NormalizationZ(double? z) => Assign(a =>
		{
			a.Normalization = Normalization.Z;
			a.NormalizationH1C = z;
		});
	}

}