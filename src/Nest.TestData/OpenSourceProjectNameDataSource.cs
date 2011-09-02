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
	public class LongSource : DatasourceBase<long>
	{
		private Random mRandom = new Random(1337);
		public override long Next(IGenerationSession session)
		{
			long f = (long)((mRandom.NextDouble() * 2.0 - 1.0) * long.MaxValue); ;
			return f;
		}
	}
	public class DoubleSource : DatasourceBase<double>
	{
		private Random mRandom = new Random(1337);
		public override double Next(IGenerationSession session)
		{
			double f = mRandom.Next(0, 100);
			f = f + mRandom.NextDouble();
			return f;
		}

	}
	public class LOCSource : DatasourceBase<int>
	{
		private Random mRandom = new Random(1337);
		public override int Next(IGenerationSession session)
		{
			int f = mRandom.Next(10000, 20000);
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

    public class ElasticSearchProjectDescriptionSource : DatasourceBase<String>
    {
        private Random mRandom = new Random(1337);

        public override string Next(IGenerationSession session)
        {
            return Descriptions[mRandom.Next(0, Descriptions.Length)];
        }

        public static int Count()
        {
            return Descriptions.Count();
        }

        private static string[] Descriptions = new String[]{
               "Bacon ipsum dolor sit amet tail non prosciutto shankle turducken, officia bresaola aute filet mignon pork belly do ex tenderloin. Ut laboris quis spare ribs est prosciutto, non short ribs voluptate fugiat. Adipisicing ex ad jowl short ribs corned beef. Commodo cillum aute, sint dolore ribeye ham hock bresaola id jowl ut. Velit mollit tenderloin non, biltong officia et venison irure chuck filet mignon. Meatloaf veniam sausage prosciutto qui cow. Spare ribs non bresaola, in venison sint short loin deserunt magna laborum pork loin cillum.",
               "Nostrud frankfurter deserunt ullamco. Spare ribs tongue dolore laboris aute. Capicola rump in veniam cupidatat. Beef dolore pork belly andouille incididunt in. Bresaola qui sirloin, velit beef ribs cupidatat nulla consequat sed. Do sirloin nisi fatback ut swine shank, consectetur ea pariatur mollit. Flank quis shank, ball tip short loin kielbasa pork loin.",
               "Leberkäse turkey ea culpa incididunt, jowl pastrami voluptate ut pariatur. Tenderloin magna consectetur, aute qui nostrud turkey. Frankfurter tri-tip sed laborum, salami ea bresaola. Tempor drumstick pork belly, filet mignon reprehenderit strip steak boudin fatback in elit incididunt cupidatat et. Spare ribs turducken short ribs, ea exercitation hamburger turkey ut sint fugiat pastrami salami. Turkey eiusmod ea, pastrami leberkäse ham hock corned beef anim nulla enim strip steak filet mignon. Qui pork in ut meatball tail, in sint ex shankle jerky.",
               "Salami kielbasa eu meatball. Pancetta shankle ex bacon pariatur cow. Jerky kielbasa filet mignon non hamburger, deserunt pork loin pork chop. Pork et sirloin corned beef aliqua. Shoulder turkey ea, occaecat adipisicing capicola officia ut. Meatball et spare ribs sausage, laboris filet mignon quis laborum adipisicing excepteur prosciutto. Short loin sint short ribs tail, pig flank cupidatat velit salami shankle andouille.",
               "Pastrami laboris cillum, pork loin qui prosciutto ut minim venison bacon ex andouille. Sausage in nisi cillum ad, turkey tempor pork loin tail ut pariatur veniam. Sirloin meatloaf occaecat ut, pariatur fugiat labore sunt proident aliquip. Cow fatback venison corned beef. Excepteur ham hock elit meatball non fatback labore. T-bone magna ea in, sunt anim laboris andouille irure frankfurter sint bacon. Ham pork belly est salami consequat incididunt voluptate.",
               "Sint tail laboris, corned beef pork chop ullamco aliquip. Nulla rump beef ribs, esse turkey qui sint ex ground round strip steak pig deserunt cillum t-bone do. Do shank officia sirloin ground round voluptate. Ullamco shankle irure meatball sirloin beef, swine in pork belly. Tongue pariatur hamburger commodo. Officia fugiat venison swine sausage non. Beef ribs consectetur irure ribeye rump jowl tri-tip.",
               "Turkey mollit nostrud chuck, magna officia in. Non sunt ball tip meatball sausage, ground round commodo sed bacon. Ribeye strip steak turkey pork chop dolore cow beef. Excepteur jerky frankfurter aute, ribeye turkey officia. Strip steak in shoulder aute, corned beef exercitation beef ribs tail nostrud proident velit capicola quis. Ea pork loin nostrud dolor pancetta, boudin biltong consequat turkey. Pork chop capicola voluptate aliqua, pork loin deserunt sunt proident kielbasa."
        };
    }
}
