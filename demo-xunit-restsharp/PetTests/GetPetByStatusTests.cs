using RestSharp;
using RestSharp.Authenticators;
using System.Net;
using Xunit;
using FluentAssertions;
using static PetStore.EndToEndTests.DataEntities.Variables;
using PetStore.EndToEndTests.RestClientNamespace;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace PetStore.EndToEndTests.GetPetByStatus
{
  public class GetPetByStatusTests
  {

    public static IEnumerable<object[]> availabilityStatus()
    {
        yield return new object[] { petStatusListAvailable };
        yield return new object[] { petStatusListToBeVerified };
        yield return new object[] { petStatusListSold };
    }


    [Trait("Category", "DataIndependent")]
    [Theory]
    [MemberData(nameof(availabilityStatus))]
    public void the_api_can_provide_a_list_of_all_the_pets_by_specifying_the_availability_status(string[] currentAvailabilityStatus)
    {
      var restClient = new RestClientInitializationAndActions();

      var response = restClient.get_pet_with_availability_statuses(currentAvailabilityStatus);      

      response.StatusCode.Should().Be(HttpStatusCode.OK);            
      JArray listOfPets = JArray.Parse(response.Content);     
      var numberOfPets = listOfPets.Count;
      for (int i=0; i < numberOfPets; i++)
      {
          // of course, this assertion will be more precise when I will test a request containing more than one status
          listOfPets[i]["status"].Value<string>().Should().Be(currentAvailabilityStatus[0]);
      }
            
    }







  }
}
