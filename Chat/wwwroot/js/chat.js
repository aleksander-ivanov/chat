"use strict";

var roomId = document.getElementById("Room_Id").value;

var connection = new signalR.HubConnectionBuilder()
    .withUrl(`/chatHub?roomId=${roomId}`)
    .configureLogging(signalR.LogLevel.Information)
    .build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;
startConnection();

connection.onclose(function () {
    startConnection();
});

connection.on("ReceiveMessage", function (message) {
    var html = ` <div class="row">
            <div class="col-md-8" style="word-wrap: break-word">
                <div style="font-style: oblique">${message.text}</div>
                <div style="font-size: x-small">${message.createdBy}</div>
                    <div style="font-size: x-small">${message.createdDate.toString()}</div>
                <hr />
            </div>
        </div>`;
    $("#messagesList").prepend(html);
});

connection.on("newUserConnected", function (message) {
    alert(message);
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var text = $('#messageInput').val();
    connection.invoke("SendMessage", roomId, text).catch(function (err) {
        onError(err);
    });
    event.preventDefault();
    $('#messageInput').val('');
});

async function startConnection() {
    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
    }).catch(function (err) {
        onError(err);
        sleep(5000);
        console.log("Reconnecting Socket");
        startConnection();
    });
}

function onError(err) {
    document.getElementById("sendButton").disabled = true;
    alert(err.toString());
    console.error(err.toString());
}

async function sleep(msec) {
    return new Promise(resolve => setTimeout(resolve, msec));
}