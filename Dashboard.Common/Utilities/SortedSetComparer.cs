using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Dashboard.Common.Utilities
{
    public class SortedSetComparer<T> : IComparer<T>
    {
        #region Fields

        private string _memberName = string.Empty ;
        private string _sortOrder = string.Empty ;
        private readonly Collection<object> _methodParameters = new Collection<object>() ;
        private PropertyInfo _propertyInfo = null ;
        private MethodInfo _methodInfo = null ;

        #endregion

        #region Properties

        public string MemberName
        {
            get { return _memberName ; }
            set
            {
                _memberName = value ;
                GetReflected() ;
            }
        }

        public string SortOrder
        {
            get { return _sortOrder ; }
            set { _sortOrder = value ; }
        }

        public Collection<object> MethodParameters
        {
            get { return _methodParameters ; }
        }

        #endregion

        #region Ctors

        /// <summary>
        /// Default constructor for use in instances.
        /// </summary>
        public SortedSetComparer() {}

        /// <summary>
        /// Constructor for in-line instantiation
        /// </summary>
        /// <param name="memberName">string of the member name to use for comparison
        /// can be either a method name or a property.
        /// </param>
        /// <param name="sortOrder">string of the sort order (case independent). 
        /// "ASC" for ascending order or anyting else for descending order</param>
        /// <param name="methodParameters">object array of parameters to use for method, null otherwise.</param>
        public SortedSetComparer ( string memberName, string sortOrder, Collection<object> methodParameters )
        {
            this._memberName = memberName ;
            this._sortOrder = sortOrder ;
            this._methodParameters = methodParameters ;

            GetReflected() ;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Sets the global field, propertyInfo and/or memberInfo using the underlying Type
        /// </summary>
        private void GetReflected()
        {
            Type[] _underlyingTypes = this.GetType().GetGenericArguments() ;
            Type _thisUnderlyingtype = _underlyingTypes[ 0 ] ;

            MemberInfo[] _memberInfos = _thisUnderlyingtype.GetMember( _memberName ) ;
            if ( _memberInfos.Length > 0 )
            {
                if ( _memberInfos[ 0 ].MemberType == MemberTypes.Property )
                    _propertyInfo = _thisUnderlyingtype.GetProperty( _memberName ) ;
                else if ( _memberInfos[ 0 ].MemberType == MemberTypes.Method )
                {
                    Type[] _signatureTypes = new Type[ 0 ] ;
                    if ( _methodParameters != null && _methodParameters.Count > 0 )
                    {
                        _signatureTypes = new Type[ _methodParameters.Count ] ;
                        for ( int _index = 0; _index < _methodParameters.Count; _index++ )
                        {
                            _signatureTypes[ _index ] = _methodParameters[ _index ].GetType() ;
                        }
                        _methodInfo = _thisUnderlyingtype.GetMethod( _memberName, _signatureTypes ) ;
                    }
                    else
                        _methodInfo = _thisUnderlyingtype.GetMethod( _memberName, _signatureTypes ) ;
                }
            }
        }

        /// <summary>
        /// Return an IComparable for use in the Compare method
        /// </summary>
        /// <param name="obj">object to get IComparable from</param>
        /// <returns>IComparable for this object</returns>
        private IComparable GetComparable ( T obj )
        {
            if ( _methodInfo != null )
                return (IComparable) _methodInfo.Invoke( obj, _methodParameters.ToArray() ) ;
            else
                return (IComparable) _propertyInfo.GetValue( obj, null ) ;
        }

        #endregion

        #region IComparer Implementation

        /// <summary>
        /// Implementing method for IComparer
        /// </summary>
        /// <param name="x">Object to compare from</param>
        /// <param name="y">Object to compare to</param>
        /// <returns>int of the comparison, or 0 if equal</returns>
        public int Compare ( T x, T y )
        {
            IComparable _iComparable1 = GetComparable( x ) ;
            IComparable _iComparable2 = GetComparable( y ) ;

            if ( _sortOrder != null && _sortOrder.ToUpper().Equals( "ASC" ) )
                return _iComparable1.CompareTo( _iComparable2 ) ;
            else
                return _iComparable2.CompareTo( _iComparable1 ) ;
        }

        #endregion
    }
}