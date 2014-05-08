using System;
using System.Collections.Generic;
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
