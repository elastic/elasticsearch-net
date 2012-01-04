using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Linq.Expressions;
using Nest.DSL;

namespace Nest
{
	[JsonObject]
	public class SettingsOperationResponse : BaseResponse
	{
		public SettingsOperationResponse()
		{
			this.IsValid = true;
		}

		[JsonProperty(PropertyName = "ok")]
		public bool OK { get; internal set; }
	}
}