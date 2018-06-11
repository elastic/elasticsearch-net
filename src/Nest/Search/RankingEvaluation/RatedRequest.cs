using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A typical search request to rank evaluate
	/// </summary>
	public class RatedRequest
	{
		/// <summary>
		/// The id of the request. Used to group result details later
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		///	The query to evaluate
		/// </summary>
		[JsonProperty("request")]
		public QueryContainer Request { get; set; }

		/// <summary>
		/// A reference to the template definition to use
		/// </summary>
		[JsonProperty("template_id")]
		public string TemplateId { get; set; }

		/// <summary>
		/// The parameters to use to fill the template
		/// </summary>
		[JsonProperty("params")]
		[JsonConverter(typeof(VerbatimDictionaryKeysPreservingNullJsonConverter<string, object>))]
		Dictionary<string, object> Params { get; set; }

		/// <summary>
		/// A collection of document ratings containing the index, type, id and rating
		/// </summary>
		[JsonProperty("ratings")]
		public IEnumerable<RatedDocument> Ratings{ get; set; }
	}
}