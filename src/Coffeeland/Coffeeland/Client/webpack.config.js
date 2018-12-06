const path = require('path');

module.exports = {
    mode: "production",
    entry: './src/index.js',
    resolve: {
        extensions: [ '.js' ]
    },
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, 'dist')
    },
    module: {
        rules: [
          {
            test: /\.css$/,
            use: [
              { loader: "css-loader" }
            ]
          }
        ]
      }
};
