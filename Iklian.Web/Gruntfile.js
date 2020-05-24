/// <binding BeforeBuild='clean' AfterBuild='copy, uglify, cssmin' />
module.exports = function (grunt) {
    grunt.initConfig({
        clean: ["wwwroot/*", "temp/"],
        cssmin: {
            target: {
                files: {
                    "wwwroot/css/site.min.css":
                        ["node_modules/bootstrap/dist/css/*.css",
                            "Static/Css/*.css"]
                }
            }
        },
        uglify: {
            target: {
                files: {
                    'wwwroot/js/site.min.js':
                        ["node_modules/jquery/dist/jquery.js",
                            "node_modules/bootstrap/dist/js/bootstrap.js"]
                }
            }
        },
        copy: {
            target: {
                expand: true,
                flatten: true,
                src: 'Static/Scripts/*.js',
                dest: 'wwwroot/js/'
            },
            favicon: {
                expand: true,
                flatten: true,
                src: 'Static/favicon-*.png',
                dest: 'wwwroot/'
            }
        }
    });

    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks("grunt-contrib-copy");
};