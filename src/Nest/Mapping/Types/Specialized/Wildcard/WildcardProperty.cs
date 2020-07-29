// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Diagnostics;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A wildcard field stores values optimised for wildcard grep-like queries.
	/// <para />
	/// Available in Elasticsearch 7.9.0+ with at least a basic license level
	/// </summary>
	[InterfaceDataContract]
	public interface IWildcardProperty : IProperty
	{
		/// <summary>
		/// Do not index any string longer than this value. Defaults to 2147483647 so that all values would be accepted.
		/// </summary>
		[DataMember(Name = "ignore_above")]
		int? IgnoreAbove { get; set; }
	}

	/// <inheritdoc cref="IWildcardProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class WildcardProperty : PropertyBase, IWildcardProperty
	{
		public WildcardProperty() : base(FieldType.Wildcard) { }

		/// <inheritdoc cref="IWildcardProperty.IgnoreAbove" />
		public int? IgnoreAbove { get; set; }
	}

	/// <inheritdoc cref="IWildcardProperty" />
	[DebuggerDisplay("{DebugDisplay}")]
	public class WildcardPropertyDescriptor<T>
		: PropertyDescriptorBase<WildcardPropertyDescriptor<T>, IWildcardProperty, T>, IWildcardProperty
		where T : class
	{
		public WildcardPropertyDescriptor() : base(FieldType.Wildcard) { }

		int? IWildcardProperty.IgnoreAbove { get; set; }

		/// <inheritdoc cref="IWildcardProperty.IgnoreAbove" />
		public WildcardPropertyDescriptor<T> IgnoreAbove(int? ignoreAbove) => Assign(ignoreAbove, (a, v) => a.IgnoreAbove = v);
	}
}
