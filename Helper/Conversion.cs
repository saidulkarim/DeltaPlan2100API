﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace DeltaPlan2100API.Helper
{
    public static class Conversion
    {
        /// <summary>
        /// Convert Byte to String
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ConvertByteToString(this byte[] bytes)
        {
            string response = string.Empty;

            foreach (byte b in bytes)
                response += (Char)b;

            return response;
        }

        public static TConvert ConvertTo<TConvert>(this object entity) where TConvert : new()
        {
            var convertProperties = TypeDescriptor.GetProperties(typeof(TConvert)).Cast<PropertyDescriptor>();
            var entityProperties = TypeDescriptor.GetProperties(entity).Cast<PropertyDescriptor>();

            var convert = new TConvert();

            foreach (var entityProperty in entityProperties)
            {
                var property = entityProperty;
                var convertProperty = convertProperties.FirstOrDefault(prop => prop.Name == property.Name);
                if (convertProperty != null)
                {
                    convertProperty.SetValue(convert, Convert.ChangeType(entityProperty.GetValue(entity), convertProperty.PropertyType));
                }
            }

            return convert;
        }

        public static int ToInt(this string val)
        {
            return int.Parse(val);
        }

        public static Int16 ToInt16(this string val)
        {
            return Int16.Parse(val);
        }

        public static Int32 ToInt32(this string val)
        {
            return Int32.Parse(val);
        }

        public static Int64 ToInt64(this string val)
        {
            return Int64.Parse(val);
        }

        public static float ToFloat(this string val)
        {
            return float.Parse(val);
        }

        public static long ToLong(this string val)
        {
            val = string.IsNullOrEmpty(val) ? "0" : val;
            return long.Parse(val);
        }

        public static decimal ToDecimal(this string val)
        {
            decimal result = string.IsNullOrEmpty(val) ? 0 : decimal.Parse(val);
            return Convert.ToDecimal(string.Format("{0:0.00}", result));
        }

        public static bool ToBool(this string val)
        {
            return bool.Parse(val);
        }

        public static Boolean ToBoolean(this string val)
        {
            return Boolean.Parse(val);
        }

        //public static string ToDateFormat(this string val)
        //{
        //    DateTime dtResult = DateTime.Parse(val);
        //    return dtResult.ToString("MMM dd, yyyy");
        //}

        public static string ToTitleCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            else
                return CultureInfo.CurrentUICulture.TextInfo.ToTitleCase(str);
        }

        public static string ToDescription(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DisplayText), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DisplayText)attrs[0]).Text;
            }

            return en.ToString();
        }

        // requires object instance, but you can skip specifying T
        public static string GetPropertyName<T>(Expression<Func<T>> exp)
        {
            return (((MemberExpression)(exp.Body)).Member).Name;
        }

        // requires explicit specification of both object type and property type
        public static string GetPropertyName<TObject, TResult>(Expression<Func<TObject, TResult>> exp)
        {
            // extract property name
            return (((MemberExpression)(exp.Body)).Member).Name;
        }

        // requires explicit specification of object type
        public static string GetPropertyName<TObject>(Expression<Func<TObject, object>> exp)
        {
            var body = exp.Body;
            var convertExpression = body as UnaryExpression;
            if (convertExpression != null)
            {
                if (convertExpression.NodeType != ExpressionType.Convert)
                {
                    throw new ArgumentException("Invalid property expression.", "exp");
                }
                body = convertExpression.Operand;
            }
            return ((MemberExpression)body).Member.Name;
        }

        public static DateTime ToDatabaseDateFormat(this string val)
        {
            val = string.IsNullOrEmpty(val) ? DateTime.Now.ToString("MM/dd/yyyy") : DateTime.Parse(val).ToString("MM/dd/yyyy");
            DateTime dtResult = DateTime.Parse(val);

            return dtResult;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();

            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }

    public class DisplayText : Attribute
    {
        public DisplayText(string Text)
        {
            this.text = Text;
        }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
    }
}