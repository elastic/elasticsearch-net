using System.IO;
using NUnit.Framework;
using Nest.FactoryDsl.Tests;
using Nest.FactoryDsl;
using Nest.FactoryDsl.Query;

namespace Nest.FactoryDsl.Tests.Filter
{
    [TestFixture]
    public class FilterTests
    {
        private SearchBuilder _sb;

        [SetUp]
        public void Setup()
        {
            _sb = new SearchBuilder();
        }

        [Test]
        public void AndFilterTestOutput()
        {
            var andFilter = FilterFactory.AndFilter(FilterFactory.TermFilter("name.first", "shay1"), FilterFactory.TermFilter("name.first", "shay4"));
            var filter = new FilteredQueryBuilder(QueryFactory.TermQuery("name.first", "shay"), andFilter);

            Assert.AreEqual(File.ReadAllText("DSL/Filter/and-filter.json").Strip(), filter.ToJsonObject().Strip());
        }

        [Test]
        public void AndFilterNamedTestOutput()
        {
            var andFilter = FilterFactory.AndFilter(FilterFactory.TermFilter("name.first", "shay1"), FilterFactory.TermFilter("name.first", "shay4"));
            andFilter.FilterName("test");
            var filter = new FilteredQueryBuilder(QueryFactory.TermQuery("name.first", "shay"), andFilter);

            Assert.AreEqual(File.ReadAllText("DSL/Filter/and-filter-named.json").Strip(), filter.ToJsonObject().Strip());
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

            Assert.AreEqual(File.ReadAllText("DSL/Filter/bool-filter.json").Strip(), filter.ToJsonObject().Strip());
        }

        [Test]
        public void TermFilterTestOutput()
        {
            var filter = new FilteredQueryBuilder(QueryFactory.TermQuery("name.first", "shay"), FilterFactory.TermFilter("name.last","banon"));

            Assert.AreEqual(File.ReadAllText("DSL/Filter/filtered-query.json").Strip(), filter.ToJsonObject().Strip());    
        }
    }
}