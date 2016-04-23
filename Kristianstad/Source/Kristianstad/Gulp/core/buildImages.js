
var gulp = require('gulp'),
    del = require("del");

function delImages(done) {
    del([
      './dist/Static/img/**/*.*',
      './Static/img/**/*.*'
    ], { force: true }).then(function () { done() });
}

function build() {
    return gulp.src(['./Content/core/img/**/*.*', './Content/img/**/*.*'])
        .pipe(gulp.dest('./dist/Static/img'))
        .pipe(gulp.dest('./Static/img'));
};

module.exports = {
    build: gulp.series(delImages, build)
};