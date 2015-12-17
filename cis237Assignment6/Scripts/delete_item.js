
//You almost always need this first "document ready" function. Code goes inside it.
$(document).ready(function () {

	var openDialog = function () {
		$("#dialog").dialog();
	};

	var closeDialog = function () {
		$("#dialog").dialog("close");
	}

	$("#delete-button").click(function () {
		openDialog();
	})

	$("#yes-button").click(function () {
		$("#result").text("<TODO: Delete item and return to index>");
		closeDialog();
	});

	$("#no-button").click(function () {
		closeDialog();
	});


});