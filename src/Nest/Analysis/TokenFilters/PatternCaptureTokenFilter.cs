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

using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// The pattern_capture token filter, unlike the pattern tokenizer, emits a token for every capture group in the regular expression.
	/// </summary>
	public interface IPatternCaptureTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The regular expression patterns to capture
		/// </summary>
		[DataMember(Name ="patterns")]
		IEnumerable<string> Patterns { get; set; }

		/// <summary>
		/// If preserve_original is set to true then it would also emit the original token
		/// </summary>
		[DataMember(Name ="preserve_original")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? PreserveOriginal { get; set; }
	}

	/// <inheritdoc />
	public class PatternCaptureTokenFilter : TokenFilterBase, IPatternCaptureTokenFilter
	{
		public PatternCaptureTokenFilter() : base("pattern_capture") { }

		/// <inheritdoc />
		public IEnumerable<string> Patterns { get; set; }

		/// <inheritdoc />
		public bool? PreserveOriginal { get; set; }
	}

	/// <inheritdoc />
	public class PatternCaptureTokenFilterDescriptor
		: TokenFilterDescriptorBase<PatternCaptureTokenFilterDescriptor, IPatternCaptureTokenFilter>, IPatternCaptureTokenFilter
	{
		protected override string Type => "pattern_capture";
		IEnumerable<string> IPatternCaptureTokenFilter.Patterns { get; set; }

		bool? IPatternCaptureTokenFilter.PreserveOriginal { get; set; }

		/// <inheritdoc />
		public PatternCaptureTokenFilterDescriptor PreserveOriginal(bool? preserve = true) => Assign(preserve, (a, v) => a.PreserveOriginal = v);

		/// <inheritdoc />
		public PatternCaptureTokenFilterDescriptor Patterns(IEnumerable<string> patterns) => Assign(patterns, (a, v) => a.Patterns = v);

		/// <inheritdoc />
		public PatternCaptureTokenFilterDescriptor Patterns(params string[] patterns) => Assign(patterns, (a, v) => a.Patterns = v);
	}
}
