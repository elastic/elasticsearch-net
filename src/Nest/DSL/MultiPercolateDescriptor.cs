using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IMultiPercolateRequest : IFixedIndexTypePath<MultiPercolateRequestParameters>
	{
		IList<IPercolateOperation> Percolations { get; set; }
	}

	internal static class MultiPercolatePathInfo
	{
		public static void Update(ElasticsearchPathInfo<MultiPercolateRequestParameters> pathInfo, IMultiPercolateRequest request)
		{
			pathInfo.HttpMethod = PathInfoHttpMethod.POST;
		}
	}
	
	public partial class MultiPercolateRequest : FixedIndexTypePathBase<MultiPercolateRequestParameters>, IMultiPercolateRequest
	{
		public IList<IPercolateOperation> Percolations { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MultiPercolateRequestParameters> pathInfo)
		{
			MultiPercolatePathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("Mpercolate")]
	public partial class MultiPercolateDescriptor : FixedIndexTypePathDescriptor<MultiPercolateDescriptor, MultiPercolateRequestParameters>, IMultiPercolateRequest
	{
		private IMultiPercolateRequest Self { get { return this; } }

		IList<IPercolateOperation> IMultiPercolateRequest.Percolations { get; set; }

		public MultiPercolateDescriptor()
		{
			this.Self.Percolations = new List<IPercolateOperation>();
		}

		public MultiPercolateDescriptor Percolate<T>(Func<PercolateDescriptor<T>, PercolateDescriptor<T>> getSelector) 
			where T : class
		{
			getSelector.ThrowIfNull("getSelector");
			var descriptor = getSelector(new PercolateDescriptor<T>().Index<T>().Type<T>());
			Self.Percolations.Add(descriptor);
			return this;

		}

		public MultiPercolateDescriptor Count<T>(Func<PercolateCountDescriptor<T>, PercolateCountDescriptor<T>> getSelector) 
			where T : class
		{
			getSelector.ThrowIfNull("getSelector");
			var descriptor = getSelector(new PercolateCountDescriptor<T>().Index<T>().Type<T>());
			Self.Percolations.Add(descriptor);
			return this;

		}
		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<MultiPercolateRequestParameters> pathInfo)
		{
			MultiPercolatePathInfo.Update(pathInfo, this);
		}
	}
}
