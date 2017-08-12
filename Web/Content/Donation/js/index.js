(function ($) {
    if (!$.stone) {
        $.stone = {};
    }
    $.stone.initMoneyItem = function () {
        $(".money-item").click(function () {
            $(".money-item").removeClass("selected");
            var $this = $(this);
            $this.addClass("selected");
            var fee = $this.attr("data-fee") || "auto";
            if (fee == "auto") {
                fee = getRandom();
            }
            $(".fee").val(fee);
        });
        $(".fee").val(getRandom(0.2));
    }
    function getRandom(min) {
        var random = Math.random(min || 0, 1);
        fee = Math.ceil(random * 100);
        return fee;
    }
    $.stone.saveOrder = function ($sender, options, data) {
        if (data.statusCode == $.stone.constants.statusCode.Succeed) {
            $.stone.redirect($sender, options, data);
        }
    }
})($);