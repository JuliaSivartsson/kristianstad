
var gulp = require('gulp'),
    del = require("del");

function delFonts(done) {
    del([
      './dist/Static/font/**/*.*',
      './Static/font/**/*.*'
    ], { force: true }).then(function () { done() });
}

function fontAwesome() {
    return gulp.src(['node_modules/font-awesome/fonts/**/*.*'])
        .pipe(gulp.dest('./dist/Static/font/font-awesome'))
        .pipe(gulp.dest('./Static/font/font-awesome'));
}

function fonts() {
    return gulp.src(['./Content/core/font/**/*.*', './Content/font/**/*.*'])
        .pipe(gulp.dest('./dist/Static/font/'))
        .pipe(gulp.dest('./Static/font/'));
};

module.exports = {
    build: gulp.series(delFonts, fontAwesome, fonts)
};