/* eslint-env node */
const path = require('path');

module.exports = {
  mode: 'development',
  entry: {
    index: [
      '@weavy/dropin-js/dist/weavy-dropin.css',
      './js/index.js',
    ]
  },
  output: {
    filename: 'js/[name].js',
    path: path.resolve(__dirname, 'wwwroot'),
    assetModuleFilename: 'css/[name][ext]'
  },
  module: {
    //noParse: /dropin-js/,
    rules: [
      {
        test: /\.css/,
        type: 'asset/resource'
      }
    ]
  }
};

