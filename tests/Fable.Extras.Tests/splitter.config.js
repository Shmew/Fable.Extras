const path = require("path");

module.exports = {
    allFiles: true,
    entry: path.join(__dirname, "./Fable.Extras.Tests.fsproj"),
    outDir: path.join(__dirname, "../../dist/tests/FC"),
    babel: {
        plugins: ["@babel/plugin-transform-modules-commonjs"],
        sourceMaps: "inline"
    }
};