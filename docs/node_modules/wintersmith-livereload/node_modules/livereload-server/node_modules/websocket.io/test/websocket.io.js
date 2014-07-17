
/**
 * Test dependencies.
 */

var ws = require('../lib/websocket.io')
  , http = require('http')

/**
 * Tests.
 */

describe('websocket.io', function () {

  it('must expose version number', function () {
    ws.version.should.match(/[0-9]+\.[0-9]+\.[0-9]+/);
  });

  it('must expose public constructors', function () {
    ws.Socket.should.be.a('function');
    ws.Server.should.be.a('function');
    ws.protocols.drafts.should.be.a('function');
    ws.protocols['7'].should.be.a('function');
    ws.protocols['8'].should.be.a('function');
    ws.protocols['13'].should.be.a('function');
  });

  it('must connect', function (done) {
    listen(function (addr, server) {
      var cl = client(addr);
      cl.on('open', function () {
        cl.close();
        server.close();
        done();
      });
    });
  });

  it('must handle upgrade requests of a normal http server', function (done) {
    var httpServer = http.createServer(function (req, res) {
      res.writeHead(200);
      res.end('Hello World');
    })

    var server = ws.attach(httpServer);

    httpServer.listen(function (err) {
      if (err) throw err;

      var addr = httpServer.address();

      http.get({ host: addr.address, port: addr.port }, function (res) {
        res.on('data', function (data) {
          data.toString().should.equal('Hello World');

          var cl = client(addr);
          cl.on('open', function () {
            cl.close();
            httpServer.close();
            done();
          });
        });
      });
    });
  });

  it('must listen on a port', function (done) {
    var server = ws.listen(null, function () {
      var addr = server.httpServer.address();

      http.get({ host: addr.address, port: addr.port }, function (res) {
        res.statusCode.should.equal(501);
        res.on('data', function (data) {
          data.toString().should.equal('Not Implemented');

          var cl = client(addr);
          cl.on('open', function () {
            cl.close();
            server.httpServer.close();
            done();
          });
        });
      });
    });
  });

});
