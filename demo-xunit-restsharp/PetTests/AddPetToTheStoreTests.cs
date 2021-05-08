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
  public class AddPetToTheStoreTests
    {

    public static IEnumerable<object[]> petsAreArriving()
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        long unixTimeMilliseconds = now.ToUnixTimeMilliseconds();
        long uniqueIdentifierOfThePet = unixTimeMilliseconds;
        yield return new object[] { uniqueIdentifierOfThePet, "petToBeRemoved", petStatusAvailable };
    }


    [Trait("Category", "DataIndependent")]
    [Theory]
    [MemberData(nameof(petsAreArriving))]
    public void the_api_allow_to_add_a_pet_to_the_store_by_specifying(long uniqueIdentifierForThePet, string nameOfThePet, string currentAvailabilityStatus)
    {
      var restClient = new RestClientInitializationAndActions();

      var response = restClient.add_pet_to_the_store(uniqueIdentifierForThePet, nameOfThePet, currentAvailabilityStatus);      

      response.StatusCode.Should().Be(HttpStatusCode.OK);
            /* I would have expected a Created
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            */
      response = restClient.get_pet_with_availability_status(currentAvailabilityStatus);
      JArray listOfPets = JArray.Parse(response.Content);     
      var numberOfPets = listOfPets.Count;
      bool petSuccessfullyAdded = false;

      for (int i=0; i < numberOfPets; i++)
      {
          if (uniqueIdentifierForThePet == (long)listOfPets[i]["id"])
                {
                    petSuccessfullyAdded = true;
                    i = numberOfPets; // to break out of the for cycle
                }
      }
      petSuccessfullyAdded.Should().BeTrue();


    }







  }
}
