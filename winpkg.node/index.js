const {GraphQLServer} = require('graphql-yoga');
const mongoose = require('mongoose');
mongoose.connect("mongodb://localhost:27017/winstall")

const Package= mongoose.model("packages",{
    PackageId: String,
    Pub: String,
    Version: String,
    Name: String,
    Publisher: String,
    AppMoniker: String,
    License: String,
    Homepage: String,
    LicenseUrl: String,
    Description: String
});

const typeDefs = `type Query {
    getPackage(id: ID!): Package
    getPackages: [Package]
}
type Package {
    id: ID!
    PackageId: String!
    Pub: String!
    Version: String!
    Name: String!
    Publisher: String!
    AppMoniker: String!
    License: String!
    Homepage: String!
    LicenseUrl: String!
    Description: String!
}

type Mutation{
    addPackage(PackageId: String!, Pub: String!, Version: String!,  Name: String!,    Publisher: String!,    AppMoniker: String!,    License: String!,    Homepage: String!,    LicenseUrl: String!,    Description: String!): Package!,
    deletePackage(PackageId: ID!): String
}
`

const resolvers = {
    Query: {
        getPackages: () => Package.find(),
        getPackage: async (_,{id}) =>{
            var result = await Package.findById(id);
            return result;
        }
    },
    Mutation: {
        addPackage: async (_, {PackageId, Pub, Version,  Name,    Publisher,    AppMoniker,    License,    Homepage,    LicenseUrl,    Description}) => {
            const package = new Package({ PackageId, Pub, Version,  Name,    Publisher,    AppMoniker,    License,    Homepage,    LicenseUrl,    Description});
            await package.save();
            return package;
        },
        deletePackage: async (_, {id}) => {
            await Package.fundByIdAndRemove(id);
            return "Package deleted";
        }
    }
}

const server = new GraphQLServer({typeDefs, resolvers})
mongoose.connection.once("open",function(){
    server.start(() => console.log('Server is running on localhost:4000'))
})

