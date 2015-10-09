using System;

namespace Nest
{
	public interface IDocumentPath
	{
		Id Id { get; set; }
		IndexName Index { get; set; }
		TypeName Type { get; set; }
	}

	public class DocumentPath<T> : IDocumentPath where T : class
	{
		internal IDocumentPath Self => this;
		internal T Document { get; set; }
		Id IDocumentPath.Id { get; set; }
		IndexName IDocumentPath.Index { get; set; }
		TypeName IDocumentPath.Type { get; set; }


		public DocumentPath(T document) : this(Nest.Id.From(document)) { this.Document = document; }
		public DocumentPath(Id id)
		{
			Self.Id = id;
			Self.Index = typeof(T);
			Self.Type = typeof(T);
		}

		public static DocumentPath<T> Id(Id id) => new DocumentPath<T>(id);
		public static DocumentPath<T> Id(T @object) => new DocumentPath<T>(@object);

		public static implicit operator DocumentPath<T>(T @object) => new DocumentPath<T>(@object);
		public static implicit operator DocumentPath<T>(Id id) => new DocumentPath<T>(id);
		public static implicit operator DocumentPath<T>(long id) => new DocumentPath<T>(id);
		public static implicit operator DocumentPath<T>(string id) => new DocumentPath<T>(id);
		public static implicit operator DocumentPath<T>(Guid id) => new DocumentPath<T>(id);

		public DocumentPath<T> Index(IndexName index)
		{
			Self.Index = index;
			return this;
		}
		public DocumentPath<T> Type(TypeName type)
		{
			Self.Type = type;
			return this;
		}
	}
}