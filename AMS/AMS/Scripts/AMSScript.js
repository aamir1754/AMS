var AMS = function () {

    var handleEmailAddress = function (emailAddress) {
        var pattern = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return pattern.test(emailAddress);
    };

    var handleGetPartialView = function (PartialViewName) {
       
        $.get('/Home/GetPartial', { PartialViewName: PartialViewName }, function (data) {
            $('#MainContent').empty();
            $('#MainContent').html(data);
        });
    }
    return {
        EmailAddress: function (emailAddress) {
            handleEmailAddress(emailAddress);
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

        handleGetPartialView: function ($that) {
            var $thatElment = $($that);
            var $Parent = $thatElment.parents('ul.sidebar-menu.tree');
            $Parent.find('li.active').removeClass('active');
            $thatElment.parent().addClass('active');
            
            var RenderPartialView = $thatElment.attr('data-Partial');
            handleGetPartialView(RenderPartialView);
        },
        AddNewTag: function () {
            if ($("#textntagName").val() == "") {
                $.toaster({ priority: 'error', message: 'please enter tag title' });
            }
            else {
                $.post('/Home/AddTag', { Title: $("#textntagName").val()  }, function (data) {
                    if (data.success) {
                        $("#textntagName").val('');
                        AMS.GetTags();
                        $.toaster({ priority: 'success', message: data.message });
                    }
                    else {
                        $.toaster({ priority: 'error', message: data.message });
                    }
                });
            }
        },

        InitializeDataTable: function (Table) {
            var UsersTable = $("#" + Table).dataTable({
                "autoWidth": false,
                destroy: true,
                paging: true,
                sort: true,
                searching: true,
                "sDom": '<"row view-filter"<"col-sm-12"<"pull-right"><"">>>t<"row view-pager"<"col-sm-12"<"pull-right"ip><"pull-left"l>>>',
                "orderCellsTop": true,

            });

            $('#' + Table + ' tbody').on('mouseenter', 'td', function () {
                if ($(this).find(".fa-trash-o").length <= 0) {
                    this.setAttribute('title', $(this).text());
                }
            });
        },

        UpdateTag: function ($this) {
        },
        DeleteTag: function (TagId) {
            var options = AMS.SetSweetAlertOptions("Are you sure, you want to delete this tag?");
            swal(options, function (isConfirm) {
                if (isConfirm) {
                    $.post('/Home/DeleteTag/', { TagId: TagId }, function (data) {
                        if (data.success) {
                            AMS.GetTags();
                            $.toaster({ priority: 'success', message: 'Tag deleted successfully' });
                        }
                        else {
                            $.toaster({ priority: 'error', message: data.message });
                        }
                    });
                }
            });
        },

        GetTags: function () {
            $.get('/Home/_GetTags', function (data) {
                $("#TagsContainer").html(data);
            });
        },
        GetUserPartial: function () {
            $.get('/Home/GetUserPartial', function (data) {
                $("#MainContent").html(data);
            });
        },
        GetAllUsers: function () {
            $.get('/Home/_GetAllUsers', function (data) {
                $("#UsersContainer").html(data);
            });
        },
        DeleteTag: function (userId) {
            var options = AMS.SetSweetAlertOptions("Are you sure, you want to delete this tag?");
            swal(options, function (isConfirm) {
                if (isConfirm) {
                    $.post('/Home/DeleteUser/', { userId: userId }, function (data) {
                        if (data.success) {
                            AMS.GetAllUsers();
                            $.toaster({ priority: 'success', message: 'Tag deleted successfully' });
                        }
                        else {
                            $.toaster({ priority: 'error', message: data.message });
                        }
                    });
                }
            });
        },
        GetPartsPartial: function () {
            $.get('/Home/GetPartsPartial', function (data) {
                $("#MainContent").html(data);
            });
        },
        GetTagParts: function (TagId) {
            $.get('/Home/GetTagParts', { TagId: TagId}, function (data) {
                $("#PartsContainer").html(data);
            });
        },
        AddNewPart: function (TagId) {
            if ($("#txtPartName").val() == "") {
                $.toaster({ priority: 'error', message: 'please enter tag title' });
            }
            else {
                $.post('/Home/AddNewPart', { Title: $("#txtPartName").val(), TagId: TagId }, function (data) {
                    if (data.success) {
                        $("#txtPartName").val('');
                        AMS.GetTagParts(TagId);
                        $.toaster({ priority: 'success', message: data.message });
                    }
                    else {
                        $.toaster({ priority: 'error', message: data.message });
                    }
                });
            }
        },
        UpdatePart: function ($this) {
        },
        DeletePart: function (PartId) {
            var options = AMS.SetSweetAlertOptions("Are you sure, you want to delete this part?");
            swal(options, function (isConfirm) {
                if (isConfirm) {
                    $.post('/Home/DeletePart/', { PartId: PartId }, function (data) {
                        if (data.success) {
                            AMS.GetTagParts($('#TagsIdHidden').val());
                            $.toaster({ priority: 'success', message: 'Part deleted successfully' });
                        }
                        else {
                            $.toaster({ priority: 'error', message: data.message });
                        }
                    });
                }
            });
        },
        GetOptions: function (PartId, PartName) {
            $("#PartIdHidden").val(PartId);
            $("#ModalTitle").text(PartName);
            $("#OptionsModal").modal("show");
            AMS.GetOptionsList(PartId);
           
        },

        AddNewOption: function (PartId) {
            if ($("#Optiontxtbx").val() == "") {
                $.toaster({ priority: 'error', message: 'please enter option title' });
            }
            else {
                $.post('/Home/AddNewOption', { Title: $("#Optiontxtbx").val(), PartId: PartId }, function (data) {
                    if (data.success) {
                        $("#Optiontxtbx").val('');
                        AMS.GetOptionsList(PartId);
                        $.toaster({ priority: 'success', message: data.message });
                    }
                    else {
                        $.toaster({ priority: 'error', message: data.message });
                    }
                });
            }
        },

        GetOptionsList: function (PartId) {
            $.get('/Home/_GetOptions', { PartId: PartId }, function (data) {
                $("#MainDiv").html(data);
            });
        },

        DeleteOption: function (OptionId) {
            var options = AMS.SetSweetAlertOptions("Are you sure, you want to delete this option?");
            swal(options, function (isConfirm) {
                if (isConfirm) {
                    $.post('/Home/DeleteOption/', { OptionId: OptionId }, function (data) {
                        if (data.success) {
                            AMS.GetOptionsList($('#PartIdHidden').val());
                            $.toaster({ priority: 'success', message: 'Option deleted successfully' });
                        }
                        else {
                            $.toaster({ priority: 'error', message: data.message });
                        }
                    });
                }
            });
        },
        CloseModal: function(ModalId) {
            $("#" + ModalId).modal("hide");
        },
        GetDocuments: function () {
            $.get('/Home/_GetDocuments', function (data) {
                $("#MainContent").html(data);
            });
        },
        TriggerDocument: function () {
            $("#txtDocumentName").trigger("click");
        },
        GetDocumentsList: function () {
            $.get('/Home/_GetDocumentsList', function (data) {
                $("#DocumentsContainer").html(data);
            });
        },
        DeleteDocument: function (DocumentId) {
            var options = AMS.SetSweetAlertOptions("Are you sure, you want to delete this document?");
            swal(options, function (isConfirm) {
                if (isConfirm) {
                    $.post('/Home/DeleteDocument/', { DocumentId: DocumentId }, function (data) {
                        if (data.success) {
                            AMS.GetDocumentsList();
                            $.toaster({ priority: 'success', message: 'Document deleted successfully' });
                        }
                        else {
                            $.toaster({ priority: 'error', message: data.message });
                        }
                    });
                }
            });
        },
        DownloadDocument: function (DocumentId) {
            window.location = "/Home/DownloadDocument?DocumentId=" + DocumentId ;
        },
        GetItemsPartial: function () {
            $.get('/Home/_GetItemsPartial', function (data) {
                $("#MainContent").html(data);
            });
        },
        GetPartOptions: function (TagId) {
            $.get('/Home/GetPartsWithOptions', { TagId, TagId }, function (data) {
                var html = '';
                $.each(data.data, function () {
                    html += '<div class="col-md-9 form-group">';
                    html += '<div class="col-md-2">';
                    html += '<input type="checkbox" class="PartscheckBox" style="margin-right:10px;" >';
                    html += '<label> '+ this.PartName +'</label>';
                    html += '</div>';

                    html += '<div class="col-md-4">';
                    html += '<select class="form-control">';

                    $.each(this.Options, function () {
                        html += '<option value="' + this.OptionId + '" > '+ this.OptionTitle +' </option>';
                    });

                    html += '</select>';
                    html += '</div>';

                    html += '<div class="col-md-3">';
                    html +='<input type="text" class="form-control" placeholder="Serial No" />';
                    html += '</div>';

                    html += '</div>';
                });

                $("#PartsSection").html(html);
            });
        },
    };
}();

$(document).ready(function () {
    $(document).ajaxStart(showLoadingScreen(true)).ajaxStop(showLoadingScreen(false));
});


function showLoadingScreen(enabled) {
    return enabled ? $.blockUI({ message: '<div class="blockUI"><div class="spinner arrow"><div class="arrow1"></div></div></div>' }) : $.unblockUI();
}
$(document).ajaxStart(function () {
    showLoadingScreen(true);
}).ajaxStop(function () {
    showLoadingScreen(false);
    });


$(document).on('change', '#txtDocumentName', function () {
    if ($("#txtDocumentName input.file").val() == "") {
        return;
    }
    if ($('#txtDocumentName')[0].files[0] != undefined) {
        var formdata = new FormData($('#DocumentForm').get(0));
        $.ajax({
            url: "/Home/AddDocument",
            type: 'POST',
            data: formdata,
            success: function (data) {
                if (data.success) {
                    AMS.GetDocumentsList();
                    $.toaster({ priority: 'success', message: "Dcoument added successfully" });
                }
                else {
                    $.toaster({ priority: 'error', message: data.ee });
                }
            },
            processData: false,
            contentType: false,
        });
    }
});