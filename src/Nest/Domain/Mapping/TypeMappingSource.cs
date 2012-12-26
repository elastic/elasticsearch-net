using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    public class SourceMapping
    {
        public SourceMapping()
        {
            this.Enabled = true;
        }

        [JsonProperty("enabled")]
        public bool Enabled { get; internal set; }
		
		[JsonProperty("compress")]
		public bool Compress { get; internal set; }

		[JsonProperty("compress_treshold")]
		public string CompressTreshold { get; internal set; }

		[JsonProperty("includes")]
		public IEnumerable<string> Includes { get; internal set; }

		[JsonProperty("excludes")]
		public IEnumerable<string> Excludes { get; internal set; }

		public SourceMapping SetDisabled(bool disabled = true)
		{
			this.Enabled = !disabled;
			return this;
		}

		public SourceMapping SetCompressionTreshold(string compressionTreshold)
		{
			this.Compress = true;
			this.CompressTreshold = compressionTreshold;
			return this;
		}

		public SourceMapping SetIncludes(IEnumerable<string> includes)
		{
			this.Includes = includes;
			return this;
		}

		public SourceMapping SetExcludes(IEnumerable<string> excludes)
		{
			this.Excludes = excludes;
			return this;
		}
    }
}