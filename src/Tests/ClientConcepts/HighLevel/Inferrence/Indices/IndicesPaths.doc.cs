using System;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Nest;
using Newtonsoft.Json.Linq;
using Tests.Framework;
using Tests.Framework.MockData;
using Xunit.Sdk;
using static Tests.Framework.RoundTripper;
using static Nest.Static;

namespace Tests.ClientConcepts.HighLevel.Inferrence.Indices
{
	public class IndicesPaths
	{
		/** # Indices paths
		 * 
		 * Some API's in elasticsearch take one or many index name or a special "_all" marker to send the request to all the indices
		 * In nest this is encoded using `Indices`
		 */

		/** Several types implicitly convert to `Indices` */
		[U] public void ImplicitConversionFromString()
		{
			Nest.Indices singleIndexFromString = "name";
			Nest.Indices multipleIndicesFromString = "name1, name2";
			Nest.Indices allFromString = "_all";
			Nest.Indices allWithOthersFromString = "_all, name2";

			singleIndexFromString.Match(
				all => all.Should().BeNull(),
				many => many.Indices.Should().HaveCount(1).And.Contain("name")
			);
		}

		/** to ease creating FieldName's from expressions there is a static Property class you can use */
		[U] public void UsingStaticPropertyField()
		{
			/** */
			var all = Nest.Indices.All;
			var many = Nest.Indices.Index("name1", "name2");
			var manyTyped = Nest.Indices.Index<Project>().And<CommitActivity>();
			var singleTyped = Nest.Indices.Index<Project>();
			var singleString = Nest.Indices.Index("name1");
			var invalidSingleString = Nest.Indices.Index("name1, name2");

		}
	}
}
