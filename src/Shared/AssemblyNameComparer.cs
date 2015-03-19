﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// <summary>Compare two AssemblyNameExtensions to each other</summary>
//-----------------------------------------------------------------------

using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Build.Shared
{
    /// <summary>
    /// IKeyComparer implementation that compares AssemblyNames for using in Hashtables.
    /// </summary>
    [Serializable]
    sealed internal class AssemblyNameComparer : IComparer, IEqualityComparer, IEqualityComparer<AssemblyNameExtension>
    {
        /// <summary>
        /// Comparer for two assembly name extensions
        /// </summary>
        internal readonly static IComparer Comparer = new AssemblyNameComparer(false);

        /// <summary>
        /// Comparer for two assembly name extensions
        /// </summary>
        internal readonly static IComparer ComparerConsiderRetargetable = new AssemblyNameComparer(true);

        /// <summary>
        /// Comparer for two assembly name extensions
        /// </summary>
        internal readonly static IEqualityComparer<AssemblyNameExtension> GenericComparer = Comparer as IEqualityComparer<AssemblyNameExtension>;

        /// <summary>
        /// Comparer for two assembly name extensions
        /// </summary>
        internal readonly static IEqualityComparer<AssemblyNameExtension> GenericComparerConsiderRetargetable = ComparerConsiderRetargetable as IEqualityComparer<AssemblyNameExtension>;

        /// <summary>
        /// Should the comparer consider the retargetable flag when doing comparisons
        /// </summary>
        private bool _considerRetargetableFlag;

        /// <summary>
        /// Private construct so there's only one instance.
        /// </summary>
        private AssemblyNameComparer(bool considerRetargetableFlag)
        {
            _considerRetargetableFlag = considerRetargetableFlag;
        }

        /// <summary>
        /// Compare o1 and o2 as AssemblyNames.
        /// </summary>
        public int Compare(object o1, object o2)
        {
            AssemblyNameExtension a1 = (AssemblyNameExtension)o1;
            AssemblyNameExtension a2 = (AssemblyNameExtension)o2;

            int result = a1.CompareTo(a2, _considerRetargetableFlag);
            return result;
        }

        /// <summary>
        /// Treat o1 and o2 as AssemblyNames. Are they equal?
        /// </summary>
        new public bool Equals(object o1, object o2)
        {
            AssemblyNameExtension a1 = (AssemblyNameExtension)o1;
            AssemblyNameExtension a2 = (AssemblyNameExtension)o2;
            return Equals(a1, a2);
        }

        /// <summary>
        /// Get a hashcode for AssemblyName.
        /// </summary>
        public int GetHashCode(object o)
        {
            AssemblyNameExtension a = (AssemblyNameExtension)o;
            return GetHashCode(a);
        }

        #region IEqualityComparer<AssemblyNameExtension> Members

        /// <summary>
        /// Determine if the assembly name extensions are equal
        /// </summary>
        public bool Equals(AssemblyNameExtension x, AssemblyNameExtension y)
        {
            bool result = x.Equals(y, _considerRetargetableFlag);
            return result;
        }

        /// <summary>
        /// Get a hashcode for AssemblyName.
        /// </summary>
        public int GetHashCode(AssemblyNameExtension obj)
        {
            int result = obj.GetHashCode();
            return result;
        }

        #endregion
    }
}