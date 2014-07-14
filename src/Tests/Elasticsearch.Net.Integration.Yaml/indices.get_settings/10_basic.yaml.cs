using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.IndicesGetSettings1
{
	public partial class IndicesGetSettings1YamlTests
	{	
	
		public class IndicesGetSettings110BasicYamlBase : YamlTestsBase
		{
			public IndicesGetSettings110BasicYamlBase() : base()
			{	

				//do indices.create 
				_body = new {
					settings= new {
						number_of_shards= "5",
						number_of_replicas= "1"
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do indices.create 
				_body = new {
					settings= new {
						number_of_shards= "3",
						number_of_replicas= "0"
					}
				};
				this.Do(()=> _client.IndicesCreate("test_2", _body));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetSettings2Tests : IndicesGetSettings110BasicYamlBase
		{
			[Test]
			public void GetSettings2Test()
			{	

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettingsForAll());

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_1.settings.index.number_of_replicas: 
				this.IsMatch(_response.test_1.settings.index.number_of_replicas, 1);

				//match _response.test_2.settings.index.number_of_shards: 
				this.IsMatch(_response.test_2.settings.index.number_of_shards, 3);

				//match _response.test_2.settings.index.number_of_replicas: 
				this.IsMatch(_response.test_2.settings.index.number_of_replicas, 0);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexSettings3Tests : IndicesGetSettings110BasicYamlBase
		{
			[Test]
			public void GetIndexSettings3Test()
			{	

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettings("test_1"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_1.settings.index.number_of_replicas: 
				this.IsMatch(_response.test_1.settings.index.number_of_replicas, 1);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexSettingsAll4Tests : IndicesGetSettings110BasicYamlBase
		{
			[Test]
			public void GetIndexSettingsAll4Test()
			{	

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettings("test_1", "_all"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_1.settings.index.number_of_replicas: 
				this.IsMatch(_response.test_1.settings.index.number_of_replicas, 1);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexSettings5Tests : IndicesGetSettings110BasicYamlBase
		{
			[Test]
			public void GetIndexSettings5Test()
			{	

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettings("test_1", "*"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_1.settings.index.number_of_replicas: 
				this.IsMatch(_response.test_1.settings.index.number_of_replicas, 1);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexSettingsName6Tests : IndicesGetSettings110BasicYamlBase
		{
			[Test]
			public void GetIndexSettingsName6Test()
			{	

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettings("test_1", "index.number_of_shards"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//is_false _response.test_1.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_1.settings.index.number_of_replicas);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexSettingsNameName7Tests : IndicesGetSettings110BasicYamlBase
		{
			[Test]
			public void GetIndexSettingsNameName7Test()
			{	

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettings("test_1", "index.number_of_shards,index.number_of_replicas"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_1.settings.index.number_of_replicas: 
				this.IsMatch(_response.test_1.settings.index.number_of_replicas, 1);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexSettingsName8Tests : IndicesGetSettings110BasicYamlBase
		{
			[Test]
			public void GetIndexSettingsName8Test()
			{	

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettings("test_1", "index.number_of_s*"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//is_false _response.test_1.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_1.settings.index.number_of_replicas);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetSettingsName9Tests : IndicesGetSettings110BasicYamlBase
		{
			[Test]
			public void GetSettingsName9Test()
			{	

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettingsForAll("index.number_of_shards"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_2.settings.index.number_of_shards: 
				this.IsMatch(_response.test_2.settings.index.number_of_shards, 3);

				//is_false _response.test_1.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_1.settings.index.number_of_replicas);

				//is_false _response.test_2.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_2.settings.index.number_of_replicas);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAllSettingsName10Tests : IndicesGetSettings110BasicYamlBase
		{
			[Test]
			public void GetAllSettingsName10Test()
			{	

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettings("_all", "index.number_of_shards"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_2.settings.index.number_of_shards: 
				this.IsMatch(_response.test_2.settings.index.number_of_shards, 3);

				//is_false _response.test_1.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_1.settings.index.number_of_replicas);

				//is_false _response.test_2.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_2.settings.index.number_of_replicas);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetSettingsName11Tests : IndicesGetSettings110BasicYamlBase
		{
			[Test]
			public void GetSettingsName11Test()
			{	

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettings("*", "index.number_of_shards"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_2.settings.index.number_of_shards: 
				this.IsMatch(_response.test_2.settings.index.number_of_shards, 3);

				//is_false _response.test_1.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_1.settings.index.number_of_replicas);

				//is_false _response.test_2.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_2.settings.index.number_of_replicas);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexIndexSettingsName12Tests : IndicesGetSettings110BasicYamlBase
		{
			[Test]
			public void GetIndexIndexSettingsName12Test()
			{	

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettings("test_1,test_2", "index.number_of_shards"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_2.settings.index.number_of_shards: 
				this.IsMatch(_response.test_2.settings.index.number_of_shards, 3);

				//is_false _response.test_1.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_1.settings.index.number_of_replicas);

				//is_false _response.test_2.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_2.settings.index.number_of_replicas);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexSettingsName13Tests : IndicesGetSettings110BasicYamlBase
		{
			[Test]
			public void GetIndexSettingsName13Test()
			{	

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettings("*2", "index.number_of_shards"));

				//match _response.test_2.settings.index.number_of_shards: 
				this.IsMatch(_response.test_2.settings.index.number_of_shards, 3);

				//is_false _response.test_1; 
				this.IsFalse(_response.test_1);

				//is_false _response.test_2.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_2.settings.index.number_of_replicas);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetSettingsWithLocalFlag14Tests : IndicesGetSettings110BasicYamlBase
		{
			[Test]
			public void GetSettingsWithLocalFlag14Test()
			{	

				//do indices.get_settings 
				this.Do(()=> _client.IndicesGetSettingsForAll(nv=>nv
					.AddQueryString("local", @"true")
				));

				//is_true _response.test_1; 
				this.IsTrue(_response.test_1);

				//is_true _response.test_2; 
				this.IsTrue(_response.test_2);

			}
		}
	}
}

