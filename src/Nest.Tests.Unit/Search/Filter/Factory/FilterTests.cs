using System.IO;
using System.Reflection;
using NUnit.Framework;
using Nest.FactoryDsl;
using Nest.FactoryDsl.Query;

namespace Nest.Tests.Unit.Search.Filter.Factory
{
    [TestFixture]
	public class FilterTests : BaseJsonTests
    {
        private SearchBuilder _sb;

        [SetUp]
        public void Setup()
        {
            this._sb = new SearchBuilder();
        }

        [Test]
        public void AndFilterTestOutput()
        {
            var andFilter = FilterFactory.AndFilter(FilterFactory.TermFilter("name.first", "shay1"), FilterFactory.TermFilter("name.first", "shay4"));
            var filter = new FilteredQueryBuilder(QueryFactory.TermQuery("name.first", "shay"), andFilter);

			this.JsonEquals(filter.ToJsonObject(), MethodInfo.GetCurrentMethod(), "and-filter");
        }

        [Test]
        public void AndFilterNamedTestOutput()
        {
            var andFilter = FilterFactory.AndFilter(FilterFactory.TermFilter("name.first", "shay1"), FilterFactory.TermFilter("name.first", "shay4"));
            andFilter.FilterName("test");
            var filter = new FilteredQueryBuilder(QueryFactory.TermQuery("name.first", "shay"), andFilter);

			this.JsonEquals(filter.ToJsonObject(), MethodInfo.GetCurrentMethod(), "and-filter-named");
        }

        [Test]
        public void BoolFilterTestOutput()
        {
            var boolFilter = FilterFactory.BoolFilter();
            boolFilter.Must(FilterFactory.TermFilter("name.first", "shay1"));
            boolFilter.Must(FilterFactory.TermFilter("name.first", "shay4"));
            boolFilter.MustNot(FilterFactory.TermFilter("name.first", "shay2"));
            boolFilter.Should(FilterFactory.TermFilter("name.first", "shay3"));

            var filter = new FilteredQueryBuilder(QueryFactory.TermQuery("name.first", "shay"), boolFilter);

			this.JsonEquals(filter.ToJsonObject(), MethodInfo.GetCurrentMethod(), "bool-filter");
        }

        [Test]
        public void TermFilterTestOutput()
        {
            var filter = new FilteredQueryBuilder(QueryFactory.TermQuery("name.first", "shay"), FilterFactory.TermFilter("name.last","banon"));

			this.JsonEquals(filter.ToJsonObject(), MethodInfo.GetCurrentMethod(), "filtered-query");
        }
		
    }
}