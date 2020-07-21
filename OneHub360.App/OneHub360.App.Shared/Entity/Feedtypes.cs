using System;
using System.Text;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;


namespace OneHub360.App.Shared
{
    
    public  partial class FeedTypes : OneHub360.DB.NHEntity {
        public  FeedTypes() { }
        public virtual string Title { get; set; }
        [NotNullNotEmpty]
        public virtual bool ShowInHome { get; set; }
        public virtual string TemplateUrl { get; set; }
        public virtual string NewFormUrl { get; set; }
        [NotNullNotEmpty]
        public virtual bool ShowInNewItemStrip { get; set; }
        public virtual string NewStripImageUrl { get; set; }

        public virtual string GroupTitle { get; set; }
    }
}
