using System;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("ccr.follow_stats.json")]
	[ContractJsonConverter(typeof(ReadAsTypeJsonConverter<FollowIndexStatsRequest>))]
	public partial interface IFollowIndexStatsRequest
	{
		/// <summary>
		///
		/// <para>
		/// </para>
		/// </summary>
		[JsonProperty("cursor")]
		string Cursor { get; set; }
	}

	public partial class FollowIndexStatsRequest
	{
		/// <inheritdoc cref="IFollowIndexStatsRequest.Cursor" />
		public string Cursor { get; set; }
	}

	public partial class FollowIndexStatsDescriptor
	{
		string IFollowIndexStatsRequest.Cursor { get; set; }

		/// <inheritdoc cref="IFollowIndexStatsRequest.Cursor" />
		public FollowIndexStatsDescriptor Cursor(string cursor) => Assign(a => a.Cursor = cursor);
	}
}
