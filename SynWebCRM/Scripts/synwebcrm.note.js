function deleteNote(id) {
    $.post("/api/note/" + id + "/delete", null, function (res) {
        if (res.succeed) {
            $("#note" + id).remove();
        }
    });
}

function createNote(url, targetId) {
    var txtarea = $("textarea.new-note-text");
    var val = txtarea.val();
    txtarea.val("");
    if (val) {
        $.post(url, { targetId: targetId, text: val }, function (res) {
            if (res.succeed) {
                location.reload();
            }
        });
    }
}