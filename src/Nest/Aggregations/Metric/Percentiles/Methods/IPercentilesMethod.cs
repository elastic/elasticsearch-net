using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public interface IPercentilesMethod 
	{
	}

	public class PercentilesMethodDescriptor
	{
		public IPercentilesMethod HDRHistogram(Func<HDRHistogramMethodDescriptor, IHDRHistogramMethod> hdrSelector = null) =>
			hdrSelector?.InvokeOrDefault(new HDRHistogramMethodDescriptor());

		public IPercentilesMethod TDigest(Func<TDigestMethodDescriptor, ITDigestMethod> tdigestSelector = null) =>
			tdigestSelector?.InvokeOrDefault(new TDigestMethodDescriptor());
	}
}
