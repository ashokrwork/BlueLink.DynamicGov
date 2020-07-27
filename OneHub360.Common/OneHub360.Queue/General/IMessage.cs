using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace OneHub360.Queue
{
    public interface IMessage
    {
        /// <summary>
        /// Number of iteration to process this message
        /// </summary>
        [DefaultValue(0)]
        int ProcessingIterations { get; set; }

        /// <summary>
        /// Date Message Created in 
        /// </summary>
        DateTime CreationDate { get; set; }

        /// <summary>
        /// Get, Set the User ID Created this item
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Process the Current Message  
        /// </summary>
        /// <returns>bool value indicate success of the the processing for each Message</returns>
        Task<bool> Process();

        Task<bool> Retry();
    }
}
