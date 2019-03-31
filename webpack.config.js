const path = require('path')
const webpack = require('webpack')
const VueLoaderPlugin = require('vue-loader/lib/plugin')
const bundleOutputDir = './wwwroot/dist';
const ExtractTextPlugin = require('extract-text-webpack-plugin');

module.exports = {
 context: __dirname,
  entry: { main: './Front/index.js' },
  module: {
    rules: [
      {
        test: /\.css$/,
        use: ExtractTextPlugin.extract({
          use: ['css-loader']
        })
      },
      {
        test: /\.scss$/,
        use: ExtractTextPlugin.extract({
          use: ["css-loader", "sass-loader"]
        })
      },
      {
        test: /\.vue$/,
        loader: 'vue-loader',
        options: {
          loaders: {
            'scss': [
              'vue-style-loader',
              'css-loader',
              'sass-loader'
            ],
            'sass': [
              'vue-style-loader',
              'css-loader',
              'sass-loader?indentedSyntax'
            ]
          }
        }
      },
      {
        test: /\.js$/,
        loader: 'babel-loader',
        exclude: /node_modules/
      },
      {
        test: /\.(png|jpg|gif|svg)$/,
        loader: 'file-loader',
        options: {
          name: '[name].[ext]?[hash]'
        }
      }
    ]
  },
  plugins: [
    new VueLoaderPlugin(),
    new ExtractTextPlugin('bundle.css')
  ],
  resolve: {
    alias: {
      'vue$': 'vue/dist/vue.esm.js'
    },
    extensions: ['*', '.js', '.vue', '.json']
  },
  devServer: {
    historyApiFallback: true,
    noInfo: true,
    overlay: true
  },
  performance: {
    hints: false
  },output: {
    path: path.join(__dirname, bundleOutputDir),
    filename: 'main.js',
    publicPath: 'dist/'
},
  devtool: '#eval-source-map'
}

if (process.env.NODE_ENV === 'production') {
  module.exports.devtool = '#source-map'
  module.exports.plugins = (module.exports.plugins || []).concat([
    new webpack.DefinePlugin({
      'process.env': {
        NODE_ENV: '"production"'
      }
    }),
    new webpack.optimize.UglifyJsPlugin({
      sourceMap: true,
      compress: {
        warnings: false
      }
    }),
    new webpack.LoaderOptionsPlugin({
      minimize: true
    })
  ])
}