using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Cinch
{
    /// <summary>
    /// A small helper class that has a method to help create
    /// PropertyChangedEventArgs when using the INotifyPropertyChanged
    /// interface
    /// </summary>
    //// VRU :code promu à une utilisation plus large que le client. Déplacé dans Sopra.Technique.Util.
    //// Conservé simplement en classe interne pour le fonctionnement propre de Cinch.
    internal static class ObservableHelper
    {
        #region Public Methods
        /// <summary>
        /// Creates PropertyChangedEventArgs
        /// </summary>
        /// <param name="propertyExpression">Expression to make 
        /// PropertyChangedEventArgs out of</param>
        /// <returns>PropertyChangedEventArgs</returns>
        public static PropertyChangedEventArgs CreateArgs<T>(
            Expression<Func<T, Object>> propertyExpression)
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
