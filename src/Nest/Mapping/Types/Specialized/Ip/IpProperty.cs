using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IIpProperty : IProperty
	{
		[JsonProperty("index")]
		NonStringIndexOption? Index { get; set; }

		[JsonProperty("precision_step")]
		int? PrecisionStep { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("null_value")]
		string NullValue { get; set; }

		[JsonProperty("include_in_all")]
		bool? IncludeInAll { get; set; }
	}

	public class IpProperty : PropertyBase, IIpProperty
	{
		public IpProperty() : base("ip") { }

		public double? Boost { get; set; }
		public bool? IncludeInAll { get; set; }
		public NonStringIndexOption? Index { get; set; }
		public string NullValue { get; set; }
		public int? PrecisionStep { get; set; }
	}

	public class IpPropertyDescriptor<T>
		: PropertyDescriptorBase<IpPropertyDescriptor<T>, IIpProperty, T>, IIpProperty
		where T : class
	{
		NonStringIndexOption? IIpProperty.Index { get; set; }
		int? IIpProperty.PrecisionStep { get; set; }
		double? IIpProperty.Boost { get; set; }
		string IIpProperty.NullValue { get; set; }
		bool? IIpProperty.IncludeInAll { get; set; }

		public IpPropertyDescriptor() : base("ip") { }

		public IpPropertyDescriptor<T> Index(NonStringIndexOption? index) => Assign(a => a.Index = index);

		public IpPropertyDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);

		public IpPropertyDescriptor<T> NullValue(string nullValue) => Assign(a => a.NullValue = nullValue);

		public IpPropertyDescriptor<T> PrecisionStep(int precisionStep) => Assign(a => a.PrecisionStep = precisionStep);

		public IpPropertyDescriptor<T> IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);
	}
}