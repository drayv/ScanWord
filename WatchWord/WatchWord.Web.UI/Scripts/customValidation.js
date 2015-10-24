/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />

$.validator.setDefaults({ ignore: "" });

$.validator.setDefaults({
    highlight: function (element) {
        $(element).closest(".form-group").addClass("has-error");
    },
    unhighlight: function (element) {
        $(element).closest(".form-group").removeClass("has-error");
    }
});

$.validator.unobtrusive.adapters.add("reqif", ["val", "field"], function (options) {
    var params = { "val": options.params["val"], "compareElem": options.params["field"] };

    options.rules["reqif"] = params;
    options.messages["reqif"] = options.message;
});

$.validator.addMethod("reqif", function (value, element, params) {
    var compareValue = $(element).closest("form").find("input[type=radio][name=" + params["compareElem"] + "]:checked").val();

    if (compareValue === params["val"]) {
        return value !== 0 && value.trim().length !== 0;
    }
    return true;
});

$.validator.unobtrusive.adapters.add("maxfilesize", ["value"], function (options) {
    var size = { size: options.params["value"] };
    options.rules["maxfilesize"] = size;

    if (options.message) {
        options.messages["maxfilesize"] = options.message;
    }
});

$.validator.addMethod("maxfilesize", function (value, element, params) {
    if (window.File && window.FileReader && window.FileList && window.Blob) {
        var files = $(element)[0].files;

        if (files.length > 0) {
            return files[0].size < params["size"];
        }
    }
    return true;
});

$.validator.unobtrusive.adapters.add("extension", ["extensions"], function (options) {
    var extensions = {
        extensions: options.params["extensions"].split(/[ ,]/).filter(
            function (element) {
                return element.length !== 0;
            })
    };

    options.rules["extension"] = extensions;

    if (options.message) {
        options.messages["extension"] = options.message;
    }
});

$.validator.addMethod("extension", function (value, element, params) {
    var extensions = params["extensions"];

    if (!value.trim()) {
        return true;
    }

    for (var i = 0; i < extensions.length; i++) {
        if (value.endsWith(extensions[i]))
            return true;
    }
    return false;
});


