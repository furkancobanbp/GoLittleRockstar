
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Globalization;
using Newtonsoft.Json.Converters;
using GoLittleRockstar.Model;

namespace GoLittleRockstar.Functions
{
    public class restApi
    {
        public String Url = "https://api.epias.com.tr/epias/exchange/transparency";
        HttpClient httpClient = new HttpClient();
        
        public String WeatherApiBaseUrl = "http://api.weatherapi.com/v1";

        enum period
        {
            DAILY,
            WEEKLY,
            MONTHLY,
            PERIODIC
        }
        public interface SistemYonu
        {
            public String? SistemYonu { get; set; }
        }

        public List<clsDagitimModel> getDagitimBolgesi()
        {
            var client = new RestClient(Url);
            var request = new RestRequest("/consumption/distribution", Method.Get);
            var response = client.Execute(request).Content;
            JObject jobj = JObject.Parse(response);
            var body = jobj["body"]["distributionList"].ToString();
            var jsonModel = JsonConvert.DeserializeObject<List<clsDagitimModel>>(body);
            return jsonModel;
        }
        public List<clsPiyasaFiyatlariModel> getPtfSmf(DateTime basTar, DateTime bitTar)
        {
            var client = new RestClient(Url);

            var request = new RestRequest("/market/mcp-smp", Method.Get);
            request.AddParameter("startDate", basTar.ToString("yyyy-MM-dd"));
            request.AddParameter("endDate", bitTar.ToString("yyyy-MM-dd"));

            var response = client.Execute(request).Content;
            JObject jobj = JObject.Parse(response);
            var body = jobj["body"]["mcpSmps"].ToString();


            var jsonModel = JsonConvert.DeserializeObject<List<clsPiyasaFiyatlariModel>>(body);
            return jsonModel;
        }
        public List<clsDgpTalimatOzet> dgpTalimatOzet(DateTime basTar, DateTime bitTar)
        {
            var client = new RestClient(Url);

            var request = new RestRequest("/market/bpm-order-summary", Method.Get);
            request.AddParameter("startDate", basTar.ToString("yyyy-MM-dd"));
            request.AddParameter("endDate", bitTar.ToString("yyyy-MM-dd"));

            var response = client.Execute(request).Content;
            JObject jobj = JObject.Parse(response);
            var body = jobj["body"]["bpmOrderSummaryList"].ToString();


            var jsonModel = JsonConvert.DeserializeObject<List<clsDgpTalimatOzet>>(body);
            return jsonModel;

        }
        public List<clsGercekZamanliUretim> gercekZamanliUretim(DateTime basTar, DateTime bitTar)
        {
            var client = new RestClient(Url);

            var request = new RestRequest("/production/real-time-generation", Method.Get);
            request.AddParameter("startDate", basTar.ToString("yyyy-MM-dd"));
            request.AddParameter("endDate", bitTar.ToString("yyyy-MM-dd"));

            var response = client.Execute(request).Content;
            JObject jobj = JObject.Parse(response);
            var body = jobj["body"]["hourlyGenerations"].ToString();
            var jsonModel = JsonConvert.DeserializeObject<List<clsGercekZamanliUretim>>(body);
            return jsonModel;
        }
        public List<clsAuf> getAuf(DateTime basTar, DateTime bitTar)
        {
            var client = new RestClient(Url);

            var request = new RestRequest("/market/auf", Method.Get);
            request.AddParameter("startDate", basTar.ToString("yyyy-MM-dd"));
            request.AddParameter("endDate", bitTar.ToString("yyyy-MM-dd"));

            var response = client.Execute(request).Content;
            JObject jobj = JObject.Parse(response);
            var body = jobj["body"]["aufList"].ToString();
            var jsonModel = JsonConvert.DeserializeObject<List<clsAuf>>(body);
            return jsonModel;
        }
        public List<clsGrf> getGrf(DateTime basTar, DateTime bitTar)
        {

            var client = new RestClient(Url);

            var request = new RestRequest("/stp/grf", Method.Get);
            request.AddParameter("startDate", basTar.ToString("yyyy-MM-dd"));
            request.AddParameter("endDate", bitTar.ToString("yyyy-MM-dd"));
            request.AddParameter("period", period.DAILY);

            var response = client.Execute(request).Content;
            JObject jobj = JObject.Parse(response);
            var body = jobj["body"]["prices"].ToString();
            var jsonModel = JsonConvert.DeserializeObject<List<clsGrf>>(body);
            return jsonModel;
        }
        public object translateToTurkish(SistemYonu model)
        {
            if (model.SistemYonu == "ENERGY_DEFICIT")
            {
                model.SistemYonu = "YAL";
            }
            else if (model.SistemYonu == "IN_BALANCE")
            {
                model.SistemYonu = "DENGEDE";
            }
            else if (model.SistemYonu == "ENERGY_SURPLUS")
            {
                model.SistemYonu = "YAT";
            }
            else
            {
                model.SistemYonu = "Belirlenmedi";
            }
            return model;
        }
        public async Task<List<clsDolarKuru>> DolarKurAl(DateTime basTar, DateTime bitTar)
        {
            String baslangicTarihi = basTar.ToShortDateString().Replace(".", "-");
            String bitisTarihi = bitTar.ToShortDateString().Replace(".", "-");

            var response = await httpClient.GetAsync("https://evds2.tcmb.gov.tr/service/evds/series=TP.DK.USD.S.YTL&startDate=" + baslangicTarihi + "&endDate=" + bitisTarihi + "&type=json&key=" + evdsApiKey + "&frequency=1&aggregationTypes=avg").Result.Content.ReadAsStringAsync();

            JObject jobj = JObject.Parse(response);
            foreach (JObject i in jobj["items"])
            {
                i.Property("UNIXTIME").Remove();
            }

            var kurList = jobj["items"].ToString().Replace("-", ".");

            var format = "dd.MM.yyyy";
            var dateTimeConverter = new IsoDateTimeConverter { DateTimeFormat = format };

            var jsonModel = JsonConvert.DeserializeObject<List<clsDolarKuru>>(kurList, dateTimeConverter);
            return jsonModel;
        }

        public Root GecmisHavaDurumu(DateTime basTar, DateTime bitTar, String Sehir)
        {
            String baslangicTarihi = basTar.ToString("yyyy-MM-dd");
            String bitisTarihi = bitTar.ToString("yyyy-MM-dd");

            RestClient client = new RestClient(WeatherApiBaseUrl);
            var request = new RestRequest("/history.json");

            request.AddParameter("key", WeatherApiKey);
            request.AddParameter("dt", baslangicTarihi);
            request.AddParameter("end_dt", bitisTarihi);
            request.AddParameter("q", Sehir);
            request.AddParameter("lang", "TR");


            var Response = client.Execute(request).Content;

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(Response);

            return myDeserializedClass;
        }

        public Root TahminHavaDurumu(DateTime bitTar, String Sehir)
        {
            String bitisTarihi = bitTar.ToString("yyyy-MM-dd");

            RestClient client = new RestClient(WeatherApiBaseUrl);
            var request = new RestRequest("/forecast.json");

            request.AddParameter("key", WeatherApiKey);
            request.AddParameter("dt", bitisTarihi);
            request.AddParameter("q", Sehir);
            request.AddParameter("lang", "tr");

            var Response = client.Execute(request).Content;

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(Response);

            return myDeserializedClass;
        }

        public List<MyForecastData> ForecastData(Root RawData)
        {
            List<MyForecastData> dataList = new List<MyForecastData>();

            foreach (Forecastday i in RawData.forecast.forecastday)
            {
                foreach (Hour j in i.hour)
                {
                    MyForecastData data = new MyForecastData();

                    data.city = RawData.location.name;
                    data.time = Convert.ToDateTime(j.time);
                    data.uv = j.uv;
                    data.cloud = j.cloud;
                    data.wind_kph = j.wind_kph;
                    data.wind_mph = j.wind_mph;
                    data.pressure_in = j.pressure_in;
                    data.pressure_mb = j.pressure_mb;
                    data.wind_degree = j.wind_degree;
                    data.wind_dir = j.wind_dir;
                    data.temp_c = j.temp_c;
                    data.temp_f = j.temp_f;
                    data.precip_mm = j.precip_mm;
                    data.precip_in = j.precip_in;
                    data.humidity = j.humidity;
                    data.cloud = j.cloud;
                    data.feelslike_c = j.feelslike_c;
                    data.feelslike_f = j.feelslike_f;
                    data.windchill_c = j.windchill_c;
                    data.windchill_f = j.windchill_f;
                    data.heatindex_c = j.heatindex_c;
                    data.heatindex_f = j.heatindex_f;
                    data.dewpoint_c = j.dewpoint_c;
                    data.dewpoint_f = j.dewpoint_f;
                    data.will_it_rain = j.will_it_rain;
                    data.chance_of_rain = j.chance_of_rain;
                    data.will_it_snow = j.will_it_snow;
                    data.chance_of_snow = j.chance_of_snow;
                    data.vis_km = j.vis_km;
                    data.vis_miles = j.vis_miles;
                    data.gust_kph = j.gust_kph;

                    dataList.Add(data);
                }
            }

            return dataList;
        }
    }
}
