//$(document).ready(function(){

//    $("#registration_form").validate({
//        debug:false,
//        rules:{
//            FirstName: {
//                required: true,
//                maxLength: 20,
//            },
//            LastName: {
//                required: true,
//                maxLength: 20,
//            },
//            Email: {
//                required: true,
//                email: true,
//            },
//            Password: {
//                required: true,
//                maxLength: 20,
//                minLength: 8,
//                strongpassword: true,
//            },
//            ConfirmPassword: {
//                equalTo:"#Password",
//            }
//        },
//        messages: {
//            FirstName: {
//                required: "Please provide your first name.",
//                maxLength: "First name cannot exceed 20 characters.",
//            },
//            LastName: {
//                required: "Please provide your last name.",
//                maxLength: "First name cannot exceed 20 characters.",
//            },
//            Email: {
//                required: "Please provide your email.",
//                email: "Please provide an aprropriate email address (e.g. joker@gotham.com)",
//            },
//            Password: {
//                required: "Please provide a password.",
//                maxLength: "Password cannot exceed 20 characters.",
//                minLength: "Password cannot be less than 8 characters.",
//            },
//            ConfirmPassword: {
//                equalTo:"Make sure passwords match",
//            }
//        },
//        errorClass: "error",
//        validClass:"valid"
//    });
//});

//$.validator.addMethod("strongpassword", function (value, index) {
//    return value.match(/[A-Z]/) && value.match(/[a-z]/) && value.match(/\d/);
//}, "Please enter a strong password (one capital, one lower case, and one number");
$(document).ready(function () {

    //Material CHECKBOXES

    $total = "";
    let costTemp = $("#currentPlotCost").text();
    let currentCost = parseInt(costTemp);
    $('input:checkbox').on('change', function () {
        if (this.checked)
           
            currentCost += +this.value;
        else
           
            currentCost -= +this.value;
        $('#currentPlotCost').html((currentCost).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,'));
    });
    
});