(function ($) {
    if (!$.stone) {
        $.stone = {};
    }
    $.stone.initShare = function () {
        var shareData = {
            title: $("input[name='share-info-title']").val(),
            content: $("input[name='share-info-content']").val() || "",
            url: window.location.href.split('#')[0],
            image: $("input[name='share-info-image']").val()
        };
        if (shareData.content != "") {
            shareData.content = shareData.content.replace(/<\/?[^>]*>/g, "").replace(/&nbsp;/ig, "");
        }
        var $body = $("body");
        var shareInfo = {
            AppID: $("input[name='share-info-appId']").val(),
            Timestamp: $("input[name='share-info-timestamp']").val(),
            NonceStr: $("input[name='share-info-nonceStr']").val(),
            Signature: $("input[name='share-info-signature']").val()
        };
        share(shareInfo);
        function share(shareInfo) {
            wx.config({
                debug: false,
                appId: shareInfo.AppID,
                timestamp: shareInfo.Timestamp,
                nonceStr: shareInfo.NonceStr,
                signature: shareInfo.Signature,
                jsApiList: [
                    'onMenuShareTimeline',
                    'onMenuShareAppMessage',
                    'onMenuShareQQ',
                    'onMenuShareWeibo',
                    'onMenuShareQZone',
                    'startRecord',
                    'stopRecord',
                    'onVoiceRecordEnd',
                    'playVoice',
                    'pauseVoice',
                    'stopVoice',
                    'onVoicePlayEnd',
                    'uploadVoice',
                    'downloadVoice',
                    'chooseImage',
                    'previewImage',
                    'uploadImage',
                    'downloadImage',
                    'translateVoice',
                    'getNetworkType',
                    'openLocation',
                    'getLocation',
                    'hideOptionMenu',
                    'showOptionMenu',
                    'hideMenuItems',
                    'showMenuItems',
                    'hideAllNonBaseMenuItem',
                    'showAllNonBaseMenuItem',
                    'closeWindow',
                    'scanQRCode',
                    'chooseWXPay',
                    'openProductSpecificView',
                    'addCard',
                    'chooseCard',
                    'openCard'
                ]
            });
            wx.ready(function () {
                wx.onMenuShareAppMessage({
                    title: shareData.title,
                    desc: shareData.content,
                    link: shareData.url,
                    imgUrl: shareData.image,
                    trigger: function (res) {
                        //alert('用户点击发送给朋友');
                    },
                    success: function (res) {
                        //alert('已分享');
                    },
                    cancel: function (res) {
                        //alert('已取消');
                    },
                    fail: function (res) {
                        //alert(JSON.stringify(res));
                    }
                });
                wx.onMenuShareTimeline({
                    title: shareData.title,
                    link: shareData.url,
                    imgUrl: shareData.image,
                    trigger: function (res) {
                        //alert('用户点击发送给朋友');

                    },
                    success: function (res) {
                        //alert('已分享');
                    },
                    cancel: function (res) {
                        //alert('已取消');
                    },
                    fail: function (res) {
                        //alert(JSON.stringify(res));
                    }
                });
                wx.onMenuShareQQ({
                    title: shareData.title,
                    desc: shareData.content,
                    link: shareData.url,
                    imgUrl: shareData.image,
                    trigger: function (res) {
                        //alert('用户点击分享到QQ');
                    },
                    complete: function (res) {
                        //alert(JSON.stringify(res));
                    },
                    success: function (res) {
                        //alert('已分享');
                    },
                    cancel: function (res) {
                        //alert('已取消');
                    },
                    fail: function (res) {
                        //alert(JSON.stringify(res));
                    }
                });
                wx.onMenuShareWeibo({
                    title: shareData.title,
                    desc: shareData.content,
                    link: shareData.url,
                    imgUrl: shareData.image,
                    trigger: function (res) {
                        //alert('用户点击分享到微博');
                    },
                    complete: function (res) {
                        //alert(JSON.stringify(res));
                    },
                    success: function (res) {
                        //alert('已分享');
                    },
                    cancel: function (res) {
                        //alert('已取消');
                    },
                    fail: function (res) {
                        //alert(JSON.stringify(res));
                    }
                });
            });
            wx.error(function (res) {
                //alert(JSON.stringify(res));
            });
        }
    }
    $.stone.hideMenu = function () {
        if (wx && wx.config && wx.ready) {
            wx.config({
                debug: false,
                appId: "",
                timestamp: "",
                nonceStr: "",
                signature: "",
                jsApiList: [
                    'hideOptionMenu'
                ]
            });
            wx.ready(function () {
                wx.hideOptionMenu();
            });
        }
    }
    $.stone.initPay = function ($sender) {
        var options = $.stone.methods.getData($sender);
        wx.config({
            debug: false,
            appId: options.appid,
            timestamp: options.timestamp,
            nonceStr: options.noncestr,
            signature: options.signature,
            jsApiList: [
                'hideOptionMenu',
                'chooseWXPay'
            ]
        });
        wx.ready(function () {
            wx.hideAllNonBaseMenuItem();
        });
        wx.error(function (res) {
            //alert(JSON.stringify(res));
        });
    }
    $.stone.pay = function (obj) {
        var $this = $(obj);
        var options = $.stone.methods.getData($this);
        wx.chooseWXPay({
            timestamp: options.timestamp,
            nonceStr: options.noncestr,
            package: options.package,
            signType: options.signtype,
            paySign: options.paysignature,
            success: function (res) {
                $.stone.showAlert({
                    message: "支付成功",
                    callback: function (op) {
                        $.stone.gotoURL(options.url);
                    }
                });
            },
            cancel: function (res) {
                $.stone.showAlert({
                    message: "已取消支付",
                    callback: function (op) {
                        $.stone.gotoURL(options.cancelurl);
                    }
                });
            },
            fail: function (res) {
                $.stone.showAlert({
                    message: "支付失败"
                });
            }
        });
    }
})($);