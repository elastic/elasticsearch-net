using System;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("ccr.delete_auto_follow_pattern.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<DeleteAutoFollowPatternRequest>))]
	public partial interface IDeleteAutoFollowPatternRequest
	{
		/// <summary>
		///
		/// <para>
		/// </para>
		/// </summary>
		[JsonProperty("cursor")]
		string Cursor { get; set; }
	}

	public partial class DeleteAutoFollowPatternRequest
	{
		/// <inheritdoc cref="IDeleteAutoFollowPatternRequest.Cursor" />
		public string Cursor { get; set; }
	}

	public partial class DeleteAutoFollowPatternDescriptor
	{
		string IDeleteAutoFollowPatternRequest.Cursor { get; set; }

		/// <inheritdoc cref="IDeleteAutoFollowPatternRequest.Cursor" />
		public DeleteAutoFollowPatternDescriptor Cursor(string cursor) => Assign(a => a.Cursor = cursor);
	}
}
