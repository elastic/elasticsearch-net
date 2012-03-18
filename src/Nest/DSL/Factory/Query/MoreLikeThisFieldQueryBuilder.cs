using System;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl.Query
{
    /// <summary>
    /// A more like this query that runs against a specific field.
    /// </summary>
    public class MoreLikeThisFieldQueryBuilder : IQueryBuilder
    {
        private const string NAME = NameRegistry.MoreLikeThisFieldQueryBuilder;
        private readonly string _name;
        private string _likeText;
        private float? _percentTermsToMatch;
        private int? _minTermFreq;
        private int? _maxQueryTerms;
        private string[] _stopWords;
        private int? _minDocFreq;
        private int? _maxDocFreq;
        private int? _minWordLen;
        private int? _maxWordLen;
        private float? _boostTerms;
        private float? _boost;
        private string _analyzer;

        /// <summary>
        /// A more like this query that runs against a specific field.
        /// </summary>
        /// <param name="name">The field name to run the query against</param>
        public MoreLikeThisFieldQueryBuilder(string name)
        {
            _name = name;
        }

        /// <summary>
        /// The text to use in order to find documents that are "like" this.
        /// </summary>
        /// <param name="likeText"></param>
        /// <returns></returns>
        public MoreLikeThisFieldQueryBuilder LikeText(string likeText)
        {
            _likeText = likeText;
            return this;
        }

        /// <summary>
        /// The percentage of terms to match. Defaults to <tt>0.3</tt>.
        /// </summary>
        /// <param name="percentTermsToMatch"></param>
        /// <returns></returns>
        public MoreLikeThisFieldQueryBuilder PercentTermsToMatch(float percentTermsToMatch)
        {
            _percentTermsToMatch = percentTermsToMatch;
            return this;
        }

        /// <summary>
        /// The frequency below which terms will be ignored in the source doc. The default
        /// frequency is <tt>2</tt>.
        /// </summary>
        /// <param name="minTermFreqy"></param>
        /// <returns></returns>
        public MoreLikeThisFieldQueryBuilder MinTermFreq(int minTermFreqy)
        {
            _minTermFreq = minTermFreqy;
            return this;
        }

        /// <summary>
        /// Sets the maximum number of query terms that will be included in any generated query.
        /// Defaults to <tt>25</tt>.
        /// </summary>
        /// <param name="maxQueryTerms"></param>
        /// <returns></returns>
        public MoreLikeThisFieldQueryBuilder MaxQueryTerms(int maxQueryTerms)
        {
            _maxQueryTerms = maxQueryTerms;
            return this;
        }

        /// <summary>
        /// Set the set of stopwords.
        ///
        /// <p>Any word in this set is considered "uninteresting" and ignored. Even if your Analyzer allows stopwords, you
        /// might want to tell the MoreLikeThis code to ignore them, as for the purposes of document similarity it seems
        /// reasonable to assume that "a stop word is never interesting".</p>
        /// </summary>
        /// <param name="stopWords"></param>
        /// <returns></returns>
        public MoreLikeThisFieldQueryBuilder StopWords(params string[] stopWords) 
        {
            _stopWords = stopWords;
            return this;
        }

        /// <summary>
        /// Sets the frequency at which words will be ignored which do not occur in at least this
        /// many docs. Defaults to <tt>5</tt>.
        /// </summary>
        /// <param name="minDocFreq"></param>
        /// <returns></returns>
        public MoreLikeThisFieldQueryBuilder MinDocFreq(int minDocFreq)
        {
            _minDocFreq = minDocFreq;
            return this;
        }

        /// <summary>
        /// Set the maximum frequency in which words may still appear. Words that appear
        /// in more than this many docs will be ignored. Defaults to unbounded.
        /// </summary>
        /// <param name="maxDocFreq"></param>
        /// <returns></returns>
        public MoreLikeThisFieldQueryBuilder MaxDocFreq(int maxDocFreq)
        {
            _maxDocFreq = maxDocFreq;
            return this;
        }

        /// <summary>
        /// Sets the minimum word length below which words will be ignored. Defaults
        /// to <tt>0</tt>.
        /// </summary>
        /// <param name="minWordLen"></param>
        /// <returns></returns>
        public MoreLikeThisFieldQueryBuilder MinWordLen(int minWordLen)
        {
            _minWordLen = minWordLen;
            return this;
        }

        /// <summary>
        /// Sets the maximum word length above which words will be ignored. Defaults to
        /// unbounded (<tt>0</tt>).
        /// </summary>
        /// <param name="maxWordLen"></param>
        /// <returns></returns>
        public MoreLikeThisFieldQueryBuilder MaxWordLen(int maxWordLen)
        {
            _maxWordLen = maxWordLen;
            return this;
        }

        /// <summary>
        /// Sets the boost factor to use when boosting terms. Defaults to <tt>1</tt>.
        /// </summary>
        /// <param name="boostTerms"></param>
        /// <returns></returns>
        public MoreLikeThisFieldQueryBuilder BoostTerms(float boostTerms)
        {
            _boostTerms = boostTerms;
            return this;
        }

        /// <summary>
        /// The analyzer that will be used to analyze the text. Defaults to the analyzer associated with the fied.
        /// </summary>
        /// <param name="analyzer"></param>
        /// <returns></returns>
        public MoreLikeThisFieldQueryBuilder Analyzer(String analyzer)
        {
            _analyzer = analyzer;
            return this;
        }

        public MoreLikeThisFieldQueryBuilder Boost(float boost)
        {
            _boost = boost;
            return this;
        }

        #region IQueryBuilder Members

        public object ToJsonObject()
        {
            var content = new JObject(new JProperty(NAME, new JObject()));
            content[NAME][_name] = new JObject();

            if(string.IsNullOrEmpty(_likeText))
            {
                throw new QueryBuilderException("moreLikeThisField requires 'like_text' to be provided");
            }

            content[NAME][_name]["like_text"] = _likeText;

            if(_percentTermsToMatch != null)
            {
                content[NAME][_name]["percent_terms_to_match"] = _percentTermsToMatch;
            }

            if(_minTermFreq != null)
            {
                content[NAME][_name]["min_term_freq"] =_minTermFreq;    
            }

            if(_maxQueryTerms != null)
            {
                content[NAME][_name]["max_query_terms"] = _maxQueryTerms; 
            }

            if(_stopWords != null && _stopWords.Length > 0)
            {
                content[NAME][_name]["stop_words"] = new JArray(_stopWords);
            }

            if (_minDocFreq != null)
            {
                content[NAME][_name]["min_doc_freq"] = _minDocFreq;
            }

            if (_maxDocFreq != null)
            {
                content[NAME][_name]["max_doc_freq"] = _maxDocFreq;
            }

            if (_minWordLen != null)
            {
                content[NAME][_name]["min_word_len"] = _minWordLen;
            }

            if (_maxWordLen != null)
            {
                content[NAME][_name]["max_word_len"] = _maxWordLen;
            }

            if (_boostTerms != null)
            {
                content[NAME][_name]["boost_terms"] = _boostTerms;
            }

            if (_boost != null)
            {
                content[NAME][_name]["boost"] = _boost;
            }

            if (_analyzer != null)
            {
                content[NAME][_name]["analyzer"] = _analyzer;
            }

            return content;
        }

        public override string ToString()
        {
            return ToJsonObject().ToString();
        }

        #endregion
    }
}
