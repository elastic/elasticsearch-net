using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Nest.Settings;

namespace Nest
{
	[JsonObject]
	public class IndexSettingsResponse : BaseResponse
	{
		public IndexSettings Settings { get; internal set; }
	}
}
