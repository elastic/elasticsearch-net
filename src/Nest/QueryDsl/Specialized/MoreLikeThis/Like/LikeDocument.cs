using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<LikeDocument<object>>))]
	public interface ILikeDocument
	{
		bool CanBeFlattened { get; }

		Type ClrType { get; }

		[JsonProperty(PropertyName = "doc")]
		object Document { get; set; }

		[JsonProperty(PropertyName = "fields")]
		Fields Fields { get; set; }

		[JsonProperty(PropertyName = "_id")]
		Id Id { get; set; }

		[JsonProperty(PropertyName = "_index")]
		IndexName Index { get; set; }

		[JsonProperty(PropertyName = "per_field_analyzer")]
		IPerFieldAnalyzer PerFieldAnalyzer { get; set; }

		[JsonProperty(PropertyName = "_routing")]
		string Routing { get; set; }

		[JsonProperty(PropertyName = "_type")]
		TypeName Type { get; set; }
	}

	public abstract class LikeDocumentBase : ILikeDocument
	{
		public object Document { get; set; }

		public Fields Fields { get; set; }

		public Id Id { get; set; }

		public IndexName Index { get; set; }

		public IPerFieldAnalyzer PerFieldAnalyzer { get; set; }

		public string Routing { get; set; }

		public TypeName Type { get; set; }
		protected abstract Type ClrType { get; }

		bool ILikeDocument.CanBeFlattened =>
			Index == null
			&& Type == null
			&& Routing == null
			&& Fields == null;

		Type ILikeDocument.ClrType => ClrType;
	}

	public class LikeDocument<T> : LikeDocumentBase
	{
		internal LikeDocument() { }

		public LikeDocument(Id id)
		{
			Id = id;
			Index = typeof(T);
			Type = typeof(T);
		}

		protected override Type ClrType => typeof(T);
	}

	public class LikeDocumentDescriptor<T> : DescriptorBase<LikeDocumentDescriptor<T>, ILikeDocument>, ILikeDocument
		where T : class
	{
		public LikeDocumentDescriptor()
		{
			Self.Index = Self.ClrType;
			Self.Type = Self.ClrType;
		}

		bool ILikeDocument.CanBeFlattened =>
			Self.Index == null
			&& Self.Type == null
			&& Self.Routing == null
			&& Self.Fields == null;

		Type ILikeDocument.ClrType => typeof(T);
		object ILikeDocument.Document { get; set; }
		Fields ILikeDocument.Fields { get; set; }
		Id ILikeDocument.Id { get; set; }
		IndexName ILikeDocument.Index { get; set; }
		IPerFieldAnalyzer ILikeDocument.PerFieldAnalyzer { get; set; }
		string ILikeDocument.Routing { get; set; }
		TypeName ILikeDocument.Type { get; set; }

		public LikeDocumentDescriptor<T> Index(IndexName index) => Assign(a => a.Index = index);

		public LikeDocumentDescriptor<T> Type(TypeName type) => Assign(a => a.Type = type);

		public LikeDocumentDescriptor<T> Id(Id id) => Assign(a => a.Id = id);

		public LikeDocumentDescriptor<T> Routing(string routing) => Assign(a => a.Routing = routing);

		public LikeDocumentDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public LikeDocumentDescriptor<T> Fields(Fields fields) => Assign(a => a.Fields = fields);

		public LikeDocumentDescriptor<T> Document(T document) => Assign(a => a.Document = document);

		public LikeDocumentDescriptor<T> PerFieldAnalyzer(Func<PerFieldAnalyzerDescriptor<T>, IPromise<IPerFieldAnalyzer>> analyzerSelector) =>
			Assign(a => a.PerFieldAnalyzer = analyzerSelector?.Invoke(new PerFieldAnalyzerDescriptor<T>())?.Value);
	}
}
