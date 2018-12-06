import $ from 'jquery';
window.jQuery = $;
require('signalr');

var displayResponse = function(data) {
    data = $('<div />').text(data).html();
    $('#responses').append('<li><strong>' + data + '</li>');
}


var set_buttons_for_tests = function(queryProcessor) {

    $('#sendQuery1').click(function () {
        queryProcessor.processQuery({name: "getSomeNumberQuery"}).then(data => displayResponse("1: " + data));;
    });
    $('#sendQuery2').click(function () {
        queryProcessor.processQuery({name: "getStringQuery"}).then(data => displayResponse("2: " + data));;
    });
    $('#sendQuery3').click(function () {
        queryProcessor.processQuery({name: "getSameResponseQuery", message: $('#message').val()}).then(data => displayResponse("3: " + data));;
    });
    $('#sendQuery4').click(function () {
        queryProcessor.processQuery({name: "getSomeJsonQuery"}).then(data => displayResponse("4: " + data));
    });
}

class QueryProcessor {
    constructor() {
        this.chat = $.connection.communicationHub;
        $.connection.hub.start();
        set_buttons_for_tests(this);
    }

    processQuery(query) {
        return this.chat.server.send(JSON.stringify(query));
    }
}
var queryProcessor;
$(function() {
    queryProcessor = new QueryProcessor();
});





