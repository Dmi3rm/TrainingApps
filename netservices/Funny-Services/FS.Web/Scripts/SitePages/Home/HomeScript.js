$(document).ready(function ()
{
    $("#btnLogin").click(function () {
        var href = "/Account/Login";
        $("#LoginButton").attr("href", href).click();
    });

    $("#btnReg").click(function () {
        var href = "/Account/Register";
        $("#RegisterButton").attr("href", href).click();
    });

    $("#btnQuickUserData").click(function () {
        var href = "/User/QuickUserData";
        $("#QuickUserDataButton").attr("href", href).click();
    });


    $("#btnQuickUserData").click();
    $("#btnUserPage").click();
    $("#btnLogin").click();
});

function LoginOnFailure(result) {
    alert(result.message);
}

$("#btnReg").mouseover(function () {
    $("#btnReg").css('background', 'red');
})

$("#btnReg").mouseout(function () {
    $("#btnReg").css('background', '#c8c6c6');
})

$("#btnLogin").mouseover(function () {
    $("#btnLogin").css('background', 'red');
})

$("#btnLogin").mouseout(function () {
    $("#btnLogin").css('background', '#c8c6c6');
})



var btnPressed = 0;

$("#GreetingsBtn").mouseover(function () {
    if (btnPressed != 1)
    $("#GreetingsBtn").css('background', 'red');
})

$("#GreetingsBtn").mouseout(function () {
    if (btnPressed != 1)
        $("#GreetingsBtn").css('background', '#3eff20');
})

$("#NovationsBtn").mouseover(function () {
    if (btnPressed != 2)
    $("#NovationsBtn").css('background', 'red');
})

$("#NovationsBtn").mouseout(function () {
    if (btnPressed != 2)
        $("#NovationsBtn").css('background', '#3eff20');
})

$("#NotesBtn").mouseover(function () {
    if (btnPressed != 3)
    $("#NotesBtn").css('background', 'red');
})

$("#NotesBtn").mouseout(function () {
    if (btnPressed != 3)
        $("#NotesBtn").css('background', '#3eff20');
})


$("#GreetingsBtn").click(function () {
    $("#GreetingsBtn").css('height', '85%');
    $("#NovationsBtn").css('height', '85%');
    $("#NotesBtn").css('height', '85%');
    $("#GreetingsBtn").css('background', 'red');
    $("#NovationsBtn").css('background', '#3eff20');
    $("#NotesBtn").css('background', '#3eff20');
    GetGreetings();

    btnPressed = 1;
})


$("#NovationsBtn").click(function () {
    $("#GreetingsBtn").css('height', '85%');
    $("#NovationsBtn").css('height', '95%');
    $("#NotesBtn").css('height', '85%');
    $("#GreetingsBtn").css('background', '#3eff20');
    $("#NovationsBtn").css('background', 'red');
    $("#NotesBtn").css('background', '#3eff20');
    GetNovations();

    btnPressed = 2;
})


$("#NotesBtn").click(function () {
    $("#GreetingsBtn").css('height', '85%');
    $("#NovationsBtn").css('height', '85%');
    $("#NotesBtn").css('height', '95%');
    $("#GreetingsBtn").css('background', '#3eff20');
    $("#NovationsBtn").css('background', '#3eff20');
    $("#NotesBtn").css('background', 'red');
    GetNotes();

    btnPressed = 3;
})

$("#GreetingsBtn").click();




function GetGreetings() {
    jQuery.ajax(
            {
                type: "GET",
                url: "/Home/GetGreetings",
                dataType: "html",
                success: function (html) {
                    $("#news").html(html);
                },
            })
}


function GetNovations() {
    jQuery.ajax(
            {
                type: "GET",
                url: "/Home/GetNovations",
                dataType: "html",
                success: function (html) {
                    $("#news").html(html);
                },
            })
}


function GetNotes() {
    jQuery.ajax(
            {
                type: "GET",
                url: "/Home/GetNotes",
                dataType: "html",
                success: function (html) {
                    $("#news").html(html);
                },
            })
}
