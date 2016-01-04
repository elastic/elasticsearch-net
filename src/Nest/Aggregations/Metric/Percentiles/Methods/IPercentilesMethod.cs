using System;

namespace Nest
{
	public interface IPercentilesMethod { }

	public class PercentilesMethodDescriptor : DescriptorBase<PercentilesMethodDescriptor, IPercentilesMethod>, IPercentilesMethod
	{
		public IPercentilesMethod HDRHistogram(Func<HDRHistogramMethodDescriptor, IHDRHistogramMethod> hdrSelector = null) =>
			hdrSelector.InvokeOrDefault(new HDRHistogramMethodDescriptor());

		public IPercentilesMethod TDigest(Func<TDigestMethodDescriptor, ITDigestMethod> tdigestSelector = null) =>
			tdigestSelector.InvokeOrDefault(new TDigestMethodDescriptor());
	}
}
