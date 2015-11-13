using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IHDRHistogramMethod : IPercentilesMethod
	{
		[JsonProperty("number_of_significant_value_digits")]
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

		public HDRHistogramMethodDescriptor NumberOfSignificantValueDigits(int numDigits) =>
			Assign(a => a.NumberOfSignificantValueDigits = numDigits);
	}
}
