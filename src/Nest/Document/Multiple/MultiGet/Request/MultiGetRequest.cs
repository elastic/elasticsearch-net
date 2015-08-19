using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IMultiGetRequest : IFixedIndexTypePath<MultiGetRequestParameters>
	{
		[JsonProperty("docs")]
		IList<IMultiGetOperation> GetOperations { get; set; }
	}

	internal static class MultiGetPathInfo
	{
		public static void Update(ElasticsearchPathInfo<MultiGetRequestParameters> pathInfo, IMultiGetRequest request)
		{
			pathInfo.HttpMethod = HttpMethod.POST;
		}
	}
	
	public partial class MultiGetRequest : FixedIndexTypePathBase<MultiGetRequestParameters>, IMultiGetRequest
	{
		public IList<IMultiGetOperation> GetOperations { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MultiGetRequestParameters> pathInfo)
		{
			MultiGetPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("Mget")]
	public partial class MultiGetDescriptor : FixedIndexTypePathDescriptor<MultiGetDescriptor, MultiGetRequestParameters>, IMultiGetRequest
	{
		private IMultiGetRequest Self => this;

		IList<IMultiGetOperation> IMultiGetRequest.GetOperations { get; set; }

		public MultiGetDescriptor()
		{
			this.Self.GetOperations = new List<IMultiGetOperation>();
		}

		public MultiGetDescriptor Get<T>(Func<MultiGetOperationDescriptor<T>, MultiGetOperationDescriptor<T>> getSelector) 
			where T : class
		{
			getSelector.ThrowIfNull("getSelector");
			var descriptor = getSelector(new MultiGetOperationDescriptor<T>());
			Self.GetOperations.Add(descriptor);
			return this;

		}

		public MultiGetDescriptor GetMany<T>(IEnumerable<long> ids, Func<MultiGetOperationDescriptor<T>, long, MultiGetOperationDescriptor<T>> getSelector=null) 
			where T : class
		{
			getSelector = getSelector ?? ((sg, s) => sg);
			foreach (var sg in ids.Select(id => getSelector(new MultiGetOperationDescriptor<T>().Id(id), id)))
				this.Self.GetOperations.Add(sg);
			return this;

		}
		public MultiGetDescriptor GetMany<T>(IEnumerable<string> ids, Func<MultiGetOperationDescriptor<T>, string, MultiGetOperationDescriptor<T>> getSelector=null)
			where T : class
		{
			getSelector = getSelector ?? ((sg, s) => sg);
			foreach (var sg in ids.Select(id => getSelector(new MultiGetOperationDescriptor<T>().Id(id), id)))
				this.Self.GetOperations.Add(sg);
			return this;

		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MultiGetRequestParameters> pathInfo)
		{
			MultiGetPathInfo.Update(pathInfo, this);
		}
	}
}
