/// <binding BeforeBuild='clean, build' Clean='clean' />
"use strict";

var gulp = require("gulp"),
  rimraf = require("rimraf"),
  concat = require("gulp-concat"),
  cssmin = require("gulp-cssmin"),
  uglify = require("gulp-uglify"),
  rename = require("gulp-rename"),
  gulpif = require("gulp-if"),
  path = require('path');

var paths = {
    webroot: "./wwwroot/",
    libs: "./wwwroot/lib/"
};

/** Array of arrays: [source (glob), destination (folder or file)]
 * If destination == file, source will be renamed.
 * If destination == folder, source filename will be preserved.
   */
var libs = [
    [
        'node_modules/knockout/build/output/knockout-latest.js',
        paths.webroot + "lib/knockout/knockout.js"
    ],
    [
        'node_modules/bootstrap/dist/css/bootstrap.css',
        paths.webroot + "lib/bootstrap/css/bootstrap.css"
    ],
    [
        'node_modules/bootstrap/dist/js/bootstrap.js',
        paths.webroot + "lib/bootstrap/js/bootstrap.js"
    ],
    [
        'node_modules/bootstrap/dist/fonts/*',
        paths.webroot + "lib/bootstrap/fonts/"
    ],
    [
        'node_modules/jquery/dist/jquery.js',
        paths.webroot + "lib/jquery/jquery.js"
    ]
];

/** Destination for production tasks */
paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
      .pipe(concat(paths.concatJsDest))
      .pipe(uglify())
      .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
      .pipe(concat(paths.concatCssDest))
      .pipe(cssmin())
      .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);

/** Copy libs from npm.
 * @see libs
  */
gulp.task("build:libs", function () {
    libs.map(function (pathPair) {                                                                  //foreach array in libs
        gulp.src(pathPair[0])
            .pipe(gulpif(
                path.extname(pathPair[1]) !== '',
                rename(path.basename(pathPair[1]))))
            .pipe(gulpif(
                path.extname(pathPair[1]) !== '',
                gulp.dest(path.dirname(pathPair[1])),
                gulp.dest(pathPair[1])));                                            //put in dir of [1]
    });
});

/** Build front-end side of the site. 
 Should be ran before build of the main project. */
gulp.task("build", ["build:libs"]);