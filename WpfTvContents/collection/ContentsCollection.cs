using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WpfTvContents.common;
using WpfTvContents.data;
using WpfTvContents.service;

namespace WpfTvContents.collection
{
    class ContentsCollection
    {
        public List<ContentsData> dataList;
        public ICollectionView ColViewListData;

        ContentsService service;

        public ContentsCollection()
        {
            service = new ContentsService();
            DataSet();
            ColViewListData = CollectionViewSource.GetDefaultView(dataList);
        }

        public void DataSet()
        {
            dataList = service.GetList(new MySqlDbConnection());

        }

        public void Add(ContentsData myStoreData)
        {
            //service.DbExport(myStoreData, new MySqlDbConnection());
            dataList.Add(myStoreData);
        }

        public ContentsData GetMatchByPath(string myPath)
        {
            /*
            var matchdata = from storedata in dataList
                            where storedata.Path.ToUpper() == myPath.ToUpper()
                            select storedata;

            int cnt = matchdata.Count<ContentsData>();
            if (cnt <= 0)
                throw new Exception("storeに[" + myPath + "]に対応するパスの登録がありません");

            if (cnt > 1)
                throw new Exception("storeに[" + myPath + "]に対応するパスが複数あります");
             */

            return null;
            //return matchdata.First();
        }

        public void SearchByText(string mySearchText)
        {
            if (mySearchText.Trim().Length <= 0)
                ColViewListData.Filter = null;

            string[] SearchArray = mySearchText.Trim().Split(' ');

            ColViewListData.Filter = delegate (object o)
            {

                ContentsData data = o as ContentsData;

                foreach (string text in SearchArray)
                {
                    if (data.Name.IndexOf(text) < 0)
                        return false;
                }

                return true;
            };
        }

        public ContentsData GetMatchByLabel(string myLabel)
        {
            var matchdata = from storedata in dataList
                            where storedata.Label.ToUpper() == myLabel.ToUpper()
                            select storedata;

            int cnt = matchdata.Count<ContentsData>();
            if (cnt <= 0)
                throw new Exception("storeに[" + myLabel + "]に対応するLabelの登録がありません");

            if (cnt > 1)
                throw new Exception("storeに[" + myLabel + "]に対応するLabelが複数あります");

            return matchdata.First();
        }

    }
}
