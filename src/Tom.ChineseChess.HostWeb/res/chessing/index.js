var Index = function () {
    var commandList = {};


    var handle = function () {

        $("#btnSit").click(function () {
            var commandModel = {
                CommandType:"sit",
                ApiName : commandList["sit"],
                Data: {
                    table_id: $("#tableID").val()
                }
            };
            var strCommandModel = JSON.stringify(commandModel);
            
            if (ws.readyState == WebSocket.OPEN) {
                ws.send(strCommandModel);
            }
            else {
                $("messageSpan").text("Connection is Closed!");
            }

        });
    };


    return {
        Init: function () {
            commandList = JSON.parse($("#commandList").val());
            handle();
        },

    };
}();