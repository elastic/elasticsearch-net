using Newtonsoft.Json;

namespace Nest
{
    public class IndexFieldMapping
    {
		public IndexFieldMapping()
		{
			this.Enabled = true;
		}

		[JsonProperty("enabled")]
		public bool Enabled { get; internal set; }

		public IndexFieldMapping SetDisabled(bool disabled = true)
		{
			this.Enabled = !disabled;
			return this;
		}
    }
}