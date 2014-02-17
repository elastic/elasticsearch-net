using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using NUnit.Framework;
using Nest.Tests.Integration.Yaml;


namespace Nest.Tests.Integration.Yaml.SnapshotGetRepository1
{
	public partial class SnapshotGetRepository1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Setup1Tests : YamlTestsBase
		{
			[Test]
			public void Setup1Test()
			{	

				//do snapshot.create_repository 
				_body = new {
					type= "url",
					settings= new {
						url= "http=//snapshot.test1"
					}
				};
				this.Do(()=> this._client.SnapshotCreateRepositoryPut("test_repo1", _body));

				//do snapshot.create_repository 
				_body = new {
					type= "url",
					settings= new {
						url= "http=//snapshot.test2"
					}
				};
				this.Do(()=> this._client.SnapshotCreateRepositoryPut("test_repo2", _body));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAllRepositories2Tests : YamlTestsBase
		{
			[Test]
			public void GetAllRepositories2Test()
			{	

				//do snapshot.get_repository 
				this.Do(()=> this._client.SnapshotGetRepository());

				//is_true _response.test_repo1; 
				this.IsTrue(_response.test_repo1);

				//is_true _response.test_repo2; 
				this.IsTrue(_response.test_repo2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetRepositoryByName3Tests : YamlTestsBase
		{
			[Test]
			public void GetRepositoryByName3Test()
			{	

				//do snapshot.get_repository 
				this.Do(()=> this._client.SnapshotGetRepository("test_repo1"));

				//is_true _response.test_repo1; 
				this.IsTrue(_response.test_repo1);

				//is_false _response.test_repo2; 
				this.IsFalse(_response.test_repo2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetMissingRepositoryByName4Tests : YamlTestsBase
		{
			[Test]
			public void GetMissingRepositoryByName4Test()
			{	

				//do snapshot.get_repository 
				this.Do(()=> this._client.SnapshotGetRepository("test_repo2"));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAllRepositoriesWithLocalFlag5Tests : YamlTestsBase
		{
			[Test]
			public void GetAllRepositoriesWithLocalFlag5Test()
			{	

				//do snapshot.get_repository 
				this.Do(()=> this._client.SnapshotGetRepository(nv=>nv
					.Add("local", @"true")
				));

				//is_true _response.test_repo1; 
				this.IsTrue(_response.test_repo1);

				//is_true _response.test_repo2; 
				this.IsTrue(_response.test_repo2);

			}
		}
	}
}

