var gulp = require('gulp'),
    connect = require('gulp-connect'),
    buildJs = require('./Gulp/core/buildJs'),
    buildFonts = require('./Gulp/core/buildFonts'),
    buildCss = require('./Gulp/core/buildCss'),
    buildTemplates = require('./Gulp/core/buildTemplates'),
    buildImages = require('./Gulp/core/buildImages');

gulp.task('buildJs:debug', buildJs.debug);
gulp.task('buildJs:release', buildJs.release);

gulp.task('buildFonts:debug', buildFonts.build);
gulp.task('buildFonts:release', buildFonts.build);

gulp.task('buildCss:debug', buildCss.debug);
gulp.task('buildCss:release', buildCss.release);

gulp.task('buildImages:debug', buildImages.build);
gulp.task('buildImages:release', buildImages.build);

gulp.task('buildTemplates:debug', buildTemplates.build);

gulp.task('build:debug', gulp.parallel('buildJs:debug', 'buildFonts:debug', 'buildCss:debug', 'buildImages:debug', 'buildTemplates:debug'));
gulp.task('build:release', gulp.parallel('buildJs:release', 'buildFonts:release', 'buildCss:release', 'buildImages:release'));

gulp.task('connect', function () {
    return connect.server({
        root: 'dist',
        livereload: true
    });
});

function reload() {
    return gulp.src(['./gulpfile.js'])
        .pipe(connect.reload());
}

gulp.task('watch', function () {
    gulp.watch(['./Content/**/*.js'], gulp.series('buildJs:debug', reload));
    gulp.watch(['./Content/**/font/**/*.*'], gulp.series('buildFonts:debug', reload));
    gulp.watch(['./Content/**/*.scss'], gulp.series('buildCss:debug', reload));
    gulp.watch(['./Content/**/img/**/*.*'], gulp.series('buildImages:debug', reload));
    gulp.watch(['./Templates/**/*.html'], gulp.series('buildTemplates:debug', reload));
});

gulp.task('default', gulp.parallel('build:debug', 'connect', 'watch'));