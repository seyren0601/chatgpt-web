namespace ChatGPTCaller.Models
{
    public class MonHoc
    {
        public string IdMonhoc { get; set; }
        public string TitleMonhoc { get; set; }
        public string ContentMonhoc { get; set; }
        public MonHoc() { }
        public MonHoc(string idMonhoc, string titlemonhoc, string contentmonhoc)
        {
            IdMonhoc = idMonhoc;
            TitleMonhoc = titlemonhoc;
            ContentMonhoc = contentmonhoc;
        }
    }
    public class Chuong
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string IdMonhoc { get; set; }
        public string ParentId { get; set; }
        public Chuong() { }
        public Chuong(string id, string title, string idMonhoc, string parentId)
        {
            Id = id;
            Title = title;
            IdMonhoc = idMonhoc;
            ParentId = parentId;
        }
    }
}
