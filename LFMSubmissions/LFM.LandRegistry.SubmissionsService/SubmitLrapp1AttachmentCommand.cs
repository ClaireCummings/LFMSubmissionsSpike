namespace LFM.LandRegistry.SubmissionsService
{
    public class SubmitLrapp1AttachmentCommand
    {
        public string AttachmentId { set; get; }
        public string ApplicationId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Payload { get; set; }
    }
}
