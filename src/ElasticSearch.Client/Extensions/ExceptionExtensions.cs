using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
    public static class ExceptionExtensions
    {
        public static void ThrowIfNull(this object @object, string parameterName)
        {
            if (@object == null)
                throw new ArgumentNullException("Argument can't be null", parameterName);

        }
 
        public static void ThrowIfNullOrEmpty(this object @object, string parameterName)
        {
            @object.ThrowIfNull(parameterName);

            string @string = @object as string;

            if (@string == null || @string.Trim() == "")
                throw new ArgumentException("Argument can't be null or empty", parameterName);

        }
    }
}
