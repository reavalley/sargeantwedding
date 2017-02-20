System.register([], function (exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var User;
    return {
        setters: [],
        execute: function () {
            User = (function () {
                function User(UserName, Password, PasswordNew, Email, DisplayName, IsSocialLogin) {
                    this.UserName = UserName;
                    this.Password = Password;
                    this.PasswordNew = PasswordNew;
                    this.Email = Email;
                    this.DisplayName = DisplayName;
                    this.IsSocialLogin = IsSocialLogin;
                }
                return User;
            }());
            exports_1("User", User);
        }
    };
});
