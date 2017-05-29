$(function () {
    $(".button-collapse").sideNav();

    $("#main-carousel").owlCarousel({
        autoHeight: true,
        loop: true,
        autoplay: true,
        autoplayHoverPause: true,
        nav: true,
        dots: false,
        navText: ['', ''],
        responsive: {
            0: {
                items: 1
            }
        }
    });

    $('.tooltipped').tooltip();
    
    $('select').material_select();

    $('.datepicker').pickadate({
        selectMonths: true, 
        selectYears: 2
    });
});