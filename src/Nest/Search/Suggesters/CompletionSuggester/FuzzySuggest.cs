using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(FuzzySuggester))]
	public interface IFuzzySuggester
	{
		[DataMember(Name = "fuzziness")]
		IFuzziness Fuzziness { get; set; }

		[DataMember(Name = "min_length")]
		int? MinLength { get; set; }

		[DataMember(Name = "prefix_length")]
		int? PrefixLength { get; set; }

		[DataMember(Name = "transpositions")]
		bool? Transpositions { get; set; }

		[DataMember(Name = "unicode_aware")]
		bool? UnicodeAware { get; set; }
	}

	public class FuzzySuggester : IFuzzySuggester
	{
		public IFuzziness Fuzziness { get; set; }
		public int? MinLength { get; set; }
		public int? PrefixLength { get; set; }
		public bool? Transpositions { get; set; }
		public bool? UnicodeAware { get; set; }
	}

	public class FuzzySuggestDescriptor<T> : DescriptorBase<FuzzySuggestDescriptor<T>, IFuzzySuggester>, IFuzzySuggester
		where T : class
	{
		IFuzziness IFuzzySuggester.Fuzziness { get; set; }
		int? IFuzzySuggester.MinLength { get; set; }
		int? IFuzzySuggester.PrefixLength { get; set; }
		bool? IFuzzySuggester.Transpositions { get; set; }
		bool? IFuzzySuggester.UnicodeAware { get; set; }

		public FuzzySuggestDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		public FuzzySuggestDescriptor<T> UnicodeAware(bool? aware = true) => Assign(a => a.UnicodeAware = aware);

		public FuzzySuggestDescriptor<T> Transpositions(bool? transpositions = true) => Assign(a => a.Transpositions = transpositions);

		public FuzzySuggestDescriptor<T> MinLength(int? length) => Assign(a => a.MinLength = length);

		public FuzzySuggestDescriptor<T> PrefixLength(int? length) => Assign(a => a.PrefixLength = length);
	}
}
