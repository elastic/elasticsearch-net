using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IDeleteRequest<T> : IRequest<DeleteRequestParameters>
		where T : class
	{
		
	}
		

	internal static class DeletePathInfo
	{
		public static void Update<T>(ElasticsearchPathInfo<DeleteRequestParameters> pathInfo, IDeleteRequest<T> request)
			where T : class
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.DELETE;
		}
	}
	
	public partial class DeleteRequest<T> : DocumentPathBase<DeleteRequestParameters, T>, IDeleteRequest<T>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRequestParameters> pathInfo)
		{
			DeletePathInfo.Update(pathInfo, this);
		}
	}
	[DescriptorFor("Delete")]
	public partial class DeleteDescriptor<T> : DocumentPathDescriptorBase<DeleteDescriptor<T>, T, DeleteRequestParameters>, IDeleteRequest<T>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<DeleteRequestParameters> pathInfo)
		{
			DeletePathInfo.Update(pathInfo, this);
		}
	}
}
