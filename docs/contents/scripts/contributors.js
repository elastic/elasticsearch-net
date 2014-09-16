$(function() {
    $.ajax({
        url: 'https://api.github.com/repos/elasticsearch/elasticsearch-net/contributors',
        dataType: 'jsonp',
        success: function(result) {
          result.data = result.data.sort(function (a, b) { 
            if (a.contributions > b.contributions) return -1;
            if (a.contributions < b.contributions) return 1;
            return 0;
          });
          result.count = result.data.length;
          $("#contributorTemplate").tmpl(result).appendTo("#nest-contributors");
        }
    });
  });