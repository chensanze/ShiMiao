function InitPay($sender) {
    var options = {
        debug: false,
        appId: $sender.attr("data-appid"),
        timestamp: $sender.attr("data-timestamp"),
        nonceStr: $sender.attr("data-noncestr"),
        signature: $sender.attr("data-signagure"),
        jsApiList: [
              'chooseWXPay'
        ]
    };
    wx.config(options);
    wx.ready(function () {
        $.stone.weiXinPay($sender);
    });
    wx.error(function (res) {
    });
}
$.stone.weiXinPay = function (obj) {
    var $this = $(obj);
    var options = {
        appid: $this.attr("data-appid"),
        timestamp: $this.attr("data-timestamp"),
        nonceStr: $this.attr("data-noncestr"),
        package: $this.attr("data-package"),
        signType: $this.attr("data-signtype"),
        paySign: $this.attr("data-paysignature"),
        url: $this.attr("data-url"),
        cancelURL: $this.attr("data-cancelurl"),
        showMask: eval($this.attr("data-mask")) || false
    };
    wx.chooseWXPay({
        timestamp: options.timestamp,
        nonceStr: options.nonceStr,
        package: options.package,
        signType: options.signType,
        paySign: options.paySign,
        success: function (res) {
            $.stone.gotoURL(options.url);
        },
        cancel: function (res) {
            $.stone.showAlert({
                message: "已取消支付",
                callback: function () {
                    $(".wrapper").removeClass("hide");
                }
            });
        },
        fail: function (res) {
            $.stone.showAlert({
                message: "支付失败",
                callback: function () {
                    $(".wrapper").removeClass("hide");
                }
            });
        }
    });
}