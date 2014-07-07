using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.Exists2
{
	public partial class Exists2YamlTests
	{	
	
		public class Exists230ParentYamlBase : YamlTestsBase
		{
			public Exists230ParentYamlBase() : base()
			{	

				//do indices.create 
				_body = new {
					mappings= new {
						test= new {
							_parent= new {
								type= "foo"
							}
						}
					}
				};
				this.Do(()=> _client.IndicesCreate("test_1", _body));

				//do cluster.health 
				this.Do(()=> _client.ClusterHealth(nv=>nv
					.AddQueryString("wait_for_status", @"yellow")
				));

			}
		}


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Parent2Tests : Exists230ParentYamlBase
		{
			[Test]
			public void Parent2Test()
			{	

				//do index 
				_body = new {
					foo= "bar"
				};
				this.Do(()=> _client.Index("test_1", "test", "1", _body, nv=>nv
					.AddQueryString("parent", 5)
				));

				//do exists 
				this.Do(()=> _client.Exists("test_1", "test", "1", nv=>nv
					.AddQueryString("parent", 5)
				));

				//is_true this._status; 
				this.IsTrue(this._status);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ParentOmitted3Tests : Exists230ParentYamlBase
		{
			[Test]
			public void ParentOmitted3Test()
			{	

				//do exists 
				this.Do(()=> _client.Exists("test_1", "test", "1"), shouldCatch: @"request");

			}
		}
	}
}

