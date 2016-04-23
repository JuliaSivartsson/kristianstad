
/* Add event to submit event of the cookie form, if available.
 * Post to the same action as the form but using Ajax.
  */
(function ($) {

    $(document).ready(function () {
        var cookieNotice = $('#cookie-notice');

        if (cookieNotice !== null) {
            var form = cookieNotice.find('form');

            form.on('submit', function () {
                var action = form.attr('action');

                $.post(action, form.serialize(), function (data) {
                    //Success
                }).fail(function (data) {
                    //Error
                });

                cookieNotice.remove();

                return false;
            });
            
        }
    });

})(jQuery);