using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public interface IGetIndexRequest : IIndicesOptionalExplicitAllPath<GetIndexRequestParameters>
	{
		/// <summary>
		/// Be selective which features to return, i.e GetIndexFeature.Mappings | GetIndexFeature.Settings
		/// </summary>
		GetIndexFeature Features { get; set; }
	}

	internal static class GetIndexPathInfo
	{
		public static void Update(ElasticsearchPathInfo<GetIndexRequestParameters> pathInfo, IGetIndexRequest request)
		{
			if (pathInfo.Index.IsNullOrEmpty())
				throw new DslException("Can not call GetIndex without specifying one or more indices or explicitly calling .AllIndices()");

			pathInfo.HttpMethod = PathInfoHttpMethod.GET;
			if (request.Features == default(GetIndexFeature) || request.Features == GetIndexFeature.All)
				return;

			var features = new List<string>();
			if ((request.Features & GetIndexFeature.Settings) == GetIndexFeature.Settings)
				features.Add("_settings");
			if ((request.Features & GetIndexFeature.Mappings) == GetIndexFeature.Mappings)
				features.Add("_mappings");
			if ((request.Features & GetIndexFeature.Warmers) == GetIndexFeature.Warmers)
				features.Add("_warmers");
			if ((request.Features & GetIndexFeature.Aliases) == GetIndexFeature.Aliases)
				features.Add("_aliases");

			pathInfo.Feature = string.Join(",", features);
		}
	}

	public partial class GetIndexRequest : IndicesOptionalExplicitAllPathBase<GetIndexRequestParameters>, IGetIndexRequest
	{
		public GetIndexRequest() : base()
		{
			this.AllIndices = true;
		}

		public GetIndexRequest(IndexNameMarker index) : this()
		{
			this.Indices = new[] {index};
		}

		public GetIndexRequest(IEnumerable<IndexNameMarker> indices) : this()
		{
			this.Indices = indices;
		}

		/// <summary>
		/// Be selective which features to return, i.e GetIndexFeature.Mappings | GetIndexFeature.Settings
		/// </summary>
		public GetIndexFeature Features { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetIndexRequestParameters> pathInfo)
		{
			GetIndexPathInfo.Update(pathInfo, this);
		}
	}

	[DescriptorFor("IndicesGet")]
	public partial class GetIndexDescriptor : IndicesOptionalExplicitAllPathDescriptor<GetIndexDescriptor, GetIndexRequestParameters>, IGetIndexRequest
	{
		private IGetIndexRequest Self { get { return this; } }
		GetIndexFeature IGetIndexRequest.Features { get; set; }
		
		/// <summary>
		/// Be selective which features to return, i.e GetIndexFeature.Mappings | GetIndexFeature.Settings
		/// </summary>
		public GetIndexDescriptor Features(GetIndexFeature features)
		{
			Self.Features = features;
			return this;
		}

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<GetIndexRequestParameters> pathInfo)
		{
			GetIndexPathInfo.Update(pathInfo, this);
		}
	}
}
