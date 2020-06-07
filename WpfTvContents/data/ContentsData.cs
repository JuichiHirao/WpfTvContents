using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTvContents.data
{
    public class ContentsData
    {
        public int Id { get; set; }
        public int ContentsId { get; set; }
        public int DetailId { get; set; }
        public int StoreId { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public string Source { get; set; }
        public long Duration { get; set; }

        public string VideoInfo { get; set; }

        public string Comment { get; set; }

        public long Size { get; set; }

        public int PriorityNum { get; set; }

        public DateTime FileDate { get; set; }

        public int Quality { get; set; }

        public int Rating1 { get; set; }

        public int Rating2 { get; set; }

        public string Remark { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Path { get; set; }
    }
}
