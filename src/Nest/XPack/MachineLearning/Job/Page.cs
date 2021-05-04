// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	public interface IPage
	{
		/// <summary>
		/// Skips the specified number of buckets.
		/// </summary>
		[DataMember(Name ="from")]
		int? From { get; set; }

		/// <summary>
		/// Specifies the maximum number of buckets to obtain.
		/// </summary>
		[DataMember(Name ="size")]
		int? Size { get; set; }
	}

	public class Page : IPage
	{
		/// <inheritdoc />
		public int? From { get; set; }

		/// <inheritdoc />
		public int? Size { get; set; }
	}

	public class PageDescriptor : DescriptorBase<PageDescriptor, IPage>, IPage
	{
		int? IPage.From { get; set; }
		int? IPage.Size { get; set; }

		/// <inheritdoc />
		public PageDescriptor From(int? from) => Assign(from, (a, v) => a.From = v);

		/// <inheritdoc />
		public PageDescriptor Size(int? size) => Assign(size, (a, v) => a.Size = v);
	}
}
