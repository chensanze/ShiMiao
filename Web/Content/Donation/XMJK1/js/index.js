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
        var fee = Number(Math.ceil(random * 100));
        var last = fee % 10;
        if (last == 7
            || last == 8
            || last == 9) {
            fee += 0.88;
        }
        else {
            var lastRandom = Math.random(0, 1);
            if (lastRandom >= 0.5) {
                fee += 0.66;
            }
            else {
                fee += 0.22;
            }
        }
        return fee.toFixed(2);
    }
    $.stone.saveOrder = function ($sender, options, data) {
        if (data.statusCode == $.stone.constants.statusCode.Succeed) {
            $.stone.redirect($sender, options, data);
        }
    }
    $.stone.addMessageCallBack = function($this, options, data){
        if (data.statusCode == $.stone.constants.statusCode.Succeed) {

        }
    }
    $.stone.loadOrderListCallBack = function ($sender, options, data) {
        if (data.statusCode == $.stone.constants.statusCode.Succeed) {
            $sender.closest(".tab-container").find(".tab-container-tabs").find("li.active").trigger("click");
        }
    }
})($);