using Nest.Resolvers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Nest
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CompletionSuggestDescriptor<T> : BaseSuggestDescriptor<T> where T : class
    {
        public CompletionSuggestDescriptor<T> Text(string text)
        {
            this._Text = text;
            return this;
        }

        public CompletionSuggestDescriptor<T> OnField(string field)
        {
            this._Field = field;
            return this;
        }

        public CompletionSuggestDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
        {
            var fieldName = new PropertyNameResolver().Resolve(objectPath);
            return this.OnField(fieldName);
        }
    }
}
