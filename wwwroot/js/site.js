$(document).ready(function () {
    $('ul > li').hover(function () {
        $('li').removeClass('active');
        $('li').addClass('active');
        $('li').removeClass('active');
    });
});