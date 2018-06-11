using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A rated document
	/// </summary>
	public class RatedDocument : IDocumentPath
	{
		/// <summary>
		/// The id of the document
		/// </summary>
		[JsonProperty("_id")]
		public Id Id { get; set; }

		/// <summary>
		/// The index of the document
		/// </summary>
		[JsonProperty("_index")]
		public IndexName Index { get; set; }

		/// <summary>
		/// The type of document
		/// </summary>
		[JsonProperty("_type")]
		public TypeName Type { get; set; }

		/// <summary>
		/// The ratign for the document
		/// </summary>
		[JsonProperty("rating")]
		public int Rating { get; set; }
	}
}