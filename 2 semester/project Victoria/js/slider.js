let swiper = new Swiper('.image-slider', {
    pagination: {
        el: '.swiper-pagination',
        clickable: true,
        dynamicBullets: true,

    },
    slidesPerView: 1,
    touchRatio: 1,
    loop: true,
    autoplay: {
        delay: 5000,
        disableOnInteraction: false,
    },
    /*parallax: true,*/
});