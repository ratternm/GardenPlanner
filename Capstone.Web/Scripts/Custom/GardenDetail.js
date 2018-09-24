$(document).ready(function () {


});

function GetPlants(plotId) {
    //Get message from service
    $.ajax({
        url: `/api/GardenAPIController/${plotId}`,
        type: "GET",
        dataType: "json"
    }).done(function (data) {
        console.log("Data: ", data);
        if (data && data.CurrentMessage) {
            UpdateMessage(data.CurrentMessage);
        }
    }).fail(function (xhr, status, error) {
        console.error("Error retrieving message: ", status, error);
    });
}