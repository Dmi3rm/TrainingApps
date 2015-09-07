var Piccontainer;
var Soncontainer;
var Vidcontainer;

function ShowBtnClicked(btn)
{
    if (Piccontainer != null)
    {
        Piccontainer.innerHTML = "";
    }
    var Name = btn.getAttribute("data-name");
    Piccontainer = document.getElementById(Name);
    Piccontainer.innerHTML = "<img src=\"/Home/pic?name=" + Name + "\"</img>"
}

function ShowSongBtnClicked(btn)
{
    if (Soncontainer != null)
    {
        Soncontainer.innerHTML = "";
    }
    var Name = btn.getAttribute("data-name");
    Soncontainer = document.getElementById(Name);
    Soncontainer.innerHTML = "<audio controls><source src=\"/Home/son?name=" + Name + "\"></audio>"
}

function ShowVideoBtnClicked(btn) {
    if (Vidcontainer != null) {
        Vidcontainer.innerHTML = "";
    }
    var Name = btn.getAttribute("data-name");
    Vidcontainer = document.getElementById(Name);
    Vidcontainer.innerHTML = "<video controls style=\"width:90%; margin-left:5%;\"><source src=\"/Home/vid?name=" + Name + "\"></video>"
}


function GetPicturesByNovelty(Names)
{
    var adress = "PicsByNames?names=" + Names;
    jQuery.ajax(
            {
                type: "GET",
                url: adress,
                dataType: "html",
                success: function (html) {
                    $("#PicsContainer").html(html);
                },
            })
}



function GetSongsByNovelty(Names) {
    var adress = "SonsByNames?names=" + Names;
    jQuery.ajax(
            {
                type: "GET",
                url: adress,
                dataType: "html",
                success: function (html) {
                    $("#SonsContainer").html(html);
                },
            })
}


function GetVideosByNovelty(Names) {
    var adress = "VidsByNames?names=" + Names;
    jQuery.ajax(
            {
                type: "GET",
                url: adress,
                dataType: "html",
                success: function (html) {
                    $("#VidsContainer").html(html);
                },
            })
}


function RoleBtnClicked(btn)
{
    var link = "/Admin/SetUserRole?page=" + document.getElementById("UserList").getAttribute('data-page') + "&&userId=" + btn.getAttribute('data-userId') +
        "&&roleId=" + document.getElementById(btn.getAttribute('data-userId')).value + "&&code=1" ;
    document.location.href = link;
}


