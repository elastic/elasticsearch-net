using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public interface IIpProperty : IDocValuesProperty
	{
		[JsonProperty("index")]
		bool? Index { get; set; }

		[JsonProperty("boost")]
		double? Boost { get; set; }

		[JsonProperty("null_value")]
		string NullValue { get; set; }

		[JsonProperty("include_in_all")]
		bool? IncludeInAll { get; set; }
	}

	public class IpProperty : DocValuesPropertyBase, IIpProperty
	{
		public IpProperty() : base("ip") { }

		public double? Boost { get; set; }
		public bool? IncludeInAll { get; set; }
		public bool? Index { get; set; }
		public string NullValue { get; set; }
	}

	public class IpPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<IpPropertyDescriptor<T>, IIpProperty, T>, IIpProperty
		where T : class
	{
		bool? IIpProperty.Index { get; set; }
		double? IIpProperty.Boost { get; set; }
		string IIpProperty.NullValue { get; set; }
		bool? IIpProperty.IncludeInAll { get; set; }

		public IpPropertyDescriptor() : base("ip") { }

		public IpPropertyDescriptor<T> Index(bool index) => Assign(a => a.Index = index);

		public IpPropertyDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);

		public IpPropertyDescriptor<T> NullValue(string nullValue) => Assign(a => a.NullValue = nullValue);

		public IpPropertyDescriptor<T> IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);
	}
}
