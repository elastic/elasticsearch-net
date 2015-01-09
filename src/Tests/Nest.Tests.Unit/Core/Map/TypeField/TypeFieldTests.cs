using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using System.Reflection;

namespace Nest.Tests.Unit.Core.Map.TypeField
{
	[TestFixture]
	public class TypeFieldTests : BaseJsonTests
	{

		//Prior to Elasticsearch store took a yes/no (still supported)
		//We now favor sending actually booleans
		//These tests there no longer test wheter they send actual "yes" "no"
		//strings, but are not removed for living documentation purposes.

		[Test]
		public void TypeFieldSerializesYes()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.TypeField(t => t
					.Index()
					.Store()
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod()); 
		}
		[Test]
		public void TypeFieldSerializesNo()
		{
			var result = this._client.Map<ElasticsearchProject>(m => m
				.TypeField(t => t
					.Index(NonStringIndexOption.No)
					.Store(false)
				)
			);
			this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
		}
	}
}
