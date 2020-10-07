// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Elastic.Transport;

namespace Nest
{
	/// <summary>
	/// Block type for an index.
	/// </summary>
	public class IndexBlock : IUrlParameter
	{
		private IndexBlock(string value) => Value = value;

		public string Value { get; }

		public string GetString(ITransportConfigurationValues settings) => Value;

		/// <summary>
		/// Disable metadata changes, such as closing the index.
		/// </summary>
		public static IndexBlock Metadata { get; } = new IndexBlock("metadata");

		/// <summary>
		/// Disable read operations.
		/// </summary>
		public static IndexBlock Read { get; } = new IndexBlock("read");

		/// <summary>
		/// Disable write operations and metadata changes.
		/// </summary>
		public static IndexBlock ReadOnly { get; } = new IndexBlock("read_only");

		/// <summary>
		/// Disable write operations. However, metadata changes are still allowed.
		/// </summary>
		public static IndexBlock Write { get; } = new IndexBlock("write");
	}
}
