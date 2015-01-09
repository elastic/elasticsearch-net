using System.Reflection;
using Elasticsearch.Net;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Script
{
    [TestFixture]
    public class PutScriptRequestTests : BaseJsonTests
    {
        [Test]
        [ExpectedException]
        public void NullIdThrowsException()
        {
            this._client.PutScript(s => s.Id(null));
        }

        [Test]
        [ExpectedException]
        public void NullLangThrowsException()
        {
            this._client.PutScript(s => s.Lang(null));
        }

        [Test]
        [ExpectedException(typeof(DispatchException))]
        public void ThrowExceptionWhenLangIsNotSpecified()
        {
            this._client.PutScript(s => s.Id("id").Script("script"));
        }

        [Test]
        [ExpectedException(typeof(DispatchException))]
        public void ThrowExceptionWhenIdIsNotSpecified()
        {
            this._client.PutScript(s => s.Lang("lang").Script("script"));
        }

        [Test]
        public void PutScript()
        {
            var result = this._client.PutScript(s => s.Lang("lang").Id("id").Script("script"));
            this.JsonEquals(result.ConnectionStatus.Request, MethodInfo.GetCurrentMethod());
        }

        [Test]
        public void DefaultPath()
        {
            var result = this._client.PutScript(s => s.Lang("lang").Id("id").Script("script"));
            Assert.NotNull(result, "PutScript result should not be null");
            var status = result.ConnectionStatus;
            StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.ResponseRaw.Utf8String());
            StringAssert.EndsWith("/_scripts/lang/id", status.RequestUrl);
            StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
        }

        [Test]
        public void DefaultPath_ScriptLangEnum()
        {
            var result = this._client.PutScript(s => s.Lang(ScriptLang.Groovy).Id("id").Script("script"));
            Assert.NotNull(result, "PutScript result should not be null");
            var status = result.ConnectionStatus;
            StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.ResponseRaw.Utf8String());
            StringAssert.EndsWith("/_scripts/groovy/id", status.RequestUrl);
            StringAssert.AreEqualIgnoringCase("POST", status.RequestMethod);
        }
    }
}
