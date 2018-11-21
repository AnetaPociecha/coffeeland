const path = require('path');

module.exports = {
    mode: "production",
    entry: './App/App.js',
    resolve: {
        extensions: [ '.js' ]
    },
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, 'dist')
    }
};
