using MySql.Data.MySqlClient;
using NLog;
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
    class ContentsService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public List<ContentsData> GetList(MySqlDbConnection myDbCon)
        {
            List<ContentsData> listData = new List<ContentsData>();

            if (myDbCon == null)
                myDbCon = new MySqlDbConnection();

            string queryString
                        = "SELECT f.id"
                        + "    , f.contents_id, f.detail_id, f.store_id, f.label"
                        + "    , f.name, f.source, f.duration, f.time "
                        + "    , f.video_info, f.comment, f.size, f.priority_num  "
                        + "    , f.file_date, f.quality, f.rating1, f.rating2  "
                        + "    , f.remark  "
                        + "    , f.created_at, f.updated_at, d.path "
                        + "  FROM tv.file as f LEFT JOIN tv.real_dir as d "
                        + "    ON f.store_id = d.id  "
                        + "  ORDER BY f.created_at DESC "
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
                        ContentsData data = new ContentsData();

                        int colIdx = 0;
                        data.Id = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.ContentsId = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.DetailId = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.StoreId = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.Label = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.Name = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.Source = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.Duration = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.Time = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.VideoInfo = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.Comment = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.Size = MySqlDbExportCommon.GetLong(reader, colIdx++);
                        data.PriorityNum = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.FileDate = MySqlDbExportCommon.GetDbDateTime(reader, colIdx++);
                        data.Quality = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.Rating1 = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.Rating2 = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.Remark = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.CreatedAt = MySqlDbExportCommon.GetDbDateTime(reader, colIdx++);
                        data.UpdatedAt = MySqlDbExportCommon.GetDbDateTime(reader, colIdx++);
                        data.Path = MySqlDbExportCommon.GetDbString(reader, colIdx++);

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
            string querySting = "UPDATE file " +
                "SET rating1 = @pRating1" +
                ", rating2 = @pRating2 " +
                ", comment = @pComment " +
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
