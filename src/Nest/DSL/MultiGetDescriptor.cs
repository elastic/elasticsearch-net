using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IMultiGetRequest : IRequest<MultiGetRequestParameters>
	{
		[JsonProperty("docs")]
		IEnumerable<IMultiGetOperation> GetOperations { get; set; }
	}

	internal static class MultiGetPathInfo
	{
		public static void Update(ElasticsearchPathInfo<MultiGetRequestParameters> pathInfo, IMultiGetRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class MultiGetRequest : FixedIndexTypePathBase<MultiGetRequestParameters>, IMultiGetRequest
	{
		public IEnumerable<IMultiGetOperation> GetOperations { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MultiGetRequestParameters> pathInfo)
		{
			MultiGetPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("Mget")]
	public partial class MultiGetDescriptor : FixedIndexTypePathDescriptor<MultiGetDescriptor, MultiGetRequestParameters>, IMultiGetRequest
	{

		private IList<IMultiGetOperation> _getOperations = new List<IMultiGetOperation>();

		IEnumerable<IMultiGetOperation> IMultiGetRequest.GetOperations
		{
			get { return this._getOperations; }
			set { this._getOperations = value == null ? null : value.ToList();  }
		}

		public MultiGetDescriptor Get<T>(Func<MultiGetOperationDescriptor<T>, MultiGetOperationDescriptor<T>> getSelector) 
			where T : class
		{
			getSelector.ThrowIfNull("getSelector");

			var descriptor = getSelector(new MultiGetOperationDescriptor<T>());

			this._getOperations.Add(descriptor);
			return this;

		}

		public MultiGetDescriptor GetMany<T>(IEnumerable<long> ids, Func<MultiGetOperationDescriptor<T>, long, MultiGetOperationDescriptor<T>> getSelector=null) 
			where T : class
		{
			getSelector = getSelector ?? ((sg, s) => sg);
			foreach (var sg in ids.Select(id => getSelector(new MultiGetOperationDescriptor<T>().Id(id), id)))
				this._getOperations.Add(sg);
			return this;

		}
		public MultiGetDescriptor GetMany<T>(IEnumerable<string> ids, Func<MultiGetOperationDescriptor<T>, string, MultiGetOperationDescriptor<T>> getSelector=null)
			where T : class
		{
			getSelector = getSelector ?? ((sg, s) => sg);
			foreach (var sg in ids.Select(id => getSelector(new MultiGetOperationDescriptor<T>().Id(id), id)))
				this._getOperations.Add(sg);
			return this;

		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MultiGetRequestParameters> pathInfo)
		{
			MultiGetPathInfo.Update(pathInfo, this);
		}
	}
}
