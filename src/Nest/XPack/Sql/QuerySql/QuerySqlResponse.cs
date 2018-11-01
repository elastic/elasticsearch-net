using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public interface IQuerySqlResponse : IResponse
	{
		/// <summary>
		///     Describes the columns being returned, this property will only be set on the first page of results.
		/// </summary>
		[JsonProperty("columns")]
		IReadOnlyCollection<SqlColumn> Columns { get; }

		/// <summary>
		///     <para>
		///         You’ve reached the last page when there is no cursor returned in the results. Like Elasticsearch’s scroll,
		///         SQL may keep state in Elasticsearch to support the cursor.
		///         Unlike scroll, receiving the last page is enough to guarantee that the Elasticsearch state is cleared.
		///     </para>
		/// </summary>
		[JsonProperty("cursor")]
		string Cursor { get; }

		[JsonProperty("rows")]
		IReadOnlyCollection<SqlRow> Rows { get; }
	}

	public class QuerySqlResponse : ResponseBase, IQuerySqlResponse
	{
		/// <inheritdoc cref="IQuerySqlResponse.Columns" />
		/// >
		public IReadOnlyCollection<SqlColumn> Columns { get; internal set; } = EmptyReadOnly<SqlColumn>.Collection;

		/// <inheritdoc cref="IQuerySqlResponse.Cursor" />
		/// >
		public string Cursor { get; internal set; }

		public IReadOnlyCollection<SqlRow> Rows { get; internal set; } = EmptyReadOnly<SqlRow>.Collection;
	}
}
