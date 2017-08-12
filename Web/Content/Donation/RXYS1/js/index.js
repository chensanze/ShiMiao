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
            var rndday=new Date();
            var rndseed=rndday.getTime();
            rndseed = (rndseed*9301+49297) % 233280;
　　        rndseed = rndseed*2000/(233280.0);
　　        var fee = rndseed/100;
            return fee.toFixed(2);
        }
    }
})($);