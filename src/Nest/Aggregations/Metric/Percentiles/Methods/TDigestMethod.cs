using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface ITDigestMethod : IPercentilesMethod
	{
		[JsonProperty("compression")]
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

		public TDigestMethodDescriptor Compression(double compression) => Assign(a => a.Compression = compression);
	}
}
