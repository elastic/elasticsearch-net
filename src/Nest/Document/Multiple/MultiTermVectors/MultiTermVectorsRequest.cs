using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IMultiTermVectorsRequest 
	{
		[JsonProperty("docs")]
		IEnumerable<MultiTermVectorDocument> Documents { get; set; }
	}

	public partial class MultiTermVectorsRequest 
	{
		public IEnumerable<MultiTermVectorDocument> Documents { get; set; }
	}

	[DescriptorFor("Mtermvectors")]
	public partial class MultiTermVectorsDescriptor
	{
		IEnumerable<MultiTermVectorDocument> IMultiTermVectorsRequest.Documents { get; set; }	

		public MultiTermVectorsDescriptor Documents<T>(params Func<MultiTermVectorDocumentDescriptor<T>, IMultiTermVectorDocumentDescriptor>[] documentSelectors)
			where T : class
		{
			((IMultiTermVectorsRequest)this).Documents = documentSelectors.Select(s => s(new MultiTermVectorDocumentDescriptor<T>()).GetDocument()).Where(d => d != null).ToList();
			return this;
		}

		public MultiTermVectorsDescriptor Documents(IEnumerable<MultiTermVectorDocument> documents)
		{
			((IMultiTermVectorsRequest)this).Documents = documents;
			return this;
		}

		public MultiTermVectorsDescriptor Ids(params string[] ids)
		{
			return this.Documents(ids.Select(id => new MultiTermVectorDocument { Id = id }));
		}

		public MultiTermVectorsDescriptor Ids(params long[] ids)
		{
			return this.Documents(ids.Select(id => new MultiTermVectorDocument { Id = id.ToString(CultureInfo.InvariantCulture) }));
		}

		public MultiTermVectorsDescriptor Ids(IEnumerable<string> ids)
		{
			return this.Documents(ids.Select(id => new MultiTermVectorDocument { Id = id }));
		}

		public MultiTermVectorsDescriptor Ids(IEnumerable<long> ids)
		{
			return this.Documents(ids.Select(id => new MultiTermVectorDocument { Id = id.ToString(CultureInfo.InvariantCulture) }));
		}
	}
}
