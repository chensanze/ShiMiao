(function ($) {
    $("form").each(function () {
        var $this = $(this);
        $this.submit(function (e) {
            var options = {
                allowSubmit: eval($this.attr("data-allowsubmit")) || false
            };
            if (options.allowSubmit != true) {
                e.preventDefault();
            }
        });
    });
    Array.prototype.remove = function (index) {
        if (index > this.length) {
            return false;
        }
        for (var i = 0, n = 0; i < this.length; i++) {
            if (this[i] != this[index]) {
                this[n++] = this[i];
            }
        }
        this.length--;
    }
    $.fn.extend({
        refresh: function () {
            var $this = $(this);
            if ($this.hasClass("js-scroller")) {
                $this.attr("data-isAppend", "false").attr("data-nomore", "false");
                $this.empty();
                $.stone.methods.loadJsScroller($this);
            }
            else if ($this.hasClass("js-pager")) {
                $.stone.methods.loadJsPager($this);
            }
            else if ($this.hasClass("js-form")) {
                var options = {
                    containerSelector: $this.attr("data-container") || null
                };
                var $container = null;
                if (options.containerSelector != null) {
                    $container = $(options.containerSelector);
                }
                else {
                    $container = $this;
                }
                $container.attr("data-isloaded", "false");
                $.stone.methods.loadJsForm($this);
            }
        }
    });
    if (!$.stone) {
        $.stone = {};
    }
    $.stone.constants = {
        statusCode: {
            Succeed: 0
        }
    };
    $.stone.gotoURL = function (url) {
        var $mask = $.stone.methods.showMask({
            showRefresh: true
        });
        setTimeout(function () {
            $.stone.methods.hideMask({
                $container: $mask.parent(),
                showRefresh: true
            });
        }, 5000);
        window.location.href = url;
    };
    $.stone.extend = function (target, source) {
        for (var p in source) {
            if (source.hasOwnProperty(p)
                && !target.hasOwnProperty(p)) {
                target[p] = source[p];
            }
        }
        return target;
    };
    $.stone.ajax = function (options) {
        var temp = options.success;
        var success = function (data, message, xhr) {
            var nologin = xhr.getResponseHeader("nologin");
            if (nologin == "true") {
                $.stone.showLoginDialog();
                return;
            }
            if (temp) {
                temp(data, message, xhr);
            }
        }
        options.success = success;
        var defaultOptions = {
            type: "post",
            cache: false
        };
        options = $.extend(defaultOptions, options);
        $.ajax(options);
    };
    $.stone.submitForm = function (obj) {
        var $this = $(obj);
        var options = {
            formSelector: $this.attr("data-form") || null,
            callback: eval($this.attr("data-callback")) || null,
            precall: eval($this.attr("data-precall")) || null,
            showMask: eval($this.attr("data-mask")) || false,
            confirm: $this.attr("data-confirm") || null,
            submitForm: eval($this.attr("data-submitform")) || false,
            alertType: $this.attr("data-alert-type") || "alert",
            maskContainer: $this.attr("data-mask-container") || "body"
        };
        options.$form = $(options.formSelector);
        if (options.$form.length == 0) {
            options.$form = $this.closest("form");
        }
        if (options.$form.length == 0) {
            if ($this.is("form")) {
                options.$form = $this;
            }
        }
        if (options.$form.length == 0) {
            return false;
        }
        options.url = $this.attr("data-url") || options.$form.attr("data-url") || null;
        if (!options.url || options.url == "#") {
            return;
        }
        if (options.confirm != null) {
            options.okCallback = doPost;
            $.stone.showConfirm(options);
        }
        else {
            doPost();
        }
        event.preventDefault();
        return false;

        function doPost() {
            if ($.isFunction(options.precall)) {
                if (!options.precall($this, options)) {
                    event.preventDefault();
                    return false;
                }
            }
            var $maskContainer = $(options.maskContainer);
            var $mask;
            if (options.showMask == true) {
                $mask = $.stone.methods.showMask({
                    $container: $maskContainer,
                    showRefresh: true
                });
            }
            if (options.submitForm) {
                options.$form.attr("action", options.url).attr("data-allowsubmit", "true");
                if (typeof options.$form.attr("method") == "undefined") {
                    options.$form.attr("method", "post");
                }
                options.$form.submit();
            }
            else {
                var data = {};
                if (options.$form != null) {
                    data = options.$form.serializeArray();
                    var senderData = $.stone.methods.getData($this);
                    for (var field in senderData) {
                        data.push({ name: field, value: senderData[field] });
                    }
                }
                $.stone.ajax({
                    type: 'POST',
                    url: options.url,
                    data: data,
                    cache: false,
                    success: function (data) {
                        if (data.statusCode == $.stone.constants.statusCode.Succeed) {
                            if (data.message && data.message.length > 0) {
                                $.stone.showMessage({
                                    message: data.message
                                });
                            }
                        }
                        else {
                            if (options.alertType == "alert") {
                                $.stone.showAlert({
                                    message: data.message,
                                    maskContainer: options.maskContainer
                                });
                            }
                            else if (options.alertType == "message") {
                                $.stone.showMessage({
                                    message: data.message
                                });
                            }
                        }
                        if ($.isFunction(options.callback)) {
                            options.callback($this, options, data);
                        }
                        return false;
                    },
                    error: function (data) {

                    },
                    complete: function () {
                        if (options.showMask == true && $mask) {
                            $.stone.methods.hideMask({
                                $container: $mask.parent(),
                                showRefresh: true
                            });
                        }
                    }
                });
            }
        }
    };
    $.stone.ajaxTodo = function (obj) {
        var $this = $(obj);
        var options = {
            callback: eval($this.attr("data-callback")) || null,
            precall: eval($this.attr("data-precall")) || null,
            showMask: eval($this.attr("data-mask")) || false,
            maskContainer: $this.attr("data-mask-container") || "body",
            containerSelector: $this.attr("data-container") || null,
            confirm: $this.attr("data-confirm") || null,
            alertType: $this.attr("data-alert-type") || "alert"
        };
        options.url = $this.attr("data-url") || null;
        if (!options.url || options.url == "#") {
            return;
        }
        if (options.confirm != null) {
            options.okCallback = doPost;
            $.stone.showConfirm(options);
        }
        else {
            doPost();
        }
        event.preventDefault();
        return false;

        function doPost() {
            if ($.isFunction(options.precall)) {
                if (!options.precall($this, options)) {
                    event.preventDefault();
                    return false;
                }
            }
            var $container = null;
            if (options.containerSelector != null) {
                $container = $(options.containerSelector);
            }
            var $maskContainer = $(options.maskContainer);
            var $mask;
            if (options.showMask == true) {
                $mask = $.stone.methods.showMask({
                    $container: $maskContainer,
                    showRefresh: true
                });
            }
            $.stone.ajax({
                type: 'POST',
                url: options.url,
                data: $.stone.methods.getData($this),
                cache: false,
                success: function (data) {
                    if (data.statusCode == $.stone.constants.statusCode.Succeed) {
                        if ($.isFunction(options.callback)) {
                            options.callback($this, options, data);
                        }
                    }
                    else {
                        if (options.alertType == "alert") {
                            $.stone.showAlert({
                                message: data.message,
                                maskContainer: options.maskContainer
                            });
                        }
                        else if (options.alertType == "message") {
                            $.stone.showMessage({
                                message: data.message
                            });
                        }
                    }
                    return false;
                },
                error: function (data) {
                    if (data.message) {
                        $.stone.showAlert({
                            message: data.message,
                            maskContainer: options.maskContainer
                        });
                    }
                    else if (data.statusText) {
                        $.stone.showAlert({
                            message: data.statusText,
                            maskContainer: options.maskContainer
                        });
                    }
                    else {
                        $.stone.showAlert({
                            message: data,
                            maskContainer: options.maskContainer
                        });
                    }
                },
                complete: function () {
                    if (options.showMask == true && $mask) {
                        $.stone.methods.hideMask({
                            $container: $mask.parent(),
                            showRefresh: true
                        });
                    }
                }
            });
        }
    }
    $.stone.search = function (obj) {
        var $this = $(obj);
        var options = {
            precall: eval($this.attr("data-precall")) || null,
            refreshContainer: $this.attr("data-refresh-container") || ".refresh",
            sourceType: $this.attr("data-sourcetype") || "",
            submitForm: eval($this.attr("data-submitform")) || false,
            url: $this.attr("data-url") || null
        };
        if ($.isFunction(options.precall)) {
            if (!options.precall($this, options)) {
                return;
            }
        }
        if (options.submitForm) {
            if (options.url != null) {
                var $form = $("<form></form>");
                $form.attr("action", options.url).attr("data-allowsubmit", "true").attr("method", "post");
                var data = $.stone.methods.getData($this);
                for (var field in data) {
                    var $input = $('<input name="' + field + '" type="hidden" value="' + data[field] + '" />');
                    $form.append($input);
                }
                $form.submit();
                delete $form;
            }
        }
        else {
            var $refresh = $(options.refreshContainer);
            var data = {};
            if (options.sourceType == "form") {
                data = $.stone.methods.getSearchFieldData($this);
            }
            else {
                data = $.stone.methods.getData($this);
            }
            $.stone.methods.setData($refresh, data);
            $refresh.attr("data-args-pageindex", "");
            $refresh.each(function () {
                $(this).refresh();
            });
        }
    }
    $.stone.redirect = function ($sender, options, data) {
        if (data.statusCode == $.stone.constants.statusCode.Succeed) {
            $.stone.gotoURL(data.Data.url);
        }
    };
    $.stone.showMessage = function (options) {
        var defaultOptions = {
            message: "程序发生错误",
            time: 2000,
            containerSelector: "body"
        };
        if (typeof options == "undefined") {
            options = defaultOptions;
        }
        else {
            $.stone.extend(options, defaultOptions);
        }
        var $divMessage = $('<div></div>');
        $divMessage.css({
            position: "fixed",
            "z-index": "1000000"
        });
        var $innerMessage = $('<div></div>');
        $innerMessage.css({
            margin: "10px",
            padding: "10px",
            "min-width": "50px",
            "min-height": "25px",
            "vertical-align": "middle",
            color: "white",
            background: "black",
            "border-radius": "10px",
        });
        $innerMessage.html(options.message);
        $divMessage.append($innerMessage);
        $(options.containerSelector).append($divMessage);
        var top = ($(window).height() - $divMessage.height()) / 2;
        var left = ($(window).width() - $divMessage.width()) / 2;
        $divMessage.css({ "top": top, "left": left });
        setTimeout(function () {
            $divMessage.remove();
            if (typeof options.callback != "undefined") {
                options.callback(options);
            }
        }, options.time);
    };
    $.stone.showAlert = function (options) {
        var defaultOptions = {
            titleText: "提示",
            okText: "确定",
            textAlign: "center",
            callback: null,
            maskContainer: "body"
        };
        if (typeof options == "undefined") {
            options = defaultOptions;
        }
        else {
            $.stone.extend(options, defaultOptions);
        }
        var $maskContainer = $(options.maskContainer);
        if ($maskContainer.length == 0) {
            $maskContainer = $("body");
        }
        var $mask = $.stone.methods.showMask({
            $container: $maskContainer
        });
        var $dialog = $('<div class="dialog"></div>');
        $dialog.data("source-mask", $mask);
        var $title = $('<div class="dialog-header"></div>');
        $title.html(options.titleText);
        var $closeIcon = $('<a class="icon-cancel-circled remove right" href="javascript:;"></a>');
        $closeIcon.one("click", close);
        $title.append($closeIcon);
        $dialog.append($title);
        var $dialogBody = $('<div class="dialog-body"></div>');
        var $dialogContent = $('<div class=dialog-content></div>');
        $dialogContent.css({ "text-align": options.textAlign });
        $dialogContent.html(options.message);
        $dialogBody.append($dialogContent);
        $dialog.append($dialogBody);
        var $footer = $('<div class="dialog-footer"></div>');
        var $ok = $('<div class="dialog-btn"></div>');
        $ok.html(options.okText);
        $ok.one("click", close);
        $footer.append($ok);
        $dialog.append($footer);
        $maskContainer.append($dialog);
        var winHeight = $(window).height();
        var winWidth = $(window).width();
        var height = $dialog.height();
        var width = $dialog.width();
        $dialog.css({
            top: (winHeight - height) / 2,
            left: (winWidth - width) / 2
        });

        function close() {
            $dialog.remove();
            $.stone.methods.hideMask({
                $container: $maskContainer
            });
            if ($.isFunction(options.callback)) {
                options.callback(options);
            }
        }
    }
    $.stone.showConfirm = function (options) {
        var defaultOptions = {
            titleText: "提示",
            okText: "确定",
            cancelText: "取消",
            time: 2000,
            textAlign: "center",
            maskContainer: "body"
        };
        if (typeof options == "undefined") {
            options = defaultOptions;
        }
        else {
            $.stone.extend(options, defaultOptions);
        }
        var $maskContainer = $(options.maskContainer);
        if ($maskContainer.length == 0) {
            $maskContainer = $("body");
        }
        var $mask = $.stone.methods.showMask({
            $container: $maskContainer
        });
        var dialog = $('<div class="dialog"></div>');
        dialog.data("source-mask", $mask);
        $.stone.setNotScroll();
        var title = $('<div class="dialog-header"></div>');
        title.html(options.titleText);
        var closeIcon = $('<a class="icon-cancel-circled remove right" href="javascript:;"></a>');
        closeIcon.one("click", close);
        title.append(closeIcon);
        dialog.append(title);
        var body = $('<div class="dialog-body"></div>');
        var content = $('<div class=dialog-content></div>');
        content.css({ "text-align": options.textAlign });
        content.html(options.confirm);
        body.append(content);
        dialog.append(body);
        var footer = $('<div class="dialog-footer"></div>');
        var cancel = $('<div class="dialog-btn"></div>');
        cancel.html(options.cancelText);
        cancel.one("click", close);
        footer.append(cancel);
        var ok = $('<div class="dialog-btn"></div>');
        ok.html(options.okText);
        ok.one("click", callback);
        footer.append(ok);
        dialog.append(footer);
        $maskContainer.append(dialog);
        var winHeight = $(window).height();
        var winWidth = $(window).width();
        var height = dialog.height();
        var width = dialog.width();
        dialog.css({
            top: (winHeight - height) / 2,
            left: (winWidth - width) / 2
        });

        function callback() {
            if ($.isFunction(options.okCallback)) {
                options.okCallback();
                close();
            }
        }

        function close() {
            dialog.remove();
            $.stone.setScroll();
            $.stone.methods.hideMask({
                $container: $maskContainer
            });
        }
    }
    $.stone.initSelectRegion = function () {
        $(".select-region").each(function () {
            var $this = $(this);
            var $mask, $container;
            var $region = $this.find(".region");
            $region.focus(function () {
                $mask = $(".select-region-mask");
                if ($mask.length == 0) {
                    $mask = $('<div class="select-region-mask"></div>');
                    $mask.css({
                        position: "fixed",
                        left: 0,
                        right: 0,
                        top: 0,
                        bottom: 0,
                        opacity: 0.6,
                        background: "#000",
                        "z-Index": 100000011
                    });
                    $mask.click(function () {
                        close();
                    })
                    $("body").append($mask);
                }
                else {
                    $mask.show();
                }
                $container = $(".select-region-container");
                if ($container.length == 0) {
                    $container = $('<div class="select-region-container"></div>');
                    $container.css({
                        position: "fixed",
                        left: 20,
                        right: 20,
                        top: 20,
                        bottom: 20,
                        background: "#fff",
                        border: "#ff6a00 1px solid",
                        fontSize: "0.8rem",
                        "display": "-webkit-box",
                        "-webkit-box-sizing": "content-box",
                        "-webkit-box-orient": "vertical",
                        "z-Index": 100000012
                    });
                    var $provinces = $('<div></div>');
                    var $citys = $('<div></div>');
                    var $countys = $('<div></div>');
                    $provinces.css({
                        "overflow": "scroll",
                        "-webkit-box-flex": "1"
                    });
                    $citys.css({
                        "overflow": "scroll",
                        "-webkit-box-flex": "1"
                    });
                    $countys.css({
                        "overflow": "scroll",
                        "-webkit-box-flex": "1"
                    });
                    var $header = $('<div></div>');
                    $header.css({
                        "display": "-webkit-box",
                        "-webkit-box-sizing": "content-box",
                        "-webkit-box-orient": "horizontal",
                        "border-bottom": "#ff6a00 1px solid",
                        "background": "#f5f5f5",
                        "height": "30px",
                        "line-height": "30px",
                        "padding": "5px 10px"
                    });
                    var $title = $('<div>选择区域</div>');
                    $title.css({
                        "-webkit-box-flex": "1"
                    });
                    $header.append($title);
                    var $up = $('<div></div>');
                    $up.css({
                        width: 30,
                        "margin-right": "30px"
                    });
                    var $iconUp = $('<a href="javascript:;" class="icon-up"></a>');
                    $iconUp.css({
                        "display": "block",
                        "font-size": "25px",
                        "color": "#ff6a00"
                    });
                    $up.append($iconUp);
                    $header.append($up);
                    var $close = $('<div></div>');
                    $close.css({
                        width: 30
                    });
                    $close.click(function () {
                        close();
                    })
                    var $iconClose = $('<a href="javascript:;" class="icon-cancel-circled"></a>');
                    $iconClose.css({
                        "display": "block",
                        "font-size": "25px",
                        "color": "#ff6a00"
                    });
                    $close.append($iconClose);
                    $header.append($close);
                    $container.append($header);
                    var $selectedRegion = $('<div>当前所选区域：</div>');
                    $selectedRegion.css({
                        "background": "#f5f5f5",
                        "border-bottom": "#ff6a00 1px solid",
                        "padding": "10px"
                    });
                    var $regionName = $('<span></span>');
                    $selectedRegion.append($regionName);
                    $container.append($selectedRegion);
                    $up.attr("data-level", 1);
                    $up.attr("data-name", "");
                    stone.constants.provinces.forEach(function (province) {
                        province.list.forEach(function (provinceItem) {
                            var $province = $('<div></div>');
                            $province.html(provinceItem.name);
                            $province.css({
                                padding: "10px",
                                "border-bottom": "#ff6a00 1px solid"
                            });
                            $province.click(function () {
                                var name = provinceItem.name;
                                $regionName.html(name);
                                $up.attr("data-level", 1);
                                $up.attr("data-province", name);
                                $citys.children().remove();
                                var currentCitys = null;
                                for (var i in stone.constants.citys) {
                                    var city = stone.constants.citys[i];
                                    if (city.provinceID == provinceItem.id) {
                                        currentCitys = city;
                                        break;
                                    }
                                }
                                if (currentCitys != null) {
                                    currentCitys.list.forEach(function (cityItem) {
                                        var $city = $('<div></div>');
                                        $city.html(cityItem.name);
                                        $city.css({
                                            padding: "10px",
                                            "border-bottom": "#ff6a00 1px solid"
                                        });
                                        $city.click(function () {
                                            var name = provinceItem.name + "/" + cityItem.name
                                            $regionName.html(name);
                                            $up.attr("data-level", 2);
                                            $up.attr("data-province", provinceItem.name);
                                            $up.attr("data-city", name);
                                            $countys.children().remove();
                                            var currentCounties = null;
                                            for (var i in stone.constants.counties) {
                                                var currentCounty = stone.constants.counties[i];
                                                if (currentCounty.cityID == cityItem.id) {
                                                    currentCounties = currentCounty;
                                                    break;
                                                }
                                            }
                                            if (currentCounties != null) {
                                                currentCounties.list.forEach(function (countyItem) {
                                                    var $county = $('<div></div>');
                                                    $county.html(countyItem.name);
                                                    $county.css({
                                                        padding: "10px",
                                                        "border-bottom": "#ff6a00 1px solid"
                                                    });
                                                    $county.click(function () {
                                                        var name = provinceItem.name + "/" + cityItem.name + "/" + countyItem.name;
                                                        $regionName.html(name);
                                                        select({
                                                            provinceID: provinceItem.id,
                                                            cityID: cityItem.id,
                                                            countyID: countyItem.id,
                                                            region: name
                                                        });
                                                        return;
                                                    });
                                                    $countys.append($county);
                                                });
                                            }
                                            else {
                                                select({
                                                    provinceID: provinceItem.id,
                                                    cityID: cityItem.id,
                                                    region: name
                                                });
                                                return;
                                            }
                                            $citys.hide();
                                            $countys.show();
                                        });
                                        $citys.append($city);
                                    });
                                    if (currentCitys.list.length == 1) {
                                        $citys.children().first().trigger("click");
                                    }
                                }
                                else {
                                    select({
                                        provinceID: provinceItem.id,
                                        region: name
                                    });
                                    return;
                                }
                                $provinces.hide();
                                $citys.show();
                            });
                            $provinces.append($province);
                        });
                    });
                    $container.append($provinces);
                    $citys.hide();
                    $container.append($citys);
                    $countys.hide();
                    $container.append($countys);
                    $("body").append($container);
                    $up.click(function () {
                        var level = $up.attr("data-level") || 1;
                        if (level == 1) {
                            $provinces.show();
                            $citys.hide();
                            $countys.hide();
                            var name = $up.attr("data-province") || "";
                            $regionName.html(name);
                        }
                        else if (level == 2) {
                            $provinces.hide();
                            $citys.show();
                            $countys.hide();
                            $up.attr("data-level", 1)
                            var name = $up.attr("data-city") || "";
                            $regionName.html(name);
                        }
                    })
                }
                else {
                    $container.show();
                }
            })

            function close() {
                $mask.hide();
                $container.hide();
            }
            function select(options) {
                $this.find(".province").val(options.provinceID);
                $this.find(".city").val(options.cityID);
                $this.find(".county").val(options.countyID);
                $this.find(".region").val(options.region);
                close();
            }
        })
    }
    $.stone.initFormDetail = function () {
        if ($(".form-detail").find(".item").length > 0) {
            $(document).click(function (e) {
                $(e.target).closest(".item").find("input,textarea").focus();
            });
        }
    }
    $.stone.initSelectTime = function (startTime, endTime) {
        $(".seletct-time").focus(function () {
            var $this = $(this);
            var $mask, $container;
            $mask = $(".select-time-mask");
            if ($mask.length == 0) {
                $mask = $('<div class="select-time-mask"></div>');
                $mask.css({
                    position: "fixed",
                    left: 0,
                    right: 0,
                    top: 0,
                    bottom: 0,
                    opacity: 0.6,
                    background: "#000",
                    zIndex: 100000001
                });
                $mask.click(function () {
                    close();
                })
                $("body").append($mask);
            }
            else {
                $mask.show();
            }
            $container = $(".select-time-container");
            if ($container.length == 0) {
                $container = $('<div class="select-time-container"></div>');
                $container.css({
                    position: "fixed",
                    left: 20,
                    right: 20,
                    top: 20,
                    bottom: 20,
                    background: "#fff",
                    border: "#ff6a00 1px solid",
                    fontSize: "0.8rem",
                    "display": "-webkit-box",
                    "-webkit-box-sizing": "content-box",
                    "-webkit-box-orient": "vertical",
                    zIndex: 100000002
                });
                var $header = $('<div></div>');
                $header.css({
                    "display": "-webkit-box",
                    "-webkit-box-sizing": "content-box",
                    "-webkit-box-orient": "horizontal",
                    "border-bottom": "#ff6a00 1px solid",
                    "background": "#f5f5f5",
                    "height": "30px",
                    "line-height": "30px",
                    "padding": "5px 10px"
                });
                var $title = $('<div>选择时间</div>');
                $title.css({
                    "-webkit-box-flex": "1"
                });
                $header.append($title);
                var $close = $('<div></div>');
                $close.css({
                    width: 30
                });
                $close.click(function () {
                    close();
                })
                var $iconClose = $('<a href="javascript:;" class="icon-cancel-circled"></a>');
                $iconClose.css({
                    "display": "block",
                    "font-size": "25px",
                    "color": "#ff6a00"
                });
                $close.append($iconClose);
                $header.append($close);
                $container.append($header);
                var $body = $('<div></div>');
                $body.css({
                    "overflow": "scroll",
                    "-webkit-box-flex": "1"
                });
                var items = [];
                var now = new Date();
                var starts = startTime.split(':');
                var ends = endTime.split(':');
                var start = new Date(now.getYear(), now.getMonth(), now.getDay(), starts[0], starts[1], "00");
                var end = new Date(now.getYear(), now.getMonth(), now.getDay(), ends[0], ends[1], "00");
                var itemEnd = addMinutes(start, 30);
                while (true) {
                    if (start < end && itemEnd < end) {
                        items.push({
                            start: padLeft(start.getHours(), 2) + ":" + padLeft(start.getMinutes(), 2),
                            end: padLeft(itemEnd.getHours(), 2) + ":" + padLeft(itemEnd.getMinutes(), 2)
                        });
                        start = itemEnd;
                        itemEnd = addMinutes(start, 30);
                    }
                    else if (start < end && itemEnd >= itemEnd) {
                        items.push({
                            start: padLeft(start.getHours(), 2) + ":" + padLeft(start.getMinutes(), 2),
                            end: padLeft(end.getHours(), 2) + ":" + padLeft(end.getMinutes(), 2)
                        });
                        break;
                    }
                    else {
                        break;
                    }
                }
                items.forEach(function (item) {
                    var $item = $('<div></div>');
                    var value = item.start + " - " + item.end;
                    $item.html(value);
                    $item.css({
                        padding: "10px",
                        "border-bottom": "#ff6a00 1px solid"
                    });
                    $item.click(function () {
                        $this.val(value);
                        close();
                    });
                    $body.append($item);
                });
                $container.append($body);
                $("body").append($container);

                function addMinutes(date, minutes) {
                    return new Date(date.getTime() + minutes * 60 * 1000);
                }
                function padLeft(str, length) {
                    str += '';
                    if (str.length >= length)
                        return str;
                    else
                        return padLeft("0" + str, length);
                }
            }
            else {
                $container.show();
            }
            function close() {
                $mask.hide();
                $container.hide();
            }
        });
    }
    $.stone.initTextArea = function () {
        $(".js-textarea").each(function () {
            var $this = $(this);
            var rows = Number($this.attr("rows")) || 1;
            if (rows == 1) {
                $this.height($this[0].scrollHeight);
            }
            $this.keyup(function () {
                var $this = $(this);
                window.setTimeout(function () {
                    $this.height($this[0].scrollHeight);
                }, 0);
            });
        });
    }
    $.stone.initJsChoose = function () {
        $(".js-choose").each(function () {
            var $this = $(this);
            var $options = $this.find(".option");
            $options.each(function () {
                var $option = $(this);
                $option.click(function () {
                    $options.removeClass("active");
                    $option.addClass("active");
                    var data = $.stone.methods.getData($option);
                    for (var field in data) {
                        $this.find("input[name='" + field + "']").val(data[field]);
                    }
                });
            });
        });
    }

    $.stone.initJsSelect = function () {
        $("body").delegate(".js-select", "focus", function () {
            var $this = $(this);
            var options = {
                title: $this.attr("data-title") || "请选择",
                maskContainer: $this.attr("data-mask-container") || "body",
                sourceCall: eval($this.attr("data-source-call")) || null,
                type: $this.attr("data-type") || null,
                isinited: eval($this.attr("data-isinited")) || false
            };
            var $mask = $.stone.methods.showMask({
                $container: $(options.maskContainer)
            });
            var $container = $(".js-select-container-" + options.type);
            if ($container.length == 0) {
                $container = $('<div class="js-select-container js-select-container-' + options.type + '"></div>');
                var $header = $('<div class="js-select-header"></div>');
                var $title = $('<div class="js-select-title">' + options.title + '</div>');
                $header.append($title);
                var $clear = $('<div class="js-select-clear"></div>');
                $clear.click(function () {
                    var $source = $container.data("source");
                    $source.val("");
                    $source.removeAttr("data-args-value");
                    close();
                })
                var $iconClear = $('<a href="javascript:;" class="js-select-clear-btn icon-trash-empty"></a>');
                $clear.append($iconClear);
                $header.append($clear);
                var $close = $('<div class="js-select-close"></div>');
                $close.click(function () {
                    close();
                })
                var $iconClose = $('<a href="javascript:;" class="js-select-close-btn icon-cancel-circled"></a>');
                $close.append($iconClose);
                $header.append($close);
                $container.append($header);
                var $body = $('<div class="js-select-body"></div>');
                if ($.isFunction(options.sourceCall)) {
                    options.sourceCall($container, options, function (items) {
                        items.forEach(function (item) {
                            var $item = $('<div class="js-select-item"></div>');
                            $item.html(item.Value);
                            $item.click(function () {
                                var $source = $container.data("source");
                                $source.val(item.Value);
                                $source.attr("data-args-value", item.Key);
                                close();
                            });
                            $body.append($item);
                        });
                    });
                }
                $container.append($body);
                $("body").append($container);
            }
            else {
                $container.show();
            }
            $container.data("source", $this);
            function close() {
                $.stone.methods.hideMask({
                    $container: $mask.parent()
                });
                $container.hide();
            }
        });
    }
    $.stone.initSelectDate = function () {
        $(".select-date").date();
    }
    $.stone.initIconRadio = function () {
        $(".icon-radio").click(function () {
            var $this = $(this);
            if ($this.hasClass("disabled")) {
                return;
            }
            var $container = $this.closest(".icon-radio-container");
            var options = {
                number: Number($container.attr("data-number")) || 1,
                callback: eval($container.attr("data-callback")) || null
            };
            if (options.number > 1) {
                var $icons = $container.find(".icon-radio.icon-ok-circled2");
                if ($this.hasClass("icon-circle-empty")) {
                    if (options.number <= $icons.length) {
                        $.stone.showMessage({ message: "最多只能选择" + options.number + "个选项" });
                        return;
                    }
                    else {
                        $this.removeClass("icon-circle-empty").addClass("icon-ok-circled2");
                    }
                }
                else {
                    $this.removeClass("icon-ok-circled2").addClass("icon-circle-empty");
                }
            }
            else {
                var $icons = $container.find(".icon-radio");
                if ($icons.length == 1) {
                    if ($this.hasClass("icon-circle-empty")) {
                        $this.removeClass("icon-circle-empty").addClass("icon-ok-circled2");
                    }
                    else {
                        $this.removeClass("icon-ok-circled2").addClass("icon-circle-empty");
                    }
                }
                else {
                    $icons.addClass("icon-circle-empty").removeClass("icon-ok-circled2");
                    $this.addClass("icon-ok-circled2").removeClass("icon-circle-empty");
                }
            }
            var data = $.stone.methods.getData($this);
            var hasValue = false;
            if ($this.hasClass("icon-ok-circled2")) {
                hasValue = true;
            }
            for (var field in data) {
                if (hasValue) {
                    $container.find("input[name='" + field + "']").val(data[field]);
                }
                else {
                    $container.find("input[name='" + field + "']").val("");
                }
            }
            if ($.isFunction(options.callback)) {
                options.callback($this, options);
            }
        });
    }
    $.stone.initSwiper = function () {
        $(".swiper-container.js-swiper").each(function () {
            var $this = $(this);
            var options = {
                url: $this.attr("data-url"),
                pagination: eval($this.attr("data-pagination")) || false,
                playTime: Number($this.attr("data-playtime")) || 3000,
                noswiping: eval($this.attr("data-noswiping")) || false,
                height: $this.attr("data-height") || null,
                data: $.stone.methods.getData($this),
                callback: eval($this.attr("data-callback")) || null,
                isLoaded: eval($this.attr("data-isloaded")) || false
            };
            if (options.isLoaded == true) {
                return;
            }
            $.stone.ajax({
                url: options.url,
                data: options.data,
                success: function (data) {
                    if (data.statusCode == $.stone.constants.statusCode.Succeed) {
                        $this.attr("data-isloaded", "true");
                        if (data.Data.list.length > 0) {
                            var $wrapper = $("<div></div>");
                            $wrapper.addClass("swiper-wrapper");
                            $this.append($wrapper);
                            data.Data.list.forEach(function (item) {
                                var $slide = $("<div></div>");
                                $slide.addClass("swiper-slide");
                                if (options.noswiping) {
                                    $slide.addClass("swiper-no-swiping");
                                }
                                var $img = $("<img />");
                                $img.attr("src", item.ImageURL);
                                $img.css({
                                    width: "100%"
                                });
                                if (item.hasOwnProperty("TargetURL") && item.TargetURL != "") {
                                    var $a = $("<a></a>");
                                    $a.attr("href", item.TargetURL);
                                    $a.attr("target", "_blank");
                                    $a.css({ "display": "block" });
                                    $a.append($img);
                                    $slide.append($a);
                                }
                                else {
                                    $slide.append($img);
                                }
                                if (item.hasOwnProperty("Title") && item.Title != "") {
                                    var $title = $("<div></div>");
                                    $title.html(item.Title);
                                    $title.css({
                                        "position": "relative",
                                        "bottom":"0px",
                                        "left": "0px",
                                        "right": "0px",
                                        "overflow": "hidden",
                                        "word-break": "keep-all",
                                        "background": "#000",
                                        "color": "#fff",
                                        "opacity": "0.8",
                                        "font-size": "0.8rem",
                                        "padding": "3px 5px",
                                        "text-overflow": "ellipsis"
                                    });
                                    $slide.append($title);
                                }
                                $wrapper.append($slide);
                            });
                            var loop = data.Data.list.length > 1;
                            var swiperOption = {
                                autoHeight:true,
                                loop: loop,
                                autoplay: options.playTime,
                                noSwiping: options.noswiping,
                                autoplayDisableOnInteraction: false,
                                preventClicks: false,
                                lazyLoading: true
                            };
                            if (options.pagination) {
                                var $pagination = $("<div></div>");
                                $pagination.addClass("swiper-pagination");
                                $this.append($pagination);
                                swiperOption.pagination = ".js-swiper .swiper-pagination";
                            }
                            var swiper = new Swiper('.js-swiper', swiperOption);
                        }
                        if ($.isFunction(options.callback)) {
                            options.callback($this, options, data);
                        }
                    }
                }
            });
        });
    }
    $.stone.initJsPager = function () {
        $(".js-pager").each(function () {
            var $this = $(this);
            var $options = {
                autoLoad: eval($this.attr("data-autoload")) || true
            };
            if ($options.autoLoad) {
                $.stone.methods.loadJsPager($this);
            }
        });
    }
    $.stone.initJsForm = function (containerSelector) {
        containerSelector = containerSelector || "body";
        $(containerSelector).find(".js-form").each(function () {
            var $this = $(this);
            var $options = {
                autoLoad: eval($this.attr("data-autoload")) || true
            };
            if ($options.autoLoad) {
                $.stone.methods.loadJsForm($this);
            }
        });
    }
    $.stone.initClickNumber = function ($container) {
        var $clickNumber;
        if (typeof $container == "undefined") {
            $clickNumber = $(".click-number");
        }
        else {
            $clickNumber = $container.find(".click-number");
        }
        var isInited = eval($clickNumber.attr("data-isinited")) || false;
        if (isInited) {
            return;
        }
        $clickNumber.attr("data-isinited", "true");
        $clickNumber.each(function () {
            var $this = $(this);
            var options = {
                $number: $this.find("input[name='Amount']"),
                minNumber: $this.attr("data-min") || 1,
                maxNumber: $this.attr("data-max") || null,
                step: Number($this.attr("data-step")) || 1
            };
            $this.find(".decrease").click(function () {
                var $btn = $(this);
                var confirm = $btn.attr("data-confirm") || null;
                var precall = eval($btn.attr("data-precall")) || null;
                var callback = eval($btn.attr("data-callback")) || null;
                var value = Number(options.$number.val()) || 0;
                if (value == options.minNumber) {
                    return;
                }
                if ($.isFunction(precall)) {
                    if (!precall($btn, value - options.step)) {
                        return false;
                    }
                }
                if (value - options.step == options.minNumber) {
                    if (confirm != null) {
                        options.confirm = confirm;
                        options.okCallback = doPost;
                        options.maskContainer = $btn.closest(".wrapper");
                        $.stone.showConfirm(options);
                        return;
                    }
                    else {
                        doPost();
                    }
                }
                else {
                    doPost();
                }
                function doPost() {
                    var number;
                    if (value - options.step >= options.minNumber) {
                        number = value - options.step;
                    }
                    else {
                        number = options.minNumber;
                    }
                    options.$number.val(number);
                    if ($.isFunction(callback)) {
                        callback($btn, $this, number);
                    }
                }
            });
            $this.find(".add").click(function () {
                var $btn = $(this);
                var precall = eval($btn.attr("data-precall")) || null;
                var callback = eval($btn.attr("data-callback")) || null;
                var value = Number(options.$number.val()) || 0;
                if (value == options.maxNumber) {
                    return;
                }
                if ($.isFunction(precall)) {
                    if (!precall($btn, value + options.step)) {
                        return false;
                    }
                }
                var number;
                if (options.maxNumber != null && value + options.step > options.maxNumber) {
                    number = options.maxNumber
                }
                else {
                    number = value + options.step;
                }
                options.$number.val(number);
                if ($.isFunction(callback)) {
                    callback($btn, $this, number);
                }
            });
        });
    }
    $.stone.initTabs = function () {
        $(".tab-container").each(function () {
            var $container = $(this);
            if ($container.attr("data-isinited") == "1") {
                return;
            }
            $container.attr("data-isinited", "1");
            var $tabs = $container.find(".tab-container-tabs");
            var $contents = $container.find(".tab-container-content");
            var options = {
                callback: eval($container.attr("data-callback")) || null,
                autoLoadBottom: eval($container.attr("data-autoloadbottom")) || false,
                noswiping: eval($container.attr("data-noswiping")) || false,
                type: $container.attr("data-args-type") || "",
                guid: $container.attr("data-guid") || "",
                scrollerContainer: $container.attr("data-scroller-container") || null,
            };
            if (options.noswiping) {
                $contents.find(".swiper-slide").addClass("swiper-no-swiping");
            }
            var tabSwiper = new Swiper(options.guid + '.tab-container-content', {
                autoHeight: true,
                autoplayDisableOnInteraction: false,
                noswiping: options.noswiping,
                onSlideChangeEnd: function (swiper) {
                    var index = swiper.activeIndex;
                    var $li = $tabs.find("li:eq(" + index + ")");
                    var $content = $contents.find(".swiper-slide:eq(" + index + ")");
                    var call = eval($content.attr("data-call")) || null;
                    $tabs.find("li").removeClass("active");
                    $li.addClass("active");
                    if ($.isFunction(call)) {
                        call(tabSwiper, $content);
                    }
                }
            });
            $container.data("swiper", tabSwiper);
            $tabs.delegate("li", "click", function () {
                var $li = $(this);
                var index = $li.index();
                var $content = $contents.find(".swiper-slide:eq(" + index + ")");
                var call = eval($content.attr("data-call")) || null;
                var tabSwiper = $li.closest(".tab-container").data("swiper");
                tabSwiper.slideTo(index, 100, false);
                $tabs.find("li").removeClass("active");
                $li.addClass("active");
                if ($.isFunction(call)) {
                    call(tabSwiper, $content);
                }
            });
            var $activeTab = $tabs.find("li.active");
            if ($activeTab.length == 0) {
                if (options.type == "") {
                    $tabs.find("li").first().trigger("click");
                }
                else {
                    var $content = $contents.find(".swiper-slide[data-args-type='" + options.type + "']");
                    var index = $content.index();
                    $tabs.find("li:eq(" + index + ")").trigger("click");
                }
            }
            else {
                $activeTab.first().trigger("click");
            }
            if (options.autoLoadBottom) {
                var $scroller = null;
                if (options.scrollerContainer != null) {
                    $scroller = $(options.scrollerContainer);
                }
                if ($scroller == null || $scroller.length == 0) {
                    $scroller = $(window);
                }
                var isInit = eval($scroller.attr("data-isinit-scroll")) || false;
                if (isInit) {
                    return;
                }
                $scroller.attr("data-isinit-scroll", "true");
                $scroller.scroll(function () {
                    var $li = $tabs.find("li.active");
                    var index = $li.index();
                    var $content = $contents.find(".swiper-slide:eq(" + index + ")");
                    var liAutoLoadBottom = eval($content.attr("data-autoloadbottom")) || false;
                    if (!liAutoLoadBottom) {
                        return;
                    }
                    var height = $content.attr("data-height") || 80;
                    var scrollTop = $(window).scrollTop();
                    var scrollHeight = $(document).height();
                    var windowHeight = $(window).height();
                    if (scrollHeight - scrollTop - windowHeight < height) {
                        var call = eval($content.attr("data-call")) || null;
                        if ($.isFunction(call)) {
                            call(tabSwiper, $content, true);
                        }
                    }
                });
            }
        })
    }
    $.stone.initFixTop = function () {
        var fixeds = $(".fixed-top");
        if (fixeds.length > 0) {
            fixeds.each(function () {
                var $this = $(this);
                var options = {
                    targetSelector: $this.attr("data-target") || this,
                    scrollerContainer: $this.attr("data-scroller-container") || null
                }
                var $scroller = null;
                var $target;
                if (options.scrollerContainer != null) {
                    $scroller = $this.closest(options.scrollerContainer);
                }
                if ($scroller == null || $scroller.length == 0) {
                    $scroller = $(window);
                }
                var isInit = eval($scroller.attr("data-isinit-scroll-fixtop")) || false;
                if (isInit) {
                    return;
                }
                $scroller.attr("data-isinit-scroll-fixtop", "true");
                $scroller.scroll(function () {
                    $(".fixed-top").each(function () {
                        var $this = $(this);
                        var $target;
                        if ($scroller[0] == window) {
                            $target = $("body").find(options.targetSelector);
                        }
                        else {
                            $target = $scroller.find(options.targetSelector);
                        }
                        var scrollTop = $scroller.scrollTop();
                        var targetTop = $target.position().top;
                        var isFixed = false;
                        if ($scroller[0] == window) {
                            if (scrollTop < targetTop) {
                                isFixed = true;
                            }
                        }
                        else {
                            if (0 < targetTop) {
                                isFixed = true;
                            }
                        }
                        if (isFixed) {
                            if (!$this.hasClass("hide")) {
                                $this.addClass("hide");
                            }
                        } else {
                            if ($this.hasClass("hide")) {
                                $this.removeClass("hide");
                            }
                        }
                    });
                });
            });
        }
    }
    $.stone.initFixTopMoreTarget = function () {
        var fixeds = $(".fixed-top");
        if (fixeds.length > 0) {
            fixeds.each(function () {
                var $this = $(this);
                var options = {
                    targetSelector: $this.attr("data-target") || this,
                    scrollerContainer: $this.attr("data-scroller-container") || null
                }
                var $scroller = null;
                if (options.scrollerContainer != null) {
                    $scroller = $(options.scrollerContainer);
                }
                if ($scroller == null || $scroller.length == 0) {
                    $scroller = $(window);
                }
                var isInit = eval($scroller.attr("data-isinit-scroll-fixtop")) || false;
                if (isInit) {
                    return;
                }
                $scroller.attr("data-isinit-scroll-fixtop", "true");
                $scroller.scroll(function () {
                    $(".fixed-top").each(function () {
                        var $target = $(options.targetSelector);
                        var scrollTop = $scroller.scrollTop();
                        var $this = $(this);
                        var i = 0;
                        for (; i < $target.length; i++) {
                            var $t = $target.eq(i);
                            var targetTop = $t.position().top;
                            if (scrollTop <= targetTop) {
                                if (i == 0) {
                                    $this.addClass("hide");
                                    return;
                                }
                                else {
                                    $this.html($target.eq(i - 1).html());
                                    break;
                                }
                            } else {
                                if ($this.hasClass("hide")) {
                                    $this.removeClass("hide");
                                }
                            }
                        }
                        if (i == $target.length) {
                            $this.html($target.eq(i - 1).html());
                        }
                    });
                });
            });
        }
    }
    $.stone.openDetail = function (obj) {
        var $this = $(obj);
        var options = {
            url: $this.attr("data-url") || null,
        };
        if (options.url == null) {
            return false;
        }
        window.location.href = options.url;
    }
    $.stone.loadTabList = function (tabSwiper, $content, isScroll) {
        var isloaded = $content.attr("data-isloaded") || false;
        if (isloaded && !isScroll) {
            return;
        }
        var data = $.stone.methods.getData($content);
        if (data.pagercommand != "next") {
            data.pagercommand = "next";
        }
        var callBack = eval($content.attr("data-callback")) || null;
        $.stone.loadScrollList({
            $container: $content,
            url: $content.attr("data-url"),
            jsTemplate: $content.attr("data-jstemplate"),
            data: data,
            callback: function () {
                if (!isloaded) {
                    $content.attr("data-isloaded", "true");
                }
                if ($.isFunction(callBack)) {
                    callBack(tabSwiper, $content, data);
                }
                setTimeout(function () {
                    tabSwiper.onResize();
                }, 50);
            }
        });
    }
    $.stone.loadScrollList = function (options) {
        var $scrollContainer = options.$container;
        var isloading = eval($scrollContainer.attr("data-isloading")) || false;
        var isScrolling = eval($scrollContainer.attr("data-isscrolling")) || false;
        var nomore = eval($scrollContainer.attr("data-nomore")) || false;
        if (isScrolling || isloading || nomore) {
            return;
        }
        $scrollContainer.attr("data-isloading", "true");
        options.isAppend = eval($scrollContainer.attr("data-isappend")) || false;
        var $maskContainer = $(options.maskContainer);
        if (!options.isAppend && options.showMask == true) {
            var $mask = $.stone.methods.showMask({
                $container: $maskContainer,
                showRefresh: true
            });
        }
        var $noMore = $scrollContainer.find(".no-more");
        $noMore.html("正在加载").show();
        $.stone.ajax({
            type: "post",
            url: options.url,
            data: options.data,
            cache: false,
            success: function (data) {
                if (data.statusCode == $.stone.constants.statusCode.Succeed) {
                    if (data.Data.list.length > 0) {
                        $scrollContainer.attr("data-args-pageindex", data.Data.EnPageIndex);
                        var escape = eval($("#" + options.jsTemplate).attr("data-escape"));
                        if (escape == false) {
                            template.defaults.escape = false;
                        }
                        if (options.isAppend) {
                            if ($.isFunction(options.appendFun)) {
                                options.appendFun($scrollContainer, data.Data)
                            }
                            else {
                                var html = template(options.jsTemplate, data.Data);
                                $scrollContainer.append(html);
                            }
                        }
                        else {
                            var html = template(options.jsTemplate, data.Data);
                            $scrollContainer.html(html);
                            $scrollContainer.attr("data-isappend", "true");
                        }
                    }
                    if (!data.Data.HasNext) {
                        $scrollContainer.attr("data-nomore", "true");
                        if ($noMore.length == 0) {
                            $noMore = $('<div class="no-more"></div>');
                            $scrollContainer.append($noMore);
                        }
                        var isShowMessage = eval($scrollContainer.attr("data-message-show") || "true");
                        if (isShowMessage) {
                            if (options.isAppend || data.Data.list.length > 0) {
                                $noMore.html("").show();
                            }
                            else {
                                var html = $scrollContainer.attr("data-message-none") || "没有数据";
                                $noMore.html(html).show();
                            }
                        }
                        else {
                            $noMore.hide();
                        }
                    }
                    if ($.isFunction(options.callback)) {
                        options.callback($scrollContainer, options, data);
                    }
                }
            },
            error: function (data) {
                if (data.message) {
                    $.stone.showAlert({
                        message: data.message,
                        maskContainer: options.maskContainer
                    });
                }
                else if (data.statusText) {
                    $.stone.showAlert({
                        message: data.statusText,
                        maskContainer: options.maskContainer
                    });
                }
                else {
                    $.stone.showAlert({
                        message: data,
                        maskContainer: options.maskContainer
                    });
                }
            },
            complete: function () {
                $scrollContainer.attr("data-isloading", "false");
                if (!options.isAppend && options.showMask == true) {
                    $.stone.methods.hideMask({
                        $container: $maskContainer,
                        showRefresh: true
                    });
                }
            }
        });
    }
    $.stone.loadTemplateDetail = function (options) {
        $(".load-template").each(function () {
            var $this = $(this);

        });
    }
    $.stone.setNotScroll = function () {
        var $body = $("body");
        var level = Number($body.attr("data-overflow-level")) || 0;
        $body.css({ "overflow": "hidden" });
        level++;
        $body.attr("data-overflow-level", level);
    }
    $.stone.clearScroll = function () {
        var $body = $("body");
        $body.css({ "overflow": "auto" });
        $body.attr("data-overflow-level", "0");
    }
    $.stone.setScroll = function () {
        var $body = $("body");
        var level = Number($body.attr("data-overflow-level")) || 1;
        level--;
        $body.attr("data-overflow-level", level);
        if (level == 0) {
            $body.css({ "overflow": "auto" });
        }
    };
    $.stone.showScrollWrapper = function (wrapperSelector, $sender) {
        var $wrapper = $(wrapperSelector);
        var $container = null;
        if (typeof $sender != "undefined") {
            $container = $sender.closest(".wrapper");
        }
        var $mask = $.stone.methods.showMask({
            $container: $container
        });
        if ($wrapper.css("position") == "static") {
            $wrapper.addClass("wrapper-overlay");
        }
        var winHeight = $(window).height();
        var footerHeight = $wrapper.find(".wrapper-footer").height();
        $wrapper.find(".wrapper-body").css({ "min-height": winHeight - footerHeight + 1 });
        $wrapper.removeClass("hide");
        $wrapper.data("source-mask", $mask);
        $.stone.setNotScroll();
    }
    $.stone.hideScrollWrapper = function (wrapperSelector) {
        var $wrapper = $(wrapperSelector);
        var $mask = $wrapper.data("source-mask") || null;
        if ($mask != null) {
            $.stone.methods.hideMask({
                $container: $mask.parent()
            });
        }
        $wrapper.removeClass("wrapper-overlay").addClass("hide");
        $.stone.setScroll();
    }
    $.stone.statBadge = function () {
        $(".badge-container").each(function () {
            var $this = $(this);
            var options = {
                url: $this.attr("data-badge-url") || "",
                showZero: eval($this.attr("data-badge-showzero")) || false
            };
            if (options.url == "") {
                return;
            }
            $.stone.ajax({
                url: options.url,
                data: $.stone.methods.getData($this),
                success: function (data) {
                    if (data.statusCode == $.stone.constants.statusCode.Succeed) {
                        for (var i in data.Data.dict) {
                            var item = data.Data.dict[i];
                            var $badge = $(".badge-" + item.Key);
                            if (item.Value > 0) {
                                $badge.html(item.Value).removeClass("zero").addClass("active");
                            }
                            else {
                                if (options.showZero) {
                                    $badge.html(item.Value).addClass("zero").addClass("active");
                                }
                                else {
                                    $badge.html("").addClass("zero").removeClass("active");
                                }
                            }
                        }
                    }
                }
            });
        });
    }
    $.stone.initImageUpload = function () {
        $(".image-upload").each(function () {
            var $upload = $(this);
            var $img = $upload.find("img");
            var $file = $upload.find("input[type=file]");
            $img.click(function () {
                $file.trigger("click");
            });
            $file.change(function (event) {
                if ($file.val() == "") {
                    return;
                }
                var options = {
                    url: $upload.attr("data-url"),
                    showMask: eval($upload.attr("data-mask")) || false,
                    maskContainer: $upload.attr("data-mask-container") || "body",
                    precall: eval($upload.attr("data-precall")),
                    defaultImg: $upload.attr("data-defaultimg")
                };
                if ($.isFunction(options.precall)) {
                    if (!options.precall($upload, event)) {
                        return false;
                    }
                }
                var $img = $upload.find("img");
                var $imagePath = $upload.find(".image-upload-url");
                var $maskContainer = $(options.maskContainer);
                var $mask = $.stone.methods.showMask({
                    $container: $maskContainer,
                    showRefresh: true
                });
                var file = $file[0].files[0];
                var fileReader = new FileReader();
                fileReader.onload = function (file) {
                    compressImg(this.result, function (imgData) {//压缩完成后执行的callback
                        $img.attr("src", imgData);
                        $.stone.ajax({
                            type: 'POST',
                            url: options.url,
                            data: { "Image": imgData },
                            dataType: 'json',
                            cache: false,
                            success: function (data, status) {
                                if (data.statusCode == $.stone.constants.statusCode.Succeed) {
                                    $imagePath.val(data.Data.url);
                                }
                                else {
                                    $.stone.showAlert({
                                        message: data.message,
                                        maskContainer: options.maskContainer
                                    });
                                    $img.attr("src", options.defaultImg);
                                }
                            },
                            xhr: function () {
                                var xhr = $.ajaxSettings.xhr();
                                var $progress = $('<div class="image-upload-progress"></div>');
                                var $progressBar = $('<div class="image-upload-progress-bar"></div>');
                                $progress.append($progressBar);
                                var $progressPercent = $('<div class="image-upload-progress-percent"></div>');
                                $progress.append($progressPercent);
                                $upload.append($progress);
                                xhr.upload.addEventListener('progress', function (evt) {
                                    if (evt.lengthComputable) {
                                        var width = evt.loaded * 100 / evt.total + "%";
                                        $progressBar.width(width);
                                        $progressPercent.html(width);
                                        setTimeout(function () {
                                            $progress.remove();
                                        }, 2000)
                                    }
                                }, false);
                                return xhr;
                            },
                            error: function (data, status, e) {
                                if (data.Data) {
                                    $.stone.showAlert({
                                        message: data.message,
                                        maskContainer: options.maskContainer
                                    });
                                }
                                else if (data.statusText) {
                                    $.stone.showAlert({
                                        message: data.statusText,
                                        maskContainer: options.maskContainer
                                    });
                                }
                                else {
                                    $.stone.showAlert({
                                        message: data,
                                        maskContainer: options.maskContainer
                                    });
                                }
                            },
                            complete: function (XMLHttpRequest, textStatus) {
                                $.stone.methods.hideMask({
                                    $container: $mask.parent(),
                                    showRefresh: true
                                });
                            }
                        });
                    });
                };
                fileReader.readAsDataURL(file);
                $file.val("");
                function compressImg(imgData, onCompress) {
                    if (!imgData)
                        return;
                    onCompress = onCompress || function () { };
                    var canvas = document.createElement('canvas');
                    var img = new Image();
                    img.onload = function () {
                        var ctx = canvas.getContext("2d");
                        canvas.width = img.width;
                        canvas.height = img.height;
                        ctx.clearRect(0, 0, img.width, img.height); // canvas清屏
                        //重置canvans宽高 canvas.width = img.width; canvas.height = img.height;
                        ctx.drawImage(img, 0, 0, img.width, img.height); // 将图像绘制到canvas上 
                        onCompress(canvas.toDataURL("image/jpeg"));//必须等压缩完才读取canvas值，否则canvas内容是黑帆布
                    };
                    // 记住必须先绑定事件，才能设置src属性，否则img没内容可以画到canvas
                    img.src = imgData;
                }
            });
        });
    };
    $.stone.initSameScaleImage = function (options) {
        var defaultOptions = {
            $container: $("body")
        };
        if (typeof options == "undefined") {
            options = defaultOptions;
        }
        else {
            $.stone.extend(options, defaultOptions);
        }
        if (options.$container.length == 0) {
            options.$container = $("body")
        }
        options.$container.find("img.same-scale").each(function () {
            var $this = $(this);
            var width = $this.width();
            $this.height(width);
        });
    }
    $.stone.initScroller = function () {
        $(".js-scroller").each(function () {
            var $this = $(this);
            $.stone.methods.loadJsScroller($this);
        });
    };
    $.stone.methods = (function () {
        return {
            getSearchFieldData: function ($sender) {
                var $container = $sender.closest(".search-container");
                var data = {};
                $container.find(".search-field").each(function () {
                    var $field = $(this);
                    var name = $field.attr("name");
                    var value = $field.val();
                    if (data.hasOwnProperty(name)) {
                        data[name] = data[name] + "," + value;
                    }
                    else {
                        data[name] = value;
                    }
                });
                return data;
            },
            getData: function ($source) {
                var attrs = $source[0].attributes;
                var data = {};
                if (attrs.length > 0) {
                    for (var i in attrs) {
                        var attr = attrs[i];
                        if (attr.name && attr.name.indexOf("data-args-") == 0) {
                            var name = attr.name.substring(10, attr.name.length);
                            data[name] = unescape(attr.value);
                        }
                    }
                }
                return data;
            },
            setData: function ($target, data) {
                for (var property in data) {
                    $target.attr("data-args-" + property, escape(data[property]));
                }
            },
            showMask: function (options) {
                var defaultOptions = {
                    type: "",
                    $container: $("body"),
                    showRefresh: false,
                    maskClass: "ajax-mask",
                    oneClick: null
                }
                if (typeof options == "undefined") {
                    options = defaultOptions;
                }
                else {
                    $.stone.extend(options, defaultOptions);
                }
                if (options.$container == null || options.$container.length == 0) {
                    options.$container = $("body");
                }
                if (!(options.$container.css("position") == "relative"
                    || options.$container.css("position") == "fixed")) {
                    options.$container.addClass("position-relative");
                }
                var $mask = $('<div class="mask"></div>');
                $mask.addClass(options.maskClass);
                var containerHeight = options.$container.height();
                var containerWidth = options.$container.width();
                $mask.css({
                    "z-index": 10,
                    position: "fixed",
                    background: "#000",
                    opacity: 0.3,
                    left: 0,
                    top: 0,
                    right: 0,
                    bottom: 0
                });
                options.$container.append($mask);
                if ($.isFunction(options.oneClick)) {
                    $mask.one("click", function () {
                        options.oneClick($mask, options);
                    });
                }
                if (options.showRefresh) {
                    var winHeight = $(window).height();
                    var winWidth = $(window).width();
                    var $refresh = $('<div class="mask-refresh"><img src="/Content/Base/images/loading.gif" style="width:40px;max-width:100%;height:40px;max-height:100%;color:red;" /></div>')
                    var height = $refresh.height();
                    var width = $refresh.width();
                    $refresh.css({
                        "z-index": 11,
                        position: "fixed",
                        top: (winHeight - 80) / 2,
                        left: (winWidth - 80) / 2
                    });
                    options.$container.append($refresh);
                }
                return $mask;
            },
            hideMask: function (options) {
                var defaultOptions = {
                    $container: $("body"),
                    maskClass: "ajax-mask"
                };
                if (typeof options == "undefined") {
                    options = defaultOptions;
                }
                else {
                    $.stone.extend(options, defaultOptions);
                }
                if (options.$container == null || options.$container.length == 0) {
                    options.$container = $("body");
                }
                options.$container.find("." + options.maskClass).last().remove();
                if (options.showRefresh) {
                    options.$container.find(".mask-refresh").last().remove();
                }
                if (options.$container.find("." + options.maskClass).length == 0) {
                    options.$container.removeClass("position-relative");
                }
            },
            loadJsPager: function ($jsPager) {
                var $this = $jsPager;
                var options = {
                    url: $this.attr("data-url") || "",
                    containerSelector: $this.attr("data-container") || null,
                    jsTemplate: $this.attr("data-jstemplate") || "",
                    data: $.stone.methods.getData($this),
                    showMask: eval($this.attr("data-mask")) || false,
                    maskContainer: $this.attr("data-mask-container") || "body",
                    callback: eval($this.attr("data-callback")) || null,
                    noneMessage: $this.attr("data-message-none") || "没有数据",
                    autoRefresh: eval($this.attr("data-autorefresh")) || false,
                    isAppend: eval($this.attr("data-isappend")) || false
                };
                var $container = null;
                if (options.containerSelector != null) {
                    $container = $(options.containerSelector);
                }
                else {
                    $container = $this;
                }
                post();
                function post() {
                    var isloading = eval($container.attr("data-isloading")) || false;
                    if (isloading) {
                        return;
                    }
                    $container.attr("data-isloading", "true");
                    var $maskContainer = $(options.maskContainer);
                    var $mask;
                    if (options.showMask == true) {
                        $mask = $.stone.methods.showMask({
                            $container: $maskContainer,
                            showRefresh: true
                        });
                    }
                    $.stone.ajax({
                        url: options.url,
                        data: options.data,
                        success: function (data) {
                            if (data.statusCode == $.stone.constants.statusCode.Succeed) {
                                $this.attr("data-args-pageindex", data.Data.EnPageIndex);
                                options.data.pageindex = data.Data.EnPageIndex;
                                options.data.pagecount = data.Data.EnPageCount;
                                var escape = eval($("#" + options.jsTemplate).attr("data-escape"));
                                if (escape == false) {
                                    template.defaults.escape = false;
                                }
                                var html = template(options.jsTemplate, data.Data);
                                if (options.isAppend) {
                                    $container.find(".js-pager-more").remove();
                                    $container.append(html);
                                    $container.find(".next:not(.disabled)").click(function () {
                                        options.data.pagercommand = "next";
                                        post();
                                    });
                                }
                                else {
                                    $container.html(html);
                                    $container.find(".first:not(.disabled)").click(function () {
                                        options.data.pagercommand = "first";
                                        options.data.pageindex = "";
                                        post();
                                    });
                                    $container.find(".prev:not(.disabled)").click(function () {
                                        options.data.pagercommand = "prev";
                                        post();
                                    });
                                    $container.find(".next:not(.disabled)").click(function () {
                                        options.data.pagercommand = "next";
                                        post();
                                    });
                                    $container.find(".last:not(.disabled)").click(function () {
                                        options.data.pagercommand = "";
                                        options.data.pageindex = options.data.pagecount;
                                        post();
                                    });
                                }
                                if ($.isFunction(options.callback)) {
                                    options.callback($this, options, data);
                                }
                            }
                        },
                        error: $.stone.methods.ajaxError,
                        complete: function () {
                            $container.attr("data-isloading", "false");
                            if (options.showMask == true && $mask) {
                                $.stone.methods.hideMask({
                                    $container: $mask.parent(),
                                    showRefresh: true
                                });
                            }
                        }
                    });
                }
            },
            loadJsForm: function ($jsForm) {
                var options = {
                    url: $jsForm.attr("data-url") || "",
                    setTitle: eval($jsForm.attr("data-settitle")) || false,
                    containerSelector: $jsForm.attr("data-container") || null,
                    jsTemplate: $jsForm.attr("data-jstemplate") || "",
                    data: $.stone.methods.getData($jsForm),
                    showMask: eval($jsForm.attr("data-mask")) || false,
                    callback: eval($jsForm.attr("data-callback")) || null,
                    replaceFun: eval($jsForm.attr("data-replacefun")) || null,
                    noneMessage: $jsForm.attr("data-message-none") || "数据加载失败"
                };
                var $container = null;
                if (options.containerSelector != null) {
                    $container = $(options.containerSelector);
                }
                else {
                    $container = $jsForm;
                }
                post();
                function post() {
                    var isLoaded = eval($container.attr("data-isloaded")) || false;
                    if (isLoaded) {
                        return;
                    }
                    var isloading = eval($container.attr("data-isloading")) || false;
                    if (isloading) {
                        return;
                    }
                    $container.attr("data-isloading", "true");
                    var $mask;
                    if (options.showMask == true) {
                        $mask = $.stone.methods.showMask({
                            $container: $container,
                            showRefresh: true
                        });
                    }
                    $.stone.ajax({
                        url: options.url,
                        data: options.data,
                        success: function (data) {
                            if (data.statusCode == $.stone.constants.statusCode.Succeed) {
                                var escape = eval($("#" + options.jsTemplate).attr("data-escape"));
                                if (escape == false) {
                                    template.defaults.escape = false;
                                }
                                var html = "";
                                if ($.isFunction(options.replaceFun)) {
                                    html = options.replaceFun(options.jsTemplate, data.Data);
                                }
                                else {
                                    var html = template(options.jsTemplate, data.Data);
                                }
                                $container.html(html);
                                $container.attr("data-isloaded", "true");
                                if (options.setTitle && data.Data.HTML) {
                                    $("title").html(data.Data.HTML.title);
                                }
                                if ($.isFunction(options.callback)) {
                                    options.callback($jsForm, options, data);
                                }
                            }
                            else {
                                $container.html('<div class="jsform-error">' + data.message + '</div>');
                            }
                        },
                        error: $.stone.methods.ajaxError,
                        complete: function () {
                            $container.attr("data-isloading", "false");
                            if (options.showMask == true && $mask) {
                                $.stone.methods.hideMask({
                                    $container: $mask.parent(),
                                    showRefresh: true
                                });
                            }
                        }
                    });
                }
            },
            loadJsScroller: function ($jsScroller) {
                var options = {
                    height: Number($jsScroller.attr("data-height")) || 80,
                    url: $jsScroller.attr("data-url") || null,
                    jsTemplate: $jsScroller.attr("data-jstemplate") || null,
                    autoLoad: eval($jsScroller.attr("data-autoload")) || false,
                    appendFun: eval($jsScroller.attr("data-appendfun")) || null,
                    callback: eval($jsScroller.attr("data-callback")) || null,
                    scrollerContainer: $jsScroller.attr("data-scroller-container") || null,
                    showMask: eval($jsScroller.attr("data-mask")) || true,
                    maskContainer: $jsScroller.attr("data-mask-container") || "body"
                };
                loadData("none");
                var $scroller = null;
                if (options.scrollerContainer != null) {
                    $scroller = $(options.scrollerContainer);
                }
                if ($scroller == null || $scroller.length == 0) {
                    $scroller = $(window);
                }
                var isInit = eval($scroller.attr("data-isinit-scroll")) || false;
                if (isInit) {
                    return;
                }
                $scroller.attr("data-isinit-scroll", "true");
                $scroller.scroll(function () {
                    var scrollTop = $(window).scrollTop();
                    var scrollHeight = $(document).height();
                    var windowHeight = $(window).height();
                    if (scrollHeight - scrollTop - windowHeight < options.height) {
                        loadData("next");
                    }
                });
                function loadData(command) {
                    var data = $.stone.methods.getData($jsScroller);
                    data.pagercommand = command;
                    $.stone.loadScrollList({
                        $container: $jsScroller,
                        showMask: options.showMask,
                        maskContainer: options.maskContainer,
                        url: options.url,
                        jsTemplate: options.jsTemplate,
                        data: data,
                        appendFun: options.appendFun,
                        callback: options.callback
                    });
                }
            },
            lazyLoadFile: function (items) {
                items.forEach(function (item) {
                    var element = document.getElementById(item.id);
                    if (!element) {
                        if (item.type == "js") {
                            var script = document.createElement("script");
                            script.id = item.id;
                            script.src = item.url;
                            script.onload = script.onreadystatechange = function () {
                                if (!this.readyState || this.readyState === "loaded" || this.readyState === "complete") {
                                    if (item.subs && item.subs.length > 0) {
                                        $.stone.methods.lazyLoadFile(item.subs);
                                    }
                                    if ($.isFunction(item.callback)) {
                                        item.callback(item.args);
                                    }
                                    script.onload = script.onreadystatechange = null;
                                }
                            };
                            document.body.appendChild(script);
                        }
                        else if (item.type == "css") {
                            var link = document.createElement("link");
                            link.id = item.id;
                            link.rel = "stylesheet";
                            link.href = item.url;
                            if (item.subs && item.subs.length > 0) {
                                $.stone.methods.lazyLoadFile(item.subs);
                                link.onload = link.onreadystatechange = null;
                            }
                            $("head")[0].appendChild(link);
                        }
                    }
                    else {
                        if (item.subs && item.subs.length > 0) {
                            $.stone.methods.lazyLoadFile(item.subs);
                        }
                        if ($.isFunction(item.callback)) {
                            item.callback(item.args);
                        }
                    }
                });
            },
            ajaxError: function (data) {
                if (data.message && data.message.length > 0) {
                    $.stone.showMessage({ message: data.message });
                }
                else if (data.statusText) {
                    $.stone.showMessage({ message: data.statusText });
                }
                else {
                    $.stone.showMessage({ message: "操作失败" });
                }
            },
            remove: function ($sender, containerSelector) {
                $sender.closest(containerSelector).remove();
            }
        };
    })();
    $.stone.empty = function () {

    }
})($);