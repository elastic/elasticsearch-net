using System.Runtime.Serialization;
using Utf8Json;

namespace Nest
{
	[StringEnum]
	public enum HoltWintersType
	{
		[EnumMember(Value = "add")]
		Additive,

		[EnumMember(Value = "mult")]
		Multiplicative
	}

	[InterfaceDataContract]
	[ReadAs(typeof(HoltWintersModel))]
	public interface IHoltWintersModel : IMovingAverageModel
	{
		[DataMember(Name = "alpha")]
		float? Alpha { get; set; }

		[DataMember(Name = "beta")]
		float? Beta { get; set; }

		[DataMember(Name = "gamma")]
		float? Gamma { get; set; }

		[DataMember(Name = "pad")]
		bool? Pad { get; set; }

		[DataMember(Name = "period")]
		int? Period { get; set; }

		[DataMember(Name = "type")]
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

		public HoltWintersModelDescriptor Alpha(float? alpha) => Assign(a => a.Alpha = alpha);

		public HoltWintersModelDescriptor Beta(float? beta) => Assign(a => a.Beta = beta);

		public HoltWintersModelDescriptor Gamma(float? gamma) => Assign(a => a.Gamma = gamma);

		public HoltWintersModelDescriptor Pad(bool? pad = true) => Assign(a => a.Pad = pad);

		public HoltWintersModelDescriptor Period(int? period) => Assign(a => a.Period = period);

		public HoltWintersModelDescriptor Type(HoltWintersType? type) => Assign(a => a.Type = type);
	}
}
