using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<FuzzySuggester>))]
	public interface IFuzzySuggester
	{
		[JsonProperty(PropertyName = "transpositions")]
		bool? Transpositions { get; set; }

		[JsonProperty(PropertyName = "min_length")]
		int? MinLength { get; set; }

		[JsonProperty(PropertyName = "prefix_length")]
		int? PrefixLength { get; set; }

		[JsonProperty(PropertyName = "fuzziness")]
		IFuzziness Fuzziness { get; set; }

		[JsonProperty(PropertyName = "unicode_aware")]
		bool? UnicodeAware { get; set; }
	}

	public class FuzzySuggester : IFuzzySuggester
	{
		public bool? Transpositions { get; set; }
		public int? MinLength { get; set; }
		public int? PrefixLength { get; set; }
		public IFuzziness Fuzziness { get; set; }
		public bool? UnicodeAware { get; set; }
	}

	public class FuzzySuggestDescriptor<T> : DescriptorBase<FuzzySuggestDescriptor<T>, IFuzzySuggester>,  IFuzzySuggester 
		where T : class
	{
		bool? IFuzzySuggester.Transpositions { get; set; }
		int? IFuzzySuggester.MinLength { get; set; }
		int? IFuzzySuggester.PrefixLength { get; set; }
		IFuzziness IFuzzySuggester.Fuzziness { get; set; }
		bool? IFuzzySuggester.UnicodeAware { get; set; }

		public FuzzySuggestDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		public FuzzySuggestDescriptor<T> UnicodeAware(bool? aware = true) => Assign(a => a.UnicodeAware = aware);

		public FuzzySuggestDescriptor<T> Transpositions(bool? transpositions = true) => Assign(a => a.Transpositions = transpositions);

		public FuzzySuggestDescriptor<T> MinLength(int? length) => Assign(a => a.MinLength = length);

		public FuzzySuggestDescriptor<T> PrefixLength(int length) => Assign(a => a.PrefixLength = length);
	}
}
