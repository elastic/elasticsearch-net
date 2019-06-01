using System;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(LikeDocument<object>))]
	public interface ILikeDocument
	{
		[DataMember(Name = "doc")]
		[JsonFormatter(typeof(SourceFormatter<object>))]
		object Document { get; set; }

		[DataMember(Name = "fields")]
		Fields Fields { get; set; }

		[DataMember(Name = "_id")]
		Id Id { get; set; }

		[DataMember(Name = "_index")]
		IndexName Index { get; set; }

		[DataMember(Name = "per_field_analyzer")]
		IPerFieldAnalyzer PerFieldAnalyzer { get; set; }

		[DataMember(Name = "routing")]
		Routing Routing { get; set; }
	}

	public abstract class LikeDocumentBase : ILikeDocument
	{
		[IgnoreDataMember]
		private Routing _routing;

		public object Document { get; set; }

		public Fields Fields { get; set; }

		public Id Id { get; set; }
		public IndexName Index { get; set; }

		public IPerFieldAnalyzer PerFieldAnalyzer { get; set; }

		public Routing Routing
		{
			get => _routing ?? (Document == null ? null : new Routing(Document));
			set => _routing = value;
		}
	}

	public class LikeDocument<TDocument> : LikeDocumentBase
		where TDocument : class
	{
		internal LikeDocument() { }

		public LikeDocument(Id id)
		{
			Id = id;
			Index = typeof(TDocument);
		}

		public LikeDocument(TDocument document)
		{
			Id = Id.From(document);
			Document = document;
			Index = typeof(TDocument);
		}
	}

	public class LikeDocumentDescriptor<TDocument> : DescriptorBase<LikeDocumentDescriptor<TDocument>, ILikeDocument>, ILikeDocument
		where TDocument : class
	{
		private Routing _routing;

		public LikeDocumentDescriptor() => Self.Index = typeof(TDocument);

		object ILikeDocument.Document { get; set; }
		Fields ILikeDocument.Fields { get; set; }
		Id ILikeDocument.Id { get; set; }
		IndexName ILikeDocument.Index { get; set; }
		IPerFieldAnalyzer ILikeDocument.PerFieldAnalyzer { get; set; }

		Routing ILikeDocument.Routing
		{
			get => _routing ?? (Self.Document == null ? null : new Routing(Self.Document));
			set => _routing = value;
		}

		public LikeDocumentDescriptor<TDocument> Index(IndexName index) => Assign(index, (a, v) => a.Index = v);

		public LikeDocumentDescriptor<TDocument> Id(Id id) => Assign(id, (a, v) => a.Id = v);

		public LikeDocumentDescriptor<TDocument> Routing(Routing routing) => Assign(routing, (a, v) => a.Routing = v);

		public LikeDocumentDescriptor<TDocument> Fields(Func<FieldsDescriptor<TDocument>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Fields = v?.Invoke(new FieldsDescriptor<TDocument>())?.Value);

		public LikeDocumentDescriptor<TDocument> Fields(Fields fields) => Assign(fields, (a, v) => a.Fields = v);

		public LikeDocumentDescriptor<TDocument> Document(TDocument document) => Assign(document,  (a, v) =>
		{
			a.Id = Infer.Id(v);
			a.Document = v;
		});

		public LikeDocumentDescriptor<TDocument> PerFieldAnalyzer(
			Func<PerFieldAnalyzerDescriptor<TDocument>, IPromise<IPerFieldAnalyzer>> analyzerSelector
		) =>
			Assign(analyzerSelector, (a, v) => a.PerFieldAnalyzer = v?.Invoke(new PerFieldAnalyzerDescriptor<TDocument>())?.Value);
	}
}
