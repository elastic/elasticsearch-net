using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
    public class PartialFieldDescriptor<T> where T : class
    {
        internal string _Field { get; set; }

        [JsonProperty("include")]
        internal List<string> _Include { get; set; }

        [JsonProperty("exclude")]
        internal List<string> _Exclude { get; set; }

        public PartialFieldDescriptor<T> PartialField(string field)
        {
            this._Field = field;

            return this;
        }

        public PartialFieldDescriptor<T> Include(params string[] path)
        {
            List<string> includes = new List<string>();
            foreach (var include in path)
                includes.Add(include);

            this._Include = includes;

            return this;
        }

        public PartialFieldDescriptor<T> Exclude(params string[] path)
        {
            List<string> excludes = new List<string>();
            foreach (var exclude in path)
                excludes.Add(exclude);

            this._Exclude = excludes;

            return this;
        }
    }
}
