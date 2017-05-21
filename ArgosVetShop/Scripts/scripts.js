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
});