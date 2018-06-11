using System;

namespace Nest
{
	/// <summary>
	/// A path to a document consisting of an index, type and id
	/// </summary>
	public interface IDocumentPath
	{
		/// <summary>
		/// The id for the document
		/// </summary>
		Id Id { get; set; }

		/// <summary>
		/// The index for the document
		/// </summary>
		IndexName Index { get; set; }

		/// <summary>
		/// The type for the document
		/// </summary>
		TypeName Type { get; set; }
	}

	/// <inheritdoc cref="IDocumentPath"/>
	public class DocumentPath<T> : IEquatable<DocumentPath<T>>, IDocumentPath where T : class
	{
		internal IDocumentPath Self => this;
		internal T Document { get; set; }
		Id IDocumentPath.Id { get; set; }
		IndexName IDocumentPath.Index { get; set; }
		TypeName IDocumentPath.Type { get; set; }

		/// <summary>
		/// Instantiates a new instance of <see cref="DocumentPath{T}"/> from a given document,
		/// where the index, type and id are inferred from the document type <typeparamref name="T"/>
		/// </summary>
		public DocumentPath(T document) : this(Nest.Id.From(document)) { this.Document = document; }

		/// <summary>
		/// Instantiates a new instance of <see cref="DocumentPath{T}"/> with a given id
		/// </summary>
		public DocumentPath(Id id)
		{
			Self.Id = id;
			Self.Index = typeof(T);
			Self.Type = typeof(T);
		}

		/// <summary>
		/// Instantiates a new instance of <see cref="DocumentPath{T}"/> with a given id
		/// </summary>
		public static DocumentPath<T> Id(Id id) => new DocumentPath<T>(id);

		/// <summary>
		/// Instantiates a new instance of <see cref="DocumentPath{T}"/> from a given document
		/// </summary>
		public static DocumentPath<T> Id(T @object) => new DocumentPath<T>(@object);

		/// <summary>
		/// Implicit conversion that instantiates a new instance of <see cref="DocumentPath{T}"/> from a given document,
		/// where the index, type and id are inferred from the document type <typeparamref name="T"/>
		/// </summary>
		public static implicit operator DocumentPath<T>(T @object) => @object == null ? null : new DocumentPath<T>(@object);

		/// <summary>
		/// Implicit conversion that instantiates a new instance of <see cref="DocumentPath{T}"/> with a given id
		/// </summary>
		public static implicit operator DocumentPath<T>(Id id) => id == null ? null : new DocumentPath<T>(id);

		/// <summary>
		/// Implicit conversion that instantiates a new instance of <see cref="DocumentPath{T}"/> with a given id
		/// </summary>
		public static implicit operator DocumentPath<T>(long id) => new DocumentPath<T>(id);

		/// <summary>
		/// Implicit conversion that instantiates a new instance of <see cref="DocumentPath{T}"/> with a given id
		/// </summary>
		public static implicit operator DocumentPath<T>(string id) => id.IsNullOrEmpty() ? null : new DocumentPath<T>(id);

		/// <summary>
		/// Implicit conversion that instantiates a new instance of <see cref="DocumentPath{T}"/> with a given id
		/// </summary>
		public static implicit operator DocumentPath<T>(Guid id) => new DocumentPath<T>(id);

		/// <summary>
		/// Sets the index
		/// </summary>
		public DocumentPath<T> Index(IndexName index)
		{
			if (index == null) return this;
			Self.Index = index;
			return this;
		}

		/// <summary>
		/// Sets the type
		/// </summary>
		public DocumentPath<T> Type(TypeName type)
		{
			if (type == null) return this;
			Self.Type = type;
			return this;
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Self.Type?.GetHashCode() ?? 0;
				hashCode = (hashCode * 397) ^ (Self.Index?.GetHashCode() ?? 0);
				hashCode = (hashCode * 397) ^ (Self.Id?.GetHashCode() ?? 0);
				return hashCode;
			}
		}

		/// <summary>
		/// Determines whether the specified <see cref="DocumentPath{T}"/> is
		/// equal to the current <see cref="DocumentPath{T}"/>
		/// </summary>
		public bool Equals(DocumentPath<T> other)
		{
			IDocumentPath o = other, s = Self;
			return s.Index.NullOrEquals(o.Index) && s.Type.NullOrEquals(o.Type) && s.Id.NullOrEquals(o.Id)
			       && (this.Document?.Equals(other.Document) ?? true);
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case DocumentPath<T> d: return this.Equals(d);
				default: return false;
			}
		}

		public static bool operator ==(DocumentPath<T> x, DocumentPath<T> y) => Equals(x, y);

		public static bool operator !=(DocumentPath<T> x, DocumentPath<T> y)=> !Equals(x, y);
	}
}
