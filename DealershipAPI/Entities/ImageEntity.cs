namespace DealershipAPI.Entities
{
    public class ImageEntity
    {
        public Guid ImageID { get; set; }
        public Guid CarID { get; set; }
        public CarEntity Car { get; set; }
        public string ImageURL { get; set; }
        public int Main { get; set; }
    }
}
