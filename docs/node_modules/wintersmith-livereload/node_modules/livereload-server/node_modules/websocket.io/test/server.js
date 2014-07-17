
/**
 * Test dependencies.
 */

var ws = require('../lib/websocket.io')
  , should = require('should')
  , net = require('net')
  , http = require('http')
  , Request = http.IncomingMessage || http.ServerRequest

/**
 * Tests.
 */

describe('websocket server', function () {

  describe('connections', function () {
    it('must fire a connection event', function (done) {
      listen(function (addr, server) {
        var cl = client(addr);
        server.on('connection', function (c) {
          c.should.be.an.instanceof(ws.Socket);
          cl.terminate();
          server.close();
          done();
        });
      });
    });

    it('must keep a reference to the request that originated it', function (done) {
      listen(function (addr, server) {
        var cl = client(addr);
        server.on('connection', function (c) {
          c.req.should.be.an.instanceof(Request);
          c.socket.should.be.an.instanceof(net.Stream);
          cl.terminate();
          server.close();
          done();
        });
      });
    });

    it('must fire a close event when client closes', function (done) {
      listen(function (addr, server) {
        var cl = client(addr)
          , cc

        cl.on('open', function () {
          cc.on('close', function () {
            done();
          });
          cl.close();
        });

        server.on('connection', function (c) {
          cc = c;
        });
      });
    });

    it('must fire a close event when server closes', function (done) {
      listen(function (addr, server) {
        var cl = client(addr)
          , cc
          , total = 2

        cl.on('open', function () {
          cc.on('close', function () {
            --total || done();
          });
          cl.on('close', function () {
            --total || done();
          });
          cc.close();
        });

        server.on('connection', function (c) {
          cc = c;
        });
      });
    });
  });

  describe('messages', function () {
    it('can be received', function (done) {
      listen(function (addr, server) {
        var cl = client(addr);
        cl.on('open', function () {
          cl.send('hello world');
        });
        server.on('connection', function (c) {
          c.on('message', function (data) {
            data.should.equal('hello world');
            cl.close();
            server.close();
            done();
          });
        });
      });
    });

    it('can be sent', function (done) {
      listen(function (addr, server) {
        var cl = client(addr);
        cl.on('message', function (data) {
          data.should.equal('woot');
          cl.close();
          server.close();
          done();
        });
        server.on('connection', function (c) {
          c.send('woot');
        });
      });
    });
  });

  describe('request path', function () {
    it('must be checked if present', function (done) {
      listen({path: '/mypath'}, function (addr, server) {
        var cl = client(addr, '/mypath')

        cl.on('open', function () {
          cl.close();
          server.close();
          done();
        });
      });
    });

    it('must be checked, connection rejected if not matches', function (done) {
      listen({path: '/mypath'}, function (addr, server) {
        var cl = client(addr, '/mypath2');
        cl.on('open', function () {
          cl.close();
          server.close();
          throw new Error('paths do not match');
        });
        done();
      });
    });

    it('can be left null, so any path matches', function (done) {
      listen(function (addr, server) {
        var cl = client(addr, '/mypath');

        cl.on('open', function () {
          cl.close();
          server.close();
          done();
        });
      });
    });
  });

  describe('client tracking', function () {
    it('must have client objects', function (done) {
      listen(function (addr, server) {
        var cl = client(addr)

        cl.on('open', function () {
          server.clients.should.have.length(1);
          server.clients[0].should.be.an.instanceof(ws.Socket);

          var cl2 = client(addr)
          cl2.on('open', function () {
            server.clients.should.have.length(2);
            server.clients[1].should.be.an.instanceof(ws.Socket);

            cl.close();
            cl.on('close', function () {
              should.strictEqual(server.clients[0], null);

              cl2.close();
              cl2.on('close', function () {
                should.strictEqual(server.clients[1], null);
                server.close();
                done();
              });
            });
          });
        });
      });
    });

    it('must fill up gaps in the clients array', function (done) {
      listen(function (addr, server) {
        var cl = client(addr);
        cl.on('open', function () {
          server.clients.should.have.length(1);
          var cl2 = client(addr);
          cl2.on('open', function () {
            server.clients.should.have.length(2);
            cl.close();
            cl.on('close', function () {
              var cl3 = client(addr);
              cl3.on('open', function () {
                server.clients.should.have.length(2);
                server.close();
                done();
              });
            });
          });
        });
      });
    });

    it('must have client count', function (done) {
      listen(function (addr, server) {
        var cl = client(addr)

        cl.on('open', function () {
          server.clientsCount.should.equal(1);

          var cl2 = client(addr)
          cl2.on('open', function () {
            server.clientsCount.should.equal(2);

            cl.close();
            cl.on('close', function () {
              server.clientsCount.should.equal(1);

              cl2.close();
              cl2.on('close', function () {
                server.clientsCount.should.equal(0);
                server.close();
                done();
              });
            });
          });
        });
      });
    });

    it('can be disabled', function (done) {
      listen({'clientTracking': false}, function (addr, server) {
        var cl = client(addr);

        cl.on('open', function () {
          server.clients.should.have.length(0);
          server.clientsCount.should.equal(0);

          var cl2 = client(addr);
          cl2.on('open', function () {
            server.clients.should.have.length(0);
            server.clientsCount.should.equal(0);

            cl.close();
            cl.on('close', function () {
              server.clients.should.have.length(0);
              server.clientsCount.should.equal(0);

              cl2.close();
              cl2.on('close', function () {
                server.clients.should.have.length(0);
                server.clientsCount.should.equal(0);
                server.close();
                done();
              });
            })
          });          
        });
      });
    });
  });

});
