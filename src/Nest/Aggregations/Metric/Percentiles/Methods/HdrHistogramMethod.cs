using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	public interface IHDRHistogramMethod : IPercentilesMethod
	{
		[DataMember(Name ="number_of_significant_value_digits")]
		int? NumberOfSignificantValueDigits { get; set; }
	}

	public class HDRHistogramMethod : IHDRHistogramMethod
	{
		public int? NumberOfSignificantValueDigits { get; set; }
	}

	public class HDRHistogramMethodDescriptor
		: DescriptorBase<HDRHistogramMethodDescriptor, IHDRHistogramMethod>, IHDRHistogramMethod
	{
		int? IHDRHistogramMethod.NumberOfSignificantValueDigits { get; set; }

		public HDRHistogramMethodDescriptor NumberOfSignificantValueDigits(int? numDigits) =>
			Assign(a => a.NumberOfSignificantValueDigits = numDigits);
	}
}
