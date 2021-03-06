﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;


namespace Utils.Mvvm
{
    /// <summary>
    /// A small helper class that has a method to help create
    /// PropertyChangedEventArgs when using the INotifyPropertyChanged
    /// interface
    /// </summary>
    public static class ObservableHelper
    {                                                                                        
        #region Public Methods

        /// <summary>
        /// Creates PropertyChangedEventArgs
        /// </summary>
        /// <param name="propertyExpression">Expression to make 
        /// PropertyChangedEventArgs out of</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>PropertyChangedEventArgs</returns>
        public static PropertyChangedEventArgs CreateArgs<T>(
            Expression<Func<T, object>> propertyExpression)
        {
            var lambda = propertyExpression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            var propertyInfo = memberExpression.Member as PropertyInfo;

            return new PropertyChangedEventArgs(propertyInfo.Name);
        }

        #endregion
    }
}
