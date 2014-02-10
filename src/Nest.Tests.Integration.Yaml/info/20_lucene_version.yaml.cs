using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;


namespace Nest.Tests.Integration.Yaml.Info
{
	public partial class Info20LuceneVersionYaml20Tests
	{
		
		public class LuceneVersion20Tests
		{
			private readonly RawElasticClient _client;
			private object _body;
		
			public LuceneVersion20Tests()
			{
				var uri = new Uri("http:localhost:9200");
				var settings = new ConnectionSettings(uri, "nest-default-index");
				_client = new RawElasticClient(settings);
			}

			[Test]
			public void LuceneVersionTests()
			{

				//do info 
				
				this._client.InfoGet(nv=>nv);
			}
		}
	}
}
