// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;

namespace Nest
{
	public interface IPercentilesMethod { }

	public class PercentilesMethodDescriptor : DescriptorBase<PercentilesMethodDescriptor, IPercentilesMethod>, IPercentilesMethod
	{
		public IPercentilesMethod HDRHistogram(Func<HDRHistogramMethodDescriptor, IHDRHistogramMethod> hdrSelector = null) =>
			hdrSelector.InvokeOrDefault(new HDRHistogramMethodDescriptor());

	// ReSharper disable once InconsistentNaming
		public IPercentilesMethod TDigest(Func<TDigestMethodDescriptor, ITDigestMethod> tdigestSelector = null) =>
			tdigestSelector.InvokeOrDefault(new TDigestMethodDescriptor());
	}
}
