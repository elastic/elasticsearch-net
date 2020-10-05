// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.IO;

namespace Elasticsearch.Net
{
	/// <summary>
	/// A factory for creating memory streams using instances of <see cref="MemoryStream" />
	/// </summary>
	public class MemoryStreamFactory : IMemoryStreamFactory
	{
		public static MemoryStreamFactory Default { get; } = new MemoryStreamFactory();

		/// <inheritdoc />
		public MemoryStream Create() => new MemoryStream();

		/// <inheritdoc />
		public MemoryStream Create(byte[] bytes) => new MemoryStream(bytes);

		/// <inheritdoc />
		public MemoryStream Create(byte[] bytes, int index, int count) => new MemoryStream(bytes, index, count);
	}
}
