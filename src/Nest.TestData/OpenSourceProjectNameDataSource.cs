using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class ElasticSearchProjectsDataSource : DatasourceBase<String>
    {
        private Random mRandom = new Random(1337);

        public override string Next(IGenerationSession session)
        {
            return Projects[mRandom.Next(0, Projects.Length)];
        }

        public static int Count()
        {
            return Projects.Count();
        }

        private static string[] Projects = new String[]{
            "ElasticSearch.pm",
            "pyes",
            "pyelasticsearch",
            "em-elasticsearch",
            "rubberband",
            "ruby_elasticsearch",
            "slingshot",
            "erlastic_search",
            "Elastica",
            "NEST",
            "ElasticSearch.NET",
            "Grails",
            "escargot",
            "catalyst",
            "django-elasticsearch",
            "elasticflume",
            "Terrastore Search",
            "Wonderdog"
        };
    }
}
