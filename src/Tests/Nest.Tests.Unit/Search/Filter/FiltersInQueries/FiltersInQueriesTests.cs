using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Search.Filter.FiltersInQueries
{
	[TestFixture]
  public class FiltersInQueriesTests : BaseJsonTests
	{
		[Test]
		public void FilteredQueryCombines()
		{
      var s = new SearchDescriptor<ElasticsearchProject>()
        .From(0)
        .Take(10)
        .Query(q => q.Filtered(fq =>
            fq.Filter(ff=>
              ff.Term(f => f.Name, "foo")
              || ff.Term(f => f.Name, "bar")            
            )
         ));

			this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
		}
    [Test]
    public void ConstantScoreQueryCombines()
    {
      var s = new SearchDescriptor<ElasticsearchProject>()
        .From(0)
        .Take(10)
        .Query(q => q
          .ConstantScore(csq => csq
            .Filter(ff =>
              ff.Term(f => f.Name, "foo")
              || ff.Term(f => f.Name, "bar")
            )

          )
        );

      this.JsonEquals(s, System.Reflection.MethodInfo.GetCurrentMethod());
    }
    
   
	} 
}
