namespace DealershipAPI.Entities
{
    public class ModelEntity
    {
        public Guid ModelID { get; set; }
        public string Model { get ; set;}

        public Guid BodyID { get; set; }
        public BodyEntity Body { get; set; }

        public Guid BrandID { get; set; }
        public BrandEntity Brand { get; set; }


    }
}
