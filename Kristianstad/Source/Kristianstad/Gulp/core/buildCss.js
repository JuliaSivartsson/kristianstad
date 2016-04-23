var gulp = require('gulp'),
    rename = require('gulp-rename'),
    sass = require('gulp-sass'),
    autoprefixer = require('gulp-autoprefixer'),
    cssnano = require('gulp-cssnano'),
    del = require("del")
    Promise = require("bluebird"),
    coreCss = require('./cssDep'),
    dependencies = require('./../dependencies');

var autoprefixerOpts = {
    browsers: [
        'Android >= 4',
        'last 2 Chrome versions',
        'last 2 Firefox versions',
        'Explorer >= 10',
        'last 2 iOS versions',
        'last 2 Opera versions',
        'last 2 Safari versions'
    ],
    cascade: false
};

var vendorScssPaths = [
    'node_modules/foundation-sites/scss',
    'node_modules/tablesaw/dist/bare',
    'node_modules/font-awesome/scss'
];

function delCss(done) {
    del([
      './Static/css/**/*.css',
      './dist/Static/css/**/*.css'
    ], { force: true }).then(function () { done() });
}

function debug() {
    var src = coreCss.concat(dependencies.css.src);
    return gulp.src(src)
        .pipe(sass({
            includePaths: vendorScssPaths
        }).on('error', sass.logError))
        .pipe(autoprefixer(autoprefixerOpts))
        .pipe(gulp.dest('./Static/css'))
        .pipe(gulp.dest('./dist/Static/css'));
}

function guideCss() {
    return gulp.src('./Content/guide/scss/main.scss')
        .pipe(sass({
            includePaths: vendorScssPaths
        }).on('error', sass.logError))
        .pipe(autoprefixer(autoprefixerOpts))
        .pipe(rename('main' + '-guide.css'))
        .pipe(gulp.dest('./dist/Static/css'));
}

function release() {
    var src = coreCss.concat(dependencies.css.src);
    return gulp.src(src)
        .pipe(sass({
            includePaths: vendorScssPaths
        }).on('error', sass.logError))
        .pipe(autoprefixer(autoprefixerOpts))
        .pipe(cssnano())
        .pipe(rename(function (path) {
            if (path.suffix == 'main') {
                path.suffix += ".min";
            }
        }))
        .pipe(gulp.dest('./Static/css'))
        .pipe(gulp.dest('./dist/Static/css'));
}

module.exports = {
    debug: gulp.series(delCss, debug, guideCss),
    release: gulp.series(delCss, release)
};