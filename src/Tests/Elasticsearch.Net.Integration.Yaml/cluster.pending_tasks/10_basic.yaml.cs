using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;


namespace Elasticsearch.Net.Integration.Yaml.ClusterPendingTasks1
{
	public partial class ClusterPendingTasks1YamlTests
	{	


		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestPendingTasks1Tests : YamlTestsBase
		{
			[Test]
			public void TestPendingTasks1Test()
			{	

				//do cluster.pending_tasks 
				this.Do(()=> _client.ClusterPendingTasks());

				//is_true _response.tasks; 
				this.IsTrue(_response.tasks);

			}
		}

		[NCrunch.Framework.ExclusivelyUses("ElasticsearchYamlTests")]
		public class TestPendingTasksWithLocalFlag2Tests : YamlTestsBase
		{
			[Test]
			public void TestPendingTasksWithLocalFlag2Test()
			{	

				//do cluster.pending_tasks 
				this.Do(()=> _client.ClusterPendingTasks(nv=>nv
					.AddQueryString("local", @"true")
				));

				//is_true _response.tasks; 
				this.IsTrue(_response.tasks);

			}
		}
	}
}

