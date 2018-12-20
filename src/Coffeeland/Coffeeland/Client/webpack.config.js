const path = require('path');
const HtmlWebpackPlugin = require('html-webpack-plugin');


module.exports = {
    entry: ['bootstrap', 'babel-polyfill', './src/index.js'],
    output: {
        path: path.join(__dirname, '/dist'),
        filename: 'bundle.js'
    },
	devtool: 'source-map',
    module: {
        rules: [
            {
                test: [/\.jsx?$/, /\.js?$/],
                resolve: { extensions: [".js", ".jsx"]},
                exclude: /node_modules/,
                loader: 'babel-loader',
            },
			{
				test: /\.css$/,
				loader: 'style-loader!css-loader'
	      	},
	      {
				test: /\.(jpe?g|png|gif|svg)$/i,
				loader: 'file-loader'
	      }
        ]
    },
    watchOptions: {
        poll: 1000 // Check for changes every second
      }
}
