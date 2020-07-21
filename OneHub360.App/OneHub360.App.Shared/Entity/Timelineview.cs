using System;
using NHibernate.Validator.Constraints;


namespace OneHub360.App.Shared
{
    
    public  partial class TimeLineView : DB.NHEntity {
        
        [NotNullNotEmpty]
        public virtual string Title { get; set; }
        public virtual string Brief { get; set; }
        public virtual string Body { get; set; }
        public virtual string ImageUrl { get; set; }
        [NotNullNotEmpty]
        public virtual string FeedId { get; set; }
        [NotNullNotEmpty]
        public virtual int Status { get; set; }
        public virtual int Scope { get; set; }
        [NotNullNotEmpty]
        public virtual bool Pinned { get; set; }
        public virtual string FileUrl { get; set; }
        public virtual Guid FK_From { get; set; }
        public virtual Guid FK_To { get; set; }
        [NotNullNotEmpty]
        public virtual DateTime Date { get; set; }
        [NotNullNotEmpty]
        public virtual int Priority { get; set; }
        public virtual string TemplateUrl { get; set; }
       
        [NotNullNotEmpty]
        public virtual string FeedTypeName { get; set; }
        public virtual string Text1 { get; set; }
        public virtual string Text2 { get; set; }
        public virtual string Text3 { get; set; }
        public virtual DateTime? Date1 { get; set; }
        public virtual DateTime? Date2 { get; set; }
        public virtual DateTime? Date3 { get; set; }
        public virtual int? Number1 { get; set; }
        public virtual int? Number2 { get; set; }
        public virtual int? Number3 { get; set; }
        public virtual string SharedWith { get; set; }
        [NotNullNotEmpty]
        public virtual Guid FeedTypeId { get; set; }

        public virtual string FollowedBy { get; set; }
    }
}
