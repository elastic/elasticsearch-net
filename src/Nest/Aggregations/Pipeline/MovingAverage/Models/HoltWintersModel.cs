using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum HoltWintersType
	{
		[EnumMember(Value = "add")]
		Additive,

		[EnumMember(Value = "mult")]
		Multiplicative
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IHoltWintersModel : IMovingAverageModel
	{
		[JsonProperty("alpha")]
		float? Alpha { get; set; }

		[JsonProperty("beta")]
		float? Beta { get; set; }

		[JsonProperty("gamma")]
		float? Gamma { get; set; }

		[JsonProperty("pad")]
		bool? Pad { get; set; }

		[JsonProperty("period")]
		int? Period { get; set; }

		[JsonProperty("type")]
		HoltWintersType? Type { get; set; }
	}

	public class HoltWintersModel : IHoltWintersModel
	{
		public float? Alpha { get; set; }
		public float? Beta { get; set; }
		public float? Gamma { get; set; }
		public bool? Pad { get; set; }
		public int? Period { get; set; }
		public HoltWintersType? Type { get; set; }
		string IMovingAverageModel.Name { get; } = "holt_winters";
	}

	public class HoltWintersModelDescriptor
		: DescriptorBase<HoltWintersModelDescriptor, IHoltWintersModel>, IHoltWintersModel
	{
		float? IHoltWintersModel.Alpha { get; set; }
		float? IHoltWintersModel.Beta { get; set; }
		float? IHoltWintersModel.Gamma { get; set; }
		string IMovingAverageModel.Name { get; } = "holt_winters";
		bool? IHoltWintersModel.Pad { get; set; }
		int? IHoltWintersModel.Period { get; set; }
		HoltWintersType? IHoltWintersModel.Type { get; set; }

		public HoltWintersModelDescriptor Alpha(float? alpha) => Assign(alpha, (a, v) => a.Alpha = v);

		public HoltWintersModelDescriptor Beta(float? beta) => Assign(beta, (a, v) => a.Beta = v);

		public HoltWintersModelDescriptor Gamma(float? gamma) => Assign(gamma, (a, v) => a.Gamma = v);

		public HoltWintersModelDescriptor Pad(bool? pad = true) => Assign(pad, (a, v) => a.Pad = v);

		public HoltWintersModelDescriptor Period(int? period) => Assign(period, (a, v) => a.Period = v);

		public HoltWintersModelDescriptor Type(HoltWintersType? type) => Assign(type, (a, v) => a.Type = v);
	}
}
