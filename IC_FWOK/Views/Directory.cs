namespace IC_FWOK.Models
{
    public class Directory
    {
        public string Name { get; set; }
        public List<FileInfo> Files { get; set; }
        public void AddFile(FileInfo file) { /* implementation */ }
    }
}
