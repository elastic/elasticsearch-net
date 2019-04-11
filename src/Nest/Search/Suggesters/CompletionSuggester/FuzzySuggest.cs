using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Fuzziness for suggester
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(FuzzySuggester))]
	public interface IFuzzySuggester
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
	public class FuzzySuggester : IFuzzySuggester
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

	/// <inheritdoc cref="IFuzzySuggester" />
	public class FuzzySuggestDescriptor<T> : DescriptorBase<FuzzySuggestDescriptor<T>, IFuzzySuggester>, IFuzzySuggester
		where T : class
	{
		IFuzziness IFuzzySuggester.Fuzziness { get; set; }
		int? IFuzzySuggester.MinLength { get; set; }
		int? IFuzzySuggester.PrefixLength { get; set; }
		bool? IFuzzySuggester.Transpositions { get; set; }
		bool? IFuzzySuggester.UnicodeAware { get; set; }

		/// <inheritdoc cref="IFuzzySuggester.Fuzziness" />
		public FuzzySuggestDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(fuzziness, (a, v) => a.Fuzziness = v);
		/// <inheritdoc cref="IFuzzySuggester.UnicodeAware" />
		public FuzzySuggestDescriptor<T> UnicodeAware(bool? aware = true) => Assign(aware, (a, v) => a.UnicodeAware = v);
		/// <inheritdoc cref="IFuzzySuggester.Transpositions" />
		public FuzzySuggestDescriptor<T> Transpositions(bool? transpositions = true) => Assign(transpositions, (a, v) => a.Transpositions = v);
		/// <inheritdoc cref="IFuzzySuggester.MinLength" />
		public FuzzySuggestDescriptor<T> MinLength(int? length) => Assign(length, (a, v) => a.MinLength = v);
		/// <inheritdoc cref="IFuzzySuggester.PrefixLength" />
		public FuzzySuggestDescriptor<T> PrefixLength(int? length) => Assign(length, (a, v) => a.PrefixLength = v);
	}
}
