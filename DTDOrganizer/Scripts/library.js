//Loads menu on start
window.addEventListener("load", function () {
    showBooks();
});

//Makes select effect on menu and opens specific partial view
function showBooks() {
    $(".books").show();
    $(".courses").hide();
    $(".otherdocs").hide();
    $("#libraryAddBook").show();
    $("#libraryAddCourse").hide();
    $("#libraryAddDocument").hide();
    $('.blib').css('font-size', '1.25rem');
    $('.bcourse').css('font-size', '0.9rem');
    $('.odocs').css('font-size', '0.9rem');
}

//Makes select effect on menu and opens specific partial view
function showCourses() {
    $(".books").hide();
    $(".otherdocs").hide();
    $(".courses").show();
    $("#libraryAddCourse").show();
    $("#libraryAddDocument").hide();
    $("#libraryAddBook").hide();

    $('.bcourse').css('font-size', '1.25rem');
    $('.blib').css('font-size', '0.9rem');
    $('.odocs').css('font-size', '0.9rem');

}

//Makes select effect on menu and opens specific partial view
function showOther() {
    $(".books").hide();
    $(".courses").hide();
    $(".otherdocs").show();

    $("#libraryAddDocument").show();
    $("#libraryAddCourse").hide();
    $("#libraryAddBook").hide();

    $('.odocs').css('font-size', '1.25rem');
    $('.blib').css('font-size', '0.9rem');
    $('.bcourse').css('font-size', '0.9rem');
}
