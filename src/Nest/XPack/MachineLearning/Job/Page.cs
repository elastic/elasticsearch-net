/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
