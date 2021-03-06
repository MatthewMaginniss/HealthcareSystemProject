﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HealthcareProjectBeamMaginniss.cs3230f16bDataSetTableAdapters;
using HealthcareProjectBeamMaginniss.DAL.Interfaces;
using HealthcareProjectBeamMaginniss.Model;

namespace HealthcareProjectBeamMaginniss.DAL.Repository
{
    /// <summary>
    ///     Patient repository to interface directly with the datasource
    /// </summary>
    /// <seealso cref="HealthcareProjectBeamMaginniss.DAL.Interfaces.IRepository{Patient}" />
    public class PatientRepository : IRepository<Patient>
    {
        #region Methods

        /// <summary>
        ///     Adds the specified patient to the database.
        /// </summary>
        /// <param name="patient">The patient to add.</param>
        public void Add(Patient patient)
        {
            var adapter = new patientTableAdapter();
            var fname = patient.FirstName;
            var lname = patient.LastName;
            var bdate = patient.Dob;
            var sex = patient.Sex.ToString();
            var street1 = patient.Street1;
            var street2 = patient.Street2;
            var city = patient.City;
            var state = patient.State;
            var zip = patient.Zip;
            var country = patient.Country;
            var phoneNo = patient.PhoneNo;
            try
            {
                using (adapter)
                {
                    adapter.Insert(fname, lname, bdate, sex, street1, street2, city, state, zip, country, phoneNo);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        ///     Gets the patient by patientID.
        /// </summary>
        /// <param name="id">The patientID.</param>
        /// <returns>Patient with specified patientID</returns>
        public Patient GetById(int id)
        {
            var adapter = new patientTableAdapter();
            try
            {
                using (adapter)
                {
                    try
                    {
                        var patient = adapter.GetData().First(pat => pat.patientID == id);
                        return this.getPatientFromRow(patient);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        ///     Gets all patients.
        /// </summary>
        /// <returns>All patients</returns>
        public IList<Patient> GetAll()
        {
            var patientList = new List<Patient>();
            var adapter = new patientTableAdapter();
            try
            {
                using (adapter)
                {
                    foreach (var row in adapter.GetData().Rows)
                    {
                        var patient = this.getPatientFromRow((cs3230f16bDataSet.patientRow) row);
                        patientList.Add(patient);
                    }
                }
                return patientList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        private Patient getPatientFromRow(cs3230f16bDataSet.patientRow row)
        {
            var pid = row.patientID;
            var fname = row.IsfirstNameNull() ? "" : row.firstName;
            var lname = row.IslastNameNull() ? "" : row.lastName;
            var bdate = row.IsdateOfBirthNull() ? DateTime.MinValue : row.dateOfBirth;
            var sex = row.IssexNull() ? ' ' : row.sex[0];
            var street1 = row.Isstreet1Null() ? "" : row.street1;
            var street2 = row.Isstreet2Null() ? "" : row.street2;
            var city = row.IscityNull() ? "" : row.city;
            var state = row.IsstateNull() ? "" : row.state;
            var zip = row.IszipNull() ? "" : row.zip;
            var country = row.IscountryNull() ? "" : row.country;
            var phoneNo = row.Is_phone_Null() ? "" : row._phone_;
            return new Patient(pid, fname, lname, bdate, sex, street1, street2, city, state, zip, country, phoneNo);
        }

        internal Dictionary<int, int> GetHistogramData(int minYear)
        {
            var adapter = new histogramOfPatientBirthYearTableAdapter();
            var dateDict = new Dictionary<int, int>();
            try
            {
                foreach (cs3230f16bDataSet.histogramOfPatientBirthYearRow row in adapter.GetData(minYear).Rows)
                {
                    var year = row.Year;
                    var count = (int) row._Patients_born_;
                    dateDict.Add(year, count);
                }
                return dateDict;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal int GetMinYear()
        {
            var adapter = new patientTableAdapter();
            try
            {
                using (adapter)
                {
                    var oldest = adapter.GetData().Min(pat => pat.dateOfBirth);
                    return oldest.Year;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal int GetMaxYear()
        {
            var adapter = new patientTableAdapter();
            try
            {
                using (adapter)
                {
                    var youngest = adapter.GetData().Max(pat => pat.dateOfBirth);
                    return youngest.Year;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal void Update(Patient patient)
        {
            var adapter = new patientTableAdapter();
            try
            {
                DataRow patRow;
                using (adapter)
                {
                    patRow = adapter.GetData().FirstOrDefault(pat => pat.patientID == patient.PatientId);
                }
                if (patRow != null)
                {
                    patRow[1] = patient.FirstName;
                    patRow[2] = patient.LastName;
                    patRow[3] = patient.Dob;
                    patRow[4] = patient.Sex.ToString();
                    patRow[5] = patient.Street1;
                    patRow[6] = patient.Street2;
                    patRow[7] = patient.City;
                    patRow[8] = patient.State;
                    patRow[9] = patient.Zip;
                    patRow[10] = patient.Country;
                    patRow[11] = patient.PhoneNo;
                    using (adapter)
                    {
                        adapter.Update(patRow);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal IList<Patient> GetFirst30()
        {
            var patientList = new List<Patient>();
            var adapter = new patientTableAdapter();
            try
            {
                using (adapter)
                {
                    foreach (var row in adapter.GetData().Rows)
                    {
                        var patient = this.getPatientFromRow((cs3230f16bDataSet.patientRow) row);
                        patientList.Add(patient);
                        if (patientList.Count >= 30)
                        {
                            return patientList;
                        }
                    }
                }
                return patientList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal IList<Patient> GetPatientsByFirstName(string fName)
        {
            var patientList = new List<Patient>();
            var adapter = new patientTableAdapter();
            try
            {
                using (adapter)
                {
                    foreach (var row in adapter.GetData().Where(pat => pat.firstName.ToLower() == fName))
                    {
                        var patient = this.getPatientFromRow(row);

                        patientList.Add(patient);
                    }
                }

                return patientList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal IList<Patient> GetPatientsByLastName(string lName)
        {
            var patientList = new List<Patient>();
            var adapter = new patientTableAdapter();
            try
            {
                using (adapter)
                {
                    foreach (var row in adapter.GetData().Where(pat => pat.lastName.ToLower() == lName))
                    {
                        var patient = this.getPatientFromRow(row);

                        patientList.Add(patient);
                    }
                }

                return patientList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal IList<Patient> GetPatientsByFullName(string fName, string lName)
        {
            var patientList = new List<Patient>();
            var adapter = new patientTableAdapter();
            try
            {
                using (adapter)
                {
                    foreach (
                        var row in
                            adapter.GetData()
                                   .Where(pat => pat.firstName.ToLower() == fName && pat.lastName.ToLower() == lName))
                    {
                        var patient = this.getPatientFromRow(row);
                        patientList.Add(patient);
                    }
                }

                return patientList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal IList<Patient> GetPatientsByDateOfBirth(string dob)
        {
            var patientList = new List<Patient>();
            var adapter = new patientTableAdapter();
            try
            {
                using (adapter)
                {
                    foreach (var row in adapter.GetData().Where(pat => pat.dateOfBirth.ToShortDateString() == dob))
                    {
                        var patient = this.getPatientFromRow(row);
                        patientList.Add(patient);
                    }
                }

                return patientList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal IList<Patient> GetPatientsByCountry(string countryQuery)
        {
            var patientList = new List<Patient>();

            var adapter = new patientsByCountryCodeTableAdapter();
            try
            {
                foreach (cs3230f16bDataSet.patientsByCountryCodeRow row in adapter.GetData(countryQuery).Rows)
                {
                    var pid = row.patientID;
                    var fname = row.IsfirstNameNull() ? "" : row.firstName;
                    var lname = row.IslastNameNull() ? "" : row.lastName;
                    var bdate = row.IsdateOfBirthNull() ? DateTime.MinValue : row.dateOfBirth;
                    var sex = row.IssexNull() ? ' ' : row.sex[0];
                    var street1 = row.Isstreet1Null() ? "" : row.street1;
                    var street2 = row.Isstreet2Null() ? "" : row.street2;
                    var city = row.IscityNull() ? "" : row.city;
                    var state = row.IsstateNull() ? "" : row.state;
                    var zip = row.IszipNull() ? "" : row.zip;
                    var country = row.IscountryNull() ? "" : row.country;
                    var phoneNo = row.Is_phone_Null() ? "" : row._phone_;
                    patientList.Add(new Patient(pid, fname, lname, bdate, sex, street1, street2, city, state, zip,
                        country, phoneNo));
                }
                return patientList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}