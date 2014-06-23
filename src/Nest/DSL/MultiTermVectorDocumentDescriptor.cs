using System;
using System.Linq;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Nest.Resolvers;

namespace Nest
{
	public class MultiTermVectorDocumentDescriptor<T> : 
		DocumentOptionalPathDescriptorBase<MultiTermVectorDocumentDescriptor<T>, T, MultiTermVectorsRequestParameters>, 
		IMultiTermVectorDocumentDescriptor 
		where T : class
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

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MultiTermVectorsRequestParameters> pathInfo)
		{
		}
	}
}