using System;
using Elasticsearch.Net;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Script
{
    [TestFixture]
    public class DeleteScriptRequestTests : BaseJsonTests
    {
        [Test]
        [ExpectedException]
        public void NullIdThrowsException()
        {
            this._client.DeleteScript(s => s.Id(null));
        }

        [Test]
        [ExpectedException]
        public void NullLangThrowsException()
        {
            this._client.DeleteScript(s => s.Lang(null));
        }

        [Test]
        [ExpectedException(typeof(DispatchException))]
        public void ThrowExceptionWhenLangIsNotSpecified()
        {
            this._client.DeleteScript(s => s.Id("id"));
        }

        [Test]
        [ExpectedException(typeof(DispatchException))]
        public void ThrowExceptionWhenIdIsNotSpecified()
        {
            this._client.DeleteScript(s => s.Lang("lang"));
        }

        [Test]
        public void DefaultPath()
        {
            var result = this._client.DeleteScript(s => s.Lang("lang").Id("id"));
            Assert.NotNull(result, "DeleteScript result should not be null");
            var status = result.ConnectionStatus;
            StringAssert.Contains("USING NEST IN MEMORY CONNECTION", result.ConnectionStatus.ResponseRaw.Utf8String());
            StringAssert.EndsWith("/_scripts/lang/id", status.RequestUrl);
            StringAssert.AreEqualIgnoringCase("DELETE", status.RequestMethod);
        }
    }
}