﻿using System;
using System.Collections.Generic;
using System.Linq;
using HealthcareProjectBeamMaginniss.cs3230f16bDataSetTableAdapters;
using HealthcareProjectBeamMaginniss.DAL.Interfaces;
using HealthcareProjectBeamMaginniss.Model;

namespace HealthcareProjectBeamMaginniss.DAL.Repository
{
    internal class AppointmentLabOrderRepository : IRepository<AppointmentLabOrder>
    {
        #region Methods

        public void Add(AppointmentLabOrder aptLabOrder)
        {
            var adapter = new appointment_has_lab_orderTableAdapter();
            var appointmentId = aptLabOrder.AppointmentId;
            var labOrderId = aptLabOrder.LabOrderId;
            try
            {
                using (adapter)
                {
                    adapter.Insert(appointmentId, labOrderId);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public AppointmentLabOrder GetById(int id)
        {
            var adapter = new appointment_has_lab_orderTableAdapter();

            try
            {
                using (adapter)
                {
                    var aptLabOrder = adapter.GetData().FirstOrDefault(alo => alo.appointment_id == id);
                    return this.GetAptTestOrderedFromRow(aptLabOrder);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IList<AppointmentLabOrder> GetAll()
        {
            var aptTestOrderedList = new List<AppointmentLabOrder>();
            var adapter = new appointment_has_lab_orderTableAdapter();
            try
            {
                using (adapter)
                {
                    foreach (var row in adapter.GetData().Rows)
                    {
                        var test = this.GetAptTestOrderedFromRow((cs3230f16bDataSet.appointment_has_lab_orderRow) row);
                        aptTestOrderedList.Add(test);
                    }
                }
                return aptTestOrderedList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        public AppointmentLabOrder GetAptTestOrderedFromRow(cs3230f16bDataSet.appointment_has_lab_orderRow row)
        {
            var appointmentId = row.appointment_id;
            var labOrderId = row.lab_order_id;
            return new AppointmentLabOrder(appointmentId, labOrderId);
        }
    }
}