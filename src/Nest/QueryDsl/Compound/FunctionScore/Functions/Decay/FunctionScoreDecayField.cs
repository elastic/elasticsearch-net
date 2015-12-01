using Newtonsoft.Json;

namespace Nest
{

	public interface IFunctionScoreDecayField
	{
		[JsonProperty(PropertyName = "origin")]
		string Origin { get; set; }

		[JsonProperty(PropertyName = "scale")]
		string Scale { get; set; }

		[JsonProperty(PropertyName = "offset")]
		string Offset { get; set; }

		[JsonProperty(PropertyName = "decay")]
		double? Decay { get; set; }
	}


	public class FunctionScoreDecayField : IFunctionScoreDecayField
	{
		public string Origin { get; set; }

		public string Scale { get; set; }

		public string Offset { get; set; }

		public double? Decay { get; set; }
	}

	public class FunctionScoreDecayFieldDescriptor : DescriptorBase<FunctionScoreDecayFieldDescriptor, IFunctionScoreDecayField>, IFunctionScoreDecayField
	{
		string IFunctionScoreDecayField.Origin { get; set; }

		string IFunctionScoreDecayField.Scale { get; set; }

		string IFunctionScoreDecayField.Offset { get; set; }

		double? IFunctionScoreDecayField.Decay { get; set; }

		public FunctionScoreDecayFieldDescriptor Origin(string origin) => Assign(a => a.Origin = origin);

		public FunctionScoreDecayFieldDescriptor Scale(string scale) => Assign(a => a.Scale = scale);

		public FunctionScoreDecayFieldDescriptor Offset(string offset) => Assign(a => a.Offset = offset);

		public FunctionScoreDecayFieldDescriptor Decay(double? decay) => Assign(a => a.Decay = decay);
	}
}