(function ($) {
    if (!$.stone) {
        $.stone = {};
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