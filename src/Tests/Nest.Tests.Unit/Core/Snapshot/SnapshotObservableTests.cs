using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading;
using Elasticsearch.Net;
using Moq;
using NUnit.Framework;

namespace Nest.Tests.Unit.Core.Snapshot
{
    [TestFixture]
    public class SnapshotObservableTests : BaseJsonTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RequestIsNull_Exception()
        {
            var elastickClientMock = new Mock<IElasticClient>();

            var sut = new SnapshotObservable(elastickClientMock.Object, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ClientIsNull_Exception()
        {
            var sut = new SnapshotObservable(null, new SnapshotRequest("repository", "snapshot"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ObserverIsNull_Exception()
        {
            var elastickClientMock = new Mock<IElasticClient>();

            var sut = new SnapshotObservable(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"));

            using (sut.Subscribe(null))
            {
                
            }
        }

        [Test]
        public void ShouldCallSnapshotMethodOnceWithParameters()
        {
            var elastickClientMock = new Mock<IElasticClient>();

            var sut = new SnapshotObservable(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"));

            sut.Subscribe(new Observer<ISnapshotStatusResponse>());

            elastickClientMock.Verify(x => x
                .Snapshot(
                    It.Is<ISnapshotRequest>(
                        request =>
                            request.Repository == "repository" && request.Snapshot == "snapshot" &&
                            request.RequestParameters.GetQueryStringValue<bool>("wait_for_completition") == false)),
                Times.Once);
        }

        [Test]
        [ExpectedException(typeof(SnapshotException))]
        public void SnapshotResponseInvalid_Exception()
        {
            var elastickClientMock = new Mock<IElasticClient>();
            var snapshotResponseMock = new Mock<ISnapshotStatusResponse>();

            snapshotResponseMock.Setup(x => x.IsValid).Returns(false);

            elastickClientMock.Setup(x => x.SnapshotStatus(It.IsAny<Func<SnapshotStatusDescriptor, SnapshotStatusDescriptor>>()))
                .Returns(() => snapshotResponseMock.Object);

            var sut = new SnapshotStatusHumbleObject(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"));

            sut.Next += (sender, args) => Assert.Fail("");
            sut.Completed += (sender, args) => Assert.Fail("Can't complete");
            sut.Error += (sender, args) =>
            {
                throw args.Exception;
            };

            sut.Check();
        }

        [Test]
        [ExpectedException(typeof(SnapshotException))]
        public void SnapshotStatusResponseInvalid_Exception()
        {
            var elastickClientMock = new Mock<IElasticClient>();
            var snapshotStatusResponseMock = new Mock<ISnapshotStatusResponse>();

            var waitHandle = new AutoResetEvent(false);

            snapshotStatusResponseMock.Setup(x => x.IsValid).Returns(false);

            elastickClientMock.Setup(x => x.SnapshotStatus(It.IsAny<Func<SnapshotStatusDescriptor, SnapshotStatusDescriptor>>()))
                .Returns(() => snapshotStatusResponseMock.Object)
                .Callback(() => waitHandle.Set());

            var sut = new SnapshotStatusHumbleObject(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"));

            sut.Next += (sender, args) => Assert.Fail("");
            sut.Completed += (sender, args) => Assert.Fail("Can't complete");
            sut.Error += (sender, args) => { throw args.Exception; };

            sut.Check();
        }

        [Test]
        public void OnNext()
        {
            var elastickClientMock = ElastickClientMock(1, 2);

            var sut = new SnapshotStatusHumbleObject(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"));

            sut.Next += (sender, args) => Assert.AreEqual(1, args.SnapshotStatusResponse.Snapshots.Count());
            sut.Completed += (sender, args) => Assert.Fail("Can't complete");
            sut.Error += (sender, args) => Assert.Fail(args.Exception.Message);

            sut.Check();
        }

        [Test]
        public void OnComplete()
        {
            var elastickClientMock = ElastickClientMock(2,2);

            var sut = new SnapshotStatusHumbleObject(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"));

            sut.Next += (sender, args) => Assert.Fail();
            sut.Completed += (sender, args) => Assert.NotNull(args.SnapshotStatusResponse);
            sut.Error += (sender, args) => Assert.Fail(args.Exception.Message);

            sut.Check();
        }

        private static Mock<IElasticClient> ElastickClientMock(long done, long total)
        {
            var elastickClientMock = new Mock<IElasticClient>();
            var snapshotResponseMock = new Mock<ISnapshotResponse>();
            var snapshotStatusResponseMock = new Mock<ISnapshotStatusResponse>();

            snapshotResponseMock.Setup(x => x.IsValid).Returns(true);

            elastickClientMock.Setup(x => x.Snapshot(It.IsAny<ISnapshotRequest>()))
                .Returns(() => snapshotResponseMock.Object);

            snapshotStatusResponseMock.Setup(x => x.IsValid).Returns(true);
            snapshotStatusResponseMock.Setup(x => x.Snapshots)
                .Returns(() => new List<SnapshotStatus>
                {
                    new SnapshotStatusWrapper(new SnapshotShardsStatsWrapper(done, total))
                });

            elastickClientMock.Setup(x => x.SnapshotStatus(It.IsAny<Func<SnapshotStatusDescriptor, SnapshotStatusDescriptor>>()))
                .Returns(() => snapshotStatusResponseMock.Object);
            return elastickClientMock;
        }

        class SnapshotShardsStatsWrapper : SnapshotShardsStats
        {
            public SnapshotShardsStatsWrapper(long done, long total)
            {
                PropertyInfo donePropertyInfo = this.GetType().GetProperty("Done");
                donePropertyInfo.SetValue(this, done);
                PropertyInfo totalPropertyInfo = this.GetType().GetProperty("Total");
                totalPropertyInfo.SetValue(this, total);
            }
        }

        class SnapshotStatusWrapper : SnapshotStatus
        {
            public SnapshotStatusWrapper(SnapshotShardsStatsWrapper shardsStatsWrapper)
            {
                PropertyInfo propertyInfo = this.GetType().GetProperty("ShardsStats");
                propertyInfo.SetValue(this, shardsStatsWrapper);
            }
        }
    }
}