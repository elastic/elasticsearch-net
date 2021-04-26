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
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type length that removes words that are too long or too short for the stream.
	/// </summary>
	public interface ILengthTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The maximum number. Defaults to Integer.MAX_VALUE.
		/// </summary>
		[DataMember(Name ="max")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? Max { get; set; }

		/// <summary>
		/// The minimum number. Defaults to 0.
		/// </summary>
		[DataMember(Name ="min")]
		[JsonFormatter(typeof(NullableStringIntFormatter))]
		int? Min { get; set; }
	}

	/// <inheritdoc />
	public class LengthTokenFilter : TokenFilterBase, ILengthTokenFilter
	{
		public LengthTokenFilter() : base("length") { }

		/// <inheritdoc />
		public int? Max { get; set; }

		/// <inheritdoc />
		public int? Min { get; set; }
	}

	/// <inheritdoc />
	public class LengthTokenFilterDescriptor
		: TokenFilterDescriptorBase<LengthTokenFilterDescriptor, ILengthTokenFilter>, ILengthTokenFilter
	{
		protected override string Type => "length";
		int? ILengthTokenFilter.Max { get; set; }

		int? ILengthTokenFilter.Min { get; set; }

		/// <inheritdoc />
		public LengthTokenFilterDescriptor Min(int? minimum) => Assign(minimum, (a, v) => a.Min = v);

		/// <inheritdoc />
		public LengthTokenFilterDescriptor Max(int? maximum) => Assign(maximum, (a, v) => a.Max = v);
	}
}
