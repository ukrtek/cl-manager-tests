using System;
using System.Collections.Generic;
using System.Text;

namespace Iqvia.E360.CodeListManager.AutomatedTests.Utils.WebDriverBuilder
{
	/// <summary>
	/// 
	/// </summary>
    public class DriverOption
    {
		/// <summary>
		/// 
		/// </summary>
		public int TimeOutPageLoad { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int TimeOutImplicitWait { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public Browsers Browser { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string StartUrl { get; set; }

		/// <summary>
		/// 
		/// </summary> 
		public string DriverPath { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool IncognitoMode { get; set; }
	}
}
