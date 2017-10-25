using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	internal class StatefulSerializerFactory
	{
		public JsonNetSerializer CreateStateful(IConnectionSettingsValues settings, JsonConverter converter) =>
			new JsonNetSerializer(settings, converter);
	}
}
