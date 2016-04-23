(function ($) {
    'use strict';

    $(document).ready(function () {
        //Adds css class "menu-open" to body at mega menu button click.
        $(".main-top-bar .top-bar-right .mega-menu-button").click(function (event) {
            event.preventDefault();
            $("body").toggleClass("menu-open");
            return false;
        });

        $(".kr-mobile-menu .site-nav .has-children > .toggle").each(function (i) {
            $(this).click(function (event) {
                event.preventDefault();
                $(this).toggleClass("open");
                $(this).parent(".has-children").toggleClass("open");
                return;
            })
        });
    });

    //Adds css class "scroll" to body.main-top-bar at scroll.
    $(window).scroll(function () {
        var scroll = $(window).scrollTop();
        if (scroll >= 25) {
            $("body").addClass("scroll");
        }
        else {
            $("body").removeClass("scroll");
        }
    });

    //Click function for mega menu main sections.
    $(".mega-menu .section-wrapper .tabs .tabs-title").click(function () {

        $(".mega-menu .section-wrapper .menu-information").slideUp(250);

    });

})(jQuery);