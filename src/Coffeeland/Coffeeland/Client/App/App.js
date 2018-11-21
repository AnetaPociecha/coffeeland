import $ from 'jquery';
window.jQuery = $;
require('signalr');

var init_foo = function() {
// Declare a proxy to reference the hub.
var chat = $.connection.communicationHub;
// Create a function that the hub can call to broadcast messages.
chat.client.broadcastMessage = function (name, message) {
    // Html encode display name and message.
    var encodedName = $('<div />').text(name).html();
    var encodedMsg = $('<div />').text(message).html();
    // Add the message to the page.
    $('#discussion').append('<li><strong>' + encodedName
        + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
};
// Get the user name and store it to prepend to messages.
$('#displayname').val(prompt('Enter your name:', ''));
// Set initial focus to message input box.
$('#message').focus();
// Start the connection.
console.log("start()");
console.log($.connection.hub);
$.connection.hub.start().done(function () {
    console.log("start().done()");
    console.log($('#sendmessage'));
    $('#sendmessage').click(function () {
        // Call the Send method on the hub.
        console.log("wysylam");
        chat.server.send($('#displayname').val(), $('#message').val());
        // Clear text box and reset focus for next comment.
        $('#message').val('').focus();
    });
});
};

$(init_foo);
