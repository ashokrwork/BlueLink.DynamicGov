using OneHub360.App.Shared;
using OneHub360.DB;

namespace OneHub360.App.DAL
{
    public class NotificationRepository : NHEntityRepository<Notification>
    {
        public NotificationRepository(IDBContext context) : base(context)
        {
        }
    }
}
