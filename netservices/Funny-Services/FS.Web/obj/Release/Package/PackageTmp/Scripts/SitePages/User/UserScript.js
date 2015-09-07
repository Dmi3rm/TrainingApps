$("#AvaBtn").click(function ChangeAva(e)
{
    e.preventDefault();
    var block = $("#ChangeAvaDiv");
    if (block.css("display") == "none") {
        block.css("display", "block");
    }
    else
    {
        block.css("display", "none");
    }
})


$("#PasswordBtn").click(function ChangePassword(e) {
    e.preventDefault();
    var block = $("#ChangePasswordDiv");
    if (block.css("display") == "none") {
        block.css("display", "block");
    }
    else {
        block.css("display", "none");
    }
})

