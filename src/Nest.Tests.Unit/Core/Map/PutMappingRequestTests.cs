using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.Map
{
	[TestFixture]
	public class PutMappingRequestTests : BaseJsonTests
	{
		[Test]
		public void DefaultPath()
		{
			var result = this._client.Map<ElasticSearchProject>(m=>m
				
			);
			var status = result.ConnectionStatus;
			StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.Result);
			StringAssert.EndsWith("/nest_test_data/elasticsearchprojects/_mapping", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}
		[Test]
		public void AllIndices()
		{
			var result = this._client.Map<ElasticSearchProject>(m => m
				.Indices("_all")
			);
			var status = result.ConnectionStatus;
			StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.Result);
			StringAssert.EndsWith("/_all/elasticsearchprojects/_mapping", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}
		[Test]
		public void MultipleIndices()
		{
			var result = this._client.Map<ElasticSearchProject>(m => m
				.Indices("nest_test_data", "nest_test_data_clone")
			);
			var status = result.ConnectionStatus;
			StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.Result);
			StringAssert.EndsWith("/nest_test_data%2Cnest_test_data_clone/elasticsearchprojects/_mapping", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}
		[Test]
		public void MultipleIndicesIgnoreConflicts()
		{
			var result = this._client.Map<ElasticSearchProject>(m => m
				.IgnoreConflicts()
				.Indices("nest_test_data", "nest_test_data_clone")
			);
			var status = result.ConnectionStatus;
			StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.Result);
			StringAssert.EndsWith("/nest_test_data%2Cnest_test_data_clone/elasticsearchprojects/_mapping?ignore_conflicts=true", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}
		[Test]
		public void MultipleIndicesIgnoreConflictsCustomTypeName()
		{
			var result = this._client.Map<ElasticSearchProject>(m => m
				.IgnoreConflicts()
				.Type("es_projects")
				.Indices("nest_test_data", "nest_test_data_clone")
			);
			var status = result.ConnectionStatus;
			StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.Result);
			StringAssert.EndsWith("/nest_test_data%2Cnest_test_data_clone/es_projects/_mapping?ignore_conflicts=true", status.RequestUrl);
			StringAssert.AreEqualIgnoringCase("PUT", status.RequestMethod);
		}
	}
}
