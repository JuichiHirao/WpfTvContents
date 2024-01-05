using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTvContents.data
{
    class DiskData
    {
        public int Id { get; set; }

        public int No { get; set; }

        public string Label { get; set; }

        public string Path { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
