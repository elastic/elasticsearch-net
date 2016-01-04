using CodeGeneration.LowLevelClient.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGeneration.LowLevelClient.Overrides.Global
{
	public static class GlobalQueryParameters
	{
		/// <summary>
		/// Query parameters that are available on all API endpoints, but are not specified in the REST spec
		/// https://www.elastic.co/guide/en/elasticsearch/reference/current/common-options.html
		/// </summary>
		public static readonly Dictionary<string, ApiQueryParameters> Parameters = new Dictionary<string, ApiQueryParameters>
		{
			{ "source", new ApiQueryParameters 
				{ 
					Description = "The URL-encoded request definition",
					Type = "string",
					OriginalQueryStringParamName = "source"  
				} 
			},
			{ "filter_path", new ApiQueryParameters 
				{
					Description = "Comma separated list of filters used to reduce the response returned by Elasticsearch",
					Type = "string",
					OriginalQueryStringParamName = "filter_path"
				}
			}
		};
	}
}
