(function (global) {
    //Tells the system loader where to look for things
    var map = {
        'app': 'app',
        '@angular': 'js/@angular',
        'rxjs': 'js/rxjs'
    };

    //Tells the system loader which filename and/or extension to look for by default
    var packages = {
        'app': { main: 'main.js', defaultExtension: 'js' },
        'rxjs': { defaultExtension: 'js' }
    };

    // configure @angular packages
    var ngPackageNames = [
        'common',
        'compiler',
        'core',
        'http',
        'platform-browser',
        'platform-browser-dynamic',
        'upgrade',
        'forms',
        'router'
    ];
    
    function packIndex(pkgName) {
        packages['@angular/' + pkgName] = {
            main: 'main.js',
            defaultExtension: 'js'
        };
    }

    function packUmd(pkgName) {
        packages['@angular/' + pkgName] = {
            main: '/bundles/' + pkgName + '.umd.js',
            defaultExtension: 'js'
        };
    }

    var setPackageConfig = System.packageWithIndex ? packIndex : packUmd;
    ngPackageNames.forEach(setPackageConfig);

    var config = {
        map: map,
        packages: packages
    }

    System.config(config);

})(this);