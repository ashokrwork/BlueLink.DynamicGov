using OneHub360.Business;
using OneHub360.CMS.DAL;
using System;

namespace OneHub360.CMS.Business
{
    public class OutgoingMemoWorker : MemoWorker
    {
        public OutgoingMemoWorker(WorkerMode mode) : base(mode)
        {
        }

        public OutgoingMemoView GetView(Guid outgoingMemoId)
        {
            OutgoingMemoView outgoingMemoView;
            using (var context = new CMSContext())
            {
                using (var outgoingViewRepository = new OutgoingMemoViewRepository(context))
            {
                    outgoingMemoView = outgoingViewRepository.GetById(outgoingMemoId);
            }
                
            }
            return outgoingMemoView;
        }

        
    }
}
