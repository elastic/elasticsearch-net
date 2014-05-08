using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[DescriptorFor("Mtermvectors")]
	public partial class MultiTermVectorsDescriptor<T> : 
		IndexTypePathTypedDescriptor<MultiTermVectorsDescriptor<T>, MultiTermVectorsRequestParameters, T>
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

		public MultiTermVectorsDescriptor<T> Ids(params string[] ids)
		{
			return this.Documents(ids.Select(id => new MultiTermVectorDocument { Id = id }));
		}
		
		public MultiTermVectorsDescriptor<T> Ids(params long[] ids)
		{
			return this.Documents(ids.Select(id => new MultiTermVectorDocument { Id = id.ToString(CultureInfo.InvariantCulture) }));
		}
		
		public MultiTermVectorsDescriptor<T> Ids(IEnumerable<string> ids)
		{
			return this.Documents(ids.Select(id => new MultiTermVectorDocument { Id = id }));
		}
		
		public MultiTermVectorsDescriptor<T> Ids(IEnumerable<long> ids)
		{
			return this.Documents(ids.Select(id => new MultiTermVectorDocument { Id = id.ToString(CultureInfo.InvariantCulture) }));
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
}
