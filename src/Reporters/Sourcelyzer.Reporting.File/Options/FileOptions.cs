namespace Sourcelyzer.Reporting.File.Options
{
    public class FileOptions
    {
        public FileOptions(string path)
        {
            Path = path;
        }

        internal string Path { get; }

//        internal SegregationType Segregation { get; private set; }
//
//        public FileOptions SegregateBy(SegregationType type)
//        {
//            Segregation = type;
//            return this;
//        }
    }
}
