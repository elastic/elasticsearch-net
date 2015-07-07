using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(CustomJsonConverter))]
	public interface IRegisterPercolatorRequest : IIndexNamePath<IndexRequestParameters>, ICustomJson
	{
		IDictionary<string, object> MetaData { get; set; }
		QueryContainer Query { get; set; }
	}

	internal static class RegisterPercolatorPathInfo
	{
		public static void Update(ElasticsearchPathInfo<IndexRequestParameters> pathInfo, IRegisterPercolatorRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
			pathInfo.Index = pathInfo.Index;
			pathInfo.Id = pathInfo.Name;
			pathInfo.Type = ".percolator";
		}
	}

	public class RegisterPercolatorRequest : IndexNamePathBase<IndexRequestParameters>, IRegisterPercolatorRequest
	{
		public RegisterPercolatorRequest(IndexName index, string name) : base(index, name)
		{
		}

		public IDictionary<string, object> MetaData { get; set; }
		public QueryContainer Query { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexRequestParameters> pathInfo)
		{
			RegisterPercolatorPathInfo.Update(pathInfo, this);
		}

		public object GetCustomJson()
		{
			return new FluentDictionary<string, object>(this.MetaData)
				.Add("query", this.Query);
		}

	}

	public class RegisterPercolatorDescriptor<T> : IndexNamePathDescriptor<RegisterPercolatorDescriptor<T>, IndexRequestParameters, T>, IRegisterPercolatorRequest
		where T : class
	{
		private IRegisterPercolatorRequest Self => this;

		QueryContainer IRegisterPercolatorRequest.Query { get; set; }

		IDictionary<string, object> IRegisterPercolatorRequest.MetaData { get; set; }

		/// <summary>
		/// Add metadata associated with this percolator query document
		/// </summary>
		public RegisterPercolatorDescriptor<T> AddMetadata(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
		{
			if (selector == null)
				return this;

			Self.MetaData = selector(new FluentDictionary<string, object>());
			return this;
		}

		/// <summary>
		/// The query to perform the percolation
		/// </summary>
		public RegisterPercolatorDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> querySelector)
		{
			querySelector.ThrowIfNull("querySelector");
			var d = querySelector(new QueryContainerDescriptor<T>());
			Self.Query = d;
			return this;
		}

		public object GetCustomJson()
		{
			return new FluentDictionary<string, object>(Self.MetaData)
				.Add("query", Self.Query);
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<IndexRequestParameters> pathInfo)
		{
			RegisterPercolatorPathInfo.Update(pathInfo, this);
		}
	}
}
