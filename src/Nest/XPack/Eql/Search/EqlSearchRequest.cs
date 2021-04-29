// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("eql.search.json")]
	[ReadAs(typeof(QuerySqlRequest))]
	public partial interface IEqlSearchRequest
	{
		/// <summary>
		/// Indicates whether the search should be case sensitive.
		/// </summary>
		[DataMember(Name = "case_sensitive")]
		bool? CaseSensitive { get; set; }

		/// <summary>
		/// The EQL search API uses the event.category field from the ECS by default. To specify a different field,
		/// set <see cref="EventCategoryField"/>.
		/// </summary>
		[DataMember(Name = "event_category_field")]
		Field EventCategoryField { get; set; }

		/// <summary>
		/// Maximum number of events to search at a time for sequence queries.
		/// </summary>
		[DataMember(Name = "fetch_size")]
		int? FetchSize { get; set; }

		/// <summary>
		/// Allows for retrieving a list of document fields in the search response.
		/// </summary>
		[DataMember(Name = "fields")]
		Fields Fields { get; set; }

		/// <summary>
		/// Further filter the results returned by the SQL query provided on <see cref="Query" />
		/// </summary>
		[DataMember(Name = "filter")]
		QueryContainer Filter { get; set; }

		/// <summary> The SQL query you want Elasticsearch to execute </summary>
		[DataMember(Name = "query")]
		string Query { get; set; }

		[DataMember(Name = "result_position")]
		EqlResultPosition? ResultPosition { get; set; }

		/// <summary>
		/// Specifies runtime fields which exist only as part of the query.
		/// </summary>
		[DataMember(Name = "runtime_mappings")]
		IRuntimeFields RuntimeFields { get; set; }

		/// <summary>
		/// For basic queries, the maximum number of matching events to return. Defaults to 10.
		/// </summary>
		[DataMember(Name = "size")]
		float? Size { get; set; }

		/// <summary>
		/// To specify a tiebreaker field, use the <see cref="TiebreakerField"/> parameter. If you use the ECS,
		/// we recommend using event.sequence as the tiebreaker field.
		/// </summary>
		[DataMember(Name = "tiebreaker_field")]
		Field TiebreakerField { get; set; }

		/// <summary>
		/// The EQL search API uses the @timestamp field from the ECS by default. To specify a different field,
		/// set <see cref="TimestampField"/>.
		/// </summary>
		[DataMember(Name = "timestamp_field")]
		Field TimestampField { get; set; }
	}

	public partial class EqlSearchRequest : IEqlSearchRequest
	{
		/// <inheritdoc />
		public bool? CaseSensitive { get; set; }
		/// <inheritdoc />
		public Field EventCategoryField { get; set; }
		/// <inheritdoc />
		public int? FetchSize { get; set; }
		/// <inheritdoc />
		public Fields Fields { get; set; }
		/// <inheritdoc />
		public QueryContainer Filter { get; set; }
		/// <inheritdoc />
		public string Query { get; set; }
		/// <inheritdoc />
		public EqlResultPosition? ResultPosition { get; set; }
		/// <inheritdoc />
		public IRuntimeFields RuntimeFields { get; set; }
		/// <inheritdoc />
		public float? Size { get; set; }
		/// <inheritdoc />
		public Field TiebreakerField { get; set; }
		/// <inheritdoc />
		public Field TimestampField { get; set; }
	}

	public partial class EqlSearchDescriptor : IEqlSearchRequest
	{
		/// <inheritdoc cref="IEqlSearchRequest.CaseSensitive"/>
		bool? IEqlSearchRequest.CaseSensitive { get; set; }

		/// <inheritdoc cref="IEqlSearchRequest.EventCategoryField"/>
		Field IEqlSearchRequest.EventCategoryField { get; set; }

		/// <inheritdoc cref="IEqlSearchRequest.FetchSize"/>
		int? IEqlSearchRequest.FetchSize { get; set; }

		/// <inheritdoc cref="IEqlSearchRequest.Fields"/>
		Fields IEqlSearchRequest.Fields { get; set; }

		/// <inheritdoc cref="IEqlSearchRequest.Filter"/>
		QueryContainer IEqlSearchRequest.Filter { get; set; }

		/// <inheritdoc cref="IEqlSearchRequest.Query"/>
		string IEqlSearchRequest.Query { get; set; }

		/// <inheritdoc cref="IEqlSearchRequest.ResultPosition"/>
		EqlResultPosition? IEqlSearchRequest.ResultPosition { get; set; }

		/// <inheritdoc cref="IEqlSearchRequest.RuntimeFields"/>
		IRuntimeFields IEqlSearchRequest.RuntimeFields { get; set; }

		/// <inheritdoc cref="IEqlSearchRequest.Size"/>
		float? IEqlSearchRequest.Size { get; set; }

		/// <inheritdoc cref="IEqlSearchRequest.TiebreakerField"/>
		Field IEqlSearchRequest.TiebreakerField { get; set; }

		/// <inheritdoc cref="IEqlSearchRequest.TimestampField"/>
		Field IEqlSearchRequest.TimestampField { get; set; }
	}
}
