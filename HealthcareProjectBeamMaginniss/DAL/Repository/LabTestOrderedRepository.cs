﻿using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareProjectBeamMaginniss.cs3230f16bDataSetTableAdapters;
using HealthcareProjectBeamMaginniss.DAL.Controller;
using HealthcareProjectBeamMaginniss.DAL.Interfaces;
using HealthcareProjectBeamMaginniss.Model;

namespace HealthcareProjectBeamMaginniss.DAL.Repository
{
    internal class LabTestOrderedRepository : IRepository<LabTestOrder>
    {
        #region Methods

        public void Add(LabTestOrder labTestOrder)
        {
            var adapter = new test_orderedTableAdapter();
            var testId = labTestOrder.TestId;
            var doctorId = labTestOrder.DoctorId;
            var testDate = labTestOrder.TestDate;
            try
            {
                using (adapter)
                {
                    adapter.Insert(testId, doctorId, testDate);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public LabTestOrder GetById(int id)
        {
            var adapter = new test_orderedTableAdapter();
            try
            {
                using (adapter)
                {
                    var labTestOrdered = adapter.GetData().FirstOrDefault(lto => lto.test_ordered_id == id);
                    return this.GetTestOrderedFromRow(labTestOrdered);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IList<LabTestOrder> GetAll()
        {
            var testOrderedList = new List<LabTestOrder>();
            var adapter = new test_orderedTableAdapter();
            try
            {
                using (adapter)
                {
                    foreach (var row in adapter.GetData().Rows)
                    {
                        var test = this.GetTestOrderedFromRow((cs3230f16bDataSet.test_orderedRow) row);
                        testOrderedList.Add(test);
                    }
                }
                return testOrderedList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        public IList<LabTestOrder> GetByAppointmentId(int appointmentId)
        {
            var testList = new List<LabTestOrder>();
            var adapter = new appointment_has_lab_orderTableAdapter();
            try
            {
                using (adapter)
                {
                    foreach (var row in adapter.GetData().Where(tst => tst.appointment_id == appointmentId))
                    {
                        var test = this.GetById(row.lab_order_id);
                        testList.Add(test);
                    }
                }
                return testList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public LabTestOrder GetTestOrderedFromRow(cs3230f16bDataSet.test_orderedRow row)
        {
            var testOrderedId = row.test_ordered_id;
            var testId = row.test_id;
            var doctorId = row.doctor_id;
            var testDate = row.test_date;
            return new LabTestOrder(testOrderedId, testId, doctorId, testDate);
        }

        public int GetLastId()
        {
            var adapter = new test_orderedTableAdapter();
            try
            {
                using (adapter)
                {
                    var labTestOrdered = adapter.GetData().LastOrDefault();
                    if (labTestOrdered != null)
                    {
                        return labTestOrdered.test_ordered_id;
                    }
                    return -1;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IList<LabTestOrder> GetByPatientId(int patientId)
        {
            var testList = new List<LabTestOrder>();
            var adapter = new appointment_has_lab_orderTableAdapter();
            try
            {
                using (adapter)
                {
                    foreach (
                        var row in
                            adapter.GetData()
                                   .Where(
                                       tst =>
                                           new AppointmentController().GetById(tst.appointment_id).PatientId ==
                                           patientId))
                    {
                        var test = this.GetById(row.lab_order_id);
                        testList.Add(test);
                    }
                }
                return testList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}