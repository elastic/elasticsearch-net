using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
    /// <summary>
    /// Represents a typed container for object paths "field.nested.property";
    /// </summary>
    [JsonConverter(typeof(FieldMappingOuterClassConverter))]
    public class FieldMappingOuterClass : IDictionary<string,string>
    {
        public Dictionary<string,string> field_mapping { get; set; }

        //IFieldMappingInner IFieldMappingOuter.field_mapping
        //{
        //    get;
        //    set;
        //}

        public void Add(string key, string value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public ICollection<string> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out string value)
        {
            throw new NotImplementedException();
        }

        public ICollection<string> Values
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string key]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(KeyValuePair<string, string> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class FieldMappingInnerClass
    {
        public List<string> title { get; set; }
        public List<string> content { get; set; }

        //IList<string> IFieldMappingInner.title
        //{
        //    get;
        //    set;
        //}

        //IList<string> IFieldMappingInner.content
        //{
        //    get;
        //    set;
        //}
    }

}