using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.IndicesGetSettings1
{
	public partial class IndicesGetSettings1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_1", null));

				//do indices.create 
				this.Do(()=> this._client.IndicesCreatePost("test_2", null));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetSettings2Tests : YamlTestsBase
		{
			[Test]
			public void GetSettings2Test()
			{	

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettingsForAll());

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_1.settings.index.number_of_replicas: 
				this.IsMatch(_response.test_1.settings.index.number_of_replicas, 1);

				//match _response.test_2.settings.index.number_of_shards: 
				this.IsMatch(_response.test_2.settings.index.number_of_shards, 5);

				//match _response.test_2.settings.index.number_of_replicas: 
				this.IsMatch(_response.test_2.settings.index.number_of_replicas, 1);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexSettings3Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexSettings3Test()
			{	

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("test_1"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_1.settings.index.number_of_replicas: 
				this.IsMatch(_response.test_1.settings.index.number_of_replicas, 1);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexSettingsAll4Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexSettingsAll4Test()
			{	

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("test_1", "_all"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_1.settings.index.number_of_replicas: 
				this.IsMatch(_response.test_1.settings.index.number_of_replicas, 1);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexSettings5Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexSettings5Test()
			{	

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("test_1", "*"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_1.settings.index.number_of_replicas: 
				this.IsMatch(_response.test_1.settings.index.number_of_replicas, 1);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexSettingsName6Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexSettingsName6Test()
			{	

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("test_1", "index.number_of_shards"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//is_false _response.test_1.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_1.settings.index.number_of_replicas);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexSettingsNameName7Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexSettingsNameName7Test()
			{	

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("test_1", "index.number_of_shards,index.number_of_replicas"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_1.settings.index.number_of_replicas: 
				this.IsMatch(_response.test_1.settings.index.number_of_replicas, 1);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexSettingsName8Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexSettingsName8Test()
			{	

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("test_1", "index.number_of_s*"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//is_false _response.test_1.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_1.settings.index.number_of_replicas);

				//is_false _response.test_2; 
				this.IsFalse(_response.test_2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetSettingsName9Tests : YamlTestsBase
		{
			[Test]
			public void GetSettingsName9Test()
			{	

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettingsForAll("index.number_of_shards"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_2.settings.index.number_of_shards: 
				this.IsMatch(_response.test_2.settings.index.number_of_shards, 5);

				//is_false _response.test_1.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_1.settings.index.number_of_replicas);

				//is_false _response.test_2.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_2.settings.index.number_of_replicas);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAllSettingsName10Tests : YamlTestsBase
		{
			[Test]
			public void GetAllSettingsName10Test()
			{	

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("_all", "index.number_of_shards"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_2.settings.index.number_of_shards: 
				this.IsMatch(_response.test_2.settings.index.number_of_shards, 5);

				//is_false _response.test_1.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_1.settings.index.number_of_replicas);

				//is_false _response.test_2.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_2.settings.index.number_of_replicas);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetSettingsName11Tests : YamlTestsBase
		{
			[Test]
			public void GetSettingsName11Test()
			{	

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("*", "index.number_of_shards"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_2.settings.index.number_of_shards: 
				this.IsMatch(_response.test_2.settings.index.number_of_shards, 5);

				//is_false _response.test_1.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_1.settings.index.number_of_replicas);

				//is_false _response.test_2.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_2.settings.index.number_of_replicas);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexIndexSettingsName12Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexIndexSettingsName12Test()
			{	

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("test_1,test_2", "index.number_of_shards"));

				//match _response.test_1.settings.index.number_of_shards: 
				this.IsMatch(_response.test_1.settings.index.number_of_shards, 5);

				//match _response.test_2.settings.index.number_of_shards: 
				this.IsMatch(_response.test_2.settings.index.number_of_shards, 5);

				//is_false _response.test_1.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_1.settings.index.number_of_replicas);

				//is_false _response.test_2.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_2.settings.index.number_of_replicas);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetIndexSettingsName13Tests : YamlTestsBase
		{
			[Test]
			public void GetIndexSettingsName13Test()
			{	

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettings("*2", "index.number_of_shards"));

				//match _response.test_2.settings.index.number_of_shards: 
				this.IsMatch(_response.test_2.settings.index.number_of_shards, 5);

				//is_false _response.test_1; 
				this.IsFalse(_response.test_1);

				//is_false _response.test_2.settings.index.number_of_replicas; 
				this.IsFalse(_response.test_2.settings.index.number_of_replicas);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetSettingsWithLocalFlag14Tests : YamlTestsBase
		{
			[Test]
			public void GetSettingsWithLocalFlag14Test()
			{	

				//do indices.get_settings 
				this.Do(()=> this._client.IndicesGetSettingsForAll(nv=>nv
					.Add("local", @"true")
				));

				//is_true _response.test_1; 
				this.IsTrue(_response.test_1);

				//is_true _response.test_2; 
				this.IsTrue(_response.test_2);

			}
		}
	}
}

