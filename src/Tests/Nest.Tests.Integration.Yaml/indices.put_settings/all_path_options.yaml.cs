using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesPutSettings2
{
	public partial class IndicesPutSettings2YamlTests
	{	
	
		public class IndicesPutSettings2AllPathOptionsYamlBase : YamlTestsBase
		{
			public IndicesPutSettings2AllPathOptionsYamlBase() : base()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index1", null));

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test_index2", null));

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("foo", null));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutSettingsPerIndex2Tests : IndicesPutSettings2AllPathOptionsYamlBase
		{
			[Test]
			public void PutSettingsPerIndex2Test()
			{	

				//do indices.put_settings 
				_body = new {
					refresh_interval= "1s"
				};
				this.Do(()=> _client.IndicesPutSettings("test_index1", _body));

				//do indices.put_settings 
				_body = new {
					refresh_interval= "1s"
				};
				this.Do(()=> _client.IndicesPutSettings("test_index2", _body));

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettingsForAll());

				//match _response.test_index1.settings.index.refresh_interval: 
				this.IsMatch(_response.test_index1.settings.index.refresh_interval, @"1s");

				//match _response.test_index2.settings.index.refresh_interval: 
				this.IsMatch(_response.test_index2.settings.index.refresh_interval, @"1s");

				//is_false _response.foo.settings.index.refresh_interval; 
				this.IsFalse(_response.foo.settings.index.refresh_interval);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutSettingsInAllIndex3Tests : IndicesPutSettings2AllPathOptionsYamlBase
		{
			[Test]
			public void PutSettingsInAllIndex3Test()
			{	

				//do indices.put_settings 
				_body = new {
					refresh_interval= "1s"
				};
				this.Do(()=> _client.IndicesPutSettings("_all", _body));

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettingsForAll());

				//match _response.test_index1.settings.index.refresh_interval: 
				this.IsMatch(_response.test_index1.settings.index.refresh_interval, @"1s");

				//match _response.test_index2.settings.index.refresh_interval: 
				this.IsMatch(_response.test_index2.settings.index.refresh_interval, @"1s");

				//match _response.foo.settings.index.refresh_interval: 
				this.IsMatch(_response.foo.settings.index.refresh_interval, @"1s");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutSettingsInIndex4Tests : IndicesPutSettings2AllPathOptionsYamlBase
		{
			[Test]
			public void PutSettingsInIndex4Test()
			{	

				//do indices.put_settings 
				_body = new {
					refresh_interval= "1s"
				};
				this.Do(()=> _client.IndicesPutSettings("*", _body));

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettingsForAll());

				//match _response.test_index1.settings.index.refresh_interval: 
				this.IsMatch(_response.test_index1.settings.index.refresh_interval, @"1s");

				//match _response.test_index2.settings.index.refresh_interval: 
				this.IsMatch(_response.test_index2.settings.index.refresh_interval, @"1s");

				//match _response.foo.settings.index.refresh_interval: 
				this.IsMatch(_response.foo.settings.index.refresh_interval, @"1s");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutSettingsInPrefixIndex5Tests : IndicesPutSettings2AllPathOptionsYamlBase
		{
			[Test]
			public void PutSettingsInPrefixIndex5Test()
			{	

				//do indices.put_settings 
				_body = new {
					refresh_interval= "1s"
				};
				this.Do(()=> _client.IndicesPutSettings("test*", _body));

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettingsForAll());

				//match _response.test_index1.settings.index.refresh_interval: 
				this.IsMatch(_response.test_index1.settings.index.refresh_interval, @"1s");

				//match _response.test_index2.settings.index.refresh_interval: 
				this.IsMatch(_response.test_index2.settings.index.refresh_interval, @"1s");

				//is_false _response.foo.settings.index.refresh_interval; 
				this.IsFalse(_response.foo.settings.index.refresh_interval);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutSettingsInListOfIndices6Tests : IndicesPutSettings2AllPathOptionsYamlBase
		{
			[Test]
			public void PutSettingsInListOfIndices6Test()
			{	

				//skip 1 - 999; 
				this.Skip("1 - 999", "list of indices not implemented yet");

				//do indices.put_settings 
				_body = new {
					refresh_interval= "1s"
				};
				this.Do(()=> _client.IndicesPutSettings("test_index1, test_index2", _body));

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettingsForAll());

				//match _response.test_index1.settings.index.refresh_interval: 
				this.IsMatch(_response.test_index1.settings.index.refresh_interval, @"1s");

				//match _response.test_index2.settings.index.refresh_interval: 
				this.IsMatch(_response.test_index2.settings.index.refresh_interval, @"1s");

				//is_false _response.foo.settings.index.refresh_interval; 
				this.IsFalse(_response.foo.settings.index.refresh_interval);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class PutSettingsInBlankIndex7Tests : IndicesPutSettings2AllPathOptionsYamlBase
		{
			[Test]
			public void PutSettingsInBlankIndex7Test()
			{	

				//do indices.put_settings 
				_body = new {
					refresh_interval= "1s"
				};
				this.Do(()=> _client.IndicesPutSettingsForAll(_body));

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettingsForAll());

				//match _response.test_index1.settings.index.refresh_interval: 
				this.IsMatch(_response.test_index1.settings.index.refresh_interval, @"1s");

				//match _response.test_index2.settings.index.refresh_interval: 
				this.IsMatch(_response.test_index2.settings.index.refresh_interval, @"1s");

				//match _response.foo.settings.index.refresh_interval: 
				this.IsMatch(_response.foo.settings.index.refresh_interval, @"1s");

			}
		}
	}
}

