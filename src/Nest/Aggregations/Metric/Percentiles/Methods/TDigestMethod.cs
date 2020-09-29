// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

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
