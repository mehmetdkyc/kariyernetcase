namespace DataAccess.Entities.Common
{
    public class BaseEntity
    {
        public string Id { get; set; }

        /// <summary>
        /// İlan Yayında Kalma Süres, Geçerlilik Tarihi
        /// </summary>
        public required DateTime ExpiredDate { get; set; }
    }
}
