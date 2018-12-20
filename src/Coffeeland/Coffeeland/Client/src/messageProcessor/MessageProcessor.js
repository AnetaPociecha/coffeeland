import $ from 'jquery';
window.jQuery = $;
require('signalr');

class MessageProcessor {
    constructor() {
        $(()=>{
            this.hub = $.connection.communicationHub;
            $.connection.hub.start();
        });
        
    }

    async processQuery(query) {
        return await this.hub.server.sendQuery(JSON.stringify(query)).then(data => data);
    }

    async processCommand(command) {
        return await this.hub.server.sendCommand(JSON.stringify(command)).then(data => data);
    }
}

export default new MessageProcessor();


