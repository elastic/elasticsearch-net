using Elasticsearch.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elasticsearch.Net
{
	public static class FlushRequestParametersObsoleteExtensions
	{
		///<summary>If set to true a new index writer is created and settings that have been changed related to the index writer will be refreshed. Note: if a full flush is required for a setting to take effect this will be part of the settings update process and it not required to be executed by the user. (This setting can be considered as internal)</summary>
		[Obsolete("Scheduled to be removed in 2.0")]
		public static FlushRequestParameters Full(this FlushRequestParameters parameters, bool full)
		{
			parameters.AddQueryString("full", full);
			return parameters;
		}
	}
}
