System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var MenuOption;
    return {
        setters:[],
        execute: function() {
            MenuOption = (function () {
                function MenuOption(Title, Description) {
                    this.Title = Title;
                    this.Description = Description;
                }
                return MenuOption;
            }());
            exports_1("MenuOption", MenuOption);
        }
    }
});
