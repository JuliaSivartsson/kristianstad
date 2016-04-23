var gulp = require('gulp'),
    nunjuckshtml = require('gulp-nunjucks-html'),
    del = require("del"),
    replace = require('gulp-replace');

function delHtml(done) {
    del([
     './dist/**/*.html'
    ]).then(function () { done() });
};


function site() {
    var src = ['./Templates/site/**/*.html', '!./Templates/site/**/_*.html'];
    return gulp.src(src)
        .pipe(nunjuckshtml({
            searchPaths: ['./Templates/']
        }))
        .on('error', function (err) {
            console.log(err)
        })
        // Remove byte order marks (BOM)
        .pipe(replace(String.fromCharCode(65279), ''))
        .pipe(gulp.dest('./dist/site/'));
};

function guide() {
    var src = ['./Templates/guide/**/*.html', '!./Templates/guide/**/_*.html'];
    return gulp.src(src)
        .pipe(nunjuckshtml({
            searchPaths: ['./Templates/']
        }))
        .on('error', function (err) {
            console.log(err)
        })
        // Remove byte order marks (BOM)
        .pipe(replace(String.fromCharCode(65279), ''))
        .pipe(gulp.dest('./dist/'));
}

module.exports = {
    build: gulp.series(delHtml, site, guide)
};