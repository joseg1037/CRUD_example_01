function NamenLesen() {
    var sohnenAusweis = $("#sohnenAusweis").val();
    if ($("#sohnenAusweis").val() == "") {
        alert("Ausweis eingeben");
    }
    else {
        $.ajax({
            type: 'POST',
            url: '/Sohn/NamenLesen',
            data:
            {
                sohnenAusweis: sohnenAusweis
            },
            cache: false,
            dataType: 'json',
            success: function (data) {
                $("#NAME").val(data.sohnenNamen);

                $('#FK_ID_PERSON').val(data.FK_ID_PERSON);
                $('#FK_ID_PERSON').trigger("chosen:updated");
            },
            error: function (data) {
                alert("Fehler, checken Sie den Ausweis");
            },
        });
    }
}