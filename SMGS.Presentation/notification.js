var countNotification = 0;
$(function () {
    console.log("enter signalr");
    // Initialize the connection to the server
    try {
        var realtimeNotifier = $.connection.notificationHub;
    } catch (exception) {
        console.log(exception.message);
    }
    var a = 1;
    // Preparing a client side function called sendMessage that will be called from the server side
    realtimeNotifier.client.sendMessage = function (message) {
        countNotification++;
        countNotifications();
    };

    // Establish the connection to the server. When done, sets the click of the button
    $.connection.hub.start().done(function () {
        $('#notifier-btn').click(function () {
            // When the cutton is clicked, call the method DoLongOperation defined in the Hub
            realtimeNotifier.server.sendNotifcationForAllUsers();
        });
    });
});

$(function () {
    $.ajax({
        type: 'post',
        datatype: 'json',
        async: true,
        url: './FirstCountNotification',
        success: function (value) {
            countNotification = value;
        },
        complete: function () {
            countNotifications();
        }
    });
});

var countNotifications = function () {
    $("#notification-count").html(countNotification);
};