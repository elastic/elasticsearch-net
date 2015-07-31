using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using System;
using System.Linq.Expressions;

namespace Nest
{
	public interface IIpType : IElasticType
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

	[JsonObject(MemberSerialization.OptIn)]
	public class IpType : ElasticType, IIpType
	{
		public IpType() : base("ip") { }

		public double? Boost { get; set; }
		public bool? IncludeInAll { get; set; }
		public NonStringIndexOption? Index { get; set; }
		public string NullValue { get; set; }
		public int? PrecisionStep { get; set; }
	}

	public class IpTypeDescriptor<T>
		: TypeDescriptorBase<IpTypeDescriptor<T>, IIpType, T>, IIpType
		where T : class
	{
		NonStringIndexOption? IIpType.Index { get; set; }
		int? IIpType.PrecisionStep { get; set; }
		double? IIpType.Boost { get; set; }
		string IIpType.NullValue { get; set; }
		bool? IIpType.IncludeInAll { get; set; }

		public IpTypeDescriptor<T> Index(NonStringIndexOption? index) => Assign(a => a.Index = index);

		public IpTypeDescriptor<T> Boost(double boost) => Assign(a => a.Boost = boost);

		public IpTypeDescriptor<T> NullValue(string nullValue) => Assign(a => a.NullValue = nullValue);

		public IpTypeDescriptor<T> PrecisionStep(int precisionStep) => Assign(a => a.PrecisionStep = precisionStep);

		public IpTypeDescriptor<T> IncludeInAll(bool includeInAll = true) => Assign(a => a.IncludeInAll = includeInAll);
	}
}