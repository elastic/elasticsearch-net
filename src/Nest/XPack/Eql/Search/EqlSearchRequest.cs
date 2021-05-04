// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[MapsApi("eql.search.json")]
	[ReadAs(typeof(EqlSearchRequest))]
	public partial interface IEqlSearchRequest : ITypedSearchRequest
	{
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

	[ReadAs(typeof(EqlSearchRequest<>))]
	[InterfaceDataContract]
	// ReSharper disable once UnusedTypeParameter
	public partial interface IEqlSearchRequest<TInferDocument> { }

	public partial class EqlSearchRequest
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

		Type ITypedSearchRequest.ClrType => null;
	}

	[DataContract]
	public partial class EqlSearchRequest<TInferDocument>
	{
		Type ITypedSearchRequest.ClrType => typeof(TInferDocument);
	}

	public partial class EqlSearchDescriptor<TInferDocument> where TInferDocument : class
	{
		Type ITypedSearchRequest.ClrType => typeof(TInferDocument);

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

		/// <inheritdoc cref="IEqlSearchRequest.EventCategoryField" />
		public EqlSearchDescriptor<TInferDocument> EventCategoryField(Field eventCategoryField) => Assign(eventCategoryField, (a, v) => a.EventCategoryField = v);

		/// <inheritdoc cref = "IEqlSearchRequest.EventCategoryField" />
		public EqlSearchDescriptor<TInferDocument> EventCategoryField<TValue>(Expression<Func<TInferDocument, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.EventCategoryField = v);

		/// <inheritdoc cref="IEqlSearchRequest.FetchSize" />
		public EqlSearchDescriptor<TInferDocument> FetchSize(int? fetchSize) => Assign(fetchSize, (a, v) => a.FetchSize = v);

		/// <inheritdoc cref="IEqlSearchRequest.Fields" />
		public EqlSearchDescriptor<TInferDocument> Fields(Func<FieldsDescriptor<TInferDocument>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Fields = v?.Invoke(new FieldsDescriptor<TInferDocument>())?.Value);

		/// <inheritdoc cref="IEqlSearchRequest.Fields" />
		public EqlSearchDescriptor<TInferDocument> Fields(Fields fields) => Assign(fields, (a, v) => a.Fields = v);

		/// <inheritdoc cref="IEqlSearchRequest.Filter" />
		public EqlSearchDescriptor<TInferDocument> Filter(Func<QueryContainerDescriptor<TInferDocument>, QueryContainer> filter) =>
			Assign(filter, (a, v) => a.Filter = v?.Invoke(new QueryContainerDescriptor<TInferDocument>()));

		/// <inheritdoc cref="IEqlSearchRequest.Query" />
		public EqlSearchDescriptor<TInferDocument> Query(string query) => Assign(query, (a, v) => a.Query = v);

		/// <inheritdoc cref="IEqlSearchRequest.ResultPosition" />
		public EqlSearchDescriptor<TInferDocument> ResultPosition(EqlResultPosition? resultPosition) => Assign(resultPosition, (a, v) => a.ResultPosition = v);

		/// <inheritdoc cref="IEqlSearchRequest.RuntimeFields" />
		public EqlSearchDescriptor<TInferDocument> RuntimeFields(Func<RuntimeFieldsDescriptor, IPromise<IRuntimeFields>> runtimeFieldsSelector) =>
			Assign(runtimeFieldsSelector, (a, v) => a.RuntimeFields = v?.Invoke(new RuntimeFieldsDescriptor())?.Value);

		/// <inheritdoc cref="IEqlSearchRequest.Size" />
		public EqlSearchDescriptor<TInferDocument> Size(float? size) => Assign(size, (a, v) => a.Size = v);

		/// <inheritdoc cref="IEqlSearchRequest.TiebreakerField" />
		public EqlSearchDescriptor<TInferDocument> TiebreakerField(Field tiebreakerField) => Assign(tiebreakerField, (a, v) => a.TiebreakerField = v);

		/// <inheritdoc cref = "IEqlSearchRequest.TiebreakerField" />
		public EqlSearchDescriptor<TInferDocument> TiebreakerField<TValue>(Expression<Func<TInferDocument, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TiebreakerField = v);

		/// <inheritdoc cref="IEqlSearchRequest.TimestampField" />
		public EqlSearchDescriptor<TInferDocument> TimestampField(Field timestampField) => Assign(timestampField, (a, v) => a.TimestampField = v);

		/// <inheritdoc cref = "IEqlSearchRequest.TimestampField" />
		public EqlSearchDescriptor<TInferDocument> TimestampField<TValue>(Expression<Func<TInferDocument, TValue>> objectPath) =>
			Assign(objectPath, (a, v) => a.TimestampField = v);
	}
}
