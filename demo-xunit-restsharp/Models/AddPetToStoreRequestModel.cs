namespace PetStore.EndToEndTests.Models
{

  public class AddPetToStoreRequestModel 
  {
    public long id { get; set; }
    public string name { get; set; }
        /*
         * public string[] photoUrls { get; set; }
         * this is mandatory
         * */

        public string status { get; set; }


}


}
