function populateWindow(controller, action, id) {
    var url = "/" + controller;
    url += "/" + action;
    url += id != undefined ? "/" + id : "";
    $("#window .modal-title").text(controller + " " + action);
    $.get(url, function (data) {
        $("#window .modal-body").html(data);
    });
}