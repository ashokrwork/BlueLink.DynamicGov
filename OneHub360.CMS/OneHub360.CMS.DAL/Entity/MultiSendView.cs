namespace OneHub360.CMS.DAL
{

    public partial class MultiSendView : OneHub360.DB.NHEntity
    {
        public virtual string CopyTo { get; set; }
        public virtual string AddtionalRecipients { get; set; }
    }
}
