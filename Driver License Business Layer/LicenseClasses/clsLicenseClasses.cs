using Driver_License_DataAccess_Layer;
using Driver_License_DataAccess_Layer.LicenseClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver_License_Business_Layer.LicenseClasses
{
    public class clsLicenseClasses
    {
        public int LicenseClassID {  get; set; }
        public string ClassName {  get; set; }
        public string ClassDescription { get; set; }
        public Byte MinimumAllowedAge { get; set; }
        public Byte DefaultValidityLength { get; set; }
        public decimal ClassFees { get; set; }

        public clsLicenseClasses()
        {
            this.LicenseClassID = -1;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 18;
            this.DefaultValidityLength = 10;
            this.ClassFees = 0;

        }

        private clsLicenseClasses(int licenseClassID, string className, string classDescription, byte minimumAllowedAge, byte defaultValidityLength, decimal classFees)
        {
            LicenseClassID = licenseClassID;
            ClassName = className;
            ClassDescription = classDescription;
            MinimumAllowedAge = minimumAllowedAge;
            DefaultValidityLength = defaultValidityLength;
            ClassFees = classFees;
        }

        public static clsLicenseClasses FindLicenseClassByID(int LicenseClassID)
        {
            string className = "", classDescription = "";
            byte minimumAllowedAge = 18, defaultValidityLength = 10;
            decimal classFees = 0;

            bool Isfound = clsLicenseClassesDataAccess.FindLicenseClassByID(LicenseClassID, ref className, ref classDescription,
               ref minimumAllowedAge, ref defaultValidityLength, ref classFees);

            if (Isfound)
                return new clsLicenseClasses(LicenseClassID, className, classDescription, minimumAllowedAge, defaultValidityLength, classFees);
            else
                return null;
        }

        public static clsLicenseClasses FindLicenseClassByName(string ClassName)
        {
            string  classDescription = "";
            byte minimumAllowedAge = 18, defaultValidityLength = 10;
            decimal classFees = 0;
            int LicenseClassID = -1;

            bool Isfound = clsLicenseClassesDataAccess.FindLicenseClassByName(ClassName, ref LicenseClassID, ref classDescription,
               ref minimumAllowedAge, ref defaultValidityLength, ref classFees);

            if (Isfound)
                return new clsLicenseClasses(LicenseClassID, ClassName, classDescription, minimumAllowedAge, defaultValidityLength, classFees);
            else
                return null;
        }

        public static DataTable GetAllClasses()
        {
            return clsLicenseClassesDataAccess.GetAllClasses();
        }

    }
}
