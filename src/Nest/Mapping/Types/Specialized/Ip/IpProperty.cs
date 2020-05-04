// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// An ip field can index/store either IPv4 or IPv6 addresses.
	/// </summary>
	[InterfaceDataContract]
	public interface IIpProperty : IDocValuesProperty
	{
		[DataMember(Name ="boost")]
		double? Boost { get; set; }

		[DataMember(Name ="index")]
		bool? Index { get; set; }

		[DataMember(Name ="null_value")]
		string NullValue { get; set; }
	}

	/// <inheritdoc cref="IIpProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class IpProperty : DocValuesPropertyBase, IIpProperty
	{
		public IpProperty() : base(FieldType.Ip) { }

		public double? Boost { get; set; }
		public bool? Index { get; set; }
		public string NullValue { get; set; }
	}

	/// <inheritdoc cref="IIpProperty"/>
	[DebuggerDisplay("{DebugDisplay}")]
	public class IpPropertyDescriptor<T>
		: DocValuesPropertyDescriptorBase<IpPropertyDescriptor<T>, IIpProperty, T>, IIpProperty
		where T : class
	{
		public IpPropertyDescriptor() : base(FieldType.Ip) { }

		double? IIpProperty.Boost { get; set; }
		bool? IIpProperty.Index { get; set; }
		string IIpProperty.NullValue { get; set; }

		public IpPropertyDescriptor<T> Index(bool? index = true) => Assign(index, (a, v) => a.Index = v);

		public IpPropertyDescriptor<T> Boost(double? boost) => Assign(boost, (a, v) => a.Boost = v);

		public IpPropertyDescriptor<T> NullValue(string nullValue) => Assign(nullValue, (a, v) => a.NullValue = v);
	}
}
