using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Filters which terms to exclude from the response
	/// </summary>
	[JsonConverter(typeof(TermsExcludeJsonConverter))]
	public class TermsExclude
	{
		/// <summary>
		/// The regular expression pattern to determine terms to exclude from the response
		/// </summary>
		[JsonIgnore]
		public string Pattern { get; set; }

		/// <summary>
		/// Collection of terms to exclude from the response
		/// </summary>
		[JsonIgnore]
		public IEnumerable<string> Values { get; set; }

		/// <summary>
		/// Creates an instance of <see cref="TermsExclude"/> that uses a regular expression pattern
		/// to determine the terms to exclude from the response
		/// </summary>
		/// <param name="pattern">The regular expression pattern</param>
		public TermsExclude(string pattern) => Pattern = pattern;

		/// <summary>
		/// Creates an instance of <see cref="TermsExclude"/> that uses a collection of terms
		/// to exclude from the response
		/// </summary>
		/// <param name="values">The exact terms to exclude</param>
		public TermsExclude(IEnumerable<string> values) => Values = values;
	}
}
