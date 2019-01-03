using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IFollowIndexStatsResponse : IResponse
	{
		/// <summary>
		///
		/// </summary>
		[JsonProperty("columns")]
		IReadOnlyCollection<SqlColumn> Columns { get; }
	}

	public class FollowIndexStatsResponse : ResponseBase, IFollowIndexStatsResponse
	{
		/// <inheritdoc cref="IFollowIndexStatsResponse.Columns" />
		public IReadOnlyCollection<SqlColumn> Columns { get; internal set; } = EmptyReadOnly<SqlColumn>.Collection;
	}
}
