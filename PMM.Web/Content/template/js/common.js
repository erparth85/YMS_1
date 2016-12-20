
function isNumberkey(e) {
    var key = (e.which) ? e.which : event.keyCode;
    if (key > 31 && (key < 48 || key > 57 || key == '2E' || key == 46)) {
        return false;
    }
    return true;
};
function isDecimalValue(e) {
    var key = (e.which) ? e.which : event.keyCode;
    if (key > 31 && (key < 46 || key > 57 || key == '2E')) {
        return false;
    }
    return true;
};
function checkFormValue(input, message) {
    if ($("#" + input).val().trim() == "") {
        $("#lbl" + input + "Error").text(message);
    }
    else {
        $("#lbl" + input + "Error").text("");
    }
}

function AddNewInput(item, target) {
    var idx = target.indexOf("_chzn");
    var id = target;
    if (idx) {
        id = target.substr(0, idx);
    }
    //alert(id+"__"+item);
    $.ajax({
        cache: false,
        type: "POST",
        url: "/addnewinput",
        data: { "tag": item, "target": id },
        success: function (data) {
            if (data.html) {
                //  alert(data.value + "_RET_" + item);
                $('#' + id).append('<option value="' + data.value + '" selected="selected">' + item + '</option>');
                $('#' + id).trigger("liszt:updated");
            }

        }
    });
}




