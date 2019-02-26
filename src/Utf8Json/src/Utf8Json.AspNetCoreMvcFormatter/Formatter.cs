using Microsoft.AspNetCore.Mvc.Formatters;
using System.Threading.Tasks;

namespace Utf8Json.AspNetCoreMvcFormatter
{
    public class JsonOutputFormatter : IOutputFormatter //, IApiResponseTypeMetadataProvider
    {
        const string ContentType = "application/json";
        static readonly string[] SupportedContentTypes = new[] { ContentType };

        readonly IJsonFormatterResolver resolver;

        public JsonOutputFormatter()
            : this(null)
        {

        }
        public JsonOutputFormatter(IJsonFormatterResolver resolver)
        {
            this.resolver = resolver ?? JsonSerializer.DefaultResolver;
        }

        //public IReadOnlyList<string> GetSupportedContentTypes(string contentType, Type objectType)
        //{
        //    return SupportedContentTypes;
        //}

        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return true;
        }

        public Task WriteAsync(OutputFormatterWriteContext context)
        {
            context.HttpContext.Response.ContentType = ContentType;

            // when 'object' use the concrete type(object.GetType())
            if (context.ObjectType == typeof(object))
            {
                return JsonSerializer.NonGeneric.SerializeAsync(context.HttpContext.Response.Body, context.Object, resolver);
            }
            else
            {
                return JsonSerializer.NonGeneric.SerializeAsync(context.ObjectType, context.HttpContext.Response.Body, context.Object, resolver);
            }
        }
    }

    public class JsonInputFormatter : IInputFormatter // , IApiRequestFormatMetadataProvider
    {
        const string ContentType = "application/json";
        static readonly string[] SupportedContentTypes = new[] { ContentType };

        readonly IJsonFormatterResolver resolver;

        public JsonInputFormatter()
            : this(null)
        {

        }

        public JsonInputFormatter(IJsonFormatterResolver resolver)
        {
            this.resolver = resolver ?? JsonSerializer.DefaultResolver;
        }

        //public IReadOnlyList<string> GetSupportedContentTypes(string contentType, Type objectType)
        //{
        //    return SupportedContentTypes;
        //}

        public bool CanRead(InputFormatterContext context)
        {
            return true;
        }

        public Task<InputFormatterResult> ReadAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            var result = JsonSerializer.NonGeneric.Deserialize(context.ModelType, request.Body, resolver);
            return InputFormatterResult.SuccessAsync(result);
        }
    }
}