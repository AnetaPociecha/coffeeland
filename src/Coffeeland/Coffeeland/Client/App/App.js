import $ from 'jquery';
window.jQuery = $;
require('signalr');

var displayResponse = function(data) {
    data = $('<div />').text(data).html();
    $('#responses').append('<li><strong>' + data + '</li>');
}


var set_buttons_for_tests = function(queryProcessor) {

    $('#sendQuery1').click(function () {
        queryProcessor.processQuery({$type: "GetSomeNumberQuery", variable: 7}).then(data => console.log(data));
    });
}

class QueryProcessor {
    constructor() {
        this.chat = $.connection.communicationHub;
        $.connection.hub.start();
        set_buttons_for_tests(this);
    }

    processQuery(query) {
        return this.chat.server.sendQuery(JSON.stringify(query));
    }
}
var queryProcessor;
$(function() {
    queryProcessor = new QueryProcessor();
});





