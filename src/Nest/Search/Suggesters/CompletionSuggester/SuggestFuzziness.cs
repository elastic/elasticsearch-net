// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Fuzziness options for a suggester
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(SuggestFuzziness))]
	public interface ISuggestFuzziness
	{
		/// <summary>
		/// The fuzziness factor. defaults to AUTO
		/// </summary>
		[DataMember(Name = "fuzziness")]
		IFuzziness Fuzziness { get; set; }

		/// <summary>
		/// Minimum length of the input before fuzzy suggestions are returned. Defaults to 3
		/// </summary>
		[DataMember(Name = "min_length")]
		int? MinLength { get; set; }

		/// <summary>
		/// Minimum length of the input, which is not checked for fuzzy alternatives. Defaults to 1
		/// </summary>
		[DataMember(Name = "prefix_length")]
		int? PrefixLength { get; set; }

		/// <summary>
		/// if set to true, transpositions are counted as one change instead of two. Defaults to true
		/// </summary>
		[DataMember(Name = "transpositions")]
		bool? Transpositions { get; set; }

		/// <summary>
		/// If true, all measurements (like fuzzy edit distance, transpositions, and lengths) are measured in Unicode code
		/// points instead of in bytes. This is slightly slower than raw bytes, so it is set to false by default.
		/// </summary>
		[DataMember(Name = "unicode_aware")]
		bool? UnicodeAware { get; set; }
	}

	/// <inheritdoc />
	public class SuggestFuzziness : ISuggestFuzziness
	{
		/// <inheritdoc />
		public IFuzziness Fuzziness { get; set; }
		/// <inheritdoc />
		public int? MinLength { get; set; }
		/// <inheritdoc />
		public int? PrefixLength { get; set; }
		/// <inheritdoc />
		public bool? Transpositions { get; set; }
		/// <inheritdoc />
		public bool? UnicodeAware { get; set; }
	}

	/// <inheritdoc cref="ISuggestFuzziness" />
	public class SuggestFuzzinessDescriptor<T> : DescriptorBase<SuggestFuzzinessDescriptor<T>, ISuggestFuzziness>, ISuggestFuzziness
		where T : class
	{
		IFuzziness ISuggestFuzziness.Fuzziness { get; set; }
		int? ISuggestFuzziness.MinLength { get; set; }
		int? ISuggestFuzziness.PrefixLength { get; set; }
		bool? ISuggestFuzziness.Transpositions { get; set; }
		bool? ISuggestFuzziness.UnicodeAware { get; set; }

		/// <inheritdoc cref="ISuggestFuzziness.Fuzziness" />
		public SuggestFuzzinessDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(fuzziness, (a, v) => a.Fuzziness = v);
		/// <inheritdoc cref="ISuggestFuzziness.UnicodeAware" />
		public SuggestFuzzinessDescriptor<T> UnicodeAware(bool? aware = true) => Assign(aware, (a, v) => a.UnicodeAware = v);
		/// <inheritdoc cref="ISuggestFuzziness.Transpositions" />
		public SuggestFuzzinessDescriptor<T> Transpositions(bool? transpositions = true) => Assign(transpositions, (a, v) => a.Transpositions = v);
		/// <inheritdoc cref="ISuggestFuzziness.MinLength" />
		public SuggestFuzzinessDescriptor<T> MinLength(int? length) => Assign(length, (a, v) => a.MinLength = v);
		/// <inheritdoc cref="ISuggestFuzziness.PrefixLength" />
		public SuggestFuzzinessDescriptor<T> PrefixLength(int? length) => Assign(length, (a, v) => a.PrefixLength = v);
	}
}
