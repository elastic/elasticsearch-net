using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IClearScrollRequest 
	{
		string ScrollId { get; set; }
	}

	public partial class ClearScrollRequest 
	{
		public string ScrollId { get; set; }

		public ClearScrollRequest(string scrollId)
		{
			this.ScrollId = scrollId;
		}
	}

	[DescriptorFor("ClearScroll")]
	public partial class ClearScrollDescriptor 
	{
		string IClearScrollRequest.ScrollId { get; set; }

		/// <summary>
		/// Specify the {name} part of the operation
		/// </summary>
		public ClearScrollDescriptor ScrollId(string scrollId) => Assign(a => a.ScrollId = scrollId);
	}
}
