using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Models
{
    public class CityDTO
    {
        public CityDTO(City city)
        {
            this.Name = city.Name;
            this.CityId = city.CityId;
        }
        public int CityId { get; set; }
        public string Name { get; set; }

    }
}
