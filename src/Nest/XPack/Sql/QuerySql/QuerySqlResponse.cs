using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	public class QuerySqlResponse : ResponseBase
	{
		/// <summary>
		/// Describes the columns being returned, this property will only be set on the first page of results.
		/// </summary>
		[DataMember(Name = "columns")]
		public IReadOnlyCollection<SqlColumn> Columns { get; internal set; } = EmptyReadOnly<SqlColumn>.Collection;

		/// <summary>
		/// You’ve reached the last page when there is no cursor returned in the results. Like Elasticsearch’s scroll,
		/// SQL may keep state in Elasticsearch to support the cursor.
		/// Unlike scroll, receiving the last page is enough to guarantee that the Elasticsearch state is cleared.
		/// </summary>
		[DataMember(Name = "cursor")]
		public string Cursor { get; internal set; }

		[DataMember(Name = "rows")]
		public IReadOnlyCollection<SqlRow> Rows { get; internal set; } = EmptyReadOnly<SqlRow>.Collection;
	}
}
