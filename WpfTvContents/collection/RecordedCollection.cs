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
    class RecordedCollection
    {
        public List<RecordedData> dataList;
        public ICollectionView ColViewListData;

        RecordedService service;

        public RecordedCollection()
        {
            service = new RecordedService();
            DataSet();
            ColViewListData = CollectionViewSource.GetDefaultView(dataList);

            ColViewListData.SortDescriptions.Clear();
            ColViewListData.SortDescriptions.Add(new SortDescription("OnAirDate", ListSortDirection.Descending));
        }

        public void DataSet()
        {
            dataList = service.GetList(new MySqlDbConnection());

        }

        public void SearchByTextKpopBank(string mySearchText)
        {
            if (mySearchText.Trim().Length <= 0)
                ColViewListData.Filter = null;

            string[] SearchArray = mySearchText.Trim().Split(' ');

            int year = 0;
            string yearStr = "";
            try
            {
                year = Convert.ToInt32(mySearchText);
                yearStr = Convert.ToString(year);
            }
            catch (Exception ex)
            {

            }

            ColViewListData.Filter = delegate (object o)
            {
                RecordedData data = o as RecordedData;

                if (data.ProgramName.IndexOf("ミュージックバンク") >= 0
                || data.ProgramName.IndexOf("MusicBank") >= 0
                || data.ProgramName.IndexOf("Music Bank") >= 0)
                {
                    if (mySearchText.Length > 0)
                    {
                        if (year > 0)
                        {
                            if (year == data.OnAirDate.Year)
                                return true;

                            if (data.Detail.IndexOf(yearStr) >= 0)
                                return true;
                        }
                        else
                        {
                            if (data.Detail.IndexOf(mySearchText) >= 0)
                                return true;
                        }
                    }
                    else
                        return true;
                }

                return false;
            };
            ColViewListData.SortDescriptions.Clear();
            ColViewListData.SortDescriptions.Add(new SortDescription("OnAirDate", ListSortDirection.Ascending));
        }

        public void SearchByTextKpopCore(string mySearchText)
        {
            if (mySearchText.Trim().Length <= 0)
                ColViewListData.Filter = null;

            string[] SearchArray = mySearchText.Trim().Split(' ');

            int year = 0;
            string yearStr = "";
            try
            {
                year = Convert.ToInt32(mySearchText);
                yearStr = Convert.ToString(year);
            }
            catch (Exception ex)
            {

            }

            ColViewListData.Filter = delegate (object o)
            {
                RecordedData data = o as RecordedData;

                if (data.ProgramName.IndexOf("K-POPの") >= 0)
                {
                    if (mySearchText.Length > 0)
                    {
                        if (year > 0)
                        {
                            if (year == data.OnAirDate.Year)
                                return true;

                            if (data.Detail.IndexOf(yearStr) >= 0)
                                return true;
                        }
                        else
                        {
                            if (data.Detail.IndexOf(mySearchText) >= 0)
                                return true;
                        }
                    }
                    else
                        return true;
                }

                return false;
            };
            ColViewListData.SortDescriptions.Clear();
            ColViewListData.SortDescriptions.Add(new SortDescription("OnAirDate", ListSortDirection.Ascending));
        }

        public void SearchByTextSbs(string mySearchText)
        {
            if (mySearchText.Trim().Length <= 0)
                ColViewListData.Filter = null;

            string[] SearchArray = mySearchText.Trim().Split(' ');

            int year = 0;
            string yearStr = "";
            try
            {
                year = Convert.ToInt32(mySearchText);
                yearStr = Convert.ToString(year);
            }
            catch (Exception ex)
            {

            }

            ColViewListData.Filter = delegate (object o)
            {
                RecordedData data = o as RecordedData;

                if (data.ProgramName.IndexOf("人気歌謡") >= 0)
                {
                    if (mySearchText.Length > 0)
                    {
                        if (year > 0)
                        {
                            if (year == data.OnAirDate.Year)
                                return true;

                            if (data.Detail.IndexOf(yearStr) >= 0)
                                return true;
                        }
                        else
                        {
                            if (data.Detail.IndexOf(mySearchText) >= 0)
                                return true;
                        }
                    }
                    else
                        return true;
                }

                return false;
            };
            ColViewListData.SortDescriptions.Clear();
            ColViewListData.SortDescriptions.Add(new SortDescription("OnAirDate", ListSortDirection.Ascending));
        }

        public void SearchByTextVenus(string mySearchText)
        {
            ColViewListData.Filter = delegate (object o)
            {

                RecordedData data = o as RecordedData;

                if (data.ProgramName.IndexOf("女神降臨") >= 0)
                {
                    if (mySearchText.Length > 0 && data.Detail.IndexOf(mySearchText) >= 0)
                        return true;
                    else
                        if (mySearchText.Length <= 0)
                        return true;
                }

                return false;
            };
            ColViewListData.SortDescriptions.Clear();
            ColViewListData.SortDescriptions.Add(new SortDescription("OnAirDate", ListSortDirection.Descending));
        }

        public void SearchByTextSweetAngel(string mySearchText)
        {
            ColViewListData.Filter = delegate (object o)
            {

                RecordedData data = o as RecordedData;

                if (data.ChannelNo == 659 && data.ChannelSeq == 2)
                {
                    if (mySearchText.Length > 0 && data.Detail.IndexOf(mySearchText) >= 0)
                        return true;
                    else
                        if (mySearchText.Length <= 0)
                        return true;
                }

                return false;
            };
            ColViewListData.SortDescriptions.Clear();
            ColViewListData.SortDescriptions.Add(new SortDescription("OnAirDate", ListSortDirection.Descending));
        }

        public void SearchByTextGrachaou(string mySearchText)
        {
            ColViewListData.Filter = delegate (object o)
            {

                RecordedData data = o as RecordedData;

                if (data.ChannelNo == 618 && data.ChannelSeq == 1)
                {
                    if (mySearchText.Length > 0 && data.Detail.IndexOf(mySearchText) >= 0)
                        return true;
                    else
                        if (mySearchText.Length <= 0)
                        return true;
                }

                return false;
            };
            ColViewListData.SortDescriptions.Clear();
            ColViewListData.SortDescriptions.Add(new SortDescription("OnAirDate", ListSortDirection.Descending));
        }

    }
}
