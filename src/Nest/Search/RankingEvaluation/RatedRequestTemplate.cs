using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The template for a rated request
	/// </summary>
	public class RatedRequestTemplate
	{
		/// <summary>
		/// The id for the template
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// The template definition to use
		/// </summary>
		[JsonProperty("template")]
		public IScript Template { get; set; }
	}
}