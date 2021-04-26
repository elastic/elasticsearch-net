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
		/// The EQL search API uses the event.category field from the ECS by default. To specify a different field,
		/// set <see cref="EventCategoryField"/>.
		/// </summary>
		[DataMember(Name = "event_category_field")]
		Field EventCategoryField { get; set; }

		/// <summary>
		/// fetch_size is a hint for how many results to return in each page. SQL might chose to return more or fewer results though.
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

		/// <summary>
		/// By default, the EQL search API stores async searches for five days. After this period, any searches 
		/// and their results are deleted. Use the <see cref="KeepAlive"/> parameter to change this retention period.
		/// </summary>
		[DataMember(Name = "keep_alive")]
		DateMath KeepAlive { get; set; }

		/// <summary>
		/// To save a synchronous search, set <see cref="KeepOnCompletion"/> to <value>true</value>.
		/// </summary>
		[DataMember(Name = "keep_on_completion")]
		bool? KeepOnCompletion { get; set; }

		/// <summary> The SQL query you want Elasticsearch to execute </summary>
		[DataMember(Name = "query")]
		string Query { get; set; }

		/// <summary>
		/// Specifies runtime fields which exist only as part of the query.
		/// </summary>
		[DataMember(Name = "runtime_mappings")]
		IRuntimeFields RuntimeFields { get; set; }

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

		/// <summary>
		/// To avoid long waits, run an async EQL search. Set <see cref="WaitForCompletionTimeout"/>
		/// to a duration you’d like to wait for synchronous results.
		/// </summary>
		[DataMember(Name = "wait_for_completion_timeout")]
		DateMath WaitForCompletionTimeout { get; set; } 
	}

	public partial class EqlSearchRequest : IEqlSearchRequest
	{
		/// <inheritdoc />
		public Field EventCategoryField { get; set; }
		/// <inheritdoc />
		public int? FetchSize { get; set; }
		/// <inheritdoc />
		public Fields Fields { get; set; }
		/// <inheritdoc />
		public QueryContainer Filter { get; set; }
		/// <inheritdoc />
		public DateMath KeepAlive { get; set; }
		/// <inheritdoc />
		public bool? KeepOnCompletion { get; set; }
		/// <inheritdoc />
		public string Query { get; set; }
		/// <inheritdoc />
		public IRuntimeFields RuntimeFields { get; set; }
		/// <inheritdoc />
		public Field TiebreakerField { get; set; }
		/// <inheritdoc />
		public Field TimestampField { get; set; }
		/// <inheritdoc />
		public DateMath WaitForCompletionTimeout { get; set; }
	}

	public partial class EqlSearchDescriptor : IEqlSearchRequest
	{
		/// <inheritdoc cref="IEqlSearchRequest.EventCategoryField"/>
		Field IEqlSearchRequest.EventCategoryField { get; set; }
		/// <inheritdoc cref="IEqlSearchRequest.FetchSize"/>
		int? IEqlSearchRequest.FetchSize { get; set; }
		/// <inheritdoc cref="IEqlSearchRequest.Fields"/>
		Fields IEqlSearchRequest.Fields { get; set; }
		/// <inheritdoc cref="IEqlSearchRequest.Filter"/>
		QueryContainer IEqlSearchRequest.Filter { get; set; }
		/// <inheritdoc cref="IEqlSearchRequest.KeepAlive"/>
		DateMath IEqlSearchRequest.KeepAlive { get; set; }
		/// <inheritdoc cref="IEqlSearchRequest.KeepOnCompletion"/>
		bool? IEqlSearchRequest.KeepOnCompletion { get; set; }
		/// <inheritdoc cref="IEqlSearchRequest.Query"/>
		string IEqlSearchRequest.Query { get; set; }
		/// <inheritdoc cref="IEqlSearchRequest.RuntimeFields"/>
		IRuntimeFields IEqlSearchRequest.RuntimeFields { get; set; }
		/// <inheritdoc cref="IEqlSearchRequest.TiebreakerField"/>
		Field IEqlSearchRequest.TiebreakerField { get; set; }
		/// <inheritdoc cref="IEqlSearchRequest.TimestampField"/>
		Field IEqlSearchRequest.TimestampField { get; set; }
		/// <inheritdoc cref="IEqlSearchRequest.WaitForCompletionTimeout"/>
		DateMath IEqlSearchRequest.WaitForCompletionTimeout { get; set; }
	}
}
