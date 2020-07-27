using NHibernate.Validator.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneHub360.CMS.DAL
{
    public class OutgoingLetter : Correspondence
    {
        
        
        public virtual string IncomingNumber { get; set; }
        public virtual DateTime? IncomingDate { get; set; }
        public virtual string OutgoingNumber { get; set; }
        public virtual DateTime? OutgoingDate { get; set; }
        
        public virtual DateTime ReviewingDate { get; set; }
        
        public virtual string ReviewedBy { get; set; }
        public virtual DateTime? MovingDate { get; set; }
        public virtual string MovedBy { get; set; }
        public virtual DateTime? DeliveryDate { get; set; }
        public virtual string DeliverdTo { get; set; }
        public virtual DateTime? DeliveryConfirmationDate { get; set; }
        
       
        public virtual string G2GNumber { get; set; }
        public virtual DateTime? G2GDate { get; set; }
    }
}
