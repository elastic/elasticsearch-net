using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

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

	public class FuzzySuggestDescriptor<T> : IFuzzySuggester where T : class 
    {
		protected IFuzzySuggester Self => this;

        bool? IFuzzySuggester.Transpositions { get; set; }

        int? IFuzzySuggester.MinLength { get; set; }

        int? IFuzzySuggester.PrefixLength { get; set; }

        IFuzziness IFuzzySuggester.Fuzziness { get; set; }

        bool? IFuzzySuggester.UnicodeAware { get; set; }
		
		public FuzzySuggestDescriptor<T> Fuzziness()
		{
			Self.Fuzziness = Nest.Fuzziness.Auto;
			return this;
		}
		public FuzzySuggestDescriptor<T> Fuzziness(int editDistance)
		{
			Self.Fuzziness = Nest.Fuzziness.EditDistance(editDistance);
			return this;
		}
		public FuzzySuggestDescriptor<T> Fuzziness(double ratio)
		{
			Self.Fuzziness = Nest.Fuzziness.Ratio(ratio);
			return this;
		}

        public FuzzySuggestDescriptor<T> UnicodeAware(bool aware = true)
        {
            Self.UnicodeAware = aware;
            return this;
        }

        public FuzzySuggestDescriptor<T> Transpositions(bool transpositions)
        {
            Self.Transpositions = transpositions;
            return this;
        }

        public FuzzySuggestDescriptor<T> MinLength(int length)
        {
            Self.MinLength = length;
            return this;
        }

        public FuzzySuggestDescriptor<T> PrefixLength(int length)
        {
            Self.PrefixLength = length;
            return this;
        }
    }
}
