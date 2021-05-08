using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using Xunit;
using FluentAssertions;
using static PetStore.EndToEndTests.DataEntities.Variables;
using PetStore.EndToEndTests.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace PetStore.EndToEndTests.RestClientNamespace
{
  public class RestClientInitializationAndActions
  {
    public RestClient client;



    public RestClientInitializationAndActions()
    {

      client = new RestClient(baseUrl);

    }








    
    public IRestResponse get_pet_with_availability_statuses(string[] availabilityStatusesRequested)
    {
      var request = new RestRequest(petRoute + findPetByStatus, Method.GET);

      var numberOfAvailabilityStatusesRequested = availabilityStatusesRequested.Length;
      for (int i=0; i < numberOfAvailabilityStatusesRequested; i++)
      {
          request.AddParameter("status", availabilityStatusesRequested[i]);
      }


      /*
      var availabilityStatusesRequested = new FindPetByStatusRequestModel();
      availabilityStatusesRequested.availabilityStatuses = availabilityStatusRequested;
      request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(availabilityStatusesRequested), ParameterType.RequestBody);
      */

      System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls11;
      this.client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
      var response = client.Execute(request);

      return response;
    }


        public IRestResponse get_pet_with_availability_status(string availabilityStatusRequested)
        {
            var request = new RestRequest(petRoute + findPetByStatus, Method.GET);

            request.AddParameter("status", availabilityStatusRequested);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls11;
            this.client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var response = client.Execute(request);

            return response;
        }




        public IRestResponse add_pet_to_the_store(long uniqueIdentifier, string nameOfThePet, string availabilityStatus)
        {
            var request = new RestRequest(petRoute, Method.POST);

            
            var brandNewPet = new AddPetToStoreRequestModel();
            brandNewPet.id = uniqueIdentifier;
            brandNewPet.name = nameOfThePet;
            brandNewPet.status = availabilityStatus;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(brandNewPet), ParameterType.RequestBody);
            

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls11;
            this.client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var response = client.Execute(request);

            return response;
        }



        public IRestResponse remove_the_pet_from_the_store(long uniqueIdentifier)
        {
            var request = new RestRequest(petRoute + "/" + uniqueIdentifier, Method.DELETE);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls11;
            this.client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var response = client.Execute(request);

            return response;
        }




        public IRestResponse change_the_availability_status_for_the_pet_in_the_store(long uniqueIdentifier, string newAvailabilityStatus)
        {
            var request = new RestRequest(petRoute, Method.PUT);



            var petToBeUpdated = new AddPetToStoreRequestModel();
            petToBeUpdated.id = uniqueIdentifier;
            petToBeUpdated.name = "read the eplanation";
            /*
             * EXPLANATION
             * since the name parameter is mandatory, I would retrieve here the name of the pet by querying the endpoint /pet/{petId}
             */
            petToBeUpdated.status = newAvailabilityStatus;
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(petToBeUpdated), ParameterType.RequestBody);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls11;
            this.client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var response = client.Execute(request);

            return response;
        }



        public IRestResponse retrieve_the_pet_by(long uniqueIdentifier)
        {
            var request = new RestRequest(petRoute + "/" + uniqueIdentifier, Method.GET);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls11;
            this.client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            var response = client.Execute(request);

            return response;
        }



    }
}
