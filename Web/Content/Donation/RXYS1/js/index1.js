(function ($) {
    if (!$.stone) {
        $.stone = {};
    }
    $.stone.saveOrder = function ($sender, options, data) {
        if (data.statusCode == $.stone.constants.statusCode.Succeed) {
            $.stone.redirect($sender, options, data);
        }
    }
    $.stone.getRandom = function () {
        var fee = getFee();
        $(".wrapper-detail").find("input[name='fee']").val(fee);
        function getFee() {
            var random = Math.random(0, 1);
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
    }
})($);