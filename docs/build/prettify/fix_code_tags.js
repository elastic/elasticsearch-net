$(function() {
	$.each($("pre code"), function() {
					$(this).parent().addClass("prettyprint");
					$(this).wrap("<code class='language-cs'/>");

	});
	prettyPrint();
});
