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
                        + "    , p.name, r.detail, r.created_at, r.updated_at, d.path, d.label, r.rating1, r.rating2, r.remark "
                        + "  FROM tv.recorded as r"
                        + "    LEFT JOIN tv.program as p "
                        + "      ON r.channel_no = p.channel_no and r.channel_seq = p.channel_seq "
                        + "    LEFT JOIN tv.disk as d "
                        + "      ON r.disk_no = d.no "
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
                        data.DiskPath = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.DiskLabel = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.Rating3 = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.Rating4 = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.Comment = MySqlDbExportCommon.GetDbString(reader, colIdx++);

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
        public void UpdateRatingComment(int myRating1, int myRating2, string myComment, int myId, MySqlDbConnection myDbCon)
        {
            if (myDbCon == null)
                myDbCon = new MySqlDbConnection();

            myDbCon.openConnection();
            string querySting = "UPDATE recorded " +
                "SET rating1 = @pRating1" +
                ", rating2 = @pRating2 " +
                ", remark = @pComment " +
                "WHERE id = @pId ";

            List<MySqlParameter> sqlparamList = new List<MySqlParameter>();

            MySqlParameter param = new MySqlParameter();
            param = new MySqlParameter("@pRating1", MySqlDbType.Int32);
            param.Value = myRating1;
            sqlparamList.Add(param);

            param = new MySqlParameter("@pRating2", MySqlDbType.Int32);
            param.Value = myRating2;
            sqlparamList.Add(param);

            param = new MySqlParameter("@pComment", MySqlDbType.VarChar);
            param.Value = myComment;
            sqlparamList.Add(param);

            param = new MySqlParameter("@pId", MySqlDbType.VarChar);
            param.Value = myId;
            sqlparamList.Add(param);

            myDbCon.SetParameter(sqlparamList.ToArray());
            myDbCon.execSqlCommand(querySting);

            return;
        }
    }
}
