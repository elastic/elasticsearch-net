// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Filters which terms to exclude from the response
	/// </summary>
	[JsonFormatter(typeof(TermsExcludeFormatter))]
	public class TermsExclude
	{
		/// <summary>
		/// Creates an instance of <see cref="TermsExclude" /> that uses a regular expression pattern
		/// to determine the terms to exclude from the response
		/// </summary>
		/// <param name="pattern">The regular expression pattern</param>
		public TermsExclude(string pattern) => Pattern = pattern;

		/// <summary>
		/// Creates an instance of <see cref="TermsExclude" /> that uses a collection of terms
		/// to exclude from the response
		/// </summary>
		/// <param name="values">The exact terms to exclude</param>
		public TermsExclude(IEnumerable<string> values) => Values = values;

		/// <summary>
		/// The regular expression pattern to determine terms to exclude from the response
		/// </summary>
		public string Pattern { get; set; }

		/// <summary>
		/// Collection of terms to exclude from the response
		/// </summary>
		public IEnumerable<string> Values { get; set; }
	}
}
