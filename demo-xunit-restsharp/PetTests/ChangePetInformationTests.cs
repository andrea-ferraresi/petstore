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
  public class ChangePetInformationTests
    {

    public static IEnumerable<object[]> petIsSold()
    {
        DateTimeOffset now = DateTimeOffset.UtcNow;
        long unixTimeMilliseconds = now.ToUnixTimeMilliseconds();
        long uniqueIdentifierOfThePet = unixTimeMilliseconds;
        yield return new object[] { uniqueIdentifierOfThePet, "petToBeRemoved", petStatusAvailable, petStatusSold };
    }


    [Trait("Category", "DataIndependent")]
    [Theory]
    [MemberData(nameof(petIsSold))]
    public void the_api_allow_to_change_the_pet_information_after_a_sale(long uniqueIdentifierForThePet, string nameOfThePet, string initialAvailabilityStatus, string finalAvailabilityStatus)
    {
      var restClient = new RestClientInitializationAndActions();
      var response = restClient.add_pet_to_the_store(uniqueIdentifierForThePet, nameOfThePet, initialAvailabilityStatus);
      response.StatusCode.Should().Be(HttpStatusCode.OK);
      /* I would have expected a Created
      response.StatusCode.Should().Be(HttpStatusCode.Created);
      */

      response = restClient.change_the_availability_status_for_the_pet_in_the_store(uniqueIdentifierForThePet, finalAvailabilityStatus);

      response.StatusCode.Should().Be(HttpStatusCode.OK);
      response = restClient.retrieve_the_pet_by(uniqueIdentifierForThePet);
      JObject petWithUpdatedInformation = JObject.Parse(response.Content);
      petWithUpdatedInformation["status"].Should().BeEmpty(petStatusSold);

      response = restClient.remove_the_pet_from_the_store(uniqueIdentifierForThePet);
      response.StatusCode.Should().Be(HttpStatusCode.OK);


     }







  }
}
