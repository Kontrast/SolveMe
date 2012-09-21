var someJsonObject = [];
(function ($) {
    $('.Answer').live('change', function () {
        someJsonObject = [];
        $('.Answer').each(function () {
            someJsonObject.push($(this).val());
        });
        $("#Answers").val(someJsonObject.toString());
    });
})(jQuery);

=jQuery(function () {
    jQuery(".icon-plus-sign").click(function () {
        jQuery("#AnswersContainer").append("<input type=\"text\" class=\"Answer block\"></input>");
        jQuery("#AnswersContainer").append("<i class=\"icon-minus-sign\"></i>");
    });
});
jQuery(function () {
    jQuery(".icon-minus-sign").live("click", function () {
        jQuery(this).prev().remove();
        jQuery(this).remove();
    });
});