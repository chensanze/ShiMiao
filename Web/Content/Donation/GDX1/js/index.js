(function ($) {
    if (!$.stone) {
        $.stone = {};
    }
    $.stone.showOrder = function (obj, event) {
        var $wrapper = $(".wrapper");
        var $modal = $(".modal-order");
        var $mask = $.stone.methods.showMask({
            $container: $wrapper,
            oneClick: function ($mask, options) {
                $.stone.methods.hideMask({
                    $container: $mask.parent()
                });
                var $modal = $(".modal-order");
                $modal.addClass("hide");
                $.stone.setScroll();
            }
        });
        $modal.removeClass("hide");
        $.stone.setNotScroll();
        event.preventDefault();
    }
    $.stone.getRandom = function () {
        var fee = getFee();
        $(".modal-order").find(".money").find("span").html(fee);
        $(".modal-order").find("input[name='fee']").val(fee);
        function getFee() {
            /*var random = Math.random(0, 1);
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
            return fee.toFixed(2); */
            var rndday = new Date();
            var rndseed = rndday.getTime();
            rndseed = (rndseed * 9301 + 49297) % 233280;
            rndseed = rndseed * 2000 / (233280.0);
            var fee = (rndseed / 100).toFixed(2);
            var sfee = fee.toString();
            sfee = sfee.replace(/4/g, "8");
            var rfee = parseFloat(sfee);
            return rfee.toFixed(2);
        }
    }
    $.stone.validateDonationForm = function ($sender, options) {
        var fee = $(".modal-order").find("input[name='fee']").val();
        if (fee == "" || Number(fee) == 0)
        {
            $.stone.showAlert({
                message: "请输入金额",
                maskContainer: ".modal-order"
            });
            return false;
        }
        return true;
    }
    $.stone.saveOrder = function ($sender, options, data) {
        if (data.statusCode == $.stone.constants.statusCode.Succeed) {
            $.stone.redirect($sender, options, data);
        }
    }
    $.stone.changeMoney = function (obj) {
        var $this = $(obj);
        var fee = $this.val();
        if (fee == "") {
            fee == "0";
        }
        $(".modal-order").find(".money").find("span").html(fee);
        $(".modal-order").find("input[name='fee']").val(fee);
    }
    $.stone.changeAnonymous = function (obj) {
        if (obj.checked) {
            $(".member-nickname").html("匿名");
            $(".header-image").attr("src", "/Content/Donation/GDX1/images/default-header.png");
        }
        else {
            var nickName = $(".member-nickname").attr("data-nickname");
            $(".member-nickname").html(nickName);
            var headerImage = $(".header-image").attr("data-header");
            $(".header-image").attr("src", headerImage);
        }
    }
})($);