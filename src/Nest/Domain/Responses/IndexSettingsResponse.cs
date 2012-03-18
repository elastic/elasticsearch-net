using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject]
	public class IndexSettingsResponse : BaseResponse
	{
		public IndexSettings Settings { get; internal set; }
	}
}
