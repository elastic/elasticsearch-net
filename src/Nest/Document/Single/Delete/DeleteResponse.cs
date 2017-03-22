using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IDeleteResponse : IResponse
	{
		/// <summary>
		/// The ID of the deleted document.
		/// </summary>
		string Id { get; }

		/// <summary>
		/// The index of the deleted document.
		/// </summary>
		string Index { get; }

		/// <summary>
		/// The type of the deleted document.
		/// </summary>
		string Type { get; }

		/// <summary>
		/// The version of the deleted document.
		/// </summary>
		string Version { get; }

		/// <summary>
		/// Whether or not the document was found and deleted from the index.
		/// </summary>
		bool Found { get; }

	}


	[JsonObject]
	public class DeleteResponse : ResponseBase, IDeleteResponse
	{
		[JsonProperty("_index")]
		public string Index { get; internal set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }

		[JsonProperty("_id")]
		public string Id { get; internal set; }

		[JsonProperty("_version")]
		public string Version { get; internal set; }

		[JsonProperty("found")]
		public bool Found { get; internal set; }

		[Obsolete(@"WARNING: IsValid behavior has changed to align with 1.x and 5.x onwards.
		It now returns true for 404 responses (document not found), where previously it returned
		false.  Please use .Found to check whether the document was actually found.")]
		public override bool IsValid => base.IsValid;
	}
}
