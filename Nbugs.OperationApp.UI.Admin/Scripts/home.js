﻿$(function () {
    $('#tab_menu-tabrefresh').click(function () {
        /*重新设置该标签 */
        var url = $(".tabs-panels .panel").eq($('.tabs-selected').index()).find("iframe").attr("src");
        $(".tabs-panels .panel").eq($('.tabs-selected').index()).find("iframe").attr("src", url);
    });
    //在新窗口打开该标签
    $('#tab_menu-openFrame').click(function () {
        var url = $(".tabs-panels .panel").eq($('.tabs-selected').index()).find("iframe").attr("src");
        window.open(url);
    });
    //关闭当前
    $('#tab_menu-tabclose').click(function () {
        var currtab_title = $('.tabs-selected .tabs-inner span').text();
        $('#mainTab').tabs('close', currtab_title);
        if ($(".tabs li").length == 0) {
            //open menu
            $(".layout-button-right").trigger("click");
        }
    });
    //全部关闭
    $('#tab_menu-tabcloseall').click(function () {
        $('.tabs-inner span').each(function (i, n) {
            if ($(this).parent().next().is('.tabs-close')) {
                var t = $(n).text();
                $('#mainTab').tabs('close', t);
            }
        });
        //open menu
        $(".layout-button-right").trigger("click");
    });
    //关闭除当前之外的TAB
    $('#tab_menu-tabcloseother').click(function () {
        var currtab_title = $('.tabs-selected .tabs-inner span').text();
        $('.tabs-inner span').each(function (i, n) {
            if ($(this).parent().next().is('.tabs-close')) {
                var t = $(n).text();
                if (t != currtab_title)
                    $('#mainTab').tabs('close', t);
            }
        });
    });
    //关闭当前右侧的TAB
    $('#tab_menu-tabcloseright').click(function () {
        var nextall = $('.tabs-selected').nextAll();
        if (nextall.length == 0) {
            $.messager.alert('提示', '前面没有了!', 'warning');
            return false;
        }
        nextall.each(function (i, n) {
            if ($('a.tabs-close', $(n)).length > 0) {
                var t = $('a:eq(0) span', $(n)).text();
                $('#mainTab').tabs('close', t);
            }
        });
        return false;
    });
    //关闭当前左侧的TAB
    $('#tab_menu-tabcloseleft').click(function () {
        var prevall = $('.tabs-selected').prevAll();
        if (prevall.length == 0) {
            $.messager.alert('提示', '后面没有了!', 'warning');
            return false;
        }
        prevall.each(function (i, n) {
            if ($('a.tabs-close', $(n)).length > 0) {
                var t = $('a:eq(0) span', $(n)).text();
                $('#mainTab').tabs('close', t);
            }
        });
        return false;
    });
});
$(function () {
    /*为选项卡绑定右键*/
    $(".tabs li").live('contextmenu', function (e) {
        /*选中当前触发事件的选项卡 */
        var subtitle = $(this).text();
        $('#mainTab').tabs('select', subtitle);
        //显示快捷菜单
        $('#tab_menu').menu('show', {
            left: e.pageX,
            top: e.pageY
        });
        return false;
    });
});

function addTab(subtitle, url, icon) {
    if (!$("#mainTab").tabs('exists', subtitle)) {
        $("#mainTab").tabs('add', {
            title: subtitle,
            content: createFrame(url),
            closable: true,
            icon: icon
        });
    } else {
        $("#mainTab").tabs('select', subtitle);
        $("#tab_menu-tabrefresh").trigger("click");
    }
    //$(".layout-button-left").trigger("click");
    //tabClose();
}
function createFrame(url) {
    var s = '<iframe frameborder="0" src="' + url + '" scrolling="auto" style="width:100%; height:99%"></iframe>';
    return s;
}

$(function () {
    $(".ui-skin-nav .li-skinitem span").click(function () {
        var theme = $(this).attr("rel");
        $.messager.confirm('提示', '切换皮肤将重新加载系统！', function (r) {
            if (r) {
                $.post("../../Home/SetThemes", { value: theme }, function (data) { window.location.reload(); }, "json");
            }
        });
    });
});
$(function () {
    jQuery("#RightAccordion").accordion({ //初始化accordion
        fillSpace: true,
        fit: true,
        border: false,
        animate: false
    });
    $.post("../Account/GetTree", { "id": "0" }, //获取第一层目录
       function (data) {
           if (data == "0") {
               window.location = "/Home";
           }
           $.each(data, function (i, e) {//循环创建手风琴的项
               var id = e.id;
               $('#RightAccordion').accordion('add', {
                   title: e.text,
                   content: "<ul id='tree" + id + "' ></ul>",
                   selected: true,
                   iconCls: e.iconCls
               });
               $.parser.parse();
               $.post("../Account/GetTree?id=" + id, function (data) {//循环创建树的项
                   $("#tree" + id).tree({
                       data: data,
                       onBeforeExpand: function (node, param) {
                           $("#tree" + id).tree('options').url = "../Account/GetTree?id=" + node.id;
                       },
                       onClick: function (node) {
                           if (node.state == 'closed') {
                               $(this).tree('expand', node.target);
                           } else if (node.state == 'open') {
                               $(this).tree('collapse', node.target);
                           } else {
                               var tabTitle = node.text;
                               var url = "../../" + node.attributes;
                               var icon = node.icon;
                               addTab(tabTitle, url, icon);
                           }
                       }
                   });
               }, 'json');
           });
       }, "json");
});
//$(function () {
//    var o = {
//        showcheck: false,
//        url: "/Account/GetTree",
//        onnodeclick: function (item) {
//            var tabTitle = item.text;
//            var url = "../../" + item.value;
//            var icon = item.Icon;
//            if (!item.hasChildren) {
//                addTab(tabTitle, url, icon);
//            } else {
//                $(this).parent().find("img").trigger("click");
//            }
//        }
//    }
//    $.post("/Account/GetTree", { id: "0" },
//        function (data) {
//            o.data = data;
//            $("#RightTree").treeview(o);
//        }, "json");
//});