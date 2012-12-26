using Newtonsoft.Json;

namespace Nest
{
    public class SizeFieldMapping
    {
		public SizeFieldMapping()
		{
			
		}

		[JsonProperty("enabled")]
		public bool Enabled { get; internal set; }

		public SizeFieldMapping SetDisabled(bool disabled = true)
		{
			this.Enabled = !disabled;
			return this;
		}
    }
}