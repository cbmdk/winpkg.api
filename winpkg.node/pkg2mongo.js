const mongoose = require('mongoose');
mongoose.connect("mongodb://localhost:27017/winstall")

// const readFiles = require("node-read-yaml-files");
const yaml = require('js-yaml');
const fs   = require('fs');

// Get document, or throw exception on error
try {
  const doc = yaml.safeLoad(fs.readFileSync('C:\\code\\packages\\manifests\\3T\\Robo3T\\1.3.1.yaml', 'utf8'));
  console.log(doc);
} catch (e) {
  console.log(e);
}
// const fs = require('fs');
// const testFolder = 's:\\temp';

// fs.readdir(testFolder, (err, files) => {
//   if (err) return console.log(err);
//   files.forEach(file => {
//     console.log(file);
//   });
// });