$(function () {
    $("a.excluir").live("click", function (event) {
        event.preventDefault();

        var $el = $(this);
        var action = $el.attr("href");
        var method = $el.data("method");
        if (!method) method = "DELETE";
        
        var params = $el.data("params");
        var redirectTo = $el.data("redirect");
        
        if (confirm("Deseja realmente fazer isso?"))
            $.ajax({
                url: action,
                type: method,
                success: function(data, textStatus, jqXHR) {
                    if (!redirectTo) {
                        window.location.reload();
                        return;
                    }
                    window.location.replace(redirectTo);
                },
                data: params
            });
    });

    $("a.openModal").live("click", function (event) {
        event.preventDefault();
        
        var $el = $(this);
        var action = $el.attr("href");
        var method = $el.data("method");
        var width = $el.data("width");
        
        $.ajax({
            url: action,
            type: method,
            success: function (data, textStatus, jqXHR) {
                $("#modal").html(data);
                $("#modal").dialog({ width: width });
            }
        });
    });

    $("#contatos").each(function() {
        var $el = $(this);
        var totalPages = $el.data("total-pages");
        var url = $el.data("url");
        var params = $el.data("params");
        
        $("#contatos").pageless({
            totalPages: totalPages
            , url: url
            , loaderMsg: 'Carregando...'
            , params: params
        });
    });
});