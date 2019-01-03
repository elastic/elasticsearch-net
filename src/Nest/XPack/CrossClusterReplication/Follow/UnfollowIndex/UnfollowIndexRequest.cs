using System;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("ccr.unfollow.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<UnfollowIndexRequest>))]
	public partial interface IUnfollowIndexRequest
	{
		/// <summary>
		///
		/// <para>
		/// </para>
		/// </summary>
		[JsonProperty("cursor")]
		string Cursor { get; set; }
	}

	public partial class UnfollowIndexRequest
	{
		/// <inheritdoc cref="IUnfollowIndexRequest.Cursor" />
		public string Cursor { get; set; }
	}

	public partial class UnfollowIndexDescriptor
	{
		string IUnfollowIndexRequest.Cursor { get; set; }

		/// <inheritdoc cref="IUnfollowIndexRequest.Cursor" />
		public UnfollowIndexDescriptor Cursor(string cursor) => Assign(a => a.Cursor = cursor);
	}
}
