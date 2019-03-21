using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The percolate query can be used to match queries stored in an index
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<PercolateQuery>))]
	public interface IPercolateQuery : IQuery
	{
		/// <summary>
		/// The source of the document to percolate.
		/// </summary>
		[JsonProperty("document")]
		[JsonConverter(typeof(SourceConverter))]
		object Document { get; set; }

		/// <summary>
		/// The source of the documents to percolate. Like <see cref="Document" /> but allows
		/// multiple documents to be percolated.
		/// </summary>
		[JsonProperty("documents")]
		[JsonConverter(typeof(SourceConverter))]
		IEnumerable<object> Documents { get; set; }

		/// <summary>
		/// The name of the field containing the percolated query on an existing document. This is a required parameter.
		/// </summary>
		[JsonProperty("field")]
		Field Field { get; set; }

		/// <summary>
		/// The id of the document to fetch for percolation.
		/// Can be specified to percolate an existing document instead of providing <see cref="Document" />
		/// </summary>
		[JsonProperty("id")]
		Id Id { get; set; }

		/// <summary>
		/// The index the document resides in for percolation.
		/// Can be specified to percolate an existing document instead of providing <see cref="Document" />
		/// </summary>
		[JsonProperty("index")]
		IndexName Index { get; set; }

		/// <summary>
		/// Preference to be used to fetch the document to percolate.
		/// Can be specified to percolate an existing document instead of providing <see cref="Document" />
		/// </summary>
		[JsonProperty("preference")]
		string Preference { get; set; }

		/// <summary>
		/// Routing to be used to fetch the document to percolate.
		/// Can be specified to percolate an existing document instead of providing <see cref="Document" />
		/// </summary>
		[JsonProperty("routing")]
		Routing Routing { get; set; }

		/// <summary>
		/// The expected version of the document to be fetched for percolation.
		/// Can be specified to percolate an existing document instead of providing <see cref="Document" />
		/// </summary>
		[JsonProperty("version")]
		long? Version { get; set; }
	}

	/// <summary>
	/// The percolate query can be used to match queries stored in an index
	/// </summary>
	public class PercolateQuery : QueryBase, IPercolateQuery
	{
		private Routing _routing;

		/// <inheritdoc />
		public object Document { get; set; }

		/// <inheritdoc />
		public IEnumerable<object> Documents { get; set; }

		/// <inheritdoc />
		public Field Field { get; set; }

		/// <inheritdoc />
		public Id Id { get; set; }

		/// <inheritdoc />
		public IndexName Index { get; set; }

		/// <inheritdoc />
		public string Preference { get; set; }

		/// <inheritdoc />
		public Routing Routing
		{
			get => _routing ?? (Document == null ? null : new Routing(Document));
			set => _routing = value;
		}

		/// <inheritdoc />
		public long? Version { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Percolate = this;

		internal static bool IsConditionless(IPercolateQuery q)
		{
			var docFields = q.Document == null && q.Documents == null;
			if (!docFields) return false;

			return q.Index == null ||
				q.Id.IsConditionless() ||
				q.Field.IsConditionless();
		}
	}

	/// <summary>
	/// The percolate query can be used to match queries stored in an index
	/// </summary>
	/// <typeparam name="T">The document type that contains the percolated query</typeparam>
	public class PercolateQueryDescriptor<T>
		: QueryDescriptorBase<PercolateQueryDescriptor<T>, IPercolateQuery>
			, IPercolateQuery where T : class
	{
		private Routing _routing;

		protected override bool Conditionless => PercolateQuery.IsConditionless(this);
		object IPercolateQuery.Document { get; set; }
		IEnumerable<object> IPercolateQuery.Documents { get; set; }
		Field IPercolateQuery.Field { get; set; }
		Id IPercolateQuery.Id { get; set; }
		IndexName IPercolateQuery.Index { get; set; }

		string IPercolateQuery.Preference { get; set; }

		Routing IPercolateQuery.Routing
		{
			get => _routing ?? (Self.Document == null ? null : new Routing(Self.Document));
			set => _routing = value;
		}

		long? IPercolateQuery.Version { get; set; }

		/// <inheritdoc cref="IPercolateQuery.Field" />
		public PercolateQueryDescriptor<T> Field(Field field) => Assign(a => a.Field = field);

		/// <inheritdoc cref="IPercolateQuery.Field" />
		public PercolateQueryDescriptor<T> Field(Expression<Func<T, object>> objectPath) => Assign(a => a.Field = objectPath);

		/// <inheritdoc cref="IPercolateQuery.Document" />
		public PercolateQueryDescriptor<T> Document<TDocument>(TDocument document) => Assign(a => a.Document = document);

		/// <inheritdoc cref="IPercolateQuery.Documents" />
		public PercolateQueryDescriptor<T> Documents<TDocument>(params TDocument[] documents) =>
			Assign(a => a.Documents = documents.Cast<object>());

		/// <inheritdoc cref="IPercolateQuery.Documents" />
		public PercolateQueryDescriptor<T> Documents<TDocument>(IEnumerable<TDocument> documents) =>
			Assign(a => a.Documents = documents.Cast<object>());

		/// <inheritdoc cref="IPercolateQuery.Id" />
		public PercolateQueryDescriptor<T> Id(string id) => Assign(a => a.Id = id);

		/// <summary>
		/// The index the document resides in for percolation.
		/// Can be specified to percolate an existing document instead of providing <see cref="Document{TDocument}" />
		/// </summary>
		public PercolateQueryDescriptor<T> Index(IndexName index) => Assign(a => a.Index = index);

		/// <summary>
		/// The index the document resides in for percolation.
		/// Can be specified to percolate an existing document instead of providing <see cref="Document{TDocument}" />
		/// </summary>
		public PercolateQueryDescriptor<T> Index<TDocument>() => Assign(a => a.Index = typeof(TDocument));

		/// <inheritdoc cref="IPercolateQuery.Routing" />
		public PercolateQueryDescriptor<T> Routing(Routing routing) => Assign(a => a.Routing = routing);

		/// <inheritdoc cref="IPercolateQuery.Preference" />
		public PercolateQueryDescriptor<T> Preference(string preference) => Assign(a => a.Preference = preference);

		/// <inheritdoc cref="IPercolateQuery.Version" />
		public PercolateQueryDescriptor<T> Version(long? version) => Assign(a => a.Version = version);
	}
}
