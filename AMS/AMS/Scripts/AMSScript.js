

var AMS = function () {

    var handleCloseModal = function (Modal) {
        $("#" + Modal).modal("hide");
    };
    var handleEmailAddress = function (emailAddress) {
        var pattern = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return pattern.test(emailAddress);
    };
    var handleCloseModalManually = function (ModalId) {
        $(".modal-backdrop").remove(); 
        $('body').removeClass('modal-open');
        $('#txtProjectname').val('');
        $('body').css('padding-right', '0px');
        $("#" + ModalId).hide(150);
    };
    var handleLoginUser = function () {
        $("#EmailLogin").val($.trim($("#EmailLogin").val()));
        $("#PasswordLogin").val($.trim($("#PasswordLogin").val()));
        if ($('#EmailLogin').val() == "" || $('#PasswordLogin').val() == "") {
            $.toaster({ priority: 'danger', message: 'Please fill both fields' });
        }
        else if (!handleEmailAddress($("#EmailLogin").val())) {
            $.toaster({ priority: 'error', message: 'Invalid email address' });
        }
        else {

            $.ajax({
                type: "post",
                url: "/Account/Login",
                data: { UserName: $('#EmailLogin').val(), Password: $('#PasswordLogin').val() },
                success: function (data) {
                    if (data.success) {
                        window.location.href = data.URL;
                    }
                    else {
                        if (data.status == "Inactive")
                        {
                            $.toaster({ priority: 'danger', message: "Your account has been disabled" });
                        } 
                        if (data.status == "WrongPassword") {
                            $.toaster({ priority: 'danger', message: "Wrong password" });
                        }
                        if (data.status == "NotFound") {
                            $.toaster({ priority: 'danger', message: "account does not exist" });
                        }
                    }
                },
                error: function (data) {
                }
            });
        }
    };
    var handleRegister = function () {

        $("#FirstName").val($.trim($("#FirstName").val()));
        $("#LastName").val($.trim($("#LastName").val()));
        $("#EmailLogin").val($.trim($("#EmailLogin").val()));
        $("#PasswordLogin").val($.trim($("#PasswordLogin").val()));
        if ($('#FirstName').val() == "" || $('#LastName').val() == "" || $('#EmailLogin').val() == "" || $('#PasswordLogin').val() == "") {
            $.toaster({ priority: 'danger', message: 'Please fill all fields' });
        }
        else if (!handleEmailAddress($("#EmailLogin").val())) {
            $.toaster({ priority: 'error', message: 'Invalid email address' });
        }
        else {

            $.ajax({
                type: "post",
                url: "/Account/Register",
                data: { FirstName: $('#FirstName').val(), LastName: $('#LastName').val(), UserName: $('#EmailLogin').val(), Password: $('#PasswordLogin').val() },
                success: function (data) {
                    if (data.success) {
                        $.toaster({ priority: 'success', message: 'account created successfully' });
                        setTimeout(function () { window.location.href = "/Home/Home"; }, 3000);
                      
                    }
                    else {
                        $.toaster({ priority: 'danger', message: data.message });
                    }
                },
                error: function (data) {
                }
            });
        }
    };
    var handleOpenPostsPopup = function () {
        $("#PostsModal").modal("show");
    };
    var handleUpdateUserInfo = function ()
    {
        if ($('#PrsnlInfoFirstName').val() == "" || $('#PrsnlInfoLastName').val() == "" || $('#PrsnlInfoEmail').val() == "")
        {
            $.toaster({ priority: 'danger', message: 'Please fill all fields' });
        }
        else if (!handleEmailAddress($("#PrsnlInfoEmail").val()))
        {
            $.toaster({ priority: 'error', message: 'Invalid email address' });
        }
        else
        {
            $.ajax({
                type: "post",
                url: "/Home/UpdateUserInfo",
                data: { FirstName: $('#PrsnlInfoFirstName').val(), LastName: $('#PrsnlInfoLastName').val(), Email: $('#PrsnlInfoEmail').val(), UserId: $("#CurrentUserIdHidden").val() },
                success: function (data) {
                    if (data.success) {
                        $.toaster({ priority: 'success', message: 'profile info updated successfully, page will be refreshed to load new changes' });
                        setTimeout(function () {
                            location.reload(true);
                        }, 3000);
                    }
                    else
                    {
                        $.toaster({ priority: 'error', message: data.message });
                    }

                },
                error: function (data) {
                }
            });
        }
    };
    var handleUpdatePassword = function () {
        if ($('#CurrentPasswordBox').val() == "" || $('#NewPassswordBox').val() == "") {
            $.toaster({ priority: 'danger', message: 'Please fill all fields' });
        }
        else if ($('#CurrentPasswordBox').val() ==  $('#NewPassswordBox').val()) {
            $.toaster({ priority: 'danger', message: 'new password can not be same as current' });
        }
        else {
            $.ajax({
                type: "post",
                url: "/Home/UpdatePassword",
                data: { OldPassword: $('#CurrentPasswordBox').val(), NewPassword: $('#NewPassswordBox').val(), UserId: $("#CurrentUserIdHidden").val() },
                success: function (data) {
                    if (data.success) {
                        $.toaster({ priority: 'success', message: 'password changed successfully, you will be asked for new password on next login' });
                    }
                    else {
                        $.toaster({ priority: 'error', message: data.message });
                    }

                },
                error: function (data) {
                }
            });
        }
    };
    var handleGetPartialView = function (PartialViewName) {
       
        $.get('/Home/GetPartial', { PartialViewName: PartialViewName }, function (data) {
            debugger;
            $('#MainContent').empty();
            $('#MainContent').html(data);
        });
    }
    return {
        EmailAddress: function (emailAddress) {
            handleEmailAddress(emailAddress);
        },
        CloseModal: function (Modal) {
            handleCloseModal(Modal);
        },
        DeleteWorkItem: function (WID) {
            handleDeleteWorkItem(WID);
        },
        IsDeleteConfirm: function (title) {
            swal({
                title: title,
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                cancelButtonText: "No",
                confirmButtonText: "Yes",
                closeOnConfirm: false,
                closeOnCancel: false
            },

                function (isConfirm) {
                    if (isConfirm) {
                        check = true;
                        return check;
                    }
                    else {
                        check = false;
                        return check;
                    }
                });
        },
        SetSweetAlertOptions: function (title) {
            var options = {
                title: title,
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-default blue",
                cancelButtonClass: "btn-danger",
                confirmButtonText: "Yes",
                cancelButtonText: "No",
                closeOnConfirm: true,
                closeOnCancel: true,
                imageSize: "40x40",

            };
            return options;
        },
        CloseModalManually: function (ModalId) {
            handleCloseModalManually(ModalId);
        },
        LoginUser: function ()
        {
            handleLoginUser();
        },
        Register: function ()
        {
            handleRegister();
        },
        OpenPostsPopup: function ()
        {
            handleOpenPostsPopup();
        },
        UpdateUserInfo: function()
        {
            handleUpdateUserInfo();
        },
        UpdatePassword: function ()
        {
            handleUpdatePassword();
        },
        handleGetPartialView: function ($that) {
            var $thatElment = $($that);
            var $Parent = $thatElment.parents('ul.sidebar-menu.tree');

            //remove Active
            $Parent.find('li.active').removeClass('active');

            //add active Class to Element Requested Url
            $thatElment.parent().addClass('active');
            
            var RenderPartialView = $thatElment.attr('data-Partial');
            handleGetPartialView(RenderPartialView);
        }
    };
}();

$(document).ready(function () {
    $(document).ajaxStart(showLoadingScreen(true)).ajaxStop(showLoadingScreen(false));
    $('#summernote').summernote(
        {
            height: 200,  
        });
});


function showLoadingScreen(enabled) {
    return enabled ? $.blockUI({ message: '<div class="blockUI"><div class="spinner arrow"><div class="arrow1"></div></div></div>' }) : $.unblockUI();
}
$(document).ajaxStart(function () {
    showLoadingScreen(true);
}).ajaxStop(function () {
    showLoadingScreen(false);
});

