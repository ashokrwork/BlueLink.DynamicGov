using NHibernate.Validator.Constraints;


namespace OneHub360.App.Shared
{

    public  partial class JobTitles : DB.NHEntity {
        public  JobTitles() { }

        [NotNullNotEmpty]
        public virtual string Title { get; set; }
        public virtual string Responsibilities { get; set; }
        public virtual int? Rank { get; set; }
        public virtual string Description { get; set; }
    }
}
