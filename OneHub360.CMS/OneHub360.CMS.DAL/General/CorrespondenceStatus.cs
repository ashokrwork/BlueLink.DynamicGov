namespace OneHub360.CMS.DAL
{
    public class BaseStatus
    {
        public static int New = 1;
        public static int Sent = 2;
        public static int Accepted = 3;
        public static int Rejected = 4;
        public static int Archived = 5;
    }

    /// <summary>
    /// Status codes starts in 1
    /// </summary>
    public class DraftLetterStatus : BaseStatus
    {

    }

    /// <summary>
    /// Status codes starts in 2
    /// </summary>
    public class DraftMemoStatus : BaseStatus
    {

    }

    /// <summary>
    /// Status codes starts in 3
    /// </summary>
    public class IncomingLetterStatus : BaseStatus
    {
        public static int Registered = 31;
        public static int Indexed = 32;
        public static int Scanned = 33;
        public static int ScannedIndexed = 34;

    }

    /// <summary>
    /// Status codes starts in 4
    /// </summary>
    public class IncomingMemoStatus : BaseStatus
    {

    }

    /// <summary>
    /// Status codes starts in 5
    /// </summary>
    public class OutgoingMemoStatus : BaseStatus
    {

    }

    /// <summary>
    /// Status codes starts in 6
    /// </summary>
    public class OutgoingLetterStatus : BaseStatus
    {
        public static int SentManual = 61;
        public static int SentG2G = 62;
        public static int Signed = 63;
    }
}
