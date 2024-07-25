namespace EventShared
{
    static public class RabbitMQSettings
    {
        public const string CompanyCanShareJobEvent = "company-can-share-job-queue";
        public const string CanCompanyShareJobEventQueue = "can-company-share-job-queue";
        public const string JobCannotCreatedEventQueue = "job-cannot-created-event-queue";
        public const string CompanyDontHaveJobCountEventQueue = "company-donthave-jobcount-event-queue";
    }
}
