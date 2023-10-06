// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless;
#else
namespace Elastic.Clients.Elasticsearch;
#endif

// MARKED INTERNAL AS WE MAY NO LONGER USE THIS TYPE
// TODO - REVIEW THIS
internal interface IDocumentPath
{
	Id Id { get; set; }
	IndexName Index { get; set; }
}

// MARKED INTERNAL AS WE MAY NO LONGER USE THIS TYPE
// TODO - REVIEW THIS
internal sealed class DocumentPath<T> : IEquatable<DocumentPath<T>>, IDocumentPath
{
#if ELASTICSEARCH_SERVERLESS
	public DocumentPath(T document) : this(Elasticsearch.Serverless.Id.From(document)) => Document = document;
#else
	public DocumentPath(T document) : this(Elasticsearch.Id.From(document)) => Document = document;
#endif

	public DocumentPath(Id id)
	{
		Self.Id = id;
		Self.Index = typeof(T);
	}

	internal T Document { get; set; }
	internal IDocumentPath Self => this;
	Id IDocumentPath.Id { get; set; }
	IndexName IDocumentPath.Index { get; set; }

	public bool Equals(DocumentPath<T> other)
	{
		IDocumentPath o = other, s = Self;
		return s.Index.NullOrEquals(o.Index) && s.Id.NullOrEquals(o.Id) && (Document?.Equals(other.Document) ?? true);
	}

	public static DocumentPath<T> Id(Id id) => new(id);

	public static DocumentPath<T> Id(T @object) => new(@object);

	public static implicit operator DocumentPath<T>(T @object) => @object == null ? null : new DocumentPath<T>(@object);

	public static implicit operator DocumentPath<T>(Id id) => id == null ? null : new DocumentPath<T>(id);

	public static implicit operator DocumentPath<T>(long id) => new(id);

	public static implicit operator DocumentPath<T>(string id) => id.IsNullOrEmpty() ? null : new DocumentPath<T>(id);

	public static implicit operator DocumentPath<T>(Guid id) => new(id);

	public DocumentPath<T> Index(IndexName index)
	{
		if (index == null)
			return this;

		Self.Index = index;
		return this;
	}

	public override int GetHashCode()
	{
		unchecked
		{
			var hashCode = Self.Index?.GetHashCode() ?? 0;
			hashCode = (hashCode * 397) ^ (Self.Id?.GetHashCode() ?? 0);
			return hashCode;
		}
	}

	public override bool Equals(object obj) => obj switch
	{
		DocumentPath<T> d => Equals(d),
		_ => false,
	};

	public static bool operator ==(DocumentPath<T> x, DocumentPath<T> y) => Equals(x, y);

	public static bool operator !=(DocumentPath<T> x, DocumentPath<T> y) => !Equals(x, y);
}
