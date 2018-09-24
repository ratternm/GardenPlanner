

$(document).ready(function () {
    let linkAddress;
    $(".delete").click(function () {
        linkAddress = $(this).attr("data");
       
    });

    $(".delete").confirm({
        animation: 'none',
        closeAnimation: 'none',
        title: 'Do you want to delete?',
        content: 'This is permanent, your plot will be removed from the earth',
        buttons: {
            confirm: function () {
                $.get(linkAddress);
                location.reload();
            },
            cancel: function () {
                
            }
            
            
        }
    });


    $("#Garden").validate({
        rules: {

            Name: {
                required: true         //makes first name required
            },
            TempHigh: {
                required: true,
                number: true       //using an additional jquery validation method
            },
            TempLow: {
                required: true,
                number: true
            }
        }

    });

});

function confirmDelete(e) {
    
   
    if (confirm("Are you sure you want to delete?")) {
        txt;
    }
    else {
        e.preventDefault();
    }
}