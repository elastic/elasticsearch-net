using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Purify;

namespace PurifyNet
{
    internal class PurifierMono : IPurifier
    {
        private static readonly Type uriType = typeof(Uri);
        private static readonly FieldInfo mono_sourceField;
        private static readonly FieldInfo mono_queryField;
        private static readonly FieldInfo mono_pathField;
        private static readonly FieldInfo mono_cachedToStringField;
        private static readonly FieldInfo mono_cachedAbsoluteUriField;

        static PurifierMono()
        {
            mono_sourceField = uriType.GetField("source", BindingFlags.NonPublic | BindingFlags.Instance);
            mono_queryField = uriType.GetField("query", BindingFlags.NonPublic | BindingFlags.Instance);
            mono_pathField = uriType.GetField("path", BindingFlags.NonPublic | BindingFlags.Instance);
            mono_cachedToStringField = uriType.GetField("cachedToString", BindingFlags.NonPublic | BindingFlags.Instance);
            mono_cachedAbsoluteUriField = uriType.GetField("cachedAbsoluteUri",
                BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public void Purify(Uri uri)
        {
            var source = (string)mono_sourceField.GetValue(uri);
            var uriInfo = new UriInfo(uri, source);
            mono_pathField.SetValue(uri, 
                uriInfo.Path.StartsWith("/") 
                    ? uriInfo.Path 
                    : "/" + uriInfo.Path
            );
            mono_queryField.SetValue(uri, uriInfo.Query);
            mono_cachedToStringField.SetValue(uri, uriInfo.Source);
            mono_cachedAbsoluteUriField.SetValue(uri, uriInfo.Source);
        }
    }
}
