System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var Rsvp;
    return {
        setters:[],
        execute: function() {
            Rsvp = (function () {
                function Rsvp(Attending, IsCamping, MenuOptions) {
                    this.Attending = Attending;
                    this.IsCamping = IsCamping;
                    this.MenuOptions = MenuOptions;
                }
                return Rsvp;
            }());
            exports_1("Rsvp", Rsvp);
        }
    }
});
