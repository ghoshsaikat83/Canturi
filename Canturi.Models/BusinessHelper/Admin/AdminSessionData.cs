using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Canturi.Models.BusinessHelper.Admin
{
    public static class AdminSessionData
    {
        /* For the admin user id */
        const string AdminUserIdKey = "AdminUserId";
        public static int AdminUserId
        {
            get { return HttpContext.Current.Session[AdminUserIdKey] != null ? (int)HttpContext.Current.Session[AdminUserIdKey] : 0; }
            set { HttpContext.Current.Session[AdminUserIdKey] = value; }
        }

        /* For the admin role id */
        const string AdminRoleIdKey = "AdminRoleId";
        public static int AdminRoleId
        {
            get { return HttpContext.Current.Session[AdminRoleIdKey] != null ? (int)HttpContext.Current.Session[AdminRoleIdKey] : 0; }
            set { HttpContext.Current.Session[AdminRoleIdKey] = value; }
        }

        /* For the admin role name */
        const string AdminRoleNameKey = "AdminRoleName";
        public static string AdminRoleName
        {
            get { return HttpContext.Current.Session[AdminRoleNameKey] != null ? (string)HttpContext.Current.Session[AdminRoleNameKey] : ""; }
            set { HttpContext.Current.Session[AdminRoleNameKey] = value; }
        }


        /* For the admin user name */
        const string AdminUserNameKey = "AdminUserName";
        public static string AdminUserName
        {
            get { return HttpContext.Current.Session[AdminUserNameKey] != null ? (string)HttpContext.Current.Session[AdminUserNameKey] : ""; }
            set { HttpContext.Current.Session[AdminUserNameKey] = value; }
        }


        /* For the admin name */
        const string AdminNameKey = "AdminName";
        public static string AdminName
        {
            get { return HttpContext.Current.Session[AdminNameKey] != null ? (string)HttpContext.Current.Session[AdminNameKey] : ""; }
            set { HttpContext.Current.Session[AdminNameKey] = value; }
        }

        /* For the admin theme name */
        const string AdminThemeKey = "ThemeName";
        public static string AdminThemeName
        {
            get { return HttpContext.Current.Session[AdminThemeKey] != null ? (string)HttpContext.Current.Session[AdminThemeKey] : "skin-blue"; }
            set { HttpContext.Current.Session[AdminThemeKey] = value; }
        }

        /* For the admin theme fixed layout */
        const string AdminThemeFixedLayoutKey = "IsFixedLayout";
        public static string AdminThemeFixedLayout
        {
            get { return HttpContext.Current.Session[AdminThemeFixedLayoutKey] != null ? (string)HttpContext.Current.Session[AdminThemeFixedLayoutKey] : "0"; }
            set { HttpContext.Current.Session[AdminThemeFixedLayoutKey] = value; }
        }


        /* For the admin created on date */
        const string AdminCreatedOnKey = "AdminCreatedOn";
        public static string AdminCreatedOn
        {
            get { return HttpContext.Current.Session[AdminCreatedOnKey] != null ? (string)HttpContext.Current.Session[AdminCreatedOnKey] : ""; }
            set { HttpContext.Current.Session[AdminCreatedOnKey] = value; }
        }

        /* For the admin last login date */
        const string AdminLastLoginOnKey = "AdminLastLoginOn";
        public static string AdminLastLoginOn
        {
            get { return HttpContext.Current.Session[AdminLastLoginOnKey] != null ? (string)HttpContext.Current.Session[AdminLastLoginOnKey] : ""; }
            set { HttpContext.Current.Session[AdminLastLoginOnKey] = value; }
        }

        //const string AdminStoreIdKey = "AdminStoreId";
        //public static int AdminStoreId
        //{
        //    get { return HttpContext.Current.Session[AdminStoreIdKey] != null ? (int)HttpContext.Current.Session[AdminStoreIdKey] : 0; }
        //    set { HttpContext.Current.Session[AdminStoreIdKey] = value; }
        //}
    }
}
