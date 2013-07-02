using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The phonetic token filter is provided as a plugin.
	/// </summary>
	public class PhoneticTokenFilter : TokenFilterBase
	{
		public PhoneticTokenFilter()
			: base("phonetic")
		{

        }

        [JsonProperty("encoder")]
        public string Encoder { get; set; }

        [JsonProperty("replace")]
        public bool Replace { get; set; }

	}

}



