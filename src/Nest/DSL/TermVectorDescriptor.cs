using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;

namespace Nest
{
    public interface ITermvectorRequest : IRequest<TermvectorRequestParameters> { }

    public interface ITermvectorRequest<T> : ITermvectorRequest where T : class { }

    internal static class TermvectorPathInfo
    {
        public static void Update(IConnectionSettingsValues settings, ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo)
        {
            pathInfo.HttpMethod = PathInfoHttpMethod.GET;
        }
    }

    public partial class TermvectorRequest : DocumentPathBase<TermvectorRequestParameters>, ITermvectorRequest
    {
        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo)
        {
            TermvectorPathInfo.Update(settings, pathInfo);
        }
    }

    public partial class TermvectorRequest<T> : DocumentPathBase<TermvectorRequestParameters>, ITermvectorRequest<T>
        where T : class
    {
        protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo)
        {
            TermvectorPathInfo.Update(settings, pathInfo);
        }
    }

	public partial class TermvectorDescriptor<T> : DocumentPathDescriptor<TermvectorDescriptor<T>, TermvectorRequestParameters, T>
		where T : class
	{
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<TermvectorRequestParameters> pathInfo)
		{
            TermvectorPathInfo.Update(settings, pathInfo);
		}
	}
}
