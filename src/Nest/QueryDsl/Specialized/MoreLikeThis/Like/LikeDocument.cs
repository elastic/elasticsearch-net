using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<LikeDocument<object>>))]
	public interface ILikeDocument
	{
		[JsonProperty(PropertyName = "_index")]
		IndexName Index { get; set; }

		[JsonProperty(PropertyName = "_type")]
		TypeName Type { get; set; }

		[JsonProperty(PropertyName = "_id")]
		Id Id { get; set; }

		[JsonProperty(PropertyName = "fields")]
		Fields Fields { get; set; }

		[JsonProperty(PropertyName = "_routing")]
		string Routing { get; set; }

		[JsonProperty(PropertyName = "doc")]
		object Document { get; set; }

		[JsonProperty(PropertyName = "per_field_analyzer")]
		IPerFieldAnalyzer PerFieldAnalyzer { get; set; }

		Type ClrType { get; }

		bool CanBeFlattened { get; }
	}

	public abstract class LikeDocumentBase : ILikeDocument
	{
		Type ILikeDocument.ClrType => ClrType;
		protected abstract Type ClrType { get; }

		public IndexName Index { get; set; }

		public TypeName Type { get; set; }

		public Id Id { get; set; }

		public Fields Fields { get; set; }

		public string Routing { get; set; }

		public object Document { get; set; }

		public IPerFieldAnalyzer PerFieldAnalyzer { get; set; }

		bool ILikeDocument.CanBeFlattened =>
			this.Index == null
			&& this.Type == null
			&& this.Routing == null
			&& this.Fields == null;
	}

	public class LikeDocument<T> : LikeDocumentBase
	{
		protected override Type ClrType => typeof(T);
		internal LikeDocument() { }

		public LikeDocument(Id id)
		{
			this.Id = id;
			this.Index = typeof(T);
			this.Type = typeof(T);
		}
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
		IPerFieldAnalyzer ILikeDocument.PerFieldAnalyzer { get; set; }
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
