//Iterates over object properties, usage:
//data-bind="foreachprop: obj"
ko.bindingHandlers.foreachprop = {
    transformObject: function (obj) {
        var properties = [];
        for (var key in obj) {
            if (obj.hasOwnProperty(key)) {
                properties.push({ key: key, value: obj[key] });
            }
        }
        return ko.observableArray(properties);
    },
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor()),
            properties = ko.bindingHandlers.foreachprop.transformObject(value);

        ko.bindingHandlers['foreach'].init(element, properties, allBindingsAccessor, viewModel, bindingContext)
        return { controlsDescendantBindings: true };
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor()),
            properties = ko.bindingHandlers.foreachprop.transformObject(value);

        ko.bindingHandlers['foreach'].update(element, properties, allBindingsAccessor, viewModel, bindingContext)
        return { controlsDescendantBindings: true };
    }
};