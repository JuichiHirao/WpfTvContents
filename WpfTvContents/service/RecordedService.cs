using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTvContents.common;
using WpfTvContents.data;

namespace WpfTvContents.service
{
    class RecordedService
    {
        public List<RecordedData> GetList(MySqlDbConnection myDbCon)
        {
            List<RecordedData> listData = new List<RecordedData>();

            if (myDbCon == null)
                myDbCon = new MySqlDbConnection();

            string queryString
                        = "SELECT r.id "
                        + "    , r.disk_no, r.seq_no, r.rip_status, r.on_air_date "
                        + "    , r.time_flag, r.minute, r.channel_no, r.channel_seq "
                        + "    , p.name, r.detail, r.created_at, r.updated_at "
                        + "  FROM tv.recorded as r LEFT JOIN tv.program as p "
                        + "    ON r.channel_no = p.channel_no and r.channel_seq = p.channel_seq "
                        + "  ORDER BY r.disk_no DESC "
                        + "";

            MySqlDataReader reader = null;
            try
            {
                reader = myDbCon.GetExecuteReader(queryString);

                do
                {

                    if (reader.IsClosed)
                    {
                        //_logger.Debug("reader.IsClosed");
                        throw new Exception("av.storeの取得でreaderがクローズされています");
                    }

                    while (reader.Read())
                    {
                        RecordedData data = new RecordedData();

                        int colIdx = 0;
                        data.Id = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.DiskNo = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.SeqNo = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.RipStatus = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.OnAirDate = MySqlDbExportCommon.GetDbDateTime(reader, colIdx++);
                        data.TimeFlag = MySqlDbExportCommon.GetDbBool(reader, colIdx++);
                        data.Minute = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.ChannelNo = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.ChannelSeq = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.ProgramName = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.Detail = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.CreatedAt = MySqlDbExportCommon.GetDbDateTime(reader, colIdx++);
                        data.UpdatedAt = MySqlDbExportCommon.GetDbDateTime(reader, colIdx++);

                        listData.Add(data);
                    }
                } while (reader.NextResult());
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }
            finally
            {
                reader.Close();
            }

            reader.Close();

            myDbCon.closeConnection();

            return listData;
        }

    }
}
