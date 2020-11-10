using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTvContents.data
{
    class RecordedData
    {
        public int Id { get; set; }

        public string DiskNo { get; set; }

        public string SeqNo { get; set; }

        public string RipStatus { get; set; }

        public DateTime OnAirDate { get; set; }

        public bool TimeFlag { get; set; }

        public int Minute { get; set; }

        public int ChannelNo {get; set; }

        public int ChannelSeq { get; set; }

        public string ProgramName { get; set; }

        public string Detail { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
