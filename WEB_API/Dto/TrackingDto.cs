namespace WEB_API.Dto
{
    public class TrackingDto
    {
        public int Shipment_Id { get; set; }
        public string? From_Location { get; set; }
        public string? To_Location { get; set; }
        public string? Note { get; set; }
        public int Tracking_Status_Id { get; set; }
    }
}
