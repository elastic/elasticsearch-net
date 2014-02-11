using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Integration.Yaml
{
	public class YamlTestsBase
	{
		protected readonly RawElasticClient _client;
		protected object _body;
		protected ConnectionStatus _status;
		protected dynamic _response;
	
		public YamlTestsBase()
		{
			var uri = new Uri("http:localhost:9200");
			var settings = new ConnectionSettings(uri, "nest-default-index");
			_client = new RawElasticClient(settings);
		}

		public void IsTrue(object o)
		{
			
		}

		public void IsFalse(object o)
		{
			
		}

		public void IsLowerThan(object o, int value)
		{
			
		}

		public void IsGreaterThan(object o, int value)
		{
			
		}
		
		public void IsLength(object o, int value)
		{
			
		}
	}
}
