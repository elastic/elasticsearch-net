using System;

namespace Nest
{
	public interface IDocumentPath
	{
		Id Id { get; set; }
		IndexName Index { get; set; }
		TypeName Type { get; set; }
	}

	public class DocumentPath<T> : IEquatable<DocumentPath<T>>, IDocumentPath where T : class
	{
		public DocumentPath(T document) : this(Nest.Id.From(document)) => Document = document;

		public DocumentPath(Id id)
		{
			Self.Id = id;
			Self.Index = typeof(T);
			Self.Type = typeof(T);
		}

		internal T Document { get; set; }
		internal IDocumentPath Self => this;
		Id IDocumentPath.Id { get; set; }
		IndexName IDocumentPath.Index { get; set; }
		TypeName IDocumentPath.Type { get; set; }

		public bool Equals(DocumentPath<T> other)
		{
			IDocumentPath o = other, s = Self;
			return s.Index.NullOrEquals(o.Index) && s.Type.NullOrEquals(o.Type) && s.Id.NullOrEquals(o.Id)
				&& (Document?.Equals(other.Document) ?? true);
		}

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case DocumentPath<T> d: return Equals(d);
				default: return false;
			}
		}

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

		public static DocumentPath<T> Id(Id id) => new DocumentPath<T>(id);

		public static DocumentPath<T> Id(T @object) => new DocumentPath<T>(@object);

		public DocumentPath<T> Index(IndexName index)
		{
			if (index == null) return this;

			Self.Index = index;
			return this;
		}

		public static bool operator ==(DocumentPath<T> x, DocumentPath<T> y) => Equals(x, y);

		public static implicit operator DocumentPath<T>(T @object) => @object == null ? null : new DocumentPath<T>(@object);

		public static implicit operator DocumentPath<T>(Id id) => id == null ? null : new DocumentPath<T>(id);

		public static implicit operator DocumentPath<T>(long id) => new DocumentPath<T>(id);

		public static implicit operator DocumentPath<T>(string id) => id.IsNullOrEmpty() ? null : new DocumentPath<T>(id);

		public static implicit operator DocumentPath<T>(Guid id) => new DocumentPath<T>(id);

		public static bool operator !=(DocumentPath<T> x, DocumentPath<T> y) => !Equals(x, y);

		public DocumentPath<T> Type(TypeName type)
		{
			if (type == null) return this;

			Self.Type = type;
			return this;
		}
	}
}
