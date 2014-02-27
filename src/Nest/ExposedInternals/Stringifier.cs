using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using Nest.Resolvers;

namespace Elasticsearch.Net
{
	public class NestStringifier : IStringifier
	{
		private readonly IConnectionSettingsValues _settings;
		public ElasticInferrer Infer { get; private set; }

		public NestStringifier(IConnectionSettingsValues settings)
		{
			_settings = settings;
			this.Infer = new ElasticInferrer(settings);
		}


		public string Stringify(object o)
		{
			var s = o as string;
			if (s != null)
				return s;
			var ss = o as string[];
			if (ss != null)
				return string.Join(",", ss);

			var pns = o as IEnumerable<object>;
			if (pns != null)
				return string.Join(",", pns.Select(
					oo =>
					{
						if (oo is PropertyNameMarker)
							return this.Infer.PropertyName(oo as PropertyNameMarker);
						if (oo is PropertyPathMarker)
							return this.Infer.PropertyPath(oo as PropertyPathMarker);
						return oo.ToString();
					})
				);

			var e = o as Enum;
			if (e != null) return KnownEnums.Resolve(e);
			if (o is bool)
				return ((bool) o) ? "true" : "false";
			
			var pn = o as PropertyNameMarker;
			if (pn != null)
				return this.Infer.PropertyName(pn);

			var pp = o as PropertyPathMarker;
			if (pp != null)
				return this.Infer.PropertyPath(pp);

			return o.ToString();
		}
	
	}
}