using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.CatAliases1
{
	public partial class CatAliases1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class Help1Tests : YamlTestsBase
		{
			[Test]
			public void Help1Test()
			{	

				//do cat.aliases 
				this.Do(()=> _client.CatAliases(nv=>nv
					.AddQueryString("help", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^  alias            .+   \n
    index            .+   \n
    filter           .+   \n
    routing.index    .+   \n
    routing.search   .+   \n
$/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class EmptyCluster2Tests : YamlTestsBase
		{
			[Test]
			public void EmptyCluster2Test()
			{	

				//do cat.aliases 
				this.Do(()=> _client.CatAliases());

				//match this._status: 
				this.IsMatch(this._status, @"/^
$/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SimpleAlias3Tests : YamlTestsBase
		{
			[Test]
			public void SimpleAlias3Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test", null));

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test", "test_alias", null));

				//do cat.aliases 
				this.Do(()=> _client.CatAliases());

				//match this._status: 
				this.IsMatch(this._status, @"/^
    test_alias          \s+
    test                \s+
    -                   \s+
    -                   \s+
    -                   \s+
$/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ComplexAlias4Tests : YamlTestsBase
		{
			[Test]
			public void ComplexAlias4Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test", null));

				//do indices.put_alias 
				_body = new {
					index_routing= "ir",
					search_routing= "sr1,sr2",
					filter= new {
						term= new {
							foo= "bar"
						}
					}
				};
				this.Do(()=> _client.IndicesPutAlias("test", "test_alias", _body));

				//do cat.aliases 
				this.Do(()=> _client.CatAliases());

				//match this._status: 
				this.IsMatch(this._status, @"/^
    test_alias          \s+
    test                \s+
    [*]                 \s+
    ir                  \s+
    sr1,sr2             \s+
$/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class AliasName5Tests : YamlTestsBase
		{
			[Test]
			public void AliasName5Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test", null));

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test", "test_1", null));

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test", "test_2", null));

				//do cat.aliases 
				this.Do(()=> _client.CatAliases("test_1"));

				//match this._status: 
				this.IsMatch(this._status, @"/^test_1 .+ \n$/");

				//do cat.aliases 
				this.Do(()=> _client.CatAliases("test_2"));

				//match this._status: 
				this.IsMatch(this._status, @"/^test_2 .+ \n$/");

				//do cat.aliases 
				this.Do(()=> _client.CatAliases("test_*"));

				//match this._status: 
				this.IsMatch(this._status, @"/ (^|\n)test_1 .+ \n/");

				//match this._status: 
				this.IsMatch(this._status, @"/ (^|\n)test_2 .+ \n/");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class ColumnHeaders6Tests : YamlTestsBase
		{
			[Test]
			public void ColumnHeaders6Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test", null));

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test", "test_1", null));

				//do cat.aliases 
				this.Do(()=> _client.CatAliases(nv=>nv
					.AddQueryString("v", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^  alias           \s+
    index           \s+
    filter          \s+
    routing.index   \s+
    routing.search  \s+
    \n
    test_1          \s+
    test            \s+
    -               \s+
    -               \s+
    -               \s+
$/
");

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class SelectColumns7Tests : YamlTestsBase
		{
			[Test]
			public void SelectColumns7Test()
			{	

				//do indices.create 
				this.Do(()=> _client.IndicesCreate("test", null));

				//do indices.put_alias 
				this.Do(()=> _client.IndicesPutAlias("test", "test_1", null));

				//do cat.aliases 
				this.Do(()=> _client.CatAliases(nv=>nv
					.AddQueryString("h", new [] {
						@"index",
						@"alias"
					})
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^ test \s+ test_1 \s+ $/");

				//do cat.aliases 
				this.Do(()=> _client.CatAliases(nv=>nv
					.AddQueryString("h", new [] {
						@"index",
						@"alias"
					})
					.AddQueryString("v", @"true")
				));

				//match this._status: 
				this.IsMatch(this._status, @"/^
    index \s+ alias  \s+ \n
    test  \s+ test_1 \s+ \n
$/
");

			}
		}
	}
}

