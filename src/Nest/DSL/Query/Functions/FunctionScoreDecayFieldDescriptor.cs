using Newtonsoft.Json;

namespace Nest
{
	public class FunctionScoreDecayFieldDescriptor
	{
		[JsonProperty(PropertyName = "origin")]
		internal string _Origin { get; set; }

		[JsonProperty(PropertyName = "scale")]
		internal string _Scale { get; set; }

		[JsonProperty(PropertyName = "offset")]
		internal string _Offset { get; set; }

		[JsonProperty(PropertyName = "decay")]
		internal double? _Decay { get; set; }

		public FunctionScoreDecayFieldDescriptor Origin(string origin)
		{
			this._Origin = origin;
			return this;
		}

		public FunctionScoreDecayFieldDescriptor Scale(string scale)
		{
			this._Scale = scale;
			return this;
		}

		public FunctionScoreDecayFieldDescriptor Offset(string offset)
		{
			this._Offset = offset;
			return this;
		}

		public FunctionScoreDecayFieldDescriptor Decay(double? decay)
		{
			this._Decay = decay;
			return this;
		}
	}
}