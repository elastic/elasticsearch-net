using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface ICcrStatsResponse : IResponse
	{
		/// <summary>
		///
		/// </summary>
		[JsonProperty("columns")]
		IReadOnlyCollection<SqlColumn> Columns { get; }
	}

	public class CcrStatsResponse : ResponseBase, ICcrStatsResponse
	{
		/// <inheritdoc cref="ICcrStatsResponse.Columns" />
		public IReadOnlyCollection<SqlColumn> Columns { get; internal set; } = EmptyReadOnly<SqlColumn>.Collection;
	}
}
