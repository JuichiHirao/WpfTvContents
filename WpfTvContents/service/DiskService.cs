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
    class DiskService
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public List<DiskData> GetList(MySqlDbConnection myDbCon)
        {
            List<DiskData> listData = new List<DiskData>();

            if (myDbCon == null)
                myDbCon = new MySqlDbConnection();

            string queryString
                        = "SELECT id"
                        + "    , no, label, path "
                        + "    , created_at, updated_at"
                        + "  FROM tv.disk "
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
                        DiskData data = new DiskData();

                        int colIdx = 0;
                        data.Id = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.No = MySqlDbExportCommon.GetDbInt(reader, colIdx++);
                        data.Label = MySqlDbExportCommon.GetDbString(reader, colIdx++);
                        data.Path = MySqlDbExportCommon.GetDbString(reader, colIdx++);
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
