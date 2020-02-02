using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Helpers
{
    public class SessionHelper
    {
        

        /// <summary>
        /// Gets or sets a value indicating whether the user is Authenticated from session 
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
        /// </value>
        public static bool IsAuthenticated
        {
            get
            {
                return HttpContext.Current.Session["IsAuthenticated"] is bool && (bool)HttpContext.Current.Session["IsAuthenticated"];
                //return Convert.ToBoolean(getCookie("IsAuthenticated"));
            }

            set
            {
                HttpContext.Current.Session["IsAuthenticated"] = value;
                //setCookie("IsAuthenticated", value.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public static string UserId
        {
            get
            {
                return (string)HttpContext.Current.Session["UserId"];
                //return HttpContext.Current.Session["UserId"] is int ? (int)HttpContext.Current.Session["UserId"] : 0;
                //return Convert.ToInt32(getCookie("UserId"));
            }

            set
            {
                HttpContext.Current.Session["UserId"] = value;
                //setCookie("UserId", value.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public static string Username
        {
            get
            {
                return (string)HttpContext.Current.Session["UserName"];
                //return getCookie("UserName");
            }
            set
            {
                HttpContext.Current.Session["UserName"] = value;
                //setCookie("UserName", value.ToString());
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The full name.
        /// </value>
        public static string Title
        {
            get
            {
                return (string)HttpContext.Current.Session["Title"];
                //return getCookie("Title");
            }

            set
            {
                HttpContext.Current.Session["Title"] = value;
                //setCookie("Title", value.ToString());
            }
        }


        public static int? StudentId
        {
            get
            {
                if (HttpContext.Current.Session["StudentId"] != null)
                { return (int)HttpContext.Current.Session["StudentId"]; }
                else { return null; }
                //return getCookie("Title");
            }

            set
            {
                HttpContext.Current.Session["StudentId"] = value;
                //setCookie("Title", value.ToString());
            }
        }

        public static int ReportIndex
        {
            get
            {
                return (int) HttpContext.Current.Session["ReportIndex"];
            }
            set
            {
                HttpContext.Current.Session["ReportIndex"] = value;
            }
        }

        public static int CurrentCulture
        {
            get
            {
                return (int)HttpContext.Current.Session["CurrentCulture"];
            }
            set
            {
                HttpContext.Current.Session["CurrentCulture"] = value;
            }
        }

        ///// <summary>
        ///// Gets or sets the user identifier.
        ///// </summary>
        ///// <value>
        ///// The user identifier.
        ///// </value>
        //public static int RoleId
        //{
        //    get
        //    {
        //        //return (string)HttpContext.Current.Session["RoleId"];
        //        return HttpContext.Current.Session["RoleId"] is int ? (int)HttpContext.Current.Session["RoleId"] : 0;
        //        //return Convert.ToInt32(getCookie("UserId"));
        //    }

        //    set
        //    {
        //        HttpContext.Current.Session["RoleId"] = value;
        //        //setCookie("UserId", value.ToString());
        //    }
        //}
        public static int RegisterNo { get; set; }

        public static string SchoolName
        {
            get
            {
                return (string)HttpContext.Current.Session["SchoolName"];
            }
            set
            {
                HttpContext.Current.Session["SchoolName"] = value;
            }
        }

        public static string LogoPath
        {
            get
            {
                return (string)HttpContext.Current.Session["LogoPath"];
            }
            set
            {
                HttpContext.Current.Session["LogoPath"] = value;
            }
        }
        public static int SchoolId
        {
            get
            {
                return (int)HttpContext.Current.Session["SchoolId"];
            }
            set
            {
                HttpContext.Current.Session["SchoolId"] = value;
            }
        }

        public static int TempId
        {
            get
            {
                return (int)HttpContext.Current.Session["TempId"];
            }
            set
            {
                HttpContext.Current.Session["TempId"] = value;
            }
        }

        public static DateTime fromDate
        {
            get
            {
                return (DateTime)HttpContext.Current.Session["fromDate"];
            }
            set
            {
                HttpContext.Current.Session["fromDate"] = value;
            }
        }

        public static DateTime toDate
        {
            get
            {
                return (DateTime)HttpContext.Current.Session["toDate"];
            }
            set
            {
                HttpContext.Current.Session["toDate"] = value;
            }
        }

    }
}