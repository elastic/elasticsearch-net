using System;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("ccr.get_auto_follow_pattern.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<GetAutoFollowPatternRequest>))]
	public partial interface IGetAutoFollowPatternRequest
	{
		/// <summary>
		///
		/// <para>
		/// </para>
		/// </summary>
		[JsonProperty("cursor")]
		string Cursor { get; set; }
	}

	public partial class GetAutoFollowPatternRequest
	{
		/// <inheritdoc cref="IGetAutoFollowPatternRequest.Cursor" />
		public string Cursor { get; set; }
	}

	public partial class GetAutoFollowPatternDescriptor
	{
		string IGetAutoFollowPatternRequest.Cursor { get; set; }

		/// <inheritdoc cref="IGetAutoFollowPatternRequest.Cursor" />
		public GetAutoFollowPatternDescriptor Cursor(string cursor) => Assign(a => a.Cursor = cursor);
	}
}
