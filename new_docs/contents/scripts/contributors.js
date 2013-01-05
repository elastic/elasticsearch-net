$(function() {
    $.ajax({
        url: 'https://api.github.com/repos/Mpdreamz/NEST/contributors',
        dataType: 'jsonp',
        success: function(result) {
          result.data = result.data.sort(function (a, b) { 
            if (a.contributions > b.contributions) return -1;
            if (a.contributions < b.contributions) return 1;
            return 0;
          });
          
          $("#contributorTemplate").tmpl(result).appendTo("#nest-contributors");
        }
    });
  });