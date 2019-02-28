using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(LinearInterpolationSmoothingModel))]
	public interface ILinearInterpolationSmoothingModel : ISmoothingModel
	{
		[DataMember(Name = "bigram_lambda")]
		double? BigramLambda { get; set; }

		[DataMember(Name = "trigram_lambda")]
		double? TrigramLambda { get; set; }

		[DataMember(Name = "unigram_lambda")]
		double? UnigramLambda { get; set; }
	}

	public class LinearInterpolationSmoothingModel : SmoothingModelBase, ILinearInterpolationSmoothingModel
	{
		public double? BigramLambda { get; set; }
		public double? TrigramLambda { get; set; }
		public double? UnigramLambda { get; set; }

		internal override void WrapInContainer(ISmoothingModelContainer container) => container.LinearInterpolation = this;
	}

	public class LinearInterpolationSmoothingModelDescriptor
		: DescriptorBase<LinearInterpolationSmoothingModelDescriptor, ILinearInterpolationSmoothingModel>, ILinearInterpolationSmoothingModel
	{
		double? ILinearInterpolationSmoothingModel.BigramLambda { get; set; }
		double? ILinearInterpolationSmoothingModel.TrigramLambda { get; set; }
		double? ILinearInterpolationSmoothingModel.UnigramLambda { get; set; }

		public LinearInterpolationSmoothingModelDescriptor TrigramLambda(double? lambda) => Assign(a => a.TrigramLambda = lambda);

		public LinearInterpolationSmoothingModelDescriptor UnigramLambda(double? lambda) => Assign(a => a.UnigramLambda = lambda);

		public LinearInterpolationSmoothingModelDescriptor BigramLambda(double? lambda) => Assign(a => a.BigramLambda = lambda);
	}
}
