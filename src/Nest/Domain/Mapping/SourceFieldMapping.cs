using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
    public class SourceFieldMapping
    {
        public SourceFieldMapping()
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

		public SourceFieldMapping SetDisabled(bool disabled = true)
		{
			this.Enabled = !disabled;
			return this;
		}
		public SourceFieldMapping SetCompression(bool enabled = true)
		{
			this.Compress = enabled;
			return this;
		}


		public SourceFieldMapping SetCompressionTreshold(string compressionTreshold)
		{
			this.Compress = true;
			this.CompressTreshold = compressionTreshold;
			return this;
		}

		public SourceFieldMapping SetIncludes(IEnumerable<string> includes)
		{
			this.Includes = includes;
			return this;
		}

		public SourceFieldMapping SetExcludes(IEnumerable<string> excludes)
		{
			this.Excludes = excludes;
			return this;
		}
    }
}