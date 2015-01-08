using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace LakeDownloader
{
    class Downloader
    {
        public string DesiredDate;
            List<string> listOfDataStatements = new List<string>();

        public Downloader(string passedInDate)
        {
            DesiredDate = passedInDate;
        }

        //Download all three files
        public void DownloadAllFiles()
        {
            DownloadFile(DesiredDate, "_Air_Temp.txt");
            DownloadFile(DesiredDate, "_Wind_Gust.txt");
            DownloadFile(DesiredDate, "_Wind_Speed.txt");
        }

        //Download data to a local file. 
        public void DownloadFile(string dateOfFile, string kindOfFile)
            
        {
            string localFileName = @"data\" + dateOfFile + kindOfFile;
            string onlineFileName = "http://optimal-harbor-736.appspot.com/data/" + dateOfFile + kindOfFile;

            if (File.Exists(localFileName))
            {
                Console.WriteLine("Using cached file");
            }
            else
            {
                Console.WriteLine("Downloading File");
                using (WebClient myWebClient = new WebClient())
                {
                    myWebClient.DownloadFile(onlineFileName, localFileName);
                }
            }

            OutputString(dateOfFile, kindOfFile, localFileName);

        }

        //get three types Of Information, create a string
        public void OutputString(string dateOfFile, string kindOfFile, string localFileName)
        {
            switch (kindOfFile)
            {
                case "_Air_Temp.txt":
                    {
                        listOfDataStatements.Add("On " + dateOfFile + ", the air temperature had a mean of " +
                                                 GetMean(localFileName) +
                                                 " and a median of " + GetMedian(localFileName) + ".");
                    }
                    break;
                case "_Wind_Speed.txt":
                    {
                        listOfDataStatements.Add("On " + dateOfFile + ", the wind speed had a mean of " + GetMean(localFileName) +
                               " and a median of " + GetMedian(localFileName) + ".");
                    }
                    break;
                case "_Wind_Gust.txt":
                    {
                        listOfDataStatements.Add("On " + dateOfFile + ", the wind gust speed had a mean of " + GetMean(localFileName).ToString() +
                               " and a median of " + GetMedian(localFileName).ToString() + ".");
                    }
                    break;
            }
        }


        //Get the mean temperature.
        public double GetMean(string localFileName)
        {
            double totalOfTemperatures = 0;
            int numberOfTemperatures = 0;
            using (var temperatureReader = new StreamReader(localFileName))
            {
                while (!temperatureReader.EndOfStream)
                {
                    string[] breakThisLine = temperatureReader.ReadLine().Split(' ');
                    {
                        totalOfTemperatures += double.Parse(breakThisLine[breakThisLine.Count() - 1]);
                        numberOfTemperatures++;
                    }
                }

                double meanTemperatures = totalOfTemperatures/numberOfTemperatures;
                return meanTemperatures;
            }
        }

        //Get the median
        public double GetMedian(string localFileName)
        {
            List<double> dataForMedian = new List<double>();
             using (var temperatureReader = new StreamReader(localFileName))
            {
                while (!temperatureReader.EndOfStream)
                {
                    string[] breakThisLine = temperatureReader.ReadLine().Split(' ');
                    dataForMedian.Add(double.Parse(breakThisLine[breakThisLine.Count() - 1]));
                }

                 if (dataForMedian.Count%2 == 0)//if even number of temperatures
                 {
                     double averageOfCenters = ((dataForMedian[dataForMedian.Count/2] +
                                                 dataForMedian[(dataForMedian.Count/2 - 1)])/2);
                     return averageOfCenters;
                 }
                 else//if odd
                 {
                    return dataForMedian[(int) ((dataForMedian.Count/2) - .5)];
                 }
            }
        }

        public void PrintDataStatements()
        {
            foreach (String statement in listOfDataStatements)
            {
                Console.WriteLine(statement);
            }
        }
    }
}
