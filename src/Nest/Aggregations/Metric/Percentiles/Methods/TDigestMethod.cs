using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
	public interface ITDigestMethod : IPercentilesMethod
	{
		[DataMember(Name ="compression")]
		double? Compression { get; set; }
	}

	public class TDigestMethod : ITDigestMethod
	{
		public double? Compression { get; set; }
	}

	public class TDigestMethodDescriptor
		: DescriptorBase<TDigestMethodDescriptor, ITDigestMethod>, ITDigestMethod
	{
		double? ITDigestMethod.Compression { get; set; }

		public TDigestMethodDescriptor Compression(double? compression) => Assign(a => a.Compression = compression);
	}
}
