using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<LinearInterpolationSmoothingModel>))]
	public interface ILinearInterpolationSmoothingModel : ISmoothingModel
	{
		[JsonProperty("trigram_lambda")]
		double? TrigramLambda { get; set; }
		[JsonProperty("bigram_lambda")]
		double? BigramLambda { get; set; }
		[JsonProperty("unigram_lambda")]
		double? UnigramLambda { get; set; }
	}

	public class LinearInterpolationSmoothingModel : SmoothingModelBase, ILinearInterpolationSmoothingModel
	{
		public double? TrigramLambda { get; set; }
		public double? BigramLambda { get; set; }
		public double? UnigramLambda { get; set; }

		internal override void WrapInContainer(ISmoothingModelContainer container) => container.LinearInterpolation = this;
	}

	public class LinearInterpolationSmoothingModelDescriptor : DescriptorBase<LinearInterpolationSmoothingModelDescriptor, ILinearInterpolationSmoothingModel>, ILinearInterpolationSmoothingModel
	{
		double? ILinearInterpolationSmoothingModel.TrigramLambda { get; set; }
		double? ILinearInterpolationSmoothingModel.UnigramLambda { get; set; }
		double? ILinearInterpolationSmoothingModel.BigramLambda { get; set; }

		public LinearInterpolationSmoothingModelDescriptor TrigramLambda(double? lambda) => Assign(a => a.TrigramLambda = lambda);
		public LinearInterpolationSmoothingModelDescriptor UnigramLambda(double? lambda) => Assign(a => a.UnigramLambda = lambda);
		public LinearInterpolationSmoothingModelDescriptor BigramLambda(double? lambda) => Assign(a => a.BigramLambda = lambda);
	}
}
