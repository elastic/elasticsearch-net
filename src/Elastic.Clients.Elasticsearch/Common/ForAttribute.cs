// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch
{

	///// <summary>
	///// Makes it explicit which API this request interface maps, the name of the interface informs
	///// The generator how to name related types
	///// </summary>
	//[AttributeUsage(AttributeTargets.Interface)]
	//internal class MapsApiAttribute : Attribute
	//{
	//	// ReSharper disable once UnusedParameter.Local
	//	public MapsApiAttribute(string restSpecName) { }
	//}

	///// <summary>
	///// The preferred way to wire in a custom response formatter is for requests to override
	///// <see cref="RequestBase{TParameters}.RequestDefaults"/> however sometimes a request does not have
	///// access to enough type information. This attribute will set up the see <see cref="CustomResponseBuilder"/>
	///// in the generated client methods instead.
	///// </summary>
	//[AttributeUsage(AttributeTargets.Interface)]
	//internal class ResponseBuilderWithGeneric : Attribute
	//{
	//	// ReSharper disable once UnusedParameter.Local
	//	public ResponseBuilderWithGeneric(string pathToBuilder) { }
	//}

	public interface IDocumentPath
	{
		Id Id { get; set; }
		IndexName Index { get; set; }
	}

	public class DocumentPath<T> : IEquatable<DocumentPath<T>>, IDocumentPath
	{
		public DocumentPath(T document) : this(Elasticsearch.Id.From(document)) => Document = document;

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

		public static DocumentPath<T> Id(Id id) => new DocumentPath<T>(id);

		public static DocumentPath<T> Id(T @object) => new DocumentPath<T>(@object);

		public static implicit operator DocumentPath<T>(T @object) => @object == null ? null : new DocumentPath<T>(@object);

		public static implicit operator DocumentPath<T>(Id id) => id == null ? null : new DocumentPath<T>(id);

		public static implicit operator DocumentPath<T>(long id) => new DocumentPath<T>(id);

		public static implicit operator DocumentPath<T>(string id) => id.IsNullOrEmpty() ? null : new DocumentPath<T>(id);

		public static implicit operator DocumentPath<T>(Guid id) => new DocumentPath<T>(id);

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

		public override bool Equals(object obj)
		{
			switch (obj)
			{
				case DocumentPath<T> d:
					return Equals(d);
				default:
					return false;
			}
		}

		public static bool operator ==(DocumentPath<T> x, DocumentPath<T> y) => Equals(x, y);

		public static bool operator !=(DocumentPath<T> x, DocumentPath<T> y) => !Equals(x, y);
	}
}
