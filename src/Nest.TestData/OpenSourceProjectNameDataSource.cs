using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;
using Nest.TestData.Domain;

namespace AutoPoco.DataSources
{
	public class FloatSource :  DatasourceBase<float>
	{
		private Random mRandom = new Random(1337);
		public override float Next(IGenerationSession session)
		{
			float f = mRandom.Next(0, 100);
			f = f + (float)mRandom.NextDouble();
			return f;
		}

	}

	public class GeoLocationSource : DatasourceBase<GeoLocation>
	{
		private Random mRandom = new Random(1337);
		public override GeoLocation Next(IGenerationSession session)
		{
			return session.Single<GeoLocation>().Get();
		}

	}

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
