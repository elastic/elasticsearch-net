using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IPauseFollowIndexResponse : IResponse
	{
		/// <summary>
		///
		/// </summary>
		[JsonProperty("columns")]
		IReadOnlyCollection<SqlColumn> Columns { get; }
	}

	public class PauseFollowIndexResponse : ResponseBase, IPauseFollowIndexResponse
	{
		/// <inheritdoc cref="IPauseFollowIndexResponse.Columns" />
		public IReadOnlyCollection<SqlColumn> Columns { get; internal set; } = EmptyReadOnly<SqlColumn>.Collection;
	}
}
