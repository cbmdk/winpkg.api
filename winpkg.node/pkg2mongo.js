const mongoose = require("mongoose");
mongoose.connect("mongodb://localhost:27017/winstall");

// const readFiles = require("node-read-yaml-files");
const yaml = require("js-yaml");
const fs = require("fs");
const glob = require("glob")

var packages = [];

const dir = 'C:\\code\\packages\\manifests\*.yaml';

glob(dir, function (err, files) {
    // files is an array of filenames.
    // If the `nonull` option is set, and nothing
    // was found, then files is ["**/*.js"]
    // er is an error object or null.
    if(err){
        throw err;
    }
    console.log(files)
    // files.foreach(file =>{
    //     console.log(file);
    // })
  })

// fs.readdir(dir, (err,files) => {
//     if(err){
//         throw err;
//     }
//     files.forEach(element => {
//         console.log(element);
//     });
// });


function ReadAndStoreFiles(theFile) {
  // Get document, or throw exception on error
  try {
    const doc = yaml.safeLoad(
      fs.readFileSync(
        theFile,
        "utf8"
      )
    );
    packages.push(doc);
    
  } catch (e) {
    console.log(e);
  }
  return packages;
}
// const fs = require('fs');
// const testFolder = 's:\\temp';

// fs.readdir(testFolder, (err, files) => {
//   if (err) return console.log(err);
//   files.forEach(file => {
//     console.log(file);
//   });
// });
