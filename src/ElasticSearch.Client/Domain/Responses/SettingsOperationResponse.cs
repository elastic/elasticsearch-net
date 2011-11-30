using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using ElasticSearch.Client.DSL;

namespace ElasticSearch.Client
{
	[JsonObject]
	public class SettingsOperationResponse
	{
		public bool Success { get; internal set; }
		public ConnectionStatus ConnectionStatus { get; internal set; }
		public SettingsOperationResponse()
		{
			this.Success = true;
		}

		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }
	}
}