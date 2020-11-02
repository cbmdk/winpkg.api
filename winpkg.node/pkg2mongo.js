const mongoose = require('mongoose');
mongoose.connect("mongodb://localhost:27017/winstall")

const readFiles = require("node-read-yaml-files");

readFiles("./node_modules").then((docs) => console.log(docs));