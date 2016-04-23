var gulp = require('gulp'),
    concat = require('gulp-concat'),
    uglify = require('gulp-uglify'),
    rename = require('gulp-rename'),
    del = require("del"),
    coreJs = require('./jsDep'),
    dependencies = require('./../dependencies');

// JS
// ----------------------

function delJs(done) {
    del([
      './Static/js/**/js/*.*',
      './dist/Static/js/**/*.js'
    ], { force: true }).then(function () { done() });
}

function debug() {
    var src = coreJs.lib.concat(coreJs.src).concat(dependencies.js.lib.concat(dependencies.js.src));
    return gulp.src(src)
        .pipe(concat('main.js'))
        .pipe(gulp.dest('./Static/js'))
        .pipe(gulp.dest('./dist/Static/js'));
};

function release() {
    var src = coreJs.lib.concat(coreJs.src).concat(dependencies.js.lib.concat(dependencies.js.src));
    return gulp.src(src)
        .pipe(concat('main.min.js'))
        .pipe(uglify())
        .pipe(gulp.dest('./Static/js'))
        .pipe(gulp.dest('./dist/Static/js'));
};

module.exports = {
    debug: gulp.series(delJs, debug),
    release: gulp.series(delJs, release)
};