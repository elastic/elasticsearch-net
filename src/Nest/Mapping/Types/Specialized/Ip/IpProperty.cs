using System.Diagnostics;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface IIpProperty : IDocValuesProperty
	{
		[DataMember(Name ="boost")]
		double? Boost { get; set; }

		[DataMember(Name ="index")]
		bool? Index { get; set; }

		[DataMember(Name ="null_value")]
		string NullValue { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class IpProperty : DocValuesPropertyBase, IIpProperty
	{
		public IpProperty() : base(FieldType.Ip) { }

		public double? Boost { get; set; }
		public bool? Index { get; set; }
		public string NullValue { get; set; }
	}

	[DebuggerDisplay("{DebugDisplay}")]
	public class IpPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<IpPropertyDescriptor<T>, IIpProperty, T>, IIpProperty
		where T : class
	{
		public IpPropertyDescriptor() : base(FieldType.Ip) { }

		double? IIpProperty.Boost { get; set; }
		bool? IIpProperty.Index { get; set; }
		string IIpProperty.NullValue { get; set; }

		public IpPropertyDescriptor<T> Index(bool? index = true) => Assign(a => a.Index = index);

		public IpPropertyDescriptor<T> Boost(double? boost) => Assign(a => a.Boost = boost);

		public IpPropertyDescriptor<T> NullValue(string nullValue) => Assign(a => a.NullValue = nullValue);
	}
}
