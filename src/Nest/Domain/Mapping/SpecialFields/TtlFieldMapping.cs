using Newtonsoft.Json;

namespace Nest
{
    public class TtlFieldMapping
    {
		public TtlFieldMapping()
		{
			
		}

		[JsonProperty("enabled")]
		public bool Enabled { get; internal set; }

		[JsonProperty("default")]
		public string Default { get; internal set; }

		public TtlFieldMapping SetDisabled(bool disabled = true)
		{
			this.Enabled = !disabled;
			return this;
		}
		public TtlFieldMapping SetDefault(string defaultTtl)
		{
			this.Default = defaultTtl;
			return this;
		}
    }
}