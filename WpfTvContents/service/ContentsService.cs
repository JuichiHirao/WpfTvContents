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
                        + "    , f.name, f.source, f.duration, f.video_info "
                        + "    , f.comment, f.size, f.priority_num, f.file_date  "
                        + "    , f.quality, f.rating1, f.rating2, f.remark  "
                        + "    , f.created_at, f.updated_at, d.path "
                        + "  FROM tv.file as f LEFT JOIN tv.real_dir as d "
                        + "    ON f.store_id = d.id  "
                        + "  ORDER BY f.file_date DESC "
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

                        data.Id = MySqlDbExportCommon.GetDbInt(reader, 0);
                        data.ContentsId = MySqlDbExportCommon.GetDbInt(reader, 1);
                        data.DetailId = MySqlDbExportCommon.GetDbInt(reader, 2);
                        data.StoreId = MySqlDbExportCommon.GetDbInt(reader, 3);
                        data.Label = MySqlDbExportCommon.GetDbString(reader, 4);
                        data.Name = MySqlDbExportCommon.GetDbString(reader, 5);
                        data.Source = MySqlDbExportCommon.GetDbString(reader, 6);
                        data.Duration = MySqlDbExportCommon.GetDbInt(reader, 7);
                        data.VideoInfo = MySqlDbExportCommon.GetDbString(reader, 8);
                        data.Comment = MySqlDbExportCommon.GetDbString(reader, 9);
                        data.Size = MySqlDbExportCommon.GetLong(reader, 10);
                        data.PriorityNum = MySqlDbExportCommon.GetDbInt(reader, 11);
                        data.FileDate = MySqlDbExportCommon.GetDbDateTime(reader, 12);
                        data.Quality = MySqlDbExportCommon.GetDbInt(reader, 13);
                        data.Rating1 = MySqlDbExportCommon.GetDbInt(reader, 14);
                        data.Rating2 = MySqlDbExportCommon.GetDbInt(reader, 15);
                        data.Remark = MySqlDbExportCommon.GetDbString(reader, 16);
                        data.CreatedAt = MySqlDbExportCommon.GetDbDateTime(reader, 17);
                        data.UpdatedAt = MySqlDbExportCommon.GetDbDateTime(reader, 18);
                        data.Path = MySqlDbExportCommon.GetDbString(reader, 19);

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
