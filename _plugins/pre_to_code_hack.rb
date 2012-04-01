require 'rubygems'
require 'redcarpet'
require 'fileutils'
require 'digest/md5'
require 'redcarpet'
require 'albino'

class PreIsCode < Redcarpet::Render::HTML
  def block_quote(quote)
    quote.gsub!(/<pre>/,'<pre class="prettyprint"><code class="language-cs">')
    quote.gsub!(/<\/pre>/,'</code></pre>')
  end
end

def from_markdown(text)
  markdown = Redcarpet::Markdown.new(PreIsCode,
    :fenced_code_blocks => true,
    :no_intra_emphasis => true,
    :autolink => true,
    :strikethrough => true,
    :lax_html_blocks => true,
    :superscript => true,
    :hard_wrap => false,
    :tables => true,
    :xhtml => false)

  html = markdown.render(text)
end


PYGMENTS_CACHE_DIR = File.expand_path('../../_cache', __FILE__)
FileUtils.mkdir_p(PYGMENTS_CACHE_DIR)

class Redcarpet2Markdown < Redcarpet::Render::HTML
   
end

class Jekyll::MarkdownConverter
  def extensions
    Hash[ *@config['redcarpet']['extensions'].map {|e| [e.to_sym, true] }.flatten ]
  end

  def markdown
    @markdown ||= Redcarpet::Markdown.new(Redcarpet2Markdown.new(extensions), extensions)
  end

  def convert(content)
    return super unless @config['markdown'] == 'redcarpet2'
    x = markdown.render(content)
    x.gsub!(/<pre>/,'<pre class="prettyprint"><code class="language-cs">')
    x.gsub!(/<\/pre>/,'</code></pre>')
  end
end