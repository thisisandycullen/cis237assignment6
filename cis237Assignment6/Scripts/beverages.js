//You almost always need this first document ready function. Code goes inside it.
$(document).ready(function () {
    //This is a jQuery selector. The '.' is to select an element with the class that
    //follows the '.', the '#' is to select an element with the id that follows the '#'.
    $(".dataRow").click(function (firedEvent) {
        console.log("The event fired!");
        //document.location = "/Beverages/Edit/[id_goes_here!!!]";
    });
});