(function (global) {

    var paths = {
        'lib:': 'libs/'
    };

    var map = {
        'app': 'app',
        'rxjs': 'lib:rxjs',
        '@angular': 'lib:@angular',
        'angular-in-memory-web-api': 'lib:angular-in-memory-web-api'
    };

    var packages = {
        'app': { main: 'main.js', defaultExtension: 'js' },
        'rxjs': { defaultExtension: 'js' },
        'angular-in-memory-web-api': { defaultExtension: 'js' },
    };

    var packageNames = [
      '@angular/common',
      '@angular/compiler',
      '@angular/core',
      '@angular/forms',
      '@angular/http',
      '@angular/platform-browser',
      '@angular/platform-browser-dynamic',
      '@angular/router',
      '@angular/upgrade'
    ];

    packageNames.forEach(function (pkgName) {
        var p = pkgName.replace('@angular/', '');
        packages[pkgName] = { main: 'bundles/' + p + '.umd.js', defaultExtension: 'js' };
        //packages[pkgName] = { main: 'index.js', defaultExtension: 'js' };
    });

    var config = {
        paths: paths,
        map: map,
        packages: packages
    };

    //if (global.filterSystemConfig) { global.filterSystemConfig(config); }

    System.config(config);

})(this);
