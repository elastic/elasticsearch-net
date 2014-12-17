using System;
using System.Collections.Generic;
using System.Linq;
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
        public void ShouldCallSnapshotMethodOnceWithParameters()
        {
            var elastickClientMock = new Mock<IElasticClient>();

            var sut = new SnapshotObservable(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"));

            using (sut.Subscribe(new Observer<ISnapshotStatusResponse>()))
            {
                elastickClientMock.Verify(x => x
                    .Snapshot(
                        It.Is<ISnapshotRequest>(
                            request =>
                                request.Repository == "repository" && request.Snapshot == "snapshot" &&
                                request.RequestParameters.GetQueryStringValue<bool>("wait_for_completition") == false)), Times.Once);
            }
        }

        [Test]
        public void ShouldCallSnapshot_ThenSnapshotStatus()
        {
            var elastickClientMock = new Mock<IElasticClient>();
            var snapshotStatusMock = new Mock<ISnapshotStatusResponse>();

            var waitHandle = new AutoResetEvent(false);

            snapshotStatusMock.Setup(x => x.IsValid).Returns(true);

            elastickClientMock.Setup(x => x.SnapshotStatus(It.IsAny < Func<SnapshotStatusDescriptor, SnapshotStatusDescriptor>>()))
                .Returns(() => snapshotStatusMock.Object)
                .Callback(() => waitHandle.Set());

            var sut = new SnapshotObservable(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"), TimeSpan.FromMilliseconds(10));

            using (sut.Subscribe(new Observer<ISnapshotStatusResponse>()))
            {
                waitHandle.WaitOne();
                elastickClientMock.Verify(x => x.Snapshot(It.IsAny<ISnapshotRequest>()), Times.Once);
                elastickClientMock.Verify(x => x.SnapshotStatus(It.IsAny<Func<SnapshotStatusDescriptor, SnapshotStatusDescriptor>>()), Times.AtLeastOnce);
            }
        }

        [Test]
        public void SnapshotStatusResponseInvalid_ThrowsException()
        {
            var elastickClientMock = new Mock<IElasticClient>();
            var snapshotStatusResponseMock = new Mock<ISnapshotStatusResponse>();

            var waitHandle = new AutoResetEvent(false);

            snapshotStatusResponseMock.Setup(x => x.IsValid).Returns(false);

            elastickClientMock.Setup(x => x.SnapshotStatus(It.IsAny<Func<SnapshotStatusDescriptor, SnapshotStatusDescriptor>>()))
                .Returns(() => snapshotStatusResponseMock.Object)
                .Callback(() => waitHandle.Set());

            var sut = new SnapshotObservable(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"), TimeSpan.FromMilliseconds(10));

            using (sut.Subscribe(new Observer<ISnapshotStatusResponse>(null,Assert.IsInstanceOf<SnapshotException> ,null)))
            {
                waitHandle.WaitOne();
            }
        }

        //[Test]
        //TODO: finish this case
        public void NotifyOnProgress()
        {
            var elastickClientMock = new Mock<IElasticClient>();
            var snapshotStatusResponseMock = new Mock<ISnapshotStatusResponse>();
            var snapshotStatusMock = new Mock<SnapshotStatus>();
            var snapshotIndexStatsMock = new Mock<SnapshotIndexStats>();

            var waitHandle = new AutoResetEvent(false);

            snapshotStatusMock.Setup(x => x.Snapshot).Returns("snapshot");
            snapshotStatusMock.Setup(x => x.Repository).Returns("repository");
            snapshotStatusMock.Setup(x => x.Indices).Returns(new Dictionary<string, SnapshotIndexStats>
            {
                {"index", snapshotIndexStatsMock.Object}
            });

            snapshotStatusResponseMock.Setup(x => x.IsValid).Returns(true);
            snapshotStatusResponseMock.Setup(x => x.Snapshots)
                .Returns(() => new List<SnapshotStatus> { snapshotStatusMock.Object });

            elastickClientMock.Setup(x => x.SnapshotStatus(It.IsAny<Func<SnapshotStatusDescriptor, SnapshotStatusDescriptor>>()))
                .Returns(() => snapshotStatusResponseMock.Object);

            var sut = new SnapshotObservable(elastickClientMock.Object, new SnapshotRequest("repository", "snapshot"), TimeSpan.FromMilliseconds(10));

            using (sut.Subscribe(new Observer<ISnapshotStatusResponse>(null, null, () => waitHandle.Set())))
            {
                waitHandle.WaitOne();
            }
        }
    }
}