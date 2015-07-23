/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
$.validator.setDefaults({
    highlight: function (element) {
        $(element).closest(".form-group").addClass("has-error");
    },
    unhighlight: function (element) {
        $(element).closest(".form-group").removeClass("has-error");
    }
});

$.validator.methods.number = function (e) {
    return true;
};

$.validator.unobtrusive.adapters.add("reqif", ["val", "field"], function (options) {
    var params = { "val": options.params["val"], "compareElem": options.params["field"] };
    options.rules["reqif"] = params;
    options.messages["reqif"] = options.message;
});

$.validator.addMethod("reqif", function (value, element, params) {
    var compareValue = $(element).closest("form").find("input:radio[name=" + params["compareElem"] + "]:checked").attr("Value");
    if (compareValue == params["val"]) {
        return value && value.trim().length != 0;
    }
    return true;
});


