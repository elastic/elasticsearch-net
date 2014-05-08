using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Elasticsearch.Net;
using Nest.Resolvers;
using Newtonsoft.Json;

namespace Nest
{
	[DescriptorFor("Mtermvectors")]
	public partial class MultiTermVectorsDescriptor<T> : IndexTypePathTypedDescriptor<MultiTermVectorsDescriptor<T>, MultiTermVectorsRequestParameters, T>
		, IPathInfo<MultiTermVectorsRequestParameters> where T : class
	{
		[JsonProperty("docs")]
		internal IEnumerable<MultiTermVectorDocument> _Documents { get; set;}

		public MultiTermVectorsDescriptor<T> Documents(params Func<MultiTermVectorDocumentDescriptor<T>, IMultiTermVectorDocumentDescriptor>[] documentSelectors)
		{
			this._Documents = documentSelectors.Select(s => s(new MultiTermVectorDocumentDescriptor<T>()).GetDocument()).Where(d=>d!= null).ToList();
			return this;
		}

		public MultiTermVectorsDescriptor<T> Documents(IEnumerable<MultiTermVectorDocument> documents)
		{
			this._Documents = documents;
			return this;
		}

		ElasticsearchPathInfo<MultiTermVectorsRequestParameters> IPathInfo<MultiTermVectorsRequestParameters>.ToPathInfo(IConnectionSettingsValues settings)
		{
			var pathInfo = base.ToPathInfo(settings, this._QueryString);
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;

			return pathInfo;
		}
	}

	public interface IMultiTermVectorDocumentDescriptor
	{
		MultiTermVectorDocument Document { get; set; }
		MultiTermVectorDocument GetDocument(); 
	}

	public class MultiTermVectorDocumentDescriptor<T> : DocumentOptionalPathDescriptorBase<MultiTermVectorDocumentDescriptor<T>, T, MultiTermVectorsRequestParameters>, IMultiTermVectorDocumentDescriptor where T : class
	{

		MultiTermVectorDocument IMultiTermVectorDocumentDescriptor.Document { get; set; }
		MultiTermVectorDocument IMultiTermVectorDocumentDescriptor.GetDocument()
		{
			IMultiTermVectorDocumentDescriptor d = this;
			if (d.Document == null) d.Document = new MultiTermVectorDocument();
			d.Document.Id = this._Id;
			d.Document.Type = this._Type;
			d.Document.Index = this._Index;
			return d.Document;
		}

		private MultiTermVectorDocumentDescriptor<T> SetDocValue(Action<IMultiTermVectorDocumentDescriptor> setter)
		{
			IMultiTermVectorDocumentDescriptor d = this;
			if (d.Document == null) d.Document = new MultiTermVectorDocument();
			setter(d);
			return this;
		}

		public MultiTermVectorDocumentDescriptor<T> Fields(params string[] fields)
		{
			return this.SetDocValue(d => d.Document.Fields = fields.Select(f => (PropertyPathMarker)f).ToList());
		} 
		public MultiTermVectorDocumentDescriptor<T> Fields(params Expression<Func<T, object>>[] fields)
		{
			return this.SetDocValue(d => d.Document.Fields = fields.Select(f => (PropertyPathMarker)f).ToList());
		} 
		public MultiTermVectorDocumentDescriptor<T> Fields(Func<FluentFieldList<T>, FluentFieldList<T>>  fields)
		{
			return this.SetDocValue(d => d.Document.Fields = fields(new FluentFieldList<T>()).ToList());
		}


		public MultiTermVectorDocumentDescriptor<T> Offsets(bool offsets = true)
		{
			return this.SetDocValue(d =>d.Document.Offsets = offsets);
		}
		public MultiTermVectorDocumentDescriptor<T> Payloads(bool payloads = true)
		{
			return this.SetDocValue(d =>d.Document.Payloads = payloads);
		}
		public MultiTermVectorDocumentDescriptor<T> Positions(bool positions = true)
		{
			return this.SetDocValue(d =>d.Document.Positions = positions);
		}
		public MultiTermVectorDocumentDescriptor<T> TermStatistics(bool termStatistics = true)
		{
			return this.SetDocValue(d => d.Document.TermStatistics = termStatistics);
		}
		public MultiTermVectorDocumentDescriptor<T> FieldStatistics (bool fieldStatistics = true)
		{
			return this.SetDocValue(d => d.Document.FieldStatistics = fieldStatistics);
		}
	}

	public class MultiTermVectorDocument
	{
		[JsonProperty("_index")]
		public IndexNameMarker Index { get; set; }
		[JsonProperty("_type")]
		public TypeNameMarker Type { get; set; }
		[JsonProperty("_id")]
		public string Id { get; set; }
		[JsonProperty("fields")]
		public IEnumerable<PropertyPathMarker> Fields { get; set; }
		[JsonProperty("offsets")]
		public bool? Offsets { get; set; }
		[JsonProperty("payloads")]
		public bool? Payloads { get; set; }
		[JsonProperty("positions")]
		public bool? Positions { get; set; }
		[JsonProperty("term_statistics")]
		public bool? TermStatistics { get; set; }
		[JsonProperty("field_statistics")]
		public bool? FieldStatistics { get; set; }
	}
}
