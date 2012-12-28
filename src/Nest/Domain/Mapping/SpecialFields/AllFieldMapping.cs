using Newtonsoft.Json;

namespace Nest
{
    public class AllFieldMapping
    {
		public AllFieldMapping()
		{
			this.Enabled = true;
		}

		[JsonProperty("enabled")]
		public bool Enabled { get; internal set; }

		public AllFieldMapping SetDisabled(bool disabled = true)
		{
			this.Enabled = !disabled;
			return this;
		}
    }
}