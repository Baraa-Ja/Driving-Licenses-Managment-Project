using Driver_License_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver_License_Business_Layer
{
    public class clsTestTypes
    {
        public int VisionTrailesCount = 0;
        public int writtenTrailesCount = 0;
        public int StreetTrailsCount = 0;
        public enum enTestTypes { VisionTest = 1, WrittentTest = 2, StreetTest = 3};
        enTestTypes TestType;
        public int TestTypeID { get; set; }
        public string TestTypeTitle { get; set; }
        public string TestTypeDescription { get; set; }
        public decimal TestTypeFees { get; set; }

        public clsTestTypes()
        {
            this.TestTypeID = -1;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestTypeFees = 0;
        }

        private clsTestTypes(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            this.TestTypeID = TestTypeID;
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescription = TestTypeDescription;
            this.TestTypeFees = TestTypeFees;
        }

        public static clsTestTypes FindTestByID(int TestTypeID)
        {
            string TestTypeTitle = "", TestTypeDescription = "";
            decimal TestTypeFees = 0;

            bool isfound = clsTestTypesData.GetTestTypeByID(TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees);

            if(isfound)
            {
                return new clsTestTypes(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            else
            {
                return null;
            }

        }

        public static clsTestTypes FindTestByName(string TestTypeTitle)
        {
            int TestTypeID = -1;
            string TestTypeDescription = "";
            decimal TestTypeFees = 0;

            bool isfound = clsTestTypesData.GetTestTypeByName(ref TestTypeID, TestTypeTitle, ref TestTypeDescription, ref TestTypeFees);

            if (isfound)
            {
                return new clsTestTypes(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            else
            {
                return null;
            }

        }

        public bool UpdateTestTypes()
        {
            return clsTestTypesData.UpdateTestTypes(this.TestTypeID, this.TestTypeTitle, this.TestTypeDescription, this.TestTypeFees);
        }

        public int TestTrailesCount(int TestTypeID)
        {
            return clsTestTypesData.GetTestTrailesCount(TestTypeID);
        }

        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesData.GetAlltestTypes();
        }
    }
}
