using System;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("ccr.pause_follow.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<PauseFollowIndexRequest>))]
	public partial interface IPauseFollowIndexRequest
	{
		/// <summary>
		///
		/// <para>
		/// </para>
		/// </summary>
		[JsonProperty("cursor")]
		string Cursor { get; set; }
	}

	public partial class PauseFollowIndexRequest
	{
		/// <inheritdoc cref="IPauseFollowIndexRequest.Cursor" />
		public string Cursor { get; set; }
	}

	public partial class PauseFollowIndexDescriptor
	{
		string IPauseFollowIndexRequest.Cursor { get; set; }

		/// <inheritdoc cref="IPauseFollowIndexRequest.Cursor" />
		public PauseFollowIndexDescriptor Cursor(string cursor) => Assign(a => a.Cursor = cursor);
	}
}
