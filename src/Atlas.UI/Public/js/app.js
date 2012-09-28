$(function () {
    $("a.excluir").live("click", function (event) {
        event.preventDefault();

        var $el = $(this);
        var action = $el.attr("href");
        var method = "DELETE";
        
        if (confirm("Deseja realmente fazer isso?"))
            $.ajax({
                url: action,
                type: method,
                success: function(data, textStatus, jqXHR) {
                    window.location.reload();
                }
            });
    });

    $("a.openModal").live("click", function (event) {
        event.preventDefault();
        
        var $el = $(this);
        var action = $el.attr("href");
        var method = $el.prop("method");
        
        $.ajax({
            url: action,
            type: method,
            success: function (data, textStatus, jqXHR) {
                $("#modal").html(data);
                $("#modal").dialog();
            }
        });
    });
});