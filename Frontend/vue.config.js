// vue.config.js
module.exports = {
  // options...
  publicPath: process.env.NODE_ENV === "production" ? "/" : "/",
  assetsDir: "",
  filenameHashing: true,
  productionSourceMap: false,
  devServer: {
    port: 8088,
    https: false
  },
  transpileDependencies: ["vuetify"],
  lintOnSave: process.env.NODE_ENV !== "production"
};
