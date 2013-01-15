﻿using System;
using System.Diagnostics;
using System.Reflection;

namespace Utils.AssemblyInfoResolver
{
    public class AssemblyInfoHelper
    {
        private static Assembly m_Assembly;

        public AssemblyInfoHelper(Type type)
        {
            m_Assembly = Assembly.GetAssembly(type);
        }

        public AssemblyInfoHelper(string file)
        {
            m_Assembly = Assembly.LoadFile(file);
        }

        private T CustomAttributes<T>()
            where T : Attribute
        {
            object[] customAttributes = m_Assembly.GetCustomAttributes(typeof(T), false);

            if ((customAttributes != null) && (customAttributes.Length > 0))
            {
                return ((T)customAttributes[0]);
            }

            throw new InvalidOperationException();
        }

        public string Title
        {
            get
            {
                return CustomAttributes<AssemblyTitleAttribute>().Title;
            }
        }

        public string Description
        {
            get
            {
                return CustomAttributes<AssemblyDescriptionAttribute>().Description;
            }
        }

        public string Company
        {
            get
            {
                return CustomAttributes<AssemblyCompanyAttribute>().Company;
            }
        }

        public string Product
        {
            get
            {
                return CustomAttributes<AssemblyProductAttribute>().Product;
            }
        }

        public string Copyright
        {
            get
            {
                return CustomAttributes<AssemblyCopyrightAttribute>().Copyright;
            }
        }

        public string Trademark
        {
            get
            {
                return CustomAttributes<AssemblyTrademarkAttribute>().Trademark;
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return m_Assembly.GetName().Version.ToString();
            }
        }

        public string FileVersion
        {
            get
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(m_Assembly.Location);
                return fvi.FileVersion;
            }
        }

        public string Guid
        {
            get
            {
                return CustomAttributes<System.Runtime.InteropServices.GuidAttribute>().Value;
            }
        }

        public string FileName
        {
            get
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(m_Assembly.Location);
                return fvi.OriginalFilename;
            }
        }

        public string FilePath
        {
            get
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(m_Assembly.Location);
                return fvi.FileName;
            }
        }
    }
}


