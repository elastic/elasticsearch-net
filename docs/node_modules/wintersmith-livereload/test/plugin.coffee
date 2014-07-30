vows = require 'vows'
assert = require 'assert'
wintersmith = require 'wintersmith'

suite = vows.describe 'Plugin'

suite.addBatch
  'wintersmith environment':
    topic: -> wintersmith './example/config.json'
    'loaded ok': (env) ->
      assert.instanceOf env, wintersmith.Environment
    'contents':
      topic: (env) -> env.load @callback
      'loaded ok': (result) ->
        assert.instanceOf result.contents, wintersmith.ContentTree

suite.export module
