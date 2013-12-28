using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class FuzzySuggestDescriptor<T> where T : class
    {
        [JsonProperty(PropertyName="edit_distance")]
        internal int _EditDistance { get; set; }

        [JsonProperty(PropertyName = "transpositions")]
        internal bool _Transpositions { get; set; }

        [JsonProperty(PropertyName = "min_length")]
        internal int _MinLength { get; set; }

        [JsonProperty(PropertyName = "prefix_length")]
        internal int _PrefixLength { get; set; }

        public FuzzySuggestDescriptor()
        {
            // Default values
            this._EditDistance = 1;
            this._Transpositions = true;
            this._MinLength = 3;
            this._PrefixLength = 1;
        }

        public FuzzySuggestDescriptor<T> EditDistance(int distance)
        {
            this._EditDistance = distance;
            return this;
        }

        public FuzzySuggestDescriptor<T> Transpositions(bool transpositions)
        {
            this._Transpositions = transpositions;
            return this;
        }

        public FuzzySuggestDescriptor<T> MinLength(int length)
        {
            this._MinLength = length;
            return this;
        }

        public FuzzySuggestDescriptor<T> PrefixLength(int length)
        {
            this._PrefixLength = length;
            return this;
        }
    }
}
