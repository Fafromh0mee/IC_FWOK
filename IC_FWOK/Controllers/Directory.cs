public class Directory
{
    public string Name { get; set; }
    public List<System.IO.FileInfo> Files { get; set; } = new List<System.IO.FileInfo>();

    public void AddFile(System.IO.FileInfo file)
    {
        Files.Add(file);
    }
}