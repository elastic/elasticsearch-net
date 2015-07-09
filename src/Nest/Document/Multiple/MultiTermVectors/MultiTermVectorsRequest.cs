using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IMultiTermVectorsRequest : IIndexTypePath<MultiTermVectorsRequestParameters>
	{
		[JsonProperty("docs")]
		IEnumerable<MultiTermVectorDocument> Documents { get; set;}
	}

	internal static class MultiTermVectorsPathInfo
	{
		public static void Update(ElasticsearchPathInfo<MultiTermVectorsRequestParameters> pathInfo, IMultiTermVectorsRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class MultiTermVectorsRequest : IndexTypePathBase<MultiTermVectorsRequestParameters>, IMultiTermVectorsRequest
	{
		public MultiTermVectorsRequest(IndexName index, TypeName typeNameMarker) : base(index, typeNameMarker)
		{
		}

		public IEnumerable<MultiTermVectorDocument> Documents { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MultiTermVectorsRequestParameters> pathInfo)
		{
			MultiTermVectorsPathInfo.Update(pathInfo, this);
		}

	}

	[DescriptorFor("Mtermvectors")]
	public partial class MultiTermVectorsDescriptor<T> : IndexTypePathDescriptor<MultiTermVectorsDescriptor<T>, MultiTermVectorsRequestParameters, T>, IMultiTermVectorsRequest
		where T : class
	{
		private IMultiTermVectorsRequest Self => this;

		IEnumerable<MultiTermVectorDocument> IMultiTermVectorsRequest.Documents { get; set; }

		public MultiTermVectorsDescriptor<T> Documents(params Func<MultiTermVectorDocumentDescriptor<T>, IMultiTermVectorDocumentDescriptor>[] documentSelectors)
		{
			Self.Documents = documentSelectors.Select(s => s(new MultiTermVectorDocumentDescriptor<T>()).GetDocument()).Where(d=>d!= null).ToList();
			return this;
		}

		public MultiTermVectorsDescriptor<T> Documents(IEnumerable<MultiTermVectorDocument> documents)
		{
			Self.Documents = documents;
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

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MultiTermVectorsRequestParameters> pathInfo)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}

	}
}
