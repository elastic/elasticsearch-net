using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDocumentExistsRequest<T> : IRequest<DocumentExistsRequestParameters>
		where T : class
	{
		
	}

	internal static class DocumentExistsPathInfo
	{
		public static void Update<T>(ElasticsearchPathInfo<DocumentExistsRequestParameters> pathInfo, IDocumentExistsRequest<T> request)
			where T : class
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.HEAD;
		}
	}
	
	public partial class DocumentExistsRequest<T> : DocumentPathBase<DocumentExistsRequestParameters, T>, IDocumentExistsRequest<T>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DocumentExistsRequestParameters> pathInfo)
		{
			DocumentExistsPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("Exists")]
	public partial class DocumentExistsDescriptor<T>
		: DocumentPathDescriptor<DocumentExistsDescriptor<T>, T, DocumentExistsRequestParameters>, IDocumentExistsRequest<T>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DocumentExistsRequestParameters> pathInfo)
		{
			DocumentExistsPathInfo.Update(pathInfo, this);
		}
	}
}
