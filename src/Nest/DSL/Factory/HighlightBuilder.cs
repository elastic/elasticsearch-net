
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Nest.FactoryDsl
{
    public class HighlightBuilder : IJsonSerializable
    {
        private List<FieldInternal> _fields;

        private string _tagsSchema;

        private object[] _preTags;

        private object[] _postTags;

        private string _order;

        private string _encoder;

        /// <summary>
        /// Adds a field to be highlighted with default fragment size of 100 characters, and
        /// default number of fragments of 5 using the default encoder
        /// </summary>
        /// <param name="name">The field to highlight</param>
        /// <returns></returns>
        public HighlightBuilder Field(string name)
        {
            if (_fields == null)
            {
                _fields = new List<FieldInternal>();
            }

            _fields.Add(new FieldInternal(name));
            return this;
        }

        /// <summary>
        /// Adds a field to be highlighted with a provided fragment size (in characters), and
        /// default number of fragments of 5.
        /// </summary>
        /// <param name="name">The field to highlight</param>
        /// <param name="fragmentSize">The size of a fragment in characters</param>
        /// <returns></returns>
        public HighlightBuilder Field(string name, int fragmentSize)
        {
            if (_fields == null)
            {
                _fields = new List<FieldInternal>();
            }

            _fields.Add(new FieldInternal(name).FragmentSize(fragmentSize));
            return this;
        }

        /// <summary>
        /// Adds a field to be highlighted with a provided fragment size (in characters), and
        /// a provided (maximum) number of fragments.
        /// </summary>
        /// <param name="name">The field to highlight</param>
        /// <param name="fragmentSize">The size of a fragment in characters</param>
        /// <param name="numberOfFragments">The (maximum) number of fragments</param>
        /// <returns></returns>
        public HighlightBuilder Field(string name, int fragmentSize, int numberOfFragments)
        {
            if (_fields == null)
            {
                _fields = new List<FieldInternal>();
            }
            _fields.Add(new FieldInternal(name).FragmentSize(fragmentSize).NumOfFragments(numberOfFragments));
            return this;
        }

        /// <summary>
        /// Adds a field to be highlighted with a provided fragment size (in characters), and
        /// a provided (maximum) number of fragments.
        /// </summary>
        /// <param name="name">The field to highlight</param>
        /// <param name="fragmentSize">The size of a fragment in characters</param>
        /// <param name="numberOfFragments">The (maximum) number of fragments</param>
        /// <param name="fragmentOffset">The offset from the start of the fragment to the start of the highlight</param>
        /// <returns></returns>
        public HighlightBuilder Field(string name, int fragmentSize, int numberOfFragments, int fragmentOffset)
        {
            if (_fields == null)
            {
                _fields = new List<FieldInternal>();
            }

            _fields.Add(new FieldInternal(name).FragmentSize(fragmentSize).NumOfFragments(numberOfFragments).FragmentOffset(fragmentOffset));
            return this;
        }

        /// <summary>
        /// Set a tag scheme that encapsulates a built in pre and post tags. The allows schemes
        /// are <tt>styled</tt> and <tt>default</tt>.
        /// </summary>
        /// <param name="schemaName">The tag scheme name</param>
        /// <returns></returns>
        public HighlightBuilder TagsSchema(string schemaName)
        {
            _tagsSchema = schemaName;
            return this;
        }

        /// <summary>
        /// Set encoder for the highlighting
        /// are <tt>styled</tt> and <tt>default</tt>.
        /// </summary>
        /// <param name="encoder">Encoder name</param>
        /// <returns></returns>
        public HighlightBuilder Encoder(string encoder)
        {
            _encoder = encoder;
            return this;
        }

        /// <summary>
        /// Explicitly set the pre tags that will be used for highlighting.
        /// </summary>
        /// <param name="preTags"></param>
        /// <returns></returns>
        public HighlightBuilder PreTags(params string[] preTags)
        {
            _preTags = preTags;
            return this;
        }

        /// <summary>
        /// Explicitly set the post tags that will be used for highlighting.
        /// </summary>
        /// <param name="postTags"></param>
        /// <returns></returns>
        public HighlightBuilder PostTags(params string[] postTags)
        {
            _postTags = postTags;
            return this;
        }

        /// <summary>
        /// The order of fragments per field. By default, ordered by the order in the
        /// highlighted text. Can be <tt>score</tt>, which then it will be ordered
        /// by score of the fragments.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public HighlightBuilder Order(string order)
        {
            _order = order;
            return this;
        }

        #region IJsonSerializable Members

        public object ToJsonObject()
        {
            var content = new JProperty("highlight", new JObject());

            if(_tagsSchema != null)
            {
                content.Value["tags_schema"] = _tagsSchema;
            }

            if (_preTags != null)
            {
                content.Value["pre_tags"] = new JArray(_preTags);
            }

            if (_postTags != null)
            {
                content.Value["post_tags"] = new JArray(_postTags);
            }

            if (_order != null)
            {
                content.Value["order"] = _order;
            }

            if (_encoder != null)
            {
                content.Value["encoder"] = _encoder;
            }

            if(_fields != null && _fields.Count > 0)
            {
                content.Value["fields"] = new JObject();

                foreach (var field in _fields)
                {
                    content.Value["fields"][field.Name()] = new JObject();

                    if(field.FragmentSize().HasValue)
                    {
                        var fragmentSize = field.FragmentSize();
                        if (fragmentSize != null)
                        {
                            content.Value["fields"][field.Name()]["fragment_size"] = fragmentSize.Value;
                        }
                    }

                    if(field.NumOfFragments().HasValue)
                    {
                        var numOfFragments = field.NumOfFragments();
                        if (numOfFragments != null)
                        {
                            content.Value["fields"][field.Name()]["number_of_fragments"] = numOfFragments.Value;
                        }
                    }

                    if (field.FragmentOffset().HasValue)
                    {
                        var fragmentOffset = field.FragmentOffset();
                        if (fragmentOffset != null)
                        {
                            content.Value["fields"][field.Name()]["fragment_offset"] = fragmentOffset.Value;
                        }
                    }
                }
            }

            return content;
        }

        #endregion

        private class FieldInternal
        {
            private string _name;
            private int? _fragmentSize;
            private int? _fragmentOffset;
            private int? _numOfFragments;

            public FieldInternal(string name)
            {
                _name = name;
            }

            public string Name()
            {
                return _name;
            }

            public int? FragmentSize()
            {
                return _fragmentSize;
            }

            public FieldInternal FragmentSize(int fragmentSize)
            {
                _fragmentSize = fragmentSize;
                return this;
            }

            public int? FragmentOffset()
            {
                return _fragmentOffset;
            }

            public FieldInternal FragmentOffset(int fragmentOffset)
            {
                _fragmentOffset = fragmentOffset;
                return this;
            }

            public int? NumOfFragments()
            {
                return _numOfFragments;
            }

            public FieldInternal NumOfFragments(int numOfFragments)
            {
                _numOfFragments = numOfFragments;
                return this;
            }
        }
    }
}