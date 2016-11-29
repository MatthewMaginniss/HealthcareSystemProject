﻿using System;
using System.Windows.Forms;
using HealthcareProjectBeamMaginniss.View;

namespace HealthcareProjectBeamMaginniss
{
    /// <summary>
    ///     Entry point for application
    /// </summary>
    internal static class Program
    {
        #region Methods

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

        #endregion
    }
}