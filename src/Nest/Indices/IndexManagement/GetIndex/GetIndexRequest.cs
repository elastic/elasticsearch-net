using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetIndexRequest 
	{
		/// <summary>
		/// Be selective which features to return, i.e GetIndexFeature.Mappings | GetIndexFeature.Settings
		/// </summary>
		GetIndexFeature Features { get; set; }
	}

	//TODO This used to have a fairly complex way to build up the features route params

	//internal static class GetIndexPathInfo
	//{
	//	public static void Update(RouteValues pathInfo, IGetIndexRequest request)
	//	{
	//		if (pathInfo.Index == null)
	//			throw new DslException("Can not call GetIndex without specifying one or more indices or explicitly calling .AllIndices()");

	//		pathInfo.HttpMethod = HttpMethod.GET;
	//		if (request.Features == default(GetIndexFeature) || request.Features == GetIndexFeature.All)
	//			return;

	//		var features = new List<string>();
	//		if ((request.Features & GetIndexFeature.Settings) == GetIndexFeature.Settings)
	//			features.Add("_settings");
	//		if ((request.Features & GetIndexFeature.Mappings) == GetIndexFeature.Mappings)
	//			features.Add("_mappings");
	//		if ((request.Features & GetIndexFeature.Warmers) == GetIndexFeature.Warmers)
	//			features.Add("_warmers");
	//		if ((request.Features & GetIndexFeature.Aliases) == GetIndexFeature.Aliases)
	//			features.Add("_aliases");

	//		pathInfo.Feature = string.Join(",", features);
	//	}
	//}

	public partial class GetIndexRequest 
	{
		/// <summary>
		/// Be selective which features to return, i.e GetIndexFeature.Mappings | GetIndexFeature.Settings
		/// </summary>
		public GetIndexFeature Features { get; set; }
	}

	[DescriptorFor("IndicesGet")]
	public partial class GetIndexDescriptor 
	{
		GetIndexFeature IGetIndexRequest.Features { get; set; }

		/// <summary>
		/// Be selective which features to return, i.e GetIndexFeature.Mappings | GetIndexFeature.Settings
		/// </summary>
		public GetIndexDescriptor Features(GetIndexFeature features) => Assign(a => a.Features = features);
	}
}
