var MKS = (function () {

    /**
     * Updates validation model (created by ko.validatedObservable(...))
     * according to model state returned by server.
     * @param modelStateErrors validation errors from api
     * @param validationModel object returned by ko.validatedObservable(...)
     */
    function setValidationErrors(modelStateErrors, validationModel) {
        var validationInstance = validationModel();

        for (var key in modelStateErrors) {
            if (modelStateErrors.hasOwnProperty(key)) {
                ko.utils.arrayForEach(modelStateErrors[key], function (error) {
                    validationInstance[key].setError(error);
                });
                validationInstance[key].isModified(true);
            }
        }
    }


    //public API
    return {
        setValidationErrors: setValidationErrors
    };
}());