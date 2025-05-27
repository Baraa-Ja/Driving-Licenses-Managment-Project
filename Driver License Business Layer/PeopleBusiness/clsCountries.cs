using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Driver_License_DataAccess_Layer;

namespace Driver_License_Business_Layer
{
    public class clsCountries
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        public clsCountries()
        {
            this.CountryID = -1;
            this.CountryName = "";
        }

        private clsCountries(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }

        public static clsCountries Find(int CountryID)
        {
            string CountryName = "";

            if(clsCountriesData.FindCountryByID(CountryID, ref CountryName))
            {
                return new clsCountries(CountryID, CountryName);
            }
            else
            {
                return null;
            }
        }

        public static clsCountries Find(string CountryName)
        {
            int CountryID = -1;

            if (clsCountriesData.FindCountryByName(CountryName, ref CountryID))
            {
                return new clsCountries(CountryID, CountryName);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllCountries()
        {
            return clsCountriesData.GetAllCountries();
        }
    }
}
