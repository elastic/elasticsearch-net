using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.SnapshotGetRepository1
{
	public partial class SnapshotGetRepository1YamlTests
	{	
	
		public class SnapshotGetRepository110BasicYamlBase : YamlTestsBase
		{
			public SnapshotGetRepository110BasicYamlBase() : base()
			{	

				//do snapshot.create_repository 
				_body = new {
					type= "url",
					settings= new {
						url= "http://snapshot.test1"
					}
				};
				this.Do(()=> _client.SnapshotCreateRepository("test_repo1", _body));

				//do snapshot.create_repository 
				_body = new {
					type= "url",
					settings= new {
						url= "http://snapshot.test2"
					}
				};
				this.Do(()=> _client.SnapshotCreateRepository("test_repo2", _body));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAllRepositories2Tests : SnapshotGetRepository110BasicYamlBase
		{
			[Test]
			public void GetAllRepositories2Test()
			{	

				//do snapshot.get_repository 
				this.Do(()=> _client.SnapshotGetRepository());

				//is_true _response.test_repo1; 
				this.IsTrue(_response.test_repo1);

				//is_true _response.test_repo2; 
				this.IsTrue(_response.test_repo2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetRepositoryByName3Tests : SnapshotGetRepository110BasicYamlBase
		{
			[Test]
			public void GetRepositoryByName3Test()
			{	

				//do snapshot.get_repository 
				this.Do(()=> _client.SnapshotGetRepository("test_repo1"));

				//is_true _response.test_repo1; 
				this.IsTrue(_response.test_repo1);

				//is_false _response.test_repo2; 
				this.IsFalse(_response.test_repo2);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetMissingRepositoryByName4Tests : SnapshotGetRepository110BasicYamlBase
		{
			[Test]
			public void GetMissingRepositoryByName4Test()
			{	

				//do snapshot.get_repository 
				this.Do(()=> _client.SnapshotGetRepository("test_repo2"));

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class GetAllRepositoriesWithLocalFlag5Tests : SnapshotGetRepository110BasicYamlBase
		{
			[Test]
			public void GetAllRepositoriesWithLocalFlag5Test()
			{	

				//do snapshot.get_repository 
				this.Do(()=> _client.SnapshotGetRepository(nv=>nv
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

