using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{

	public class LikeDocument<T> : ILikeDocument
	{
		public LikeDocument(Id id)
		{
			this.Id = id;
			this.Index = typeof(T);
			this.Type = typeof(T);
		}

		Type ILikeDocument.ClrType => typeof(T);

		public IndexName Index { get; set; }

		public TypeName Type { get; set; }

		public Id Id { get; set; }

		public Fields Fields { get; set; }

		public string Routing { get; set; }

		bool ILikeDocument.CanBeFlattened =>
			this.Index == null 
			&& this.Type == null
			&& this.Routing == null
			&& this.Fields == null;


		public object Document { get; set; }

		public IDictionary<Field, string> PerFieldAnalyzer { get; set; }
	}

	public class LikeDocumentDescriptor<T> : DescriptorBase<LikeDocumentDescriptor<T>, ILikeDocument>, ILikeDocument
		where T : class
	{
		IndexName ILikeDocument.Index { get; set; }
		TypeName ILikeDocument.Type { get; set; }
		Id ILikeDocument.Id { get; set; }
		string ILikeDocument.Routing { get; set; }
		Fields ILikeDocument.Fields { get; set; }
		object ILikeDocument.Document { get; set; }
		IDictionary<Field, string> ILikeDocument.PerFieldAnalyzer { get; set; }
		Type ILikeDocument.ClrType => typeof(T);

		bool ILikeDocument.CanBeFlattened =>
			Self.Index == null
			&& Self.Type == null
			&& Self.Routing == null
			&& Self.Fields == null;

		public LikeDocumentDescriptor()
		{
			Self.Index = Self.ClrType;
			Self.Type = Self.ClrType;
		}

		public LikeDocumentDescriptor<T> Index(IndexName index) => Assign(a => a.Index = index);

		public LikeDocumentDescriptor<T> Type(TypeName type)=> Assign(a => a.Type = type);

		public LikeDocumentDescriptor<T> Id(Id id)=> Assign(a => a.Id = id);

		public LikeDocumentDescriptor<T> Routing(string routing)=> Assign(a => a.Routing = routing);

		public LikeDocumentDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public LikeDocumentDescriptor<T> Document(T document) => Assign(a => a.Document = document);

		public LikeDocumentDescriptor<T> PerFieldAnalyzer(Func<FluentDictionary<Expression<Func<T, object>>, string>, FluentDictionary<Expression<Func<T, object>>, string>> analyzerSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, string>();
			analyzerSelector(d);
			Self.PerFieldAnalyzer = d.ToDictionary(x => Field.Create(x.Key), x => x.Value);
			return this;
		}

		public LikeDocumentDescriptor<T> PerFieldAnalyzer(Func<FluentDictionary<Field, string>, FluentDictionary<Field, string>> analyzerSelector)
		{
			Self.PerFieldAnalyzer = analyzerSelector(new FluentDictionary<Field, string>());
			return this;
		}
	}
}