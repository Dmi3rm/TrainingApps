/*                                                                       ПЕРЕМЕЩЕНИЕ                                                                         */

$("#FItems tbody").sortable({

    update: function (event, ui) {
        UpdateOrder();
    }

}).disableSelection();


function UpdateOrder()
{
    var sortedIds = $("#FItems tbody").sortable("toArray");
    var link = "";
    var b = document.getElementById('FItems');
    var a = b.getAttribute("data-fsectionId");
    
    if ( a != 0)
    {
        link = "SetNewFSubsectionsOrder?fsectionId=" + a + "&arr=";
    }
    else
    {
        link = "SetNewFSectionsOrder?arr="
    }

    for (var i = 0; i < sortedIds.length; i++)
    {
        link = link + sortedIds[i] + ",";
    }
    

    jQuery.ajax(
            {
                type: "POST",
                url: link,
            })

}



function GoToPage() {
    var a = [];
    a = $('[class="GoPageInput"]');
    if ((!isNaN(a[0].value)) && (a[0].value != ""))
    {
        document.location.href = CreatePageLink(a[0].value);
    }


    if ((!isNaN(a[1].value))  && (a[1].value != ""))
    {
        document.location.href = CreatePageLink(a[1].value);
    }
}


function CreatePageLink(page)
{
    var obj = document.getElementById('FMessages');
    if (obj != null)
    {
        link = "FMessages?sectionId=" + obj.getAttribute("data-sectionId") + "&&subsectionId=" + obj.getAttribute("data-subsectionId") +
            "&&topicId=" + obj.getAttribute("data-topicId") + "&&currentPage=" + page;
        return link;
    }
    else
    {
        obj = document.getElementById('FTopics');
        link = "FTopics?sectionId=" + obj.getAttribute("data-sectionId") + "&&subsectionId=" + obj.getAttribute("data-subsectionId") +
             "&&currentPage=" + page;
        return link;
    }
}


/*-----------------------------------------------------------------УДАЛЕНИЕ И РЕДАКТИРОВАНИЕ--------------------------------------------------------------*/

var pressedEditBtn;
var pressedDeleteBtn;


function DefineType(item)
{
    if (item.getAttribute("data-ftopicId") != null)
        return 3;
    if (item.getAttribute("data-fsubsectionId") != null)
        return 2;
    if (item.getAttribute("data-fsectionId") != null)
        return 1;
    return 0;
}


function GetId(item)
{
    if (item.getAttribute("data-ftopicId") != null)
        return item.getAttribute("data-ftopicId");
    if (item.getAttribute("data-fsubsectionId") != null)
        return item.getAttribute("data-fsubsectionId");
    if (item.getAttribute("data-fsectionId") != null)
        return item.getAttribute("data-fsectionId");
    return 0;
}


function CreateDeleteLink(item)
{
    var link;
    var type = DefineType(item);
    if (type == 3)
    {
        link = "DeleteFTopic?topicId=" + item.getAttribute('data-ftopicId') + "&&subsectionId=" + item.getAttribute('data-fsubsectionId') + "&&sectionId=" +
            item.getAttribute('data-fsectionId');
    }
    if (type == 2)
    {
        link = "DeleteFSubsection?subsectionId=" + item.getAttribute('data-fsubsectionId') + "&&sectionId=" + item.getAttribute('data-fsectionId');
    }
    if (type == 1)
    {
        link = "DeleteFSection?sectionId=" + item.getAttribute('data-fsectionId');
    }
    return link;
}


function EditItem(btn)
{
    pressedEditBtn = btn;
    $("#Dialog").dialog();
    $("#Dialog").draggable();
    var title = "<button id=\"CloseDialogBtn\" onclick=\"CloseDialog()\">x</button><center>" + btn.getAttribute("data-Name")  +"</center>";
    $("#DialogTitle").html(title);
    if (btn.getAttribute('data-fix') == 0) { $("#FixTopicBtn").html("Закрепить"); }
    else { $("#FixTopicBtn").html("Открепить"); }
}


function CloseDialog()
{
    $("#Dialog").dialog("close");
    pressedEditBtn = null;
}


function CloseRenameDialog()
{
    $("#RenameDialog").dialog("close");
}


function MsgDialog(title, body)
{
    $("#MsgDialog").dialog();
    $("#MsgDialog").draggable();
    var titleStr = "<button id=\"CloseDialogBtn\" onclick=\"CloseMsgDialog()\">x</button><center>" + title + "</center>";
    $("#MsgDialogTitle").html(titleStr);
    $("#MsgDialog").append(body);
}


function CloseMsgDialog()
{
    $("#MsgDialog").dialog("close");
    $("#MsgDialog").empty().append("<div id=\"MsgDialogTitle\" class=\"DialogTitle\"></div>");
    pressedDeleteBtn = null;
}



function RenameItem()
{
    $("#RenameDialog").dialog();
    $("#RenameDialog").draggable();
    var title = "<button id=\"CloseDialogBtn\" onclick=\"CloseRenameDialog()\">x</button><center>Переименовать</center>";
    $("#RenameDialogTitle").html(title);
    $("#RenameItemInput").val(pressedEditBtn.getAttribute("data-Name"));
}


function RenameFinally()
{
    var type = DefineType(pressedEditBtn);
    var Name = $("#RenameItemInput").val();
    var itemId = GetId(pressedEditBtn);

    var link = "RenameFItem?type=" + type + "&&itemId=" + itemId + "&&name=" + Name;

    jQuery.ajax({
        type: "POST",
        url: link,
        success: function () {
            CloseRenameDialog();
            CloseDialog();
            location.reload();
        }
    })
}




function RememberItem()
{
    var type = DefineType(pressedEditBtn);
    var itemId = GetId(pressedEditBtn);

    var link = "RememberFItem?type=" + type + "&&itemId=" + itemId;

    jQuery.ajax({
        type: "POST",
        url: link,
        success: function () {
            CloseDialog();
            MsgDialog("Успешно", "Готово к вставке");
        }
    })
}


function InputItem()
{
    jQuery.ajax({
        type: "POST",
        url: "InputFItem?inputPlaceId=" + GetId(pressedEditBtn),
        success: function () {
            CloseDialog();
            MsgDialog("Успешно", "Перемещено");
        }
    })
}


function DeleteItem(btn)
{
    pressedDeleteBtn = btn;
    var type = DefineType(btn);
    var itemId = GetId(btn);
    var html = "<center style=\"margin-top:10px;\">Вы уверены?</center><br/><button class=\"DeleteLastBtn\" onclick=\"DeleteItemPost()\">Удалить</button><button class=\"DeleteCancelBtn\" onclick=\"CloseMsgDialog()\">Отмена</button> "
    MsgDialog("Подтверждение", html);
}


function DeleteItemPost() {
    var type = DefineType(pressedDeleteBtn);
    var itemId = GetId(pressedDeleteBtn);

    jQuery.ajax({
        type: "POST",
        url: CreateDeleteLink(pressedDeleteBtn),
        success: function () { location.reload(); }
    })
    
}


/*Сообщения*/


function DeleteMsg(btn)
{
    jQuery.ajax({
        type: "POST",
        url: "DeleteFMessage?messageId=" + btn.getAttribute('data-fmessageId') + "&&topicId=" + btn.getAttribute('data-ftopicId') + "&&subsectionId=" + btn.getAttribute('data-fsubsectionId') + "&&sectionId=" + btn.getAttribute('data-fsectionId'),
        success: function () {
            location.reload();
        }
    })
}


var pressedEditMsgBtn;


function EditMsg(btn)
{
    pressedEditMsgBtn = btn;
    $("#EditMsgDialog").dialog();
    $("#EditMsgDialog").draggable();
    var title = "<button id=\"CloseDialogBtn\" onclick=\"CloseEditMsgDialog()\">x</button><center>Изменить сообщение</center>";
    $("#EditMsgDialogTitle").html(title);

    jQuery.ajax({
        type: "GET",
        url: "EditFMessage?messageId=" + btn.getAttribute('data-fmessageId'),
        success: function (text)
        {
            $("#MsgEditTextarea").val(text);
        }
    })
}


function CloseEditMsgDialog()
{
    $("#EditMsgDialog").dialog("close");
    pressedEditMsgBtn = null;
}


function EditMsgOk()
{
    if ($("#MsgEditTextarea").val() != "")
    {
        jQuery.ajax({
            type: "POST",
            url: "EditFMessage?messageId=" + pressedEditMsgBtn.getAttribute('data-fmessageId') + "&&Text=" + $("#MsgEditTextarea").val(),
            success: function () {
                CloseEditMsgDialog();
                location.reload();
            }
        })
    }
}


//Открывает и закрывает части форума
function Lock(btn)
{
    var type = DefineType(btn);
    var link = "Lock?type=";
    var itemId = GetId(btn);

    link = link + type + "&&itemId=" + itemId;

    jQuery.ajax({
        type: "POST",
        url: link,
        success: function () {
            location.reload();
        }
    })
}



function FixTopic ()
{
    link = "FixFTopic?topicId=" + pressedEditBtn.getAttribute('data-ftopicId');
    jQuery.ajax({
        type: "POST",
        url: link,
        success: function () { location.reload();}
    })
}



function Search (btn)
{
    var type = DefineType(btn);
    var itemId = GetId(btn);
    var ask = $("#SearchInput").val();
    var link = "/Forum/FSearch?type=" + type + "&&itemId=" + itemId + "&&ask=" + ask;
    document.location.href = link;
}



function ShowFoundTopics(btn)
{
    var TopicsPlace = $("#FoundTopicsPlace");
    if (TopicsPlace.css("display") == "none")
    {
        btn.innerText = "Свернуть";
        TopicsPlace.css("display", "block");
    }
    else
    {
        btn.innerText = "Развернуть";
        TopicsPlace.css("display", "none");
    }
}



function ShowFoundMessages(btn)
{
    var MessagesPlace = $("#FoundMessagesPlace");
    if (MessagesPlace.css("display") == "none")
    {
        btn.innerText = "Свернуть";
        MessagesPlace.css("display", "block");
    }
    else {
        btn.innerText = "Развернуть";
        MessagesPlace.css("display", "none");
    }
}



var PageLinkArr = $(".PageLink");
SetPageListeners();

function SetPageListeners() {
    PageLinkArr.click(function (event) {
        event.preventDefault();

        var btn = event.target;
        var link = btn.getAttribute('href');
        if (link.indexOf("FoundFTopicsChangePage") != -1) {
            AjaxRefreshPage(link, "#FoundTopicsPlace");
        }
        else {
            if (link.indexOf("FoundFMessagesChangePage") != -1) {
                AjaxRefreshPage(link, "#FoundMessagesPlace");
            }
            else {
                document.location.href = link;
            }
        }
    });

    $(".SearchFTopicsPageBtn").click(function () {
        var a = [];
        a = $('[class="SearchFTopicsPageInput"]');
        if ((!isNaN(a[0].value)) && (a[0].value != "")) {
            var link = "FoundFTopicsChangePage?page=" + a[0].value;
            AjaxRefreshPage(link, "#FoundTopicsPlace");
        }


        if ((!isNaN(a[1].value)) && (a[1].value != "")) {
            var link = "FoundFTopicsChangePage?page=" + a[1].value;
            AjaxRefreshPage(link, "#FoundTopicsPlace");
        }
    });


    $(".SearchFMessagesPageBtn").click(function () {
        var a = [];
        a = $('[class="SearchFMessagesPageInput"]');
        if ((!isNaN(a[0].value)) && (a[0].value != "")) {
            var link = "FoundFMessagesChangePage?page=" + a[0].value;
            AjaxRefreshPage(link, "#FoundMessagesPlace");
        }


        if ((!isNaN(a[1].value)) && (a[1].value != "")) {
            var link = "FoundFMessagesChangePage?page=" + a[1].value;
            AjaxRefreshPage(link, "#FoundMessagesPlace");
        }
    });


    $(".CurrentPageLink").click(function (event) { event.preventDefault();})
}



function AjaxRefreshPage(link, place)
{
    jQuery.ajax({
        type: "GET",
        url: link,
        success: function (html) {
            $(place).html(html);
            PageLinkArr = $(".PageLink");
            SetPageListeners();
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(errorThrown);
        }
    })
}



//Ограничение количества символов
(function ($) {
    $.fn.extend({
        limiter: function (limit) {
            $(this).on("keyup focus", function () {
                setCount(this);
            });
            function setCount(src) {
                var chars = src.value.length;
                if (chars > limit) {
                    src.value = src.value.substr(0, limit);
                    chars = limit;
                }
            }
            setCount($(this)[0]);
        }
    });
})(jQuery);


$("#NewMsgSide").limiter(3500); //Ограничение длины нового сообщения
$("#MsgEditTextarea").limiter(3500); //Ограничение длины редактируемого сообщения