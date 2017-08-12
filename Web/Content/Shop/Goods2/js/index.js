(function ($) {
    if (!$.stone) {
        $.stone = {};
    }
    $.stone.showOrder = function (obj,price, event) {
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
        var parent = obj;
        while (parent.tagName.toUpperCase() != "FORM") {
            parent = parent.parentElement;
        }
        $modal.find(".price").find(".span").html(price);
        $modal.find("input[name='Amount']").val(parent.Amount.value);
        $modal.find("input[name='goodsID']").val(parent.goodsID.value);
        $(".header-image").attr("src", "/Content/Shop/Goods2/images/default-header.png");
        var nickName = $(".member-nickname").attr("data-nickname");
        if ('' != nickName) $(".member-nickname").html(nickName);
        var headerImage = $(".header-image").attr("data-header");
        if ('' != headerImage) $(".header-image").attr("src", headerImage);
        $modal.removeClass("hide");
        $.stone.setNotScroll();
        event.preventDefault();
    }
    $.stone.changeAnonymous = function (obj) {
        if (obj.checked) {
            $(".username").val("匿名");
           
        }
        else {
            $(".username").val("");
            
        }
    }
    $.stone.loadGoodsList = function () {
        $.stone.initClickNumber();
    }
    $.stone.saveOrder = function ($sender, options, data) {
        if (data.statusCode == $.stone.constants.statusCode.Succeed) {
            $.stone.redirect($sender, options, data);
        }
    }
    $.stone.initMarquee = function () {
        var $marquee = $(".js-marquee");
        if ($marquee.height() > 150) {
            $marquee.kxbdMarquee({
                direction: "up",
                scrollAmount: 1,
                newAmount: 60,
                eventA: 'mouseover',
                eventB: 'mouseleave'
            });
        }
    }

    var $audioContainer = $(".audio-container");
    var $audio = $audioContainer.find("audio");
    var $img = $audioContainer.find("img");
    $audio[0].addEventListener('ended', function () {
        this.currentTime = 0;
    }, false);
    $audio[0].play();
    setTimeout(function () {
        $(window).scrollTop(1);
    }, 0);
    $audio[0].play();
    document.addEventListener("WeixinJSBridgeReady", function () {
        WeixinJSBridge.invoke('getNetworkType', {}, function (e) {
            $audio[0].play();
        });
    }, false);

    var muver = "0";
    $.stone.playAudio = function (obj) {
        if (muver == "1") {
            $audio[0].play();
            muver = "0";
            $img.addClass("slowwheel infinite");
        } else {
            $audio[0].pause();
            muver = "1";
            $img.removeClass("slowwheel infinite");
        }
    }
})($);