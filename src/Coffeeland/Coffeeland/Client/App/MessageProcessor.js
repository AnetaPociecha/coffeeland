import $ from 'jquery';
window.jQuery = $;
require('signalr');

class MessageProcessor {
    constructor() {
        this.hub = $.connection.communicationHub;
        $.connection.hub.start();
    }

    processQuery(query) {
        return this.hub.server.sendQuery(JSON.stringify(query));
    }

    processCommand(command) {
        return this.hub.server.sendCommand(JSON.stringify(command));
    }
}




