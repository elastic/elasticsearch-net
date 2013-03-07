using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nest.Dsl.Factory
{
    public class BoolFilterBuilder : IFilterBuilder
    {
        private const string NAME = NameRegistry.BoolFilterBuilder;
        private readonly List<Clause> _clauses = new List<Clause>();

        private bool _cache;
        private string _cacheKey;
        private string _filterName;

        public BoolFilterBuilder Must(IFilterBuilder filterBuilder)
        {
            _clauses.Add(new Clause(filterBuilder, Occur.MUST));
            return this;
        }

        public BoolFilterBuilder MustNot(IFilterBuilder filterBuilder)
        {
            _clauses.Add(new Clause(filterBuilder, Occur.MUST_NOT));
            return this;
        }

        public BoolFilterBuilder Should(IFilterBuilder filterBuilder)
        {
            _clauses.Add(new Clause(filterBuilder, Occur.SHOULD));
            return this;
        }

        public BoolFilterBuilder FilterName(string filterName)
        {
            _filterName = filterName;
            return this;
        }

        public BoolFilterBuilder Cache(bool cache)
        {
            _cache = cache;
            return this;
        }

        public BoolFilterBuilder CacheKey(string cacheKey)
        {
            _cacheKey = cacheKey;
            return this;
        }

        #region IFilterBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));
           
            var must = _clauses.Where(c => c.Occur == Occur.MUST).ToList();
            var mustNot = _clauses.Where(c => c.Occur == Occur.MUST_NOT).ToList();
            var should = _clauses.Where(c => c.Occur == Occur.SHOULD).ToList();

            if(must.Count > 0)
            {
                if(must.Count == 1)
                {
                    content[NAME]["must"] = must[0].Filter.ToJsonObject() as JObject;  
                }
                else
                {
                    content[NAME]["must"] = new JArray(must.Select(t => t.Filter.ToJsonObject()));    
                }
            }

            if(mustNot.Count > 0)
            {
                if(mustNot.Count == 1)
                {
                    content[NAME]["must_not"] = mustNot[0].Filter.ToJsonObject() as JObject;     
                }
                else
                {
                    content[NAME]["must_not"] = new JArray(mustNot.Select(t => t.Filter.ToJsonObject()));    
                }
            }

            if(should.Count > 0)
            {
                if(should.Count == 1)
                {
                    content[NAME]["should"] = should[0].Filter.ToJsonObject() as JObject;    
                }
                else
                {
                    content[NAME]["should"] = new JArray(should.Select(t => t.Filter.ToJsonObject()));    
                }
            }

            if (_filterName != null)
            {
                content[NAME]["_name"] = _filterName;
            }

            if (_cache)
            {
               content[NAME]["_cache"] = _cache;
            }

            if (_cacheKey != null)
            {
                content[NAME]["_cache_key"] = _cacheKey;
            }

            return content;
        }

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }

        #endregion

        private sealed class Clause
        {
            public Clause(IFilterBuilder filterBuilder, Occur occur)
            {
                Filter = filterBuilder;
                Occur = occur;
            }

            public Occur Occur { get; private set; }
            public IFilterBuilder Filter { get; private set; }
        }
    }
}