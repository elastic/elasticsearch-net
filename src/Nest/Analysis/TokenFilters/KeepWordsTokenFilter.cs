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
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type keep that only keeps tokens with text contained in a predefined set of words.
	/// </summary>
	public interface IKeepWordsTokenFilter : ITokenFilter
	{
		/// <summary>
		/// A list of words to keep.
		/// </summary>
		[DataMember(Name ="keep_words")]
		IEnumerable<string> KeepWords { get; set; }

		/// <summary>
		/// A boolean indicating whether to lower case the words.
		/// </summary>
		[DataMember(Name ="keep_words_case")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? KeepWordsCase { get; set; }

		/// <summary>
		/// A path to a words file.
		/// </summary>
		[DataMember(Name ="keep_words_path")]
		string KeepWordsPath { get; set; }
	}

	/// <inheritdoc />
	public class KeepWordsTokenFilter : TokenFilterBase, IKeepWordsTokenFilter
	{
		public KeepWordsTokenFilter() : base("keep") { }

		/// <inheritdoc />
		public IEnumerable<string> KeepWords { get; set; }

		/// <inheritdoc />
		public bool? KeepWordsCase { get; set; }

		/// <inheritdoc />
		public string KeepWordsPath { get; set; }
	}

	/// <inheritdoc />
	public class KeepWordsTokenFilterDescriptor
		: TokenFilterDescriptorBase<KeepWordsTokenFilterDescriptor, IKeepWordsTokenFilter>, IKeepWordsTokenFilter
	{
		protected override string Type => "keep";
		IEnumerable<string> IKeepWordsTokenFilter.KeepWords { get; set; }

		bool? IKeepWordsTokenFilter.KeepWordsCase { get; set; }
		string IKeepWordsTokenFilter.KeepWordsPath { get; set; }

		/// <inheritdoc />
		public KeepWordsTokenFilterDescriptor KeepWordsCase(bool? keepCase = true) => Assign(keepCase, (a, v) => a.KeepWordsCase = v);

		/// <inheritdoc />
		public KeepWordsTokenFilterDescriptor KeepWordsPath(string path) => Assign(path, (a, v) => a.KeepWordsPath = v);

		/// <inheritdoc />
		public KeepWordsTokenFilterDescriptor KeepWords(IEnumerable<string> keepWords) => Assign(keepWords, (a, v) => a.KeepWords = v);

		/// <inheritdoc />
		public KeepWordsTokenFilterDescriptor KeepWords(params string[] keepWords) => Assign(keepWords, (a, v) => a.KeepWords = v);
	}
}
