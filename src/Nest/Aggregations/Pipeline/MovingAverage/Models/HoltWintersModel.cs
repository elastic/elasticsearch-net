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

		[JsonProperty("period")]
		int? Period { get; set; }

		[JsonProperty("type")]
		HoltWintersType? Type { get; set; }
		
		[JsonProperty("pad")]
		bool? Pad { get; set; }
	}

	public class HoltWintersModel : IHoltWintersModel
	{
		string IMovingAverageModel.Name { get; } = "holt_winters";

		public float? Alpha { get; set; }
		public float? Beta { get; set; }
		public float? Gamma { get; set; }
		public int? Period { get; set; }
		public HoltWintersType? Type { get; set; }
		public bool? Pad { get; set; }
	}

	public class HoltWintersModelDescriptor
		: DescriptorBase<HoltWintersModelDescriptor, IHoltWintersModel>, IHoltWintersModel
	{
		string IMovingAverageModel.Name { get; } = "holt_winters";
		float? IHoltWintersModel.Alpha { get; set; }
		float? IHoltWintersModel.Beta { get; set; }
		float? IHoltWintersModel.Gamma { get; set; }
		bool? IHoltWintersModel.Pad { get; set; }
		int? IHoltWintersModel.Period { get; set; }
		HoltWintersType? IHoltWintersModel.Type { get; set; }

		public HoltWintersModelDescriptor Alpha(float alpha) => Assign(a => a.Alpha = alpha);

		public HoltWintersModelDescriptor Beta(float beta) => Assign(a => a.Beta = beta);

		public HoltWintersModelDescriptor Gamma(float gamma) => Assign(a => a.Gamma = gamma);

		public HoltWintersModelDescriptor Pad(bool pad) => Assign(a => a.Pad = pad);

		public HoltWintersModelDescriptor Period(int period) => Assign(a => a.Period = period);

		public HoltWintersModelDescriptor Type(HoltWintersType type) => Assign(a => a.Type = type);
	}
}
