module.exports = {
    entry: './app.js',
    output: {
        filename: 'Scripts/wwwroot/dist.js'
    },
    module: {
        rules: [
            {
                test: /\.vue$/,
                loader: 'vue-loader'
            }
        ]
    }
}