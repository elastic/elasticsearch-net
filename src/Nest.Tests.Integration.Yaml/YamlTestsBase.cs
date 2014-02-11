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
		protected Dictionary<string, dynamic> _responseDictionary;

		public YamlTestsBase()
		{
			var uri = new Uri("http:localhost:9200");
			var settings = new ConnectionSettings(uri, "nest-default-index");
			_client = new RawElasticClient(settings);
		}

		protected void Do(Func<ConnectionStatus> action)
		{
			this._status = action();
			this._response = this._status.Deserialize<dynamic>();
			this._responseDictionary = this._status.Deserialize<Dictionary<string, object>>();
		}

		protected void IsTrue(object o)
		{
			
		}

		protected void IsFalse(object o)
		{
			
		}

		protected void IsLowerThan(object o, int value)
		{
			
		}

		protected void IsGreaterThan(object o, int value)
		{
			
		}
		
		protected void IsLength(object o, int value)
		{
			
		}

		protected void IsMatch(object o, object value)
		{
			
		}
	}
}
