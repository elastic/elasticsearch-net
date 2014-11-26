using System;
using System.Reflection;
using Elasticsearch.Net;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Script
{
    [TestFixture]
    public class GetScriptRequestTests : BaseJsonTests
    {
        [Test]
        [ExpectedException]
        public void NullIdThrowsException()
        {
            this._client.GetScript(s => s.Id(null));
        }

        [Test]
        [ExpectedException]
        public void NullLangThrowsException()
        {
            this._client.GetScript(s => s.Lang(null));
        }

        [Test]
        [ExpectedException(typeof(DispatchException))]
        public void ThrowExceptionWhenLangIsNotSpecified()
        {
            this._client.GetScript(s => s.Id("id"));
        }

        [Test]
        [ExpectedException(typeof(DispatchException))]
        public void ThrowExceptionWhenIdIsNotSpecified()
        {
            this._client.GetScript(s => s.Lang("lang"));
        }

        [Test]
        public void DefaultPath()
        {
            var result = this._client.GetScript(s => s.Lang("lang").Id("id"));
            Assert.NotNull(result, "GetScript result should not be null");
            var status = result.ConnectionStatus;
            StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.ResponseRaw.Utf8String());
            StringAssert.EndsWith("/_scripts/lang/id", status.RequestUrl);
            StringAssert.AreEqualIgnoringCase("GET", status.RequestMethod);
        }

        [Test]
        public void DefaultPath_ScriptLangEnum()
        {
            var result = this._client.GetScript(s => s.Lang(ScriptLang.Python).Id("id"));
            Assert.NotNull(result, "GetScript result should not be null");
            var status = result.ConnectionStatus;
            StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.ResponseRaw.Utf8String());
            StringAssert.EndsWith("/_scripts/python/id", status.RequestUrl);
            StringAssert.AreEqualIgnoringCase("GET", status.RequestMethod);
        }
    }
}