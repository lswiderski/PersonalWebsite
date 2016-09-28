/// <binding BeforeBuild='clean, sass' />
/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {

    grunt.loadNpmTasks('grunt-sass');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        clean: ["wwwroot/css/*", "temp/"],
        watch: {
            files: '../assets/css/sass/**/*.scss',
            tasks: ['clean', 'sass', 'cssmin']
        },
        // Sass
        sass: {
            options: {
                bundleExec: true,
                lineNumbers: true,
                sourcemap: 'none', // Create source map
                outputStyle: 'expanded' // Minify output
            },
            dist: {     
                files: [{
                      expand: true, // Recursive
                      cwd: 'sass', // The startup directory
                      src: ['*.scss'], // Source files
                      dest: 'wwwroot/css/', // Destination
                      ext: '.css' // File extension 
                  }]
            }
        },
        cssmin: {
            my_target: {
                files: [{
                    expand: true,
                    cwd: 'wwwroot/css/',
                    src: ['*.css', '!*.min.css'],
                    dest: 'wwwroot//css/',
                    ext: '.min.css'
                }]
            }
        }
    });
};