using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	public interface ITDigestMethod : IPercentilesMethod
	{
		[DataMember(Name ="compression")]
		double? Compression { get; set; }
	}

	// ReSharper disable once InconsistentNaming
	public class TDigestMethod : ITDigestMethod
	{
		public double? Compression { get; set; }
	}

	// ReSharper disable once InconsistentNaming
	public class TDigestMethodDescriptor
		: DescriptorBase<TDigestMethodDescriptor, ITDigestMethod>, ITDigestMethod
	{
		double? ITDigestMethod.Compression { get; set; }

		public TDigestMethodDescriptor Compression(double? compression) => Assign(compression, (a, v) => a.Compression = v);
	}
}
