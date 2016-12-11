var MKS = MKS || {};
MKS.Knockout = MKS.Knockout || {};

/**
 * Initialize module.
 * @param {object} ns - namespace
 */
(function (ns) {
    /**
     * Updates validation model (created by ko.validatedObservable(...))
     * according to model state returned by server.
     * @param modelStateErrors validation errors from api
     * @param validationModel object returned by ko.validatedObservable(...)
     */
    ns.setValidationErrors = function (modelStateErrors, validationModel) {
        var validationInstance = validationModel();

        for (var key in modelStateErrors) {
            if (modelStateErrors.hasOwnProperty(key)) {
                ko.utils.arrayForEach(modelStateErrors[key], function (error) {
                    validationInstance[key].setError(error);
                });
                validationInstance[key].isModified(true);
            }
        }
    };

    /**
     * Sends a POST request with `object` to url.
     * @param {string} url - api url
     * @param {function} onSuccess - callback on HTTP 200 (jQuery.ajax.success callback)
     * @param {function|object} onErrorOrValidationObj - if function, called on HTTP != 200, (jQuery.ajax.error callback)
     *  if object (returned by ko.validatedObservable) and received validation errors with HTTP 400, they will be merged with the object
     */
    ns.post = function (url, object, onSuccess, onErrorOrValidationObj) {
        var ajax = {
            url: url,
            method: 'POST',
            data: JSON.stringify(object),
            contentType: 'application/json',
            success: onSuccess
        };

        if (typeof onErrorOrValidationObj === 'function') {
            ajax.error = onErrorOrValidationObj;
        } else if (typeof onErrorOrValidationObj === 'object') {
            ajax.error = function (error) {
                if (error.status == 400) {
                    MKS.Knockout.setValidationErrors(error.responseJSON, validationObj);
                }
            };
        }

        $.ajax(ajax);
    };
})(MKS.Knockout);