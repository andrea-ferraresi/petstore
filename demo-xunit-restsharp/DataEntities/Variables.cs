using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace PetStore.EndToEndTests.DataEntities
{
    static class Variables
    {

        internal static string baseUrl = "https://petstore.swagger.io/v2";
        internal static string petRoute = "/pet";
        internal static string findPetByStatus = "/findByStatus";


        internal static string[] petStatusAvailable = { "available" };
        internal static string[] petStatusToBeVerified = {"pending"};
        internal static string[] petStatusSold = {"sold"};




    }
}
