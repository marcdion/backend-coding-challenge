const webpack = require('webpack');
const isProd = process.env.NODE_ENV === "production";

module.exports = {
  publicPath: isProd ? "" : "",
  configureWebpack: {
    // Set up all the aliases we use in our app.
    plugins: [
      new webpack.optimize.LimitChunkCountPlugin({
        maxChunks: 6
      })
    ],
  },
  devServer: {
    //proxy: 'http://localhost:5001'
  },
  css: {
    // Enable CSS source maps.
    sourceMap: process.env.NODE_ENV !== 'production'
  }
};
