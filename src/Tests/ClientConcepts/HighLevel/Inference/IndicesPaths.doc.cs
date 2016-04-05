using FluentAssertions;
using Tests.Framework;
using Tests.Framework.MockData;

namespace Tests.ClientConcepts.HighLevel.Inference
{
	public class IndicesPaths
	{
		/**== Indices paths
		*
		* Some API's in Elasticsearch take one or many index name or a special `_all` marker to send the request to all the indices
		* In nest this is encoded using `Indices`.
		*
		*=== Implicit Conversion
		* Several types implicitly convert to `Indices`
		*/
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
			multipleIndicesFromString.Match(
				all => all.Should().BeNull(),
				many => many.Indices.Should().HaveCount(2).And.Contain("name2")
			);
			allFromString.Match(
				all => all.Should().NotBeNull(),
				many => many.Indices.Should().BeNull()
			);
			allWithOthersFromString.Match(
				all => all.Should().NotBeNull(),
				many => many.Indices.Should().BeNull()
			);
		}

		/**[[nest-indices]]
		*=== Using Nest.Indices
		* To ease creating `IndexName` or `Indices` from expressions, there is a static `Nest.Indices` class you can use
		*/
		[U] public void UsingStaticPropertyField()
		{
			var all = Nest.Indices.All; //<1> Using `_all` indices
			var many = Nest.Indices.Index("name1", "name2"); //<2> specifying multiple indices using strings
			var manyTyped = Nest.Indices.Index<Project>().And<CommitActivity>(); //<3> speciying multiple using types
			var singleTyped = Nest.Indices.Index<Project>();
			var singleString = Nest.Indices.Index("name1");

			var invalidSingleString = Nest.Indices.Index("name1, name2"); //<4> an **invalid** single index name
		}
	}
}
